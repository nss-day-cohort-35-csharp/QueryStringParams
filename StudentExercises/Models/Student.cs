using System;
using System.Collections.Generic;

namespace StudentExercises
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public int CohortId { get; set; }
        public Cohort Cohort { get; set; }
        public List<Exercise> AssignedExercises { get; set; } = new List<Exercise>();
    }
}