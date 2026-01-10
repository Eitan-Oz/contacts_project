CREATE TABLE [dbo].[Peoples]
(
	[contID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Fname] NVARCHAR(50) NULL, 
    [Lname] NVARCHAR(50) NULL, 
    [PhoneNum] NVARCHAR(50) NULL, 
    [IsFavorite ] BIT NULL
)
