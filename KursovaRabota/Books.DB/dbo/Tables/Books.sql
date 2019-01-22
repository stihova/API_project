CREATE TABLE [dbo].[Books] (
    [BookID]      INT          NOT NULL,
    [BookName]    VARCHAR (50) NOT NULL,
    [AuthorID]    INT          NOT NULL,
    [IssueDate]   DATE         NOT NULL,
    [CategoryID]  INT          NOT NULL,
    [Price]       INT          NOT NULL,
    [Description] NTEXT        NULL,
    [DateCreated] DATE         NULL,
    CONSTRAINT [PK_Books_1] PRIMARY KEY CLUSTERED ([BookID] ASC),
    CONSTRAINT [FK_Books_Authors] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Authors] ([AuthorID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Books_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([BookName] ASC),
    CONSTRAINT [BookNameUnique] UNIQUE NONCLUSTERED ([BookName] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_Books]
    ON [dbo].[Books]([BookID] ASC);

