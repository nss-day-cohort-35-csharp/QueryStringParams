using System;
using System.Collections.Generic;

namespace StudentExercises
{
    public class Exercise
    {
        public Exercise(string name, string language)
        {
            Name = name;
            Language = language;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public List<Student> AssignedStudents { get; set; }
    }
}