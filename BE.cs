using System.Collections;

//added for mongo semi-generated class
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessEntities
{

    // data holder classes (theoreticaly may be a struct ?)
    
    class iceCreamOrder
    {
        public int  package;
        public Dictionary<int, int> fdict;
        public ArrayList toppings;

        public int Package { get => package; set => package = value; }

        public iceCreamOrder(int package, Dictionary<int, int> fdict, ArrayList toppings)
        {
            this.Package = package;
            this.fdict = fdict;
            this.toppings = toppings;
        }
    }

    class Sale
    {
        public static int id = 0;
        public DateTime date;
        public int price;

        public Sale(DateTime date, int price)
        {
            this.date = date;
            this.price = price;
            id++;
        }

        public static int getId()
        {
            return id;
        }
    }
}

namespace BusinessEntities
{

    // data holder classes (theoreticaly may be a struct ?)
    public class Owner
    {
        string name;
        string phone;
        string address;

        public Owner(string name, string phone, string address)
        {
            this.name = name;
            this.phone = phone;
            this.address = address;
        }

        public string getName() { return name; }
        public string getPhone() { return phone; }
        public string getAddress() { return address; }

        public override string ToString()
        {
            return base.ToString() + ": " + name + " , "+phone+" , "+address;
        }
    }

    public class Vehicle
    {
        string manufacturer;
        string color;
        int year;

        public Vehicle(string manufacturer, string color, int year)
        {
            this.manufacturer = manufacturer;
            this.color = color;
            this.year = year;
        }
        public string getManufacturer() { return manufacturer; }
        public string getColor() { return color; }
        public int getYear() { return year; }

        public override string ToString()
        {
            return base.ToString() + ": " + manufacturer + " , "+color+" , "+year;
        }
    }


    public class VTask
    {
        string name;
        string description;
        int price;

        public VTask(string name, string description, int price)
        {
            this.name = name;
            this.description = description;
            this.price = price;
        }

        public string getName() { return name; }
        public string getDescription() { return description; }
        public int getPrice() { return price; }

        public override string ToString()
        {
            return base.ToString() + ": " + name + " , "+description+" , "+price;
        }
    }

    public class VOwn
    {
        int idOwner;
        int idVehicle;

        public VOwn(int idOwner, int idVehicle)
        {
            this.idOwner = idOwner;
            this.idVehicle = idVehicle;
        }

        public int getIdOwner() { return idOwner; }
        public int getIdVehicle() { return idVehicle; }
        
        public override string ToString()
        {
            return base.ToString() + ": " + idOwner + " , "+idVehicle;
        }
    }
    public class Order
    {
        int idVehicle;
        int idTask;
        string orderDate;
        string completeDate;
        int completed;
        int payed;

        public Order(int idVehicle, int idTask,string orderDate, string completeDate, int completed, int payed)
        {
            this.idVehicle = idVehicle;
            this.idTask = idTask;
            this.orderDate = orderDate;
            this.completeDate = completeDate;
            this.completed = completed;
            this.payed = payed;
        }

        
        public int getIdVehicle() { return idVehicle; }
        public int getIdTask() { return idTask; }
        public string getOrderDate() { return orderDate; }
        public string getCompleteDate() { return completeDate; }
        public int getCompleted() { return completed; }
        public int getPayed() { return payed; }
        
        public override string ToString()
        {
            return base.ToString() + ": " + idVehicle + " , " + idTask + " , " + orderDate + " , " + completeDate + " , " + completed + " , " + payed;
        }
    }

    public class MongoOrder
    {
        //a work around: we will have 3 classes in one, to get its' data ?
        Owner owner;
        Vehicle vehicle;
        VTask vTask;

        string orderDate;
        string completeDate;
        int completed;
        int payed;

        public void setStatus(string orderDate, string completeDate, int completed, int payed)
        {
            this.orderDate = orderDate;
            this.completeDate = completeDate;
            this.completed = completed;
            this.payed = payed;
        }

        public void setOwner(Owner o){
            owner = o;
        }

        public void setViecle(Vehicle v){
            vehicle = v;
        }

        public void setTask(VTask t){
            vTask = t;
        }

        public Owner GetOwner(){
            return owner;
        }

        public Vehicle getVehicle(){
            return vehicle;
        }

        public VTask getVtask(){
            return vTask;
        }

        public string getOrderDate() { return orderDate; }
        public string getCompleteDate() { return completeDate; }
        public int getCompleted() { return completed; }
        public int getPayed() { return payed; }

    }
}