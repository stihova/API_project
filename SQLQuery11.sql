BEGIN
Create table [dbo].[Authors] (
[AuthorID] [int] NOT NULL,
[AuthorName] [varchar] (50) NOT NULL UNIQUE,
[Country] [varchar] (50) NOT NULL,
CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([AuthorID] ASC)
WITH (IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [AuthorNameUnique] UNIQUE NONCLUSTERED ([AuthorName] ASC)
); END
