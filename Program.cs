using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDevam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>()
            { new Category {CategoryID=1, CategoryName="Bilgisayar"},
              new Category {CategoryID=2, CategoryName="Telefon"}
            };
            List<Product> products = new List<Product>()
            {
                new Product { ProductID=1,CategoryID=1,ProductName="Acer laptop",QuantityPerUnit="32GB RAM",UnitPrice=100000,UnitsInStok=5},
                new Product { ProductID=2,CategoryID=1,ProductName="Asus laptop",QuantityPerUnit="16GB RAM",UnitPrice=8000,UnitsInStok=3},
                new Product { ProductID=3,CategoryID=1,ProductName="HP laptop",QuantityPerUnit="8GB RAM",UnitPrice=6000,UnitsInStok=2},
                new Product { ProductID=4,CategoryID=2,ProductName="Samsung Telephone",QuantityPerUnit="4GB RAM",UnitPrice=3000,UnitsInStok=15},
                new Product { ProductID=5,CategoryID=2,ProductName="Apple Telephone",QuantityPerUnit="4GB RAM",UnitPrice=8000,UnitsInStok=0},
                new Product { ProductID=6,CategoryID=2,ProductName="Huawei Telephone",QuantityPerUnit ="12GB RAM", UnitPrice=7000,UnitsInStok=0}
                //Bu veri kaynakları veri tabanından gelecek !Şimdilik boyle oldugunu varsayıyoruz =0
            };
            #region Linq çeşitli sorgulama özellikleri

            //------
            TestWhereContain(products);
            //------
            AnyTest(products);
            //-----
            //Predicate=> lambda yı yazabilecegimiz anlamına geliyor. 
            TestFind(products);
            //------
            TestFindAll(products);


            #endregion
            #region Stokta kalmayan ürünleri listele
            //List <Product> 
            GetProducts1(products);
            //-------------------------------------------------------------------------------------------------------------
            //2. gösterim şekli 
            GetProducts2(products);


            #endregion
            Console.ReadLine();
        }

        private static void TestWhereContain(List<Product> products)
        {                                                                     // fiyatı azalana gore sırala
            var resultW = products.Where(p => p.ProductName.Contains("Tel")).OrderByDescending(p=> p.UnitPrice);
            foreach (var product in resultW)
            {
                Console.WriteLine(product.ProductName);

            }
        }

        private static void TestFindAll(List<Product> products)
        {
            var resultFA = products.FindAll(p => p.ProductName.Contains("tel"));
            Console.WriteLine(resultFA);// liste döner , findAll where gibidir. Contains => içerir. findAll yerine çogunlukla where kullanılır. 
        }

        private static void TestFind(List<Product> products)
        {
            var resultF = products.Find(p => p.ProductID == 3); // aradıgımız krediye uygun nesnenin kendisini verir. 
            Console.WriteLine(resultF.ProductName);
        }

        private static void AnyTest(List<Product> products)
        {
            var resultx = products.Any(p => p.ProductName == "HP laptop"); // case sensitive büyük küçkü harf duyarlı 
            Console.WriteLine(resultx);
        }

        private static void GetProducts2(List<Product> products)
        {
            var result2 = from p in products
                          where p.UnitsInStok < 1
                          orderby p.ProductName descending
                          select p;

            foreach (var product2 in result2)
            {
                Console.WriteLine(product2.ProductName);
            }
        }

        private static void GetProducts1(List<Product> products)
        {
            var result = products.Where(p => p.UnitsInStok < 1).ToList(); // stokta kalmayanları listele 
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);

            }
        }



        class Product
        {
            public int ProductID { get; set; }
            public int CategoryID { get; set; }
            public string ProductName { get; set; }
            public string QuantityPerUnit { get; set; } //Ürün açıklaması
            public decimal UnitPrice { get; set; } // birim fiyatı
            public int UnitsInStok { get; set; } // ürünün stok adedi

        }
        class Category
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
        }


    }
}
