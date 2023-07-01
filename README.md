# SOLIDPRINCIPLES


## Single responsibility principle

def : Every class should have only one responsibility

example without singularity : 

``` c#
public class UserService
{
   public void Register(string email, string password)
   {
      if (!ValidateEmail(email))
         throw new ValidationException("Email is not an email");
         var user = new User(email, password);

         SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject="HEllo foo" });
   }
   public virtual bool ValidateEmail(string email)
   {
     return email.Contains("@");
   }
   public bool SendEmail(MailMessage message)
   {
     _smtpClient.Send(message);
   }
}

```

here we are combinig the email + user service

we need to export the email functionality as a separate servce to ensure that we follow the singularity principle.

```c#
public class UserService
{
       EmailService _emailService;
       DbContext _dbContext;
       public UserService(EmailService aEmailService, DbContext aDbContext)
       {
          _emailService = aEmailService;
          _dbContext = aDbContext;
       }
       public void Register(string email, string password)
       {
          if (!_emailService.ValidateEmail(email))
             throw new ValidationException("Email is not an email");
             var user = new User(email, password);
             _dbContext.Save(user);
             emailService.SendEmail(new MailMessage("myname@mydomain.com", email) {Subject="Hi. How are you!"});

          }
   }
   public class EmailService
   {
              SmtpClient _smtpClient;
           public EmailService(SmtpClient aSmtpClient)
           {
              _smtpClient = aSmtpClient;
           }
           public bool virtual ValidateEmail(string email)
           {
              return email.Contains("@");
           }
           public bool SendEmail(MailMessage message)
           {
              _smtpClient.Send(message);
           }
}

```


## open/close principle

The Open/closed Principle says, "A software module/class is open for extension and closed for modification."

Here "Open for extension" means we must design our module/class so that the new functionality 
can be added only when new requirements are generated. "Closed for modification" means we have already developed a class, 
and it has gone through unit testing. We should then not alter it until we find bugs. As it says, a class should be open for 
extensions; we can use inheritance. OK, let's dive into an example.

Suppose we have a Rectangle class with the properties Height and Width.

```c#
public class Rectangle{
   public double Height {get;set;}
   public double Wight {get;set; }
}

```

lets say we want to calculate the total area.

```c#
public class AreaCalculator {
   public double TotalArea(Rectangle[] arrRectangles)
   {
      double area;
      foreach(var objRectangle in arrRectangles)
      {
         area += objRectangle.Height * objRectangle.Width;
      }
      return area;
   }
}

```


now we want to add the circle then the code chanegs like below

```c#
public class Rectangle{
   public double Height {get;set;}
   public double Wight {get;set; }
}
public class Circle{
   public double Radius {get;set;}
}
public class AreaCalculator
{
   public double TotalArea(object[] arrObjects)
   {
      double area = 0;
      Rectangle objRectangle;
      Circle objCircle;
      foreach(var obj in arrObjects)
      {
         if(obj is Rectangle)
         {
            area += obj.Height * obj.Width;
         }
         else
         {
            objCircle = (Circle)obj;
            area += objCircle.Radius * objCircle.Radius * Math.PI;
         }
      }
      return area;
   }
}

```

how ever if we need to add triangle then we will end up with more if block which is not good idea

so the solution would be like below.

```c#
public abstract class Shape
{
   public abstract double Area();
}

```
this  is a abstract class

then add the class 

```c#
      public class Rectangle: Shape
      {
         public double Height {get;set;}
         public double Width {get;set;}
         public override double Area()
         {
            return Height * Width;
         }
      }
      public class Circle: Shape
      {
         public double Radius {get;set;}
         public override double Area()
         {
            return Radius * Radus * Math.PI;
         }
      }
```


now add the implimentation class

```c#

   public class AreaCalculator
   {
      public double TotalArea(Shape[] arrShapes)
      {
         double area=0;
         foreach(var objShape in arrShapes)
         {
            area += objShape.Area();
         }
         return area;
      }
   }


```

##  Liskov Substitution Principle

The Liskov Substitution Principle (LSP) states, "you should be able to use any derived class instead of a parent class and have it behave in the same manner without modification.". 
It ensures that a derived class does not affect the behavior of the parent class; in other words, a derived class must be substitutable for its base class.


```c#
using System;
namespace SOLID_PRINCIPLES.LSP
{
    class Program
    {
        static void Main(string[] args)
        {
            Apple apple = new Orange();
            Console.WriteLine(apple.GetColor());
        }
    }
    public class Apple
    {
        public virtual string GetColor()
        {
            return "Red";
        }
    }
    public class Orange : Apple
    {
        public override string GetColor()
        {
            return "Orange";
        }
    }
}

```

i think a way it might be more clear is to say Fruit can be any type and any color, but a orange cannot be the color red and an apple cannot be of color orange , meaning we cannot replace a orange with an apple but fruit can be replaced with an orange or an apple because they are both Fruits, a apple is not an orange and a orange is not a apple.

Lets say the base class was Cooldrinks, and a sub class was Coke and the other Fanta Orange, then we would be able to replace the Base Type (cooldrinks) with either Coke or Fanta Orange because they are both Cooldrinks but if we ask for a Coke we do not expect or want a Fanta we want a Coke , if we ask for cooldrink we do not know what we will receive and any of the 2 would be sufficient. So if the base type is the same any of the 2 would work but we specifically want one sub type another would not be sufficient.



AFTER APPLYING THE LISKOVES SUBSTITUTION PRINCLIPLE.

```c#

using System;
namespace SOLID_PRINCIPLES.LSP
{
    class Program
    {
        static void Main(string[] args)
        {
            IFruit fruit = new Orange();
            Console.WriteLine($"Color of Orange: {fruit.GetColor()}");
            fruit = new Apple();
            Console.WriteLine($"Color of Apple: {fruit.GetColor()}");
            Console.ReadKey();
        }
    }
    public interface IFruit
    {
        string GetColor();
    }
    public class Apple : IFruit
    {
        public string GetColor()
        {
            return "Red";
        }
    }
    public class Orange : IFruit
    {
        public string GetColor()
        {
            return "Orange";
        }
    }
}

```





more example



```c#

using System;

class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}

class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}

class Square : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a square");
    }
}

class Program
{
    static void DrawShape(Shape shape)
    {
        shape.Draw();
    }

    static void Main(string[] args)
    {
        Shape shape1 = new Circle();
        Shape shape2 = new Square();

        DrawShape(shape1); // Drawing a circle
        DrawShape(shape2); // Drawing a square
    }
}


```


one more example

```c#

using System;

class BankAccount
{
    protected decimal balance;

    public BankAccount(decimal initialBalance)
    {
        balance = initialBalance;
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= balance)
        {
            balance -= amount;
            Console.WriteLine($"Withdrawn {amount:C}. New balance: {balance:C}");
        }
        else
        {
            Console.WriteLine("Insufficient funds");
        }
    }
}

class SavingsAccount : BankAccount
{
    public SavingsAccount(decimal initialBalance) : base(initialBalance)
    {
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= balance && amount <= 1000)
        {
            balance -= amount;
            Console.WriteLine($"Withdrawn {amount:C} from savings account. New balance: {balance:C}");
        }
        else
        {
            Console.WriteLine("Insufficient funds or withdrawal limit exceeded");
        }
    }
}

class CheckingAccount : BankAccount
{
    public CheckingAccount(decimal initialBalance) : base(initialBalance)
    {
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= balance)
        {
            balance -= amount;
            Console.WriteLine($"Withdrawn {amount:C} from checking account. New balance: {balance:C}");
        }
        else
        {
            Console.WriteLine("Insufficient funds");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount savingsAccount = new SavingsAccount(5000);
        BankAccount checkingAccount = new CheckingAccount(3000);

        savingsAccount.Withdraw(200); // Withdrawn $200 from savings account. New balance: $4800
        checkingAccount.Withdraw(500); // Withdrawn $500 from checking account. New balance: $2500

        // Now let's demonstrate Liskov's Substitution Principle
        BankAccount account = new SavingsAccount(2000);
        account.Withdraw(1500); // Withdrawn $1500 from savings account. New balance: $500
    }
}


```


## Interface Segregation Principle

The Interface Segregation Principle states "that clients should not be forced to implement interfaces they don't use. 
Instead of one fat interface, many small interfaces are preferred based on groups of methods, each serving one submodule.".


lets say we have a heirarchy of programmer->techlead->manager

the example without ISP wolud look like below.


```c#
public Interface ILead
{
   void CreateSubTask();
   void AssginTask();
   void WorkOnTask();
}
public class TeamLead : ILead
{
   public void AssignTask()
   {
      //Code to assign a task.
   }
   public void CreateSubTask()
   {
      //Code to create a sub task
   }
   public void WorkOnTask()
   {
      //Code to implement perform assigned task.
   }
}

// simlaraly for manager

public class Manager: ILead
{
   public void AssignTask()
   {
      //Code to assign a task.
   }
   public void CreateSubTask()
   {
      //Code to create a sub task.
   }
   public void WorkOnTask()
   {
      throw new Exception("Manager can't work on Task");
   }
}

```

But here manager will not work on the task so we have inconsistency so lets divied the interface.


```c#

public interface IProgrammer
{
   void WorkOnTask();
}


```

for manager ans tech lead

```c#
public interface ILead
{
   void AssignTask();
   void CreateSubTask();
}


```









