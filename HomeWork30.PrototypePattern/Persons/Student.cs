using HomeWork30.PrototypePattern.Documents;
using System;

namespace HomeWork30.PrototypePattern.Persons;

/// <summary>
/// Студент
/// </summary>
public class Student(string firstName, string lastName, int age, bool isMale, Passport passport, StudentCard studentCard)
    : Sitizen(firstName, lastName, age, isMale, passport),
    ICopyable<Student>,
    ICopyable<Sitizen>,
    ICopyable<Person>,
    ICloneable
{
    public StudentCard StudentCard { get; protected set; } = studentCard ?? throw new ArgumentNullException(nameof(studentCard));
    public int CourseNumber { get; protected set; } = 1;

    public Student(Student s) : this(s.FirstName, s.LastName, s.Age, s.IsMale, s.Passport.Copy(), s.StudentCard.Copy()) { }

    public Student(Sitizen s, StudentCard studentCard) : this(s.FirstName, s.LastName, s.Age, s.IsMale, s.Passport.Copy(), studentCard.Copy()) { }

    public Student(Person p, Passport pass, StudentCard studentCard) : this(p.FirstName, p.LastName, p.Age, p.IsMale, pass.Copy(), studentCard.Copy()) { }

    public void IncreaseCourseNumber() => CourseNumber++;

    public void ChangeStudentCard(StudentCard studentCard)
    {
        StudentCard = studentCard ?? throw new ArgumentNullException(nameof(studentCard));
    }

    public override Student Copy() => new(this);

    Sitizen ICopyable<Sitizen>.Copy() => new(FirstName, LastName, Age, IsMale, Passport.Copy());

    Person ICopyable<Person>.Copy() => new(FirstName, LastName, Age, IsMale);
}
