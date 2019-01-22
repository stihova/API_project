CREATE TABLE [dbo].[Categories] (
    [CategoryID]  INT          NOT NULL,
    [CateoryName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Categories_1] PRIMARY KEY CLUSTERED ([CategoryID] ASC),
    UNIQUE NONCLUSTERED ([CateoryName] ASC),
    CONSTRAINT [CategoryNameUnique] UNIQUE NONCLUSTERED ([CateoryName] ASC)
);

