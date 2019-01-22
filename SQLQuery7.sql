BEGIN
Create table [dbo].[Categories] (
[CategoryID] [int] NOT NULL,
[CateoryName] [varchar] (50) NOT NULL UNIQUE,
CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
WITH (IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [CategoryNameUnique] UNIQUE NONCLUSTERED ([CateoryName] ASC)
); END
