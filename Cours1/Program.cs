using System;
using System.Collections.Generic;
using System.IO;
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
            // Affiche les arguments
            if (args.Length == 2 && args[0] == ARGS_DIRECTORY)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine(args[i]);
                }

                //Vérifier que le dossier existe. Afficher un message d'erreur si ce n'est pas le cas
                if (Directory.Exists(args[1]))
                {
                    // Lister les sous-dossier. Un sous-dossier pour un lot d'import
                    IEnumerable<string> allDirectorys = Directory.EnumerateDirectories(args[1], "*", SearchOption.TopDirectoryOnly);

                    // Rechercher dans chaque sous-dossier qu'il existe un fichier "*.xml". Afficher une erreur si ce n'est pas le cas
                    foreach (string directory in allDirectorys)
                    {
                        IEnumerable<string> xmlFiles = Directory.EnumerateFiles(directory, "*.xml", SearchOption.TopDirectoryOnly);

                        if (xmlFiles.Count() != 0)
                        {
                            foreach (string file in xmlFiles)
                            {
                                // Appeler la fonction ImportXMLFile pour chaque fichier "*.xml"
                                ImportXMLFile($"directory\\{file}");
                            }
                        }
                        else
                            Console.WriteLine($"Erreur : Aucun fichier XML au chemin {directory}");
                         //Directory.Delete(directory);
                    }
                }
                else
                    Console.WriteLine("Erreur : Le dossier est introuvable");
            }
            else
            {
                displayHelp();
            }
#if DEBUG
            Console.WriteLine("Appuyer pour continuer...");
            Console.Read();
#endif
        }

        /// <summary>
        /// Affiche la documentation du <see cref="Program"/>
        /// </summary>
        private static void displayHelp()
        {
            Console.WriteLine("Arguments :");
            Console.WriteLine("-d <Dossier à vérifier> : Précise le dossier à vérifier pour l'import");
            Console.WriteLine("Sans arguments : Affiche l'aide");

        // Les lignes de codes dans le if seront éxécuté si le programme est en mode debug uniquement.
        }

        private static void ImportXMLFile(string filePath)
        {
            Console.WriteLine("Import du fichier : " + filePath);
            //Console.WriteLine($"Import du fichier : {filePath}");
        }
    }
}
