using System;
using System.Data;
using System.Diagnostics;//used for Stopwatch class

using MySql.Data;
using MySql.Data.MySqlClient;

using MySqlAccess;
using BusinessLogic;
using System.Collections;
using BusinessEntities;
using System.Reflection.PortableExecutable;

// See https://aka.ms/new-console-template for more information

Stopwatch stopwatch = new Stopwatch();
int sales_amount = 0;
int sum_price = 0;

int userInput = -1;
int price;
int round_number;

int package = -1;
var toppingsArraylist = new ArrayList();
int iceCreamBallsNumber = 0;                                    /// משתנה של כמות הכדורים 
var fDict = new Dictionary<int, int>();  ///// פה אני יוצר את המילון 
for (int i = 1; i < 11; i++)
{
    fDict.Add(i, 0);
}

Console.WriteLine("Please choose a database");
Console.WriteLine("1 - MySql");
Console.WriteLine("2 - MongoDB");
int db = Int32.Parse(Console.ReadLine());

if(db == 1)
{
    Console.WriteLine("Please create tables first, by pressing '1'");
    Console.WriteLine("1 - create tables");
    userInput = Int32.Parse(Console.ReadLine());


    if (userInput == 1)
        Logic.createTables();
}

if(db == 2)
{
    MongoAccess.MongoAccess.MongoTables();
}

NEW_ORDER:
round_number = 0;
price = 0;
package = -1;
toppingsArraylist.Clear();
iceCreamBallsNumber = 0;

Console.WriteLine("\nHi! Welcome to our Ice Cream shop\n");
Console.WriteLine("Please choose a task:");
Console.WriteLine("1 - order an ice cream");
Console.WriteLine("2 - exit");
Console.WriteLine("3 - Goto sales summary");
userInput = Int32.Parse(Console.ReadLine());

if (userInput == 2)
{
    System.Environment.Exit(0);
}
if (userInput == 3)
{
      SUMMARY:
                    Console.WriteLine("Please choose a task:");
                    Console.WriteLine("1 - View daily report");
                    Console.WriteLine("2 - View incomplete sales");
                    userInput = Int32.Parse(Console.ReadLine());

                    if(userInput == 1)
                    {
                        Console.WriteLine("\n____________________________________");
                        Console.WriteLine("Number of sales: " + sales_amount);
                        Console.WriteLine("Total sales amount: " + sum_price + " nis");
                        double avg = sum_price/sales_amount;
                        Console.WriteLine("Average sale amount: " + avg + " nis");
                        Console.WriteLine("____________________________________");
                    }

                    if(userInput == 2)
                    {
                        Logic.incompleteSales(db);
                    }

                    goto NEW_ORDER;
}

EDIT:
// create a sale
DateTime date = DateTime.Now;
Sale s = new Sale(date, price);
if(db == 1)
    MySqlAccess.MySqlAccess.insertObject_Sale(s);
else
    MongoAccess.MongoAccess.insertObject_Sale(s);


ANOTHER_ORDER:
round_number++;
package = -1;
toppingsArraylist.Clear();
iceCreamBallsNumber = 0; 

for (int i = 1; i < 11; i++)
{
    fDict[i] = 0;
}


    Console.WriteLine("Please choose a package:");
    Console.WriteLine("1 - regular cone");
    Console.WriteLine("2 - special cone");
    Console.WriteLine("3 - box");
    userInput = Int32.Parse(Console.ReadLine());
    create_an_order.flavours(ref fDict, ref iceCreamBallsNumber, userInput);

    switch (userInput)
    {
        case 1:
            package = 11;
            create_an_order.toppings_for_regular(ref fDict, ref iceCreamBallsNumber, ref toppingsArraylist);
            break;
        case 2:
            package = 12;
            create_an_order.toppings_for_special(ref fDict, ref toppingsArraylist);
            break;
        case 3:
            package = 13;
            create_an_order.toppings_for_box(ref fDict, ref toppingsArraylist);
            break;
    }

// insert the round of the order to data base 
Console.WriteLine("\n________________________________\n");
Console.WriteLine("Do you want to edit your order?");
Console.WriteLine("1 - Yes");
Console.WriteLine("2 - No");
userInput = Int32.Parse(Console.ReadLine());
if(userInput == 1  )
{
    BusinessLogic.edit.delete(db);
    round_number = 0;
    goto EDIT;
}

// insert the round of the order to database 
BusinessLogic.Logic.fillTableOrder(db, ref toppingsArraylist, round_number, ref fDict, package);

// calculate the price:
if (package == 11)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 7;
            break;
        case 2:
            price += 12;
            break;
        case 3:
            price += 18;
            break;
    }
}

if (package == 12)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 9;
            break;
        case 2:
            price += 14;
            break;
        case 3:
            price += 20;
            break;
    }
}

if (package == 13)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 12;
            break;
        case 2:
            price += 17;
            break;
        case 3:
            price += 23;
            break;
        default:
            price += 23 + (iceCreamBallsNumber-3)*6;
            break;
    }
}
price += toppingsArraylist.Count*2;

Console.WriteLine("\nPlease choose a task:");
Console.WriteLine("1 - Pay (After payment, the order cannot be canceled)");
Console.WriteLine("2 - Delete");
Console.WriteLine("3 - Add another order");
Console.WriteLine("4 - Get most common ingredient");
Console.WriteLine("5 - Get most common flavour");
userInput = Int32.Parse(Console.ReadLine());

    switch (userInput)
    {
        case 1:  // chose to pay
            sum_price += price; 
            sales_amount++;         
            // update the price in database 
            if(db == 1)
                MySqlAccess.MySqlAccess.update_price(price);
            else
                MongoAccess.MongoAccess.update_price_mongo(price);

            Console.WriteLine("Please choose a task:");
            Console.WriteLine("1 - Check the bill");
            Console.WriteLine("2 - New Order");
            Console.WriteLine("3 - exit");
            userInput = Int32.Parse(Console.ReadLine());

            switch (userInput)
            {
                case 1:
                    edit.bill(date, price);
                    goto NEW_ORDER;
                    break;
                case 2:
                    goto NEW_ORDER;
                case 3:
                    Console.WriteLine("Thanks! Hope to see you next time");
                    System.Environment.Exit(0);
                    break;
            }

            break;

        case 2:
            BusinessLogic.edit.delete(db);
            Console.WriteLine("Thank you for your time");
            goto NEW_ORDER;
            break;

        case 3:
            goto ANOTHER_ORDER;
            break;
         case 4 :
            BusinessLogic.Logic.getMostCommonIN(db, userInput);
            goto NEW_ORDER;
            break;
        case 5:
             BusinessLogic.Logic.getMostCommonIN(db, userInput);
             goto NEW_ORDER;
             break ;
    }