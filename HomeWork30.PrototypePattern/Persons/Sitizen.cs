using HomeWork30.PrototypePattern.Documents;
using System;

namespace HomeWork30.PrototypePattern.Persons;

/// <summary>
/// Гражданин
/// </summary>
public class Sitizen(string firstName, string lastName, int age, bool isMale, Passport passport)
    : Person(firstName, lastName, age, isMale),
    ICopyable<Sitizen>,
    ICopyable<Person>,
    ICloneable
{
    public Passport Passport { get; protected set; } = passport ?? throw new ArgumentNullException(nameof(passport));

    public Sitizen(Person person, Passport passport) : this(person.FirstName, person.LastName, person.Age, person.IsMale, passport) { }

    public Sitizen(Sitizen s) : this(s.FirstName, s.LastName, s.Age, s.IsMale, s.Passport.Copy()) { }

    public void ChangePassport(Passport passport)
    {
        Passport = passport ?? throw new ArgumentNullException(nameof(passport));
    }

    public override Sitizen Copy() => new(this);

    Person ICopyable<Person>.Copy() => new(FirstName, LastName, Age, IsMale);
}
