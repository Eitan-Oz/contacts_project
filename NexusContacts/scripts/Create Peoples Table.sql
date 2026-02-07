CREATE TABLE [dbo].[Peoples] (
    [contID]      INT           IDENTITY (1, 1) NOT NULL,
    [Fname]       NVARCHAR (50) NULL,
    [Lname]       NVARCHAR (50) NULL,
    [PhoneNum]    NVARCHAR (50) NULL,
    [age]         TINYINT           NULL,
    [IsFavorite] BIT           NULL,
    PRIMARY KEY CLUSTERED ([contID] ASC)
);

