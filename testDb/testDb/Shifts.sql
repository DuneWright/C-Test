CREATE TABLE [dbo].[Shifts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [shift_desc] VARCHAR(50) NULL, 
    [u_Id] INT NOT NULL, 
    CONSTRAINT [FK_Shifts_ToTable] FOREIGN KEY ([u_Id]) REFERENCES [Users]([Id]) 
)
