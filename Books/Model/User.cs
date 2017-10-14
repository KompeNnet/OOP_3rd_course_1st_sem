using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Model
{
    public class User
    {
        public string Login { get; set; }
        public byte[] Pasword { get; set; }
        public string Role { get; set; }
    }
}
