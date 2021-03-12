CREATE TABLE [dbo].[Votes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VoterName] NVARCHAR(50) NOT NULL, 
    [OptionId] INT NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_Votes_PollOptions] FOREIGN KEY (OptionId) REFERENCES PollOptions(Id)
)
