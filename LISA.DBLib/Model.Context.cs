﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LISA.DBLib
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LISAEntities : DbContext
    {
        public LISAEntities()
            : base("name=LISAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleAttribut> ArticleAttributs { get; set; }
        public virtual DbSet<Attribut> Attributs { get; set; }
        public virtual DbSet<Catalogue> Catalogues { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Magasin> Magasins { get; set; }
        public virtual DbSet<MagasinCatalogue> MagasinCatalogues { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageArticle> PageArticles { get; set; }
        public virtual DbSet<PrixCatalogueArticle> PrixCatalogueArticles { get; set; }
        public virtual DbSet<TypeMedia> TypeMedias { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}