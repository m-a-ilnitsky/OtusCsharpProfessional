using System;

namespace HomeWork30.PrototypePattern.Persons;

/// <summary>
/// Человек
/// </summary>
public class Person(string firstName, string lastName, int age, bool isMale) : ICopyable<Person>, ICloneable
{
    public string FirstName { get; } = firstName ?? throw new ArgumentNullException(nameof(firstName));
    public string LastName { get; } = lastName ?? throw new ArgumentNullException(nameof(lastName));
    public int Age { get; protected set; } = age;
    public bool IsMale { get; } = isMale;

    public Person(Person p) : this(p.FirstName, p.LastName, p.Age, p.IsMale) { }

    public void IncreaseAge() => Age++;

    public override string ToString() => $"{FirstName} {LastName} ({Age})";

    public virtual Person Copy() => new(this);

    public object Clone() => Copy();
}
