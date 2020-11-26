SELECT *
FROM Sales s
JOIN Books b
  ON s.BookId = b.BookId
JOIN AspNetUsers u
  ON s.MemberId = u.Id

SELECT *
FROM Reviews r
JOIN Books b
  ON r.BookId = b.BookId
JOIN AspNetUsers u
  ON r.MemberId = u.Id

