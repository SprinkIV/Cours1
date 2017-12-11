CREATE TABLE [dbo].[Utilisateur] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Login]        NVARCHAR (200) NOT NULL,
    [MotDePasse]   NVARCHAR (200) NOT NULL,
    [Discriminant] SMALLINT       NOT NULL,
    CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Utilisateur_Login]
    ON [dbo].[Utilisateur]([Login] ASC);

