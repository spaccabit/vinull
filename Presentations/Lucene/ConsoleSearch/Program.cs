using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace ConsoleSearch {
    class Program {
        static Lucene.Net.Store.RAMDirectory ramDir = null;

        static void Main(string[] args) {

            while (true) {
                Console.Write("Command: ");
                String command = Console.ReadLine();
                String[] cArray = command.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (cArray.Length != 0) {
                    switch (cArray[0].ToUpper()) {
                        case "QUIT":
                            return;

                        case "INDEX":
                            CommandIndex(cArray);
                            break;

                        case "UPDATE":
                            CommandUpdate(cArray);
                            break;

                        case "LOAD":
                            CommandLoad(cArray);
                            break;

                        case "SEARCH":
                            CommandSearch(cArray);
                            break;

                        case "FREEMEM":
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private static void CommandIndex(string[] cArray) {

            try {
                Directory.CreateDirectory(cArray[1]);
            }
            catch (Exception ex) {
                Console.WriteLine("Unable to create index: {1}" + ex.Message);
                return;
            }

            Console.Write("Getting Data...   ");
            AdventureWorksDataContext awCtx = new AdventureWorksDataContext();
            var products = from product in awCtx.Products
                           select new {
                               product.Color,
                               product.ListPrice,
                               product.Name,
                               product.ProductNumber,
                               Model = product.ProductModel.Name,
                               Description = product.ProductModel.ProductModelProductDescriptions.Single(d => d.Culture.Equals("en")).ProductDescription.Description,
                               Category = product.ProductCategory.Name
                           };
            Console.WriteLine("Done");

            Console.Write("Creating Index...   ");
            Lucene.Net.Store.FSDirectory dir = Lucene.Net.Store.FSDirectory.GetDirectory(cArray[1], true);
            Lucene.Net.Index.IndexWriter idx = new Lucene.Net.Index.IndexWriter(dir, new Lucene.Net.Analysis.Standard.StandardAnalyzer(), true);
            Console.WriteLine("Done");

            int FailCount = 0;
            foreach (var p in products) {
                String searchText = String.Join(" ", new String[] {p.Category, p.Color, p.Description, 
                                                                   p.Model, p.Name, p.ProductNumber});

                Console.Write(String.Format("Adding {0}...   ", p.ProductNumber));

                Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();
                doc.Add(new Lucene.Net.Documents.Field("sku", p.ProductNumber, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.UN_TOKENIZED));
                doc.Add(new Lucene.Net.Documents.Field("_searchtxt", searchText, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.TOKENIZED));
                doc.Add(new Lucene.Net.Documents.Field("price", p.ListPrice.ToString("0000000.00"), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.UN_TOKENIZED));

                DateTime start = DateTime.Now;
                bool saved = false;

                while (start.AddSeconds(5) >= DateTime.Now) {
                    try {
                        idx.AddDocument(doc);
                        saved = true;
                        break;
                    }
                    catch {
                        Thread.Sleep(10);
                    }
                }

                if (saved) Console.WriteLine("Done");
                else {
                    Console.WriteLine("Failed");
                    FailCount++;
                }
            }

            Console.Write("Optimizing/Closing Index...   ");
            idx.Optimize();
            idx.Close();
            dir.Close();
            GC.Collect();
            Console.WriteLine("Done - Failed: " + FailCount.ToString());

        }

        private static void CommandUpdate(string[] cArray) {

            Console.Write("Opening Index...   ");
            Lucene.Net.Store.FSDirectory dir = Lucene.Net.Store.FSDirectory.GetDirectory(cArray[1]);
            Lucene.Net.Index.IndexModifier idx = new Lucene.Net.Index.IndexModifier(dir, new Lucene.Net.Analysis.Standard.StandardAnalyzer(), false);
            Console.WriteLine("Done");

            Console.Write("Getting Data...   ");
            AdventureWorksDataContext awCtx = new AdventureWorksDataContext();
            var products = from product in awCtx.Products
                           select new {
                               product.Color,
                               product.ListPrice,
                               product.Name,
                               product.ProductNumber,
                               Model = product.ProductModel.Name,
                               Description = product.ProductModel.ProductModelProductDescriptions.Single(d => d.Culture.Equals("en")).ProductDescription.Description,
                               Category = product.ProductCategory.Name
                           };
            Console.WriteLine("Done");


            int FailCount = 0;
            foreach (var p in products) {

                Console.Write(String.Format("Updating {0}...", p.ProductNumber));

                Lucene.Net.Index.Term tSku = new Lucene.Net.Index.Term("sku", p.ProductNumber);

                DateTime start = DateTime.Now;
                bool deleted = false;

                while (start.AddSeconds(5) >= DateTime.Now) {
                    try {
                        Int32 result = idx.DeleteDocuments(tSku);
                        Console.Write(" Del Count: " + result.ToString() + " ");
                        deleted = true;
                        break;
                    }
                    catch {
                        Thread.Sleep(10);
                    }
                }


                String searchText = String.Join(" ", new String[] {p.Category, p.Color, p.Description, 
                                                                   p.Model, p.Name, p.ProductNumber});

                Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();
                doc.Add(new Lucene.Net.Documents.Field("sku", p.ProductNumber, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.UN_TOKENIZED));
                doc.Add(new Lucene.Net.Documents.Field("_searchtxt", searchText, Lucene.Net.Documents.Field.Store.NO, Lucene.Net.Documents.Field.Index.TOKENIZED));
                doc.Add(new Lucene.Net.Documents.Field("price", p.ListPrice.ToString("0000000.00"), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.UN_TOKENIZED));

                start = DateTime.Now;
                bool saved = false;

                while (start.AddSeconds(5) >= DateTime.Now && deleted) {
                    try {
                        idx.AddDocument(doc);
                        saved = true;
                        break;
                    }
                    catch {
                        Thread.Sleep(10);
                    }
                }

                if (saved) Console.WriteLine("Done");
                else {
                    Console.WriteLine("Failed");
                    FailCount++;
                }
            }

            Console.Write("Optimizing/Closing Index...   ");
            idx.Optimize();
            idx.Close();
            dir.Close();
            GC.Collect();
            Console.WriteLine("Done - Failed: " + FailCount.ToString());

        }

        private static void CommandLoad(string[] cArray) {
            try {
                if (ramDir != null)
                    ramDir.Close();
                ramDir = new Lucene.Net.Store.RAMDirectory(cArray[1]);
                GC.Collect();
            }
            catch (Exception ex) {
                Console.WriteLine("Unable to load index: {1}" + ex.Message);
                return;
            }
        }

        private static void CommandSearch(string[] cArray) {
            if (ramDir == null) {
                Console.WriteLine("Index not loaded");
                return;
            }

            DateTime start = DateTime.Now;

            String srch = String.Join(" ", cArray, 1, cArray.Length - 1);
            Lucene.Net.Search.IndexSearcher idx = new Lucene.Net.Search.IndexSearcher(ramDir);
            Lucene.Net.QueryParsers.QueryParser qp = new Lucene.Net.QueryParsers.QueryParser("_searchtxt", new Lucene.Net.Analysis.Standard.StandardAnalyzer());
            qp.SetDefaultOperator(Lucene.Net.QueryParsers.QueryParser.Operator.AND);
            Lucene.Net.Search.Sort sort = new Lucene.Net.Search.Sort("price");
            Lucene.Net.Search.BooleanQuery.SetMaxClauseCount(1000);

            Lucene.Net.Search.Hits hits = idx.Search(qp.Parse(srch), sort);

            List<String> matches = new List<string>();
            for (int i = 0; i < hits.Length(); i++) {
                Lucene.Net.Documents.Document doc = hits.Doc(i);
                matches.Add(doc.Get("sku"));
                if (i >= 99) break;
            }

            idx.Close();
            TimeSpan lTime = DateTime.Now - start;
            
            AdventureWorksDataContext awCtx = new AdventureWorksDataContext();
            var products = from product in awCtx.Products
                           where matches.Contains(product.ProductNumber)
                           select new {
                               product.Color,
                               product.ListPrice,
                               product.Name,
                               product.ProductNumber,
                               Model = product.ProductModel.Name,
                               Description = product.ProductModel.ProductModelProductDescriptions.Single(d => d.Culture.Equals("en")).ProductDescription.Description,
                               Category = product.ProductCategory.Name
                           };

            TimeSpan dbTime = DateTime.Now - start + lTime;

            foreach (var p in products) {
                Console.WriteLine(String.Format("{0,-10} {1,10} {2,-10} {3,-20} {4}", 
                        new String[] { p.ProductNumber, p.ListPrice.ToString("c"),
                                       p.Color, p.Category, p.Model}));
            }

            Console.WriteLine(String.Format("Hits: {0} Search: {1} Lookup: {2}", 
                hits.Length(), lTime.TotalSeconds.ToString("0.00"), dbTime.TotalSeconds.ToString("0.00")));
        }
    }
}
