using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cms.MainFolder
{
    class Sqlconnection
    {
        private string p;
        private Sqlconnection con;

        public Sqlconnection(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        public Sqlconnection(string p, Sqlconnection con)
        {
            // TODO: Complete member initialization
            this.p = p;
            this.con = con;
        }

        public Sqlconnection()
        {
            // TODO: Complete member initialization
        }
    }
}
