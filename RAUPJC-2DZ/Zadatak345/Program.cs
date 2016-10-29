using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak345
{
    class Program
    {
        static void Main(string[] args)
        {

            //ZADATAK 3
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            string[] results = integers.GroupBy(i => i).Select(i => "Broj "+i.Key+" ponavlja se "+i.Count()+" puta").ToArray();
            
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine("strings[{0}] = {1}", i, results[i]);
            }

            //ZADATAK 4
            Console.WriteLine("Example1()=" + Example1()); //nadjačan operator == u Student
            Console.WriteLine("Example2()=" + Example2()); //implementiran GetHashCode u Student




            //ZADATAK 5
            var universities = GetAllCroatianUniversities();
            Student[] allCroatianStudents = universities.SelectMany(u => u.Students).Distinct().ToArray();

            Student[] croatianStudentsOnMultipleUniversities = universities.SelectMany(u => u.Students)
                                                                           .GroupBy(s => s)
                                                                           .Where(s => s.Count() >= 2)
                                                                           .Select(s => s.Key)
                                                                           .ToArray();

            Student[] studentsOnMaleOnlyUniversities = universities.Where(u => u.Students.All(s => s.Gender == Gender.Male))
                                                                   .SelectMany(u => u.Students)
                                                                   .Distinct()
                                                                   .ToArray();




            Console.WriteLine("Svi studenti:");
            foreach (var su in allCroatianStudents)
            {
                Console.WriteLine("\t" + su.Name);
            }

            Console.WriteLine("Studenti na vise fakulteta:");
            foreach (var su in croatianStudentsOnMultipleUniversities)
            {
                Console.WriteLine("\t" + su.Name);
            }

            Console.WriteLine("Studenti na muskim fakultetima:");
            foreach (var su in studentsOnMaleOnlyUniversities)
            {
                Console.WriteLine("\t" + su.Name);
            }

            Console.ReadLine();


        }

        public static University[] GetAllCroatianUniversities()
        {
            Student s1 = new Student("Ivan", "123");
            Student s2 = new Student("Stipe", "124");
            Student s3 = new Student("Marija", "125");
            Student s4 = new Student("Ana", "126");
            Student s5 = new Student("Luka", "127");
            Student s6 = new Student("Kristina", "128");
            Student s7 = new Student("Domagoj", "129");

            s1.Gender = Gender.Male;
            s2.Gender = Gender.Male;
            s3.Gender = Gender.Female;
            s4.Gender = Gender.Female;
            s5.Gender = Gender.Male;
            s6.Gender = Gender.Female;
            s7.Gender = Gender.Male;

            Student[] filozofi = { s1, s2, s3, s5 };
            Student[] ferovci = { s1, s4, s5, s6 }; //s1 s5 (Ivan, Luka) se ponavljaju
            Student[] fizicari = { s5, s7 }; //s5 (Ivan) se ponavlja

            University fer = new University("FER", ferovci);
            University ffzg = new University("FFZG", filozofi);
            University pmf_fizika = new University("PMF Fizika", fizicari);
            return new University[3] { fer, ffzg, pmf_fizika };
        }

        public static bool Example1()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567")
            };
            var ivan = new Student("Ivan", jmbag: "001234567");
            return list.Any(s => s == ivan);

        }

        public static int Example2()
        {
            var list = new List<Student>()
            {
            new Student ("Ivan", jmbag: "001234567") ,
            new Student ("Ivan", jmbag: "001234567")
            };
            
            return list.Distinct().Count();
        }
    }
}
