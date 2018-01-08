using LISA.DBLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LISA.BackEnd
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private LISAEntities _Entities;

        #endregion

        #region Properties

        public LISAEntities Entities => _Entities;

        #endregion
    }
}
