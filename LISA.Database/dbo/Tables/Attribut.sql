CREATE TABLE [dbo].[Attribut] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Libelle] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Attribut] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Attribut_Libelle]
    ON [dbo].[Attribut]([Libelle] ASC);

