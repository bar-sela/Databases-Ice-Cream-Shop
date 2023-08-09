# Ice Cream Shop

This project is a clone of a project i did with my partner in the "Databases" course. 

Link for the original: https://github.com/ShaniVahav/Databases-project-2-Ice-Cream-Shop.git


![](https://blog-assets.lightspeedhq.com/img/2021/03/e4bcf36b-blog_are-you-really-ready-to-open-an-ice-cream-shop_1200x628.jpg)

## MySQL and MongoDB project of managing Ice Cream Shop
>A costumer arrives to our shop and can order an ice cream with different types of flavors, cones and topings.

## Our Menu
> * There is 10 flavors of Ice Cream : {"Chocolate", "Mekupelet", "Vanilla", "Strawberry", "Fistuk", "Bamba", "Coffee", "Begale", "Diet", "Gluten Free"}.
> * There is 3 topings : {"Hot Chocolate", "Peanuts", "Maple"}.
> * There is 3 different type of cones : {"Regular cone", "Special cone", "Box"}.

## The rules of our shop
> * In regular cone you can put only up to 3 ice cream balls, and put toping only if number of balls > 1
> * In special cone you can put only up to 3 ice cream balls, but can add toping for any amount of balls
> * In box you can put large number of balls and topings
> * If one of the flavors is "Chocolate" or "Mekupelet" you can't add "Hot Chocolate" toping
> * If one of the flavors is "Vanilla" you can't add "Maple" toping

## The prices
> * The price for regular cone with one ball is 7, two balls is 12 and three balls is 18.
> * The price for special cone with one ball is 9, two balls is 14 and three bals is 20.
> * The price for box with one ball is 12, two balls is 17 and so on (+5 for every ball).
> * The price for every toping is 2.

## Running the program
``` bash
# Clone the repository
# Open terminal on you system
# Make sure MySQL and MongoDB packages are instulled
# Make sure you have running MySQL workbench and MongoDB Compass in the backround
$ Run "dotnet run"
# Enter your disired command from the given options
