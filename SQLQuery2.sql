USE [aaa]
GO

ALTER TABLE [dbo].[Books] DROP CONSTRAINT [FK_Books_Categories]
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories]
GO


