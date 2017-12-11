CREATE TABLE [dbo].[Page] (
    [Id]          BIGINT IDENTITY (1, 1) NOT NULL,
    [IdCatalogue] BIGINT NOT NULL,
    [Numero]      INT    NOT NULL,
    [ImportId]    BIGINT NULL,
    CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Page_Catalogue] FOREIGN KEY ([IdCatalogue]) REFERENCES [dbo].[Catalogue] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Page_IdCatalogue_Numero]
    ON [dbo].[Page]([IdCatalogue] ASC, [Numero] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page_IdCatalogue]
    ON [dbo].[Page]([IdCatalogue] ASC);

