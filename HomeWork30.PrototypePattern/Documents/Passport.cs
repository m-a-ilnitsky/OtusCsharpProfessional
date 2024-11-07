using HomeWork30.PrototypePattern.Persons;
using System;

namespace HomeWork30.PrototypePattern.Documents;

/// <summary>
/// Паспорт гражданина
/// </summary>
public class Passport(Person person, string passportNumber, string citizenship, string registrationAddress) : ICopyable<Passport>, ICloneable
{
    private readonly Person _personInfo = person?.Copy() ?? throw new ArgumentNullException(nameof(person));

    public string Number { get; } = passportNumber ?? throw new ArgumentNullException(nameof(passportNumber));
    public string Citizenship { get; } = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
    public string RegistrationAddress { get; protected set; } = registrationAddress ?? throw new ArgumentNullException(nameof(registrationAddress));

    public string FirstName => _personInfo.FirstName;
    public string LastName => _personInfo.LastName;
    public bool IsMale => _personInfo.IsMale;

    public Passport(Passport p) : this(p._personInfo, p.Number, p.Citizenship, p.RegistrationAddress) { }

    public void ChangeRegistrationAddress(string registrationAddress)
    {
        RegistrationAddress = registrationAddress ?? throw new ArgumentNullException(nameof(registrationAddress));
    }

    public Passport Copy() => new(this);

    public object Clone() => Clone();
}
