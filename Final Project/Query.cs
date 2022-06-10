using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Query
    {
        public static string loginQuery = "select * from dinfo.E_mail where Password= @password and E_mail = @Email";
    }
}
