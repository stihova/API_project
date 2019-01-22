CREATE TABLE [dbo].[Authors] (
    [AuthorID]   INT          NOT NULL,
    [AuthorName] VARCHAR (50) NOT NULL,
    [Country]    VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Authors_1] PRIMARY KEY CLUSTERED ([AuthorID] ASC),
    UNIQUE NONCLUSTERED ([AuthorName] ASC),
    CONSTRAINT [AuthorNameUnique] UNIQUE NONCLUSTERED ([AuthorName] ASC)
);

