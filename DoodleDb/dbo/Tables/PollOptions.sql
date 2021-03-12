CREATE TABLE [dbo].[PollOptions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Option] NVARCHAR(100) NOT NULL, 
    [PollId] INT NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_PollOptions_Polls] FOREIGN KEY (PollId) REFERENCES Polls(Id)
)
