CREATE TABLE [dbo].[MagasinCatalogue] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [IdMagasin]   BIGINT        NOT NULL,
    [IdCatalogue] BIGINT        NOT NULL,
    [DateDebut]   DATETIME2 (7) NOT NULL,
    [DateFin]     DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_MagasinCatalogue] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MagasinCatalogue_Catalogue] FOREIGN KEY ([IdCatalogue]) REFERENCES [dbo].[Catalogue] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_MagasinCatalogue_Magasin] FOREIGN KEY ([IdMagasin]) REFERENCES [dbo].[Magasin] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_MagasinCatalogue_IdMagasin_IdCatalogue]
    ON [dbo].[MagasinCatalogue]([IdMagasin] ASC, [IdCatalogue] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MagasinCatalogue_IdMagasin]
    ON [dbo].[MagasinCatalogue]([IdMagasin] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MagasinCatalogue_IdCatalogue]
    ON [dbo].[MagasinCatalogue]([IdCatalogue] ASC);

