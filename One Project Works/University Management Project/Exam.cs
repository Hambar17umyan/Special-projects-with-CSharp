using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_Project
{
    [Flags]
    internal enum Exam
    {
        Engineering = 1,
        Math = 2,
        Science = 4,
        ComputerScience = 8,
        History = 16,
        Politics = 32,
        Linguistics = 64,
        Literature = 128,
        Art = 256,
    }
}
