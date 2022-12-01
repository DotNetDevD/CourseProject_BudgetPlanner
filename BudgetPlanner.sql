CREATE DATABASE MyBudgetPlanner;

CREATE TABLE Person
(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(20) NOT NULL,
Surname NVARCHAR(20) NOT NULL,
Age INT
CONSTRAINT CK_Person_Age CHECK (Age > 0 and Age < 110)
)

CREATE TABLE Income
(
Id int PRIMARY KEY IDENTITY,
PersonId INT REFERENCES Person (Id),
Date DATE,
TypeOfIncomes NVARCHAR(50),
CountIncome MONEY DEFAULT 0
)

CREATE TABLE Expense
(
Id INT PRIMARY KEY IDENTITY,
PersonId INT REFERENCES Person (Id),
Date DATE,
TypeOfExpenses NVARCHAR(50),
CountExpenses MONEY DEFAULT 0
)

--Очищаем таблицу
Truncate table Expense
Truncate table Income
DELETE FROM Person
DBCC CHECKIDENT ('Person', RESEED, 0)