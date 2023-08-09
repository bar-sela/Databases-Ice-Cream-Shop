using MySql.Data;
using MySql.Data.MySqlClient;

using BusinessEntities;
using BusinessLogic;
using System.Collections;
using System.Windows.Markup;
using System.Reflection.PortableExecutable;
using System.Collections.Generic;


namespace MySqlAccess
{
    class MySqlAccess
    {
    
        static string connStr = "server=127.0.0.1;user=root;port=3306; password=Shani41128";

        // inspiration from https://www.youtube.com/watch?v=Die4mKMQ1_8
        public static void get_incompleteSales()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using(var cmd = new MySqlCommand("SELECT id_SALE, date FROM ice_cream_shop.sales WHERE price = 0", conn))
                {
                    using(var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("_______________________________");
                        while(reader.Read())  // read row by row
                        {
                            var id = reader.GetString(0);
                            var date = reader.GetString(1);

                            Console.WriteLine($"ID sale: {id}, date: {date}");
                        }
                        Console.WriteLine("_______________________________");
                    }
                }
            }
        }


         public static void deleteOrderFromDB(int id)
        {
               try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("\nConnecting to MySQL...");
                conn.Open();
                string sql = "DELETE FROM ice_cream_shop.orders WHERE id_ORDER="      +id+    ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
             }  
                   catch (Exception ex)
            {
                
                    Console.WriteLine(ex.ToString());
            }
        }
            



          public static void update_price(int price)
        {
             try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "UPDATE ice_cream_shop.sales " +
                "SET price = " + price +
                " WHERE id_SALE = " + getId() + ";";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            catch (Exception ex)
            {
                
                    Console.WriteLine("insert objcet func");
                    Console.WriteLine(ex.ToString());
            }
        }

        /*
        this call will represent CRUD operation
        CRUD stands for Create,Read,Update,Delete
        */
        public static void createTables()
        {
            
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();


                string sql = "DROP DATABASE IF EXISTS Ice_Cream_Shop;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "CREATE DATABASE Ice_Cream_Shop;";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // ------- create INGREDIENTS ------- //
                 sql = "CREATE TABLE `Ice_Cream_Shop`.`INGREDIENTS` (" +
                    "`id_INGREDIENT` INT NOT NULL AUTO_INCREMENT," +
                    "`item` VARCHAR(45) NOT NULL," +
                    "PRIMARY KEY (`id_INGREDIENT`));";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // ------- create ORDERS ------- //

                sql = "CREATE TABLE `Ice_Cream_Shop`.`SALES` (" +
                  "`id_SALE` INT NOT NULL AUTO_INCREMENT, " +
                  "`date` varchar(45) NOT NULL," +
                  "`price` INT NOT NULL," +
                  "PRIMARY KEY (`id_SALE`));";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "CREATE TABLE `Ice_Cream_Shop`.`ORDERS` (" +
                    "`id_ORDER` INT NOT NULL, " +
                    " `ROUND_NUMBER` INT NOT NULL,"+
                    "`id_INGREDIENT` INT NOT NULL," +
                    "`amount` INT NOT NULL," +
                    // "FOREIGN KEY (id_INGREDIENT) REFERENCES INGREDIENTS(id_INGREDIENT), " +
                    // "FOREIGN KEY(id_ORDER) REFERENCES SALES(id_SALE)," +
                    "PRIMARY KEY (id_ORDER,ROUND_NUMBER,id_INGREDIENT));";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ArrayList a = Logic.fillTableIN();
                insertObject_ingredients(a);
               
                conn.Close();

            }
            catch (Exception ex)
            {
               
            }
        }

       
        public static void insertObjectToOrders(iceCreamOrder a, int round_number)
        {
            try
            {
                string sql = null;

                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                
                int id = getId();
                ///////////////////// insert cone 
                sql = "INSERT INTO ice_cream_shop.ORDERS(id_ORDER,ROUND_NUMBER,id_INGREDIENT,amount) " +
                "VALUES(" + id + "," + round_number + "," +a.Package + "," + "1" + ");";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                ////////////////// insert flavours 
                foreach(var item in a.fdict)
                {
                    if(item.Value == 0 || item.Key == 11 )
                        continue;
                    conn = new MySqlConnection(connStr);
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();

                    id = getId();
                    sql = "INSERT INTO ice_cream_shop.ORDERS(id_ORDER,ROUND_NUMBER,id_INGREDIENT,amount)" +
                    "VALUES(" + id + "," + round_number + "," +item.Key + "," + item.Value + ");";

                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }                                                                   
                  //////////////////// insert topiings         

                  foreach( var item in a.toppings) { 
             

                    conn = new MySqlConnection(connStr);
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();

                   
                    sql = "INSERT INTO ice_cream_shop.ORDERS(id_ORDER,ROUND_NUMBER,id_INGREDIENT,amount)"+
                         "VALUES(" + id + "," + round_number + "," +item + "," + "1" + ");";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

                catch (Exception ex)
            {
                {   
                        Console.WriteLine("insert objcet func");
                        Console.WriteLine(ex.ToString());
                }

            }
        }

        public static void insertObject_ingredients(ArrayList ingredient_arr)
        {
            try
            {
                string sql = null;
                foreach(string ingredient in ingredient_arr)
                {
                    MySqlConnection conn = new MySqlConnection(connStr);
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                
                    //string str = ingredient;
                    sql = "INSERT INTO `ice_cream_shop`.`Ingredients` (`item`) " +
                    "VALUES ('" + ingredient + "');";
                
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void insertObject_Sale(Sale s)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = null;

                sql = "INSERT INTO `ice_cream_shop`.`Sales` (`date`, `price`) " +
                "VALUES ('" + s.date + "', '" + s.price + "');";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static ArrayList readAll(string tableName)
        {
            ArrayList all = new ArrayList();

            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();


                string sql = "SELECT * FROM `Garage`."+tableName;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                    Object[] numb = new Object[rdr.FieldCount];
                    rdr.GetValues(numb);
                    //Array.ForEach(numb, Console.WriteLine);
                    all.Add(numb);
                }
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return all;
        }
 
          
     
        public static void searchMostCommon(string str)
        {
            ArrayList all = new ArrayList();

            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "use ice_cream_shop";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "create temporary table a as (SELECT ice_cream_shop.orders.id_INGREDIENT"+
                    ",sum(amount) as amount FROM ice_cream_shop.orders group by ice_cream_shop.orders.id_INGREDIENT);";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = str;
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if(rdr.Read())
                {
                    Object[] numb = new Object[rdr.FieldCount];
                    rdr.GetValues(numb);
                    Console.WriteLine((string)numb[0]);  
                }
                rdr.Close();
                conn.Close();
           }      
            catch (Exception ex)
            {
                  Console.WriteLine(ex.ToString());
            }


        }
         
        public  static int getId() {
            int ans= -5;
              // open connection 
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
            // mysql query 
            string sql = "SELECT id_SALE FROM ice_cream_shop.sales ORDER BY id_SALE DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine(rdr.HasRows);
            while (rdr.Read())
            {  // read
                Object[] numb = new Object[rdr.FieldCount];
                Console.WriteLine(numb[0]); 
                rdr.GetValues(numb);
                ans = (int)numb[0];
            }
            conn.Close();
            rdr.Close();
            return ans;
        }
    }
    

}
      
    
