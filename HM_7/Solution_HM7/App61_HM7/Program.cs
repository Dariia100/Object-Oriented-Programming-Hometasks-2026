using System.Security.Cryptography.X509Certificates;

namespace App61_HM7
{
    /// Factory method
    abstract class Cookie
    {
        public abstract void Info();
    }

    class ChocolateCookie : Cookie
    {
        public override void Info()
        {
            Console.WriteLine("Chocolate cookie is done.");
        }
    }

    class LemonCookie : Cookie
    {
        public override void Info()
        {
            Console.WriteLine("Lemon cookie is done.");
        }
    }

    class VanillaCookie : Cookie
    {
        public override void Info()
        {
            Console.WriteLine("Vanilla cookie is done.");
        }
    }

    abstract class CookieFactory()
    {
        public abstract Cookie BakeCookie();
        public void CreateCookie()
        {
            Cookie cookie = BakeCookie();
            cookie.Info();
        }
    }

    class ChocolateCookieFactory : CookieFactory
    {
        public override Cookie BakeCookie()
        {
            return new ChocolateCookie();
        }
    }

    class LemonCookieFactory : CookieFactory
    {
        public override Cookie BakeCookie()
        {
            return new LemonCookie();
        }
    }

    class VanillaCookieFactory : CookieFactory
    {
        public override Cookie BakeCookie()
        {
            return new VanillaCookie();
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            CookieFactory f = new ChocolateCookieFactory();
            f.CreateCookie();

            f = new LemonCookieFactory();
            f.CreateCookie();

            f = new VanillaCookieFactory();
            f.CreateCookie();

            Console.ReadKey();  
        }
    }
}
