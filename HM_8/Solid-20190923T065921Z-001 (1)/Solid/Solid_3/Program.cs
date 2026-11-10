using System;


// Відповідь: порушений принцип підстановки Ліскова, бо квадрат не може замінити прямокутник.
// У квадрата параметри висоти і ширини завжди однакові, а у прямокутника незалежні одна від одної.

class Rectangle
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int GetRectangleArea()
    {
        return Width * Height;
    }
}

class Square
{
    public int Side { get; set; }
    public int GetSquareArea()
    {
        return Side * Side;
    }


    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle();
            rect.Width = 5;
            rect.Height = 10;

            Square square = new Square();
            square.Side = 1;

            Console.WriteLine(rect.GetRectangleArea());
            Console.WriteLine(square.GetSquareArea());

            //Відповідь 100? Що не так???
            Console.ReadKey();
        }
    }
}