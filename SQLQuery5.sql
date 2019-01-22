BEGIN
Create table [dbo].[Books] (
[BookID] [int] NOT NULL,
[BookName] [varchar] (50) NOT NULL UNIQUE,
[AuthorID] [int] NOT NULL,
[IssueDate] [date] NOT NULL,
[CategoryID] [int] NOT NULL,
[Price] [int] NOT NULL,
CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([BookID] ASC)
WITH (IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [BookNameUnique] UNIQUE NONCLUSTERED ([BookName] ASC)
);
END
