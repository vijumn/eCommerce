using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryzen.Shop.Infrastructure
{
    public class BaseException:Exception
    {
        public BaseException():base() { }
        public BaseException(string message) : base(message)
        {
        }
    }
}
