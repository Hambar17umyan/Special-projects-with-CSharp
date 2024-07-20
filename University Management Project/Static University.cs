using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_Project
{
    internal partial class University
    {
        private static int Count;
        public static List<University> Universities;

        static University()
        {
            Count = 0;
            Universities = new List<University>();
        }
    }
}
