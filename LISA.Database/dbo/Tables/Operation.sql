CREATE TABLE [dbo].[Operation] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Code]      NVARCHAR (50)  NOT NULL,
    [Titre]     NVARCHAR (200) NULL,
    [DateDebut] DATETIME2 (7)  NULL,
    [DateFin]   DATETIME2 (7)  NULL,
    [ImportId]  BIGINT         NULL,
    CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Operation_Code]
    ON [dbo].[Operation]([Code] ASC);

