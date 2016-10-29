using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak345
{
    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }

        public University(string name, Student[] students )
        {
            Name = name;
            Students = students;
        }
    }
}
