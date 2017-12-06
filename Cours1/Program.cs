using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cours1
{
    class Program
    {

        public const string ARGS_DIRECTORY = "-d";

        // Arguments
        // -d <Dossier à vérifier> : Précise le dossier à vérifier pour l'import

        static void Main(string[] args)
        {
            if (args[0] == ARGS_DIRECTORY && args.Length == 2)
            {

            }
            else
            {
                displayHelp();
            }
        }

        /// <summary>
        /// Affiche la documentation du <see cref="Program"/>
        /// </summary>
        private static void displayHelp()
        {
            Console.WriteLine("Arguments :");
            Console.WriteLine("-d <Dossier à vérifier> : Précise le dossier à vérifier pour l'import");
        }

        private static void ImportXMLFile(string filePath)
        {
            Console.WriteLine("Import du fichier : " + filePath);
            //Console.WriteLine($"Import du fichier : {filePath}");
        }
    }
}
