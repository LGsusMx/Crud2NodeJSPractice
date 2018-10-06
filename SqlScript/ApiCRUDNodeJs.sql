CREATE DATABASE TDBNP
GO
USE TDBNP
GO
CREATE TABLE Producto
(
Id bigint PRIMARY KEY IDENTITY (1,1)NOT NULL ,
Nombre nvarchar(50) not  null,
Precio decimal not null
)
GO
CREATE TABLE Compra
(
Id bigint PRIMARY KEY IDENTITY (1,1)NOT NULL ,
Fecha DATETIME DEFAULT GETDATE() not null,
Estatus  tinyint Default 1 not null,
Total decimal
)
GO
CREATE TABLE Venta_Detalle
(
Id bigint PRIMARY KEY IDENTITY (1,1)NOT NULL ,
IdVe bigint NOT NULL,
IdPr bigint NOT NULL,
Cantidad INT NOT NULL,
Estatus  tinyint Default 1 not null,
PrecioT decimal
CONSTRAINT FK_IDP FOREIGN KEY(IdPr) REFERENCES Producto(Id),
CONSTRAINT FK_IDV FOREIGN KEY(IdVe) REFERENCES Compra(Id)
ON DELETE CASCADE
ON UPDATE CASCADE 
)ON [PRIMARY]
go
--Trigger for updates on detail
CREATE TRIGGER tgr_Detalle
ON Venta_Detalle
AFTER UPDATE,INSERT 
AS
BEGIN 
SET NOCOUNT ON;
DECLARE @IDACT AS BIGINT
SET @IDACT=(SELECT IdVe FROM inserted);
UPDATE Compra SET
Total=(SELECT SUM(I.PrecioT*I.Cantidad) FROM Venta_Detalle I WHERE IdVe=@IDACT AND Estatus=1)
WHERE Id= (SELECT IdVe FROM inserted) 
end
GO
CREATE VIEW view_Catalogo
AS
SELECT A.Id as ID, (A.PrecioT*A.Cantidad) AS Total, A.Cantidad AS Cantidad,a.Estatus AS Estatus ,C.Fecha  AS Fecha ,P.Nombre AS Descripcion
FROM Venta_Detalle A
INNER JOIN Compra C ON C.Id=A.IdVe 
INNER JOIN Producto P ON P.Id=A.IdPr
GO
---Data for test
INSERT INTO Producto(Nombre,Precio) VALUES('Mouse Feo',60),('Cargador Pirata Lighting',20),('Audifonos Piston 2 Xiaomi',350),('Teclado Gaming',700)
INSERT INTO Compra(Total) VALUES(100),(200),(300)
INSERT INTO Venta_Detalle(IdVe,IdPr,Cantidad,PrecioT) VALUES (2,5,1,60),(2,6,2,40),(2,6,5,20),(3,5,1,60),(3,6,2,40)

GO
--TEST FOR CRUD---------------------------------------------------------------
--SELECT * FROM view_Catalogo WHERE Fecha = '2018-10-04'
--This works:
--SELECT  * FROM view_Catalogo  WHERE '2018-10-04'<= Fecha and Fecha < '2018-10-05'

--INSERT INTO Venta_Detalle(IdVe,IdPr,Cantidad,PrecioT) VALUES (1,(SELECT Id FROM Producto WHERE Nombre=''),10,(SELECT Precio FROM Producto WHERE Nombre=''))
--UPDATE Venta_Detalle SET Cantidad=2 WHERE Id=1
-----------------------------------------------------------------------------
SELECT *FROM Producto
SELECT * FROM Venta_Detalle
SELCT *FROM Compra
SELECT * FROM view_Catalogo