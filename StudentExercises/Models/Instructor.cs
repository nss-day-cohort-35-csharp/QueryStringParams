using System;

namespace StudentExercises
{
    public class Instructor
    {
        public Instructor(string firstName, string lastName, string slackHandle, Cohort cohort)
        {
            FirstName = firstName;
            LastName = lastName;
            SlackHandle = slackHandle;
            Cohort = cohort;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort Cohort { get; set; }

        public void AssignExercise(Student student, Exercise exercise)
        {
            student.AssignedExercises.Add(exercise);
            Console.WriteLine($"Instructor {FirstName} assigned the exercise, {exercise.Name}, to {student.FirstName}");
        }
    }
}