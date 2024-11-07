using HomeWork30.PrototypePattern.Persons;
using System;

namespace HomeWork30.PrototypePattern.Documents;

/// <summary>
/// Студенческий билет
/// </summary>
public class StudentCard(Person person, string cardNumber, string faculty, string groupNumber) : ICopyable<StudentCard>, ICloneable
{
    private readonly Person _personInfo = person?.Copy() ?? throw new ArgumentNullException(nameof(person));

    public string CardNumber { get; } = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber));
    public string Faculty { get; set; } = faculty ?? throw new ArgumentNullException(nameof(faculty));
    public string GroupNumber { get; set; } = groupNumber ?? throw new ArgumentNullException(nameof(groupNumber));

    public string FirstName => _personInfo.FirstName;
    public string LastName => _personInfo.LastName;
    public bool IsMale => _personInfo.IsMale;

    public StudentCard(StudentCard c) : this(c._personInfo, c.CardNumber, c.Faculty, c.GroupNumber) { }

    public StudentCard Copy() => new(this);

    public object Clone() => Copy();
}
