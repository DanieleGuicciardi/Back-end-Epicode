-- Es 1
SELECT * FROM Products;

-- Es 2
SELECT * FROM Products WHERE UnitsInStock >= 40;

-- Es 3
SELECT * FROM Employees WHERE City = 'London';

-- Es 4
SELECT * FROM Orders ORDER BY Freight DESC;

-- Es 5
SELECT * FROM Orders WHERE Freight > 90 AND Freight < 200;

-- Es 6
SELECT * FROM Products WHERE CategoryID = 1;

-- Es 7
SELECT * FROM [Order Details] WHERE Discount > 0;

-- Es 8
SELECT * FROM Orders WHERE CustomerID = 'BOTTM' AND Freight > 50;
