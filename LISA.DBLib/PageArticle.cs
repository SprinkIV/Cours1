//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PageArticle
    {
        public long Id { get; set; }
        public long IdPage { get; set; }
        public long IdArticle { get; set; }
        public int ZoneHauteur { get; set; }
        public int ZoneLargeur { get; set; }
        public int ZoneCoordonnéesX { get; set; }
        public int ZoneCoordonnéesY { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual Page Page { get; set; }
    }
}
