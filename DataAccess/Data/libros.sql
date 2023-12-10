use master;

create database Libros;

use Libros;

--Crea la tabla Authors
Create table Authors(
AuthorID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
FirstName VARCHAR (30) NOT NULL,
LastName VARCHAR (30) NOT NULL);

-- Crea la tabla Titles
create table Titles (
  ISBN varchar(20) not null primary key,
  Title varchar(100) not null,
  EditionNumber int,
  Copyright char(4)
);
 
-- Crea la tabla AuthorISBN
CREATE TABLE AuthorISBN(
  AuthorId INT NOT NULL,
  ISBN VARCHAR(20) NOT NULL,
  PRIMARY KEY (AuthorId, ISBN),
  FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId),
  FOREIGN KEY (ISBN) REFERENCES Titles(ISBN)
);
INSERT INTO Authors (FirstName, LastName) VALUES ('Andrew', 'Goldberg');
INSERT INTO Authors(LastName, FirstName) VALUES ('Deitel', 'Harvey');
INSERT INTO Authors VALUES ('Paul', 'Deitel');
INSERT INTO Authors VALUES ('Ian', 'Summerville');
INSERT INTO Authors VALUES ('Roger', 'Pressmann');
INSERT INTO Authors VALUES ('Robert', 'Martin');

-- Insertando datos en la tabla Titles
INSERT INTO Titles(ISBN, Title, EditionNumber, Copyright) VALUES ('0131869000','Visual Basic 2005 How to Program',3,'2006');
INSERT INTO Titles (ISBN, Title, EditionNumber, Copyright) VALUES ('0131525239','Visual C# 2005 How to Program',2,'2006');

-- Comandos de inserción de la práctica de laboratorio
INSERT INTO Authors (FirstName,LastName) VALUES ('Craig','Larman');
INSERT INTO Authors (FirstName,LastName) VALUES ('Grady','Booch');

INSERT INTO Titles (ISBN,Title,EditionNumber,Copyright) VALUES ('0131869000','Visual Basic 2005 How to Program',3,'2006');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (1,'0131869000');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (2,'0131869000');

INSERT INTO Titles (ISBN,Title,EditionNumber,Copyright) VALUES ('0131525239','Visual C# 2005 How to Program',2,'2006');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (1,'0131525239');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (2,'0131525239');
 
INSERT INTO Titles (ISBN,Title,EditionNumber,Copyright) VALUES ('0132222205','Java How to Program',7,'2007');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (1,'0132222205');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (2,'0132222205');
 
INSERT INTO Titles (ISBN,Title,EditionNumber,Copyright) VALUES ('0131857576','C++ How to Program',5,'2005');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (1,'0131857576');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (2,'0131857576');
 
INSERT INTO Titles (ISBN,Title,EditionNumber,Copyright)  VALUES ('0132404168','C How to Program',5,'2007');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (1,'0132404168');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (2,'0132404168');
 
INSERT INTO Titles (ISBN,Title,EditionNumber,Copyright)  VALUES ('0131450913','Internet & World Wide Web How to Program',3,'2004');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (1,'0131450913');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (2,'0131450913');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (3,'0131450913');
 
INSERT INTO Titles (ISBN,Title,EditionNumber,Copyright) VALUES ('0131828274','Operating Systems',3,'2004');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (1,'0131828274');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (2,'0131828274');
INSERT INTO AuthorISBN (AuthorID,ISBN) VALUES (4,'0131828274');
