using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.ComTypes;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name):base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double grade)
        {
            if(Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }

            var level = (int)Math.Ceiling(Students.Count * 0.2); //zwraca liczbe calkowita ilosci studentow podzielona przez 5(20%)
            var newList = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToList(); //tworzy newList(liste) z posortowanymi ocenami malejaco

            if(newList[level - 1] <= grade) //jezeli ocena wejsciowa jest wieksza od elementu w liscie rownemu 20% ilosci studentow - 1 to zwraca A
            {
                return 'A';
            }
            else if (newList[(level * 2) - 1] <= grade)
            {
                return 'B';
            }
            else if(newList[(level * 3) - 1] <= grade)
            {
                return 'C';
            }
            else if(newList[(level * 4) - 1] <= grade)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }
                base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
