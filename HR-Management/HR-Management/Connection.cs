using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management
{
    
        public class Connection
        {
            public static string GetConnectionString()
            {
                return "Data Source=DESKTOP-K4KG67K;Integrated Security=True;Database=human_resources;Connect Timeout=30;";
            }
        }
}

