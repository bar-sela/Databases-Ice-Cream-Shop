
using MongoDB.Driver;
using MongoDB.Bson;

using MongoDB.Bson.Serialization;
using BusinessEntities;
using BusinessLogic;
using System.Collections;

namespace MongoAccess
{
    class MongoAccess
    {
        public static void MongoTables()
        {
            // connection
            var client = new MongoClient("mongodb://localhost:27017");
            // creation of the db
            client.DropDatabase("ice_cream_shop");
            IMongoDatabase database = client.GetDatabase("ice_cream_shop");

            // creation of the collections
            var ingredients = database.GetCollection<BsonDocument> ("ingredients");
            var orders = database.GetCollection<BsonDocument> ("orders");
            var sales = database.GetCollection<BsonDocument> ("sales");

            insert_ing();
        }

        public static void insert_ing()
        {
            List<BsonDocument> documents = new List<BsonDocument>();
            var document = new BsonDocument
            {
                {
                //////It is desirable that the documents in mongo will be bsons. 
                        "ingredients", new BsonDocument
                            {

                                {"choclate", 1 },{"vanila", 2 },{"mecupelet", 3 },{"Banana", 4 },{"orange", 5 },{"coconut", 6 },{"Oreo", 7 }
                                ,{"coffee", 8},{"strawberry", 9 },{"mango",10 },{"regular_cone", 11 },{"special_cone", 12 },
                                {"box", 13},{"TOPchoclate", 14},{"TOPpeanuts", 15},{"TOPmaple", 16}
                                 
                            }
                 }       
            };
            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument> ("ingredients");
            sales.InsertOne(document);
         }

        public static void insertObject_Sale(Sale s)
        {
           // List<BsonDocument> documents = new List<BsonDocument>();
             var document = new BsonDocument
                {                         
                    {"id_sale", Sale.id },
                    {"date", s.date},
                    {"price", s.price}  
                };
            // documents.Add(document);

            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument> ("sales");
            sales.InsertOne(document);
        }


        public static void insertObjectToOrders_mongo(iceCreamOrder a, int round_number)
        {
            try
            {
                int id = Sale.getId();
                List<BsonDocument> documents = new List<BsonDocument>();

                // insert package
                var document = new BsonDocument
                    {                        
                        {"id_order", id},
                        {"package", new BsonDocument
                            {
                                {"round_number", round_number},
                                {"id_ingredient", a.package},
                                {"amount", 1}
                            }
                        }
                    };
                documents.Add(document);

                // insert flavours 
                foreach(var item in a.fdict)
                {
                    int k = item.Key;
                    int v = item.Value;
                    if(v == 0 || k == 11 )
                        continue;

                    document = new BsonDocument
                    { 
                        {"id_order", id },
                        {"flavour", new BsonDocument
                            {
                                {"round_number", round_number},
                                {"id_ingredient", k},
                                {"amount", v}
                            }
                        }
                    };

                documents.Add(document);
                } 


                // insert toppings         
                foreach(int item in a.toppings)
                { 
                    document = new BsonDocument
                    { 
                        {"id_order", id },
                        {"topping", new BsonDocument
                            {
                                {"round_number", round_number},
                                {"id_ingredient", item},
                                {"amount", 1}
                            }
                        }
                    };

                    documents.Add(document);
                }

                var client = new MongoClient("mongodb://localhost:27017");
                IMongoDatabase database = client.GetDatabase("ice_cream_shop");
                var orders = database.GetCollection<BsonDocument> ("orders");
                orders.InsertMany(documents);    
            }

            catch (Exception ex)
            {
                {   
                    Console.WriteLine("insert object func");
                    Console.WriteLine(ex.ToString());
                }

            }

        }

        public static void update_price_mongo(int _price)
        {  
            int _id = Sale.getId();
            MongoClient client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument>("sales");

            var _filter = Builders<BsonDocument>.Filter.Eq("id_sale", _id);
            var _update = Builders<BsonDocument>.Update.Set("price", _price);
            sales.UpdateOne(_filter, _update);
        }


        public static void deleteOrder_mongo(int id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ice_cream_shop");
            var orders = database.GetCollection<BsonDocument>("orders");

            var _filter = Builders<BsonDocument>.Filter.Eq("id_order", id);
            orders.DeleteMany(_filter);
        }


        public static void get_incompleteSalesMongo()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument>("sales");

            var _filter = Builders<BsonDocument>.Filter.Eq("price", 0);
            var doc = sales.Find(_filter).ToList();

            Console.WriteLine("_______________________________");
            foreach(BsonDocument _doc in doc)
            {
                Console.WriteLine(_doc.ToString());
            }   
            Console.WriteLine("_______________________________");
        }

    }
}