CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NULL, 
    [Fullname] VARCHAR(50) NULL, 
    [b_Id] INT NOT NULL, 
    [s_Id] INT NOT NULL, 
    CONSTRAINT [FK_Users_Branches] FOREIGN KEY ([b_Id]) REFERENCES [Branches]([Id]), 
    CONSTRAINT [FK_Users_Shifts] FOREIGN KEY ([s_Id]) REFERENCES [Shifts]([Id])
)
