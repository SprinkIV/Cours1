using LISA.DBLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                                ImportXMLFile(file);
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

            try
            {
                using (LISAEntities entities = new LISAEntities())
                {
                    entities.Database.Connection.Open();
                    XDocument document = XDocument.Load(filePath);

                    foreach (XElement element in document.Descendants(XName.Get("operation")))
                    {
                        Operation operation = ParseOperationElement(element, entities);
                    }
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Impossible d'importer le fichier {filePath + Environment.NewLine + ex.Message}");
            }

            #region Exo : Ajout opération, catalogue, page
            ////Créer la chaine de connexion
            //using (LISAEntities entities = new LISAEntities())
            //{
            //    entities.Operations.ToList().ForEach(o => entities.Operations.Remove(o));
            //    entities.Catalogues.ToList().ForEach(c => entities.Catalogues.Remove(c));
            //    entities.Pages.ToList().ForEach(p => entities.Pages.Remove(p));
            //    //Créer une opération
            //    Operation op = new Operation
            //    {
            //        Code = "Je suis le code de l'opération",
            //        Titre = "Je suis le titre de l'opération",
            //        DateDebut = DateTime.Now,
            //        DateFin = DateTime.Now,
            //        Catalogues = null                    
            //    };

            //    entities.Operations.Add(op);

            //    //Créer un catalogue
            //    Catalogue cat = new Catalogue
            //    {
            //        Hauteur = 500,
            //        Largeur = 500,
            //        Libelle = "Je suis le libelle du catalogue",
            //        Type = "Je suis le type du catalogue",
            //        Vitesse = "V3",
            //        Operation = op                    
            //    };

            //    entities.Catalogues.Add(cat);

            //    //Créer trois pages

            //    List<Page> allPages = new List<Page>();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Page page = new Page()
            //        {
            //            Numero = i,
            //            Catalogue = cat
            //        };
            //        allPages.Add(page);
            //    }

            //    //Page page1 = new Page()
            //    //{
            //    //    Numero = 1,
            //    //    Catalogue = cat
            //    //};

            //    //Page page2 = new Page()
            //    //{
            //    //    Numero = 2,
            //    //    Catalogue = cat
            //    //};

            //    //Page page3 = new Page()
            //    //{
            //    //    Numero = 3,
            //    //    Catalogue = cat
            //    //};

            //    //entities.Pages.Add(page1);
            //    //entities.Pages.Add(page2);
            //    //entities.Pages.Add(page3);

            //    foreach (Page page in allPages)
            //    {
            //        entities.Pages.Add(page);
            //    }
            #endregion
            }

        private static Operation ParseOperationElement(XElement operationElement, LISAEntities entities)
        {
            Operation result = null;
            // Récupérer les données de l'opération dans le fichier XML
            long operationId = long.Parse(operationElement.Attribute(XName.Get("id")).Value);
            string operationCode = operationElement.Element(XName.Get("code")).Value;
            string operationTitle = operationElement.Element(XName.Get("title")).Value;
            DateTime operationStartDateTime = UnixTimeStampToDateTime(double.Parse(operationElement.Element(XName.Get("startDate")).Value));
            DateTime operationEndDateTime = UnixTimeStampToDateTime(double.Parse(operationElement.Element(XName.Get("endDate")).Value));

            // Vérifier si l'opération n'existe pas déjà en base
            result = entities.Operations.FirstOrDefault(o => o.ImportId == operationId);

            // Créer l'opération
            if (result != null)
            {
                entities.Operations.Remove(result);
            }

            result = new Operation()
                {
                    ImportId = operationId,
                    Code = operationCode,
                    Titre = operationTitle,
                    DateDebut = operationStartDateTime,
                    DateFin = operationEndDateTime
                };

            entities.Operations.Add(result);

            return result;
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime());
        }
    }
}
