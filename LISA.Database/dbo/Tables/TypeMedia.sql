CREATE TABLE [dbo].[TypeMedia] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Libelle] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_TypeMedia] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_TypeMedia_Libelle]
    ON [dbo].[TypeMedia]([Libelle] ASC);

