using System;
using System.Collections.Generic;

namespace StudentExercises
{
    public class Cohort
    {
        public Cohort(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}