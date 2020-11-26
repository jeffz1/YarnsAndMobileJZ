using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YarnsAndMobile.Data;
using YarnsAndMobile.Models;
using YarnsAndMobile.Models.ImportModels;
using Microsoft.EntityFrameworkCore;

namespace YarnsAndMobile.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Admin : Controller
    {
        private readonly YamDbContext _dbContext;
        private readonly UserManager<Member> _userManager;
        private readonly IWebHostEnvironment _env;

        public enum UploadType { Book, Member, SaleReview };

        public Admin(YamDbContext dbContext, UserManager<Member> userManager, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewData["message"] = TempData["message"];
            return View();
        }

        public IActionResult ImportData()
        {
            var currentAdminId = _userManager.GetUserId(User);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.Migrate(); //Creates database if needed and applies migrations
            ApplicationDbInitializer.SeedUsers(_userManager, currentAdminId);
            var message = LoadImportData();
            if (!string.IsNullOrWhiteSpace(message))
            {
                TempData["message"] = message;
                return RedirectToAction(nameof(Index));
            }

            TempData["message"] = "Data imported";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string fileDataType)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["message"] = "File not selected";
                    return RedirectToAction(nameof(Index));
                }

                if (Path.GetExtension(file.Name).ToLower() != ".json")
                {
                    TempData["message"] = "Only json files are supported at this time";
                    return RedirectToAction(nameof(Index));
                }

                var uploadType = Enum.Parse<UploadType>(fileDataType);
                var path = GetFileName(uploadType);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch
            {
                TempData["message"] = "An unexpected error occurred";
                return RedirectToAction(nameof(Index));
            }

            TempData["message"] = "File Uploaded";
            return RedirectToAction(nameof(Index));            
        }

        private string GetFileName(UploadType uploadType)
        {
            return Path.Combine(_env.ContentRootPath, "FileUploads", $"{_userManager.GetUserId(User)}_{uploadType.ToString()}.json");
        }

        private string LoadImportData()
        {
            var sb = new StringBuilder();
            var bookFile = GetFileName(UploadType.Book);
            var memberFile = GetFileName(UploadType.Member);
            var saleReviewFile = GetFileName(UploadType.SaleReview);
            if (!System.IO.File.Exists(bookFile))
                sb.AppendLine("You need to upload a file containing Book data");
            if (!System.IO.File.Exists(memberFile))
                sb.AppendLine("You need to upload a file containing Member data");
            if (!System.IO.File.Exists(saleReviewFile))
                sb.AppendLine("You need to upload a file containing Sale and Review data");
            if (sb.Length > 0)
                return sb.ToString();

            try
            {
                var bookKeyMap = LoadBooks();
                var memberKeyMap = LoadMembers();
                LoadSalesAndReviews(bookKeyMap, memberKeyMap);
            }
            catch
            {
                return "Import failed. Please be sure to upload the correct files.";
            }
            return null;
        }

        private void LoadSalesAndReviews(Dictionary<int,int> bookKeyMap, Dictionary<int,string> memberKeyMap)
        {
            //var json = System.IO.File.ReadAllText(@"C:\Users\Jeffz\Downloads\Yarns_SaleReview.json");
            var json = System.IO.File.ReadAllText(GetFileName(UploadType.SaleReview));
            var importSaleReviews = JsonConvert.DeserializeObject<List<SaleReviewImport>>(json);

            foreach (var importSaleReview in importSaleReviews)
            {
                var book = _dbContext.Books.Find(bookKeyMap[importSaleReview.BookId]);
                var member = _dbContext.Members.Find(memberKeyMap[importSaleReview.MemberId]);

                if (importSaleReview.SaleDate.HasValue)
                {
                    var sale = new Sale
                    {
                        Date = importSaleReview.SaleDate.GetValueOrDefault(),
                        Price = importSaleReview.SalePrice.GetValueOrDefault(),
                        Book = book,
                        Member = member
                    };
                    _dbContext.Sales.Add(sale);
                }
                if (importSaleReview.ReviewDate.HasValue)
                {
                    var review = new Review
                    {
                        Date = importSaleReview.ReviewDate.GetValueOrDefault(),
                        Title = importSaleReview.ReviewTitle,
                        Body = importSaleReview.ReviewBody,
                        Rating = importSaleReview.ReviewRating.GetValueOrDefault(),
                        Book = book,
                        Member = member
                    };
                    _dbContext.Reviews.Add(review);
                }
                _dbContext.SaveChanges();
            }

        }

        private Dictionary<int,string> LoadMembers()
        {
            //var json = System.IO.File.ReadAllText(@"C:\Users\Jeffz\Downloads\Yarns_Member1.json");
            var json = System.IO.File.ReadAllText(GetFileName(UploadType.Member));
            var importMembers = JsonConvert.DeserializeObject<List<MemberImport>>(json);

            var keymap = new Dictionary<int, string>();
            foreach (var importMember in importMembers)
            {
                var address = new Address
                {
                    Line1 = importMember.Line1,
                    Line2 = importMember.Line2,
                    City = importMember.City,
                    State = importMember.State,
                    Zip = importMember.Zip
                };

                var member = new Member
                {
                    Email = importMember.Email,
                    AccountNumber = importMember.AccountNumber,
                    FirstName = importMember.FirstName,
                    LastName = importMember.LastName,
                    Address = address
                };

                var phones = new List<Phone>();
                AddPhone(importMember.Phone1, importMember.PhoneType1, member, phones);
                AddPhone(importMember.Phone2, importMember.PhoneType2, member, phones);
                AddPhone(importMember.Phone3, importMember.PhoneType3, member, phones);
                AddPhone(importMember.Phone4, importMember.PhoneType4, member, phones);

                member.Phones = phones;
                _dbContext.Members.Add(member);
                keymap.Add(importMember.Id, member.Id);
            }
            _dbContext.SaveChanges();
            return keymap;
        }

        private void AddPhone(string number, string phoneType, Member member, List<Phone> phones)
        {
            if (!string.IsNullOrWhiteSpace(number))
            {
                var phone = new Phone
                {
                    Number = number,
                    Type = new PhoneType { Description = phoneType },
                    Member = member
                };
                phones.Add(phone);
            }
        }

        private Dictionary<int,int> LoadBooks()
        {
            //var json = System.IO.File.ReadAllText(@"C:\Users\Jeffz\Downloads\Yarns_Book1.json");
            var json = System.IO.File.ReadAllText(GetFileName(UploadType.Book));
            var importBooks = JsonConvert.DeserializeObject<List<BookImport>>(json);

            var keymap = new Dictionary<int, int>();
            foreach (var importBook in importBooks)
            {
                var book = new Book
                {
                    Author = $"{importBook.AuthorFirstName} {importBook.AuthorLastName}",
                    CopyrightYear = (short) importBook.Year.GetValueOrDefault(),
                    ImageUrl = importBook.Image,
                    ISBN = importBook.ISBN,
                    CurrentSalePrice = importBook.Price,
                    Title = importBook.Title
                };
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();
                keymap.Add(importBook.Id, book.BookId);
            }
            _dbContext.SaveChanges();
            return keymap;
        }
    }
}
