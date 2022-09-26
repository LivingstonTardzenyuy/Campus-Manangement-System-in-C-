using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cms.ConFolder
{
    public class AppConnection
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["cms"].ConnectionString;
        }
        
    }
}
