CREATE TABLE [dbo].[PrixCatalogueArticle] (
    [Id]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [IdCatalogue]       BIGINT          NOT NULL,
    [IdArticle]         BIGINT          NOT NULL,
    [Prix]              DECIMAL (18, 2) NULL,
    [PrixAvantCoupon]   DECIMAL (18, 2) NULL,
    [PrixAvantCroise]   DECIMAL (18, 2) NULL,
    [ReductionEuro]     DECIMAL (18, 2) NULL,
    [ReductionPourcent] DECIMAL (18, 2) NULL,
    [AvantageEuro]      DECIMAL (18, 2) NULL,
    [AvantagePourcent]  DECIMAL (18, 2) NULL,
    [Ecotaxe]           DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_PrixCatalogueArticle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrixCatalogueArticle_Article] FOREIGN KEY ([IdArticle]) REFERENCES [dbo].[Article] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PrixCatalogueArticle_Catalogue] FOREIGN KEY ([IdCatalogue]) REFERENCES [dbo].[Catalogue] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_PrixCatalogueArticle_IdCatalogue_IdArticle]
    ON [dbo].[PrixCatalogueArticle]([IdCatalogue] ASC, [IdArticle] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrixCatalogueArticle_IdCatalogue]
    ON [dbo].[PrixCatalogueArticle]([IdCatalogue] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrixCatalogueArticle_IdArticle]
    ON [dbo].[PrixCatalogueArticle]([IdArticle] ASC);

