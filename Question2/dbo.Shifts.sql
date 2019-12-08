CREATE TABLE [dbo].[Shifts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ShiftDesc] VARCHAR(50) NULL, 
    [u_Id] NCHAR(10) NOT NULL, 
    CONSTRAINT [FK_Shifts_Users] FOREIGN KEY ([u_Id]) REFERENCES [Users]([Id])
)
