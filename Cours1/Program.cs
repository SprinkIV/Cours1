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

            // Exécute le programme si les arguments sont correct
            if (args.Length == 2 && args[0] == ARGS_DIRECTORY)
            {
                // Affiche les arguments
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine(args[i]);
                }

                //Vérifie que le dossier existe. Affiche un message d'erreur si ce n'est pas le cas
                if (Directory.Exists(args[1]))
                {
                    // Liste les sous-dossier
                    IEnumerable<string> allDirectorys = Directory.EnumerateDirectories(args[1], "*", SearchOption.TopDirectoryOnly);

                    // Recherche dans chaque sous-dossier qu'il existe un fichier "*.xml". Affiche une erreur si ce n'est pas le cas
                    foreach (string directory in allDirectorys)
                    {
                        // Liste les fichiers *.xml
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
                DisplayHelp();
            }
#if DEBUG
            Console.WriteLine("Appuyer pour continuer...");
            Console.Read();
#endif
        }

        /// <summary>
        /// Affiche la documentation du <see cref="Program"/>
        /// </summary>
        private static void DisplayHelp()
        {
            Console.WriteLine("Arguments :");
            Console.WriteLine("-d <Dossier à vérifier> : Précise le dossier à vérifier pour l'import");
            Console.WriteLine("Sans arguments : Affiche l'aide");

        // Les lignes de codes dans le if seront éxécuté si le programme est en mode debug uniquement.
        }

        /// <summary>
        /// Importe les données du fichier XML et les enregistre en base de données
        /// </summary>
        /// <param name="filePath">Chemin du fichier XML</param>
        private static void ImportXMLFile(string filePath)
        {
            Console.WriteLine("Import du fichier : " + filePath);
            //Console.WriteLine($"Import du fichier : {filePath}");

            try
            {
                using (LISAEntities entities = new LISAEntities())
                {
                    // Ouvre la connexion vers la base de donnée
                    entities.Database.Connection.Open();
                    XDocument document = XDocument.Load(filePath);

                    // Boucle sur les opérations du fichier XML
                    foreach (XElement operationElement in document.Descendants(XName.Get("operation")))
                    {
                        Operation operation = ParseOperationElement(operationElement, entities);

                        // Boucle sur les catalogue du fichier XML
                        foreach (XElement catalogElement in operationElement.Elements(XName.Get("catalog")))
                        {
                            Catalogue catalog = ParseCatalogElement(catalogElement, entities, operation);

                            // Boucle sur les magasins du fichier XML
                            foreach (XElement shopElement in catalogElement.Elements(XName.Get("shops")).Elements())
                            {
                                Magasin shop = ParseShopElement(shopElement, entities);
                                ParseShopCatalogElement(shopElement, entities, catalog, shop);
                            }

                            // Boucle sur les pages du fichier XML
                            foreach (XElement pageElement in catalogElement.Elements(XName.Get("pages")).Elements())
                            {
                                Page page = ParsePageElement(pageElement, entities, catalog);

                                foreach (XElement articleElement in pageElement.Elements(XName.Get("products")).Elements())
                                {
                                    Article article = ParseArticleElement(articleElement, entities, pageElement);

                                    PageArticle pageArticle = ParsePageArticleElement(articleElement, entities, page, article);

                                    PrixCatalogueArticle prixCatalogueArticle = ParsePrixCatalogueArticleElement(articleElement, entities, catalog, article);


                                }
                            }
                        }
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

        /// <summary>
        /// Importe un #<see cref="PrixCatalogueArticle"/> du fichier XML et l'enregistre dans la base de donnée. Si la PrixCatalogueArticle
        /// importée est déjà en base de données, la supprime et la restaure avec les nouvelles données.
        /// </summary>
        /// <param name="articleElement">XElement article</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <param name="catalog">Catalogue en liaison avec PrixCatalogueArticle</param>
        /// <param name="article">Article en liaison avec PrixCatalogueArticle</param>
        /// <returns>Retourne le PrixCatalogueArticle importé</returns>
        private static PrixCatalogueArticle ParsePrixCatalogueArticleElement(XElement articleElement, LISAEntities entities, Catalogue catalog, Article article)
        {
            decimal.TryParse(articleElement.Element(XName.Get("price")).Value, out decimal prixCatalogueArticlePrix);
            decimal.TryParse(articleElement.Element(XName.Get("price_before_coupon")).Value, out decimal prixCatalogueArticlePrixAvantCoupon);
            decimal.TryParse(articleElement.Element(XName.Get("price_crossed")).Value, out decimal prixCatalogueArticlePrixAvantCroise);
            decimal.TryParse(articleElement.Element(XName.Get("Reduction_euro")).Value, out decimal prixCatalogueArticleReductionEuro);
            decimal.TryParse(articleElement.Element(XName.Get("Reduction_percent")).Value, out decimal prixCatalogueArticleReductionPourcent);
            decimal.TryParse(articleElement.Element(XName.Get("Avantage_euro")).Value, out decimal prixCatalogueArticleAvantageEuro);
            decimal.TryParse(articleElement.Element(XName.Get("Avantage_percent")).Value, out decimal prixCatalogueArticleAvantagePourcent);
            decimal.TryParse(articleElement.Element(XName.Get("ecotaxe")).Value, out decimal prixCatalogueArticleEcotaxe);

            PrixCatalogueArticle result = entities.PrixCatalogueArticles.FirstOrDefault(p => p.IdArticle == article.Id && p.IdCatalogue == catalog.Id);

            if (result != null)
            {
                entities.PrixCatalogueArticles.Remove(result);
            }

            result = new PrixCatalogueArticle()
            {
                Prix = prixCatalogueArticlePrix,
                PrixAvantCoupon = prixCatalogueArticlePrixAvantCoupon,
                PrixAvantCroise = prixCatalogueArticlePrixAvantCroise,
                ReductionEuro = prixCatalogueArticleReductionEuro,
                ReductionPourcent = prixCatalogueArticleReductionPourcent,
                AvantageEuro = prixCatalogueArticleAvantageEuro,
                AvantagePourcent = prixCatalogueArticleAvantagePourcent,
                Ecotaxe = prixCatalogueArticleEcotaxe,
                Article = article,
                Catalogue = catalog
            };

            return result;
        }

        /// <summary>
        /// Importe une #<see cref="PageArticle"/> du fichier XML et l'enregistre dans la base de donnée. Si la PageArticle importée est déjà en base de données,
        /// la supprime et la restaure avec les nouvelles données.
        /// </summary>
        /// <param name="articleElement">XElement de article</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <param name="page">Page en liaison avec la PageArticle</param>
        /// <param name="article">Article en liaison avec la PageArticle</param>
        /// <returns>Retourne la PageArticle importée</returns>
        private static PageArticle ParsePageArticleElement(XElement articleElement, LISAEntities entities, Page page, Article article)
        {
            int pageArticleCoordX = int.Parse(articleElement.Element(XName.Get("zones")).Element(XName.Get("zone")).Element(XName.Get("coordx")).Value);
            int pageArticleCoordY = int.Parse(articleElement.Element(XName.Get("zones")).Element(XName.Get("zone")).Element(XName.Get("coordy")).Value);
            int pageArticleWidth = int.Parse(articleElement.Element(XName.Get("zones")).Element(XName.Get("zone")).Element(XName.Get("width")).Value);
            int pageArticleHeight = int.Parse(articleElement.Element(XName.Get("zones")).Element(XName.Get("zone")).Element(XName.Get("height")).Value);

            PageArticle result = entities.PageArticles.FirstOrDefault(p => p.IdPage == page.Id && p.IdArticle == article.Id);

            if (result != null)
            {
                entities.PageArticles.Remove(result);
            }

            result = new PageArticle()
            {
                Article = article,
                Page = page,
                ZoneCoordonnéesX = pageArticleCoordX,
                ZoneCoordonnéesY = pageArticleCoordY,
                ZoneHauteur = pageArticleHeight,
                ZoneLargeur = pageArticleWidth
            };

            return result;
        }

        /// <summary>
        /// Importe une #<see cref="Article"/> du fichier XML et l'enregistre dans la base de donnée. Si l'article importé est déjà en base de données,
        /// le supprime et le restaure avec les nouvelles données.
        /// </summary>
        /// <param name="articleElement">Element de article</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <param name="pageElement">Element de page</param>
        /// <returns>Retourne l'article importé</returns>
        private static Article ParseArticleElement(XElement articleElement, LISAEntities entities, XElement pageElement)
        {
            int articleId = int.Parse(articleElement.Attribute(XName.Get("id")).Value);
            string articleCode = articleElement.Element(XName.Get("code")).Value;
            string articleLibelle = articleElement.Element(XName.Get("label")).Value;
            string articleDescription = articleElement.Element(XName.Get("description")).Value;
            short articleQuantite = 1;
            string articleUnite = articleElement.Element(XName.Get("packaging")).Value;

            Article result = entities.Articles.FirstOrDefault(p => p.ImportId == articleId);

            if (result != null)
            {
                entities.Articles.Remove(result);
            }

            result = new Article()
            {
                ImportId = articleId,
                Code = articleCode,
                Libelle = articleLibelle,
                Description = articleDescription,
                Quantite = articleQuantite,
                Unite = articleUnite
            };

            return result;
        }

        /// <summary>
        /// Importe une #<see cref="Page"/> du fichier XML et l'enregistre dans la base de donnée. Si la page importée est déjà en base de données,
        /// la supprime et la restaure avec les nouvelles données.
        /// </summary>
        /// <param name="pageElement">XElement de page</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <param name="catalog">Catalogue en liaison avec la page</param>
        /// <returns>Retourne la page importée</returns>
        private static Page ParsePageElement(XElement pageElement, LISAEntities entities, Catalogue catalog)
        {
            int pageId = int.Parse(pageElement.Attribute(XName.Get("id")).Value);
            int pageNum = int.Parse(pageElement.Element(XName.Get("number")).Value);

            Page result = entities.Pages.FirstOrDefault(p => p.ImportId == pageId);

            if (result != null)
            {
                entities.Pages.Remove(result);
            }

            result = new Page()
            {
                ImportId = pageId,
                Numero = pageNum,
                Catalogue = catalog,
            };

            entities.Pages.Add(result);

            return result;
        }

        /// <summary>
        /// Importe un #<see cref="MagasinCatalogue"/> du fichier XML. Si le MagasinCatalogue est déjà en base de données,
        /// le supprime et le restaure avec les nouvelles données.
        /// </summary>
        /// <param name="shopElement">Element de magasin</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <param name="catalog">Catalogue en liaison avec la table MagasinCatalogue</param>
        /// <param name="shop">Magasin en liaison avec la table MagasinCatalogue</param>
        private static MagasinCatalogue ParseShopCatalogElement(XElement shopElement, LISAEntities entities, Catalogue catalog, Magasin shop)
        {
            DateTime shopStartdate = UnixTimeStampToDateTime(double.Parse(shopElement.Element(XName.Get("startDate")).Value));
            DateTime shopDisplaystartdate = UnixTimeStampToDateTime(double.Parse(shopElement.Element(XName.Get("displayStartDate")).Value));
            DateTime shopDisplayenddate = UnixTimeStampToDateTime(double.Parse(shopElement.Element(XName.Get("displayEndDate")).Value));


            MagasinCatalogue result = entities.MagasinCatalogues.FirstOrDefault(c => c.IdCatalogue == catalog.ImportId && c.IdMagasin == shop.Id);

            if (result != null)
            {
                entities.MagasinCatalogues.Remove(result);
            }

            result = new MagasinCatalogue()
            {
                DateDebut = shopDisplaystartdate,
                DateFin = shopDisplayenddate,
                Catalogue = catalog,
                Magasin = shop
            };

            entities.MagasinCatalogues.Add(result);

            return result;
        }

        /// <summary>
        /// Importe un #<see cref="Magasin"/> du fichier XML. Si le magasin est déjà en base de données,
        /// le supprime et le restaure avec les nouvelles données.
        /// </summary>
        /// <param name="shopElement">Element de magasin</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <param name="catalog">Element de catalogue</param>
        /// <returns>Retourne le magasin ajouté</returns>
        private static Magasin ParseShopElement(XElement shopElement, LISAEntities entities)
        {
            long shopId = long.Parse(shopElement.Attribute(XName.Get("id")).Value);

            Magasin result = entities.Magasins.FirstOrDefault(c => c.ImportId == shopId);

            if (result != null)
            {
                entities.Magasins.Remove(result);
            }

            result = new Magasin()
            {
                ImportId = shopId,
                Libelle = shopId.ToString()
            };

            entities.Magasins.Add(result);

            return result;
        }

        /// <summary>
        /// Importe un #<see cref="Catalogue"/> du fichier XML et l'enregistre dans la base de donnée. Si le catalogue est déjà en base de données,
        /// le supprime et le restaure avec les nouvelles données.
        /// </summary>
        /// <param name="catalogElement">Element de catalogue</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <param name="operation">Element d'opération</param>
        /// <returns>Retourne le catalogue importé.</returns>
        private static Catalogue ParseCatalogElement(XElement catalogElement, LISAEntities entities, Operation operation)
        {
            Catalogue result = null;

            long catalogId = long.Parse(catalogElement.Attribute(XName.Get("id")).Value);
            string catalogType = catalogElement.Element(XName.Get("type")).Value;
            string catalogLabel = catalogElement.Element(XName.Get("label")).Value;
            string catalogSpeed = catalogElement.Element(XName.Get("speed")).Value;
            int catalogWidth = int.Parse(catalogElement.Element(XName.Get("catalogWidth")).Value);
            int catalogHeight = int.Parse(catalogElement.Element(XName.Get("catalogHeight")).Value);

            result = entities.Catalogues.FirstOrDefault(c => c.ImportId == catalogId);

            if (result != null)
            {
                entities.Catalogues.Remove(result);
            }

            result = new Catalogue()
            {
                ImportId = catalogId,
                Type = catalogType,
                Libelle = catalogLabel,
                Vitesse = catalogSpeed,
                Largeur = catalogWidth,
                Hauteur = catalogHeight,
                Operation = operation
            };

            entities.Catalogues.Add(result);

            return result;
        }

        /// <summary>
        /// Importe une <see cref="Operation"/> du fichier XML et l'enregistre dans la base de données. Si l'opération est déjà en base de données,
        /// la supprime et la restaure avec les nouvelles données.
        /// </summary>
        /// <param name="operationElement">XElement d'opération</param>
        /// <param name="entities">Gestionnaire DB</param>
        /// <returns>Retourne l'operation importée</returns>
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
