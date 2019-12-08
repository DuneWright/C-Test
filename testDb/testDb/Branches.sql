CREATE TABLE [dbo].[Branches]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [BranchDesc] VARCHAR(50) NULL, 
    [u_Id] INT NULL, 
    CONSTRAINT [FK_Branches_Users] FOREIGN KEY ([u_Id]) REFERENCES [Users]([Id])
)
