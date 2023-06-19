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






