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
    
    public partial class Utilisateur
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string MotDePasse { get; set; }
        public short Discriminant { get; set; }
    }
}
