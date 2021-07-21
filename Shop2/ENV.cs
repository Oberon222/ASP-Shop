using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2
{
    public class ENV
    {
        public const string ImagePath = @"\images\products\";

        // може генеритись випадковий набір символів - ключ до сервера
        public const string SessionCart = "ShoppingcartSession";

        public static string AdminRole = "Admin";
        public static string CustomerRole = "Customer";

        public const string EmailAdmin = "pavelkost222@gmail.com";
    }
}
