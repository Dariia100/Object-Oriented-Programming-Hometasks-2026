using System;

/*Даний інтерфейс поганий тим, що він включає занадто багато методів.
 А що, якщо наш клас товарів не може мати знижок або промокодом, або для нього немає сенсу встановлювати матеріал з 
 якого зроблений (наприклад, для книг). Таким чином, щоб не реалізовувати в кожному класі невикористовувані в ньому методи, краще 
розбити інтерфейс на кілька дрібних і кожним конкретним класом реалізовувати потрібні інтерфейси.
Перепишіть, розбивши інтерфейс на декілька інтерфейсів, керуючись принципом розділення інтерфейсу. 
Опишіть класи книжки (розмір та колір не потрібні, але потрібна ціна та знижки) та верхній одяг (колір, розмір, ціна знижка),
які реалізують притаманні їм інтерфейси.*/


// Відповідь: порушено принцип розділення інтерфейсу. Варто зробити окремі спеціалізовані інтерфейси:
// інтерфес для цін, для знижки та промокоду, для характеристик товару (кольор та розміру)

interface IPrice
{
    void SetPrice(double price);
}

interface ICoupons
{
    void ApplyDiscount(String discount);
    void ApplyPromocode(String promocode);
}

interface ICharacteristics 
{
    void SetColor(byte color);
    void SetSize(byte size);
}


class Book: IPrice, ICoupons
{
    public void SetPrice(double price)
    {
        Console.WriteLine("Price: " +  price);
    }

    public void ApplyDiscount(string discount)
    {
        Console.WriteLine("Discount : " + discount);
    }

    public void ApplyPromocode(string promocode)
    {
        Console.WriteLine("Promocode : " + promocode);
    }

}

class Clothes: IPrice, ICoupons, ICharacteristics
{
    public void SetPrice(double price)
    {
        Console.WriteLine("Price: " + price);
    }

    public void ApplyDiscount(string discount)
    {
        Console.WriteLine("Discount : " + discount);
    }

    public void ApplyPromocode(string promocode)
    {
        Console.WriteLine("Promocode : " + promocode);
    }

    public void SetColor(byte color)
    {
        Console.WriteLine("Color : " + color);
    }

    public void SetSize(byte size)
    {
        Console.WriteLine("Size : " + size);
    }

class Program
{
    static void Main(string[] args)
    {
       
        Console.ReadKey();
    }
}