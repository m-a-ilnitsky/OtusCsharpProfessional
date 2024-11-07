using HomeWork30.PrototypePattern;
using HomeWork30.PrototypePattern.Documents;
using HomeWork30.PrototypePattern.Persons;
using HomeWork30.PrototypePattern.Tables;
using System;

var charsTable = new Table<char>(new[,]
{
    { 'Q', 'W', 'E', 'R', 'T', 'Y' },
    { 'Й', 'Ц', 'У', 'К', 'Е', 'Н' }
});

Console.WriteLine("charsTable:");
Console.WriteLine(charsTable);

var matrixA = new Matrix<int>(new[,]
{
    { 1, 2, 2 },
    { 3, 1, 1 }
});

var matrixB = new Matrix<int>(new[,]
{
    { 4, 2 },
    { 3, 1 },
    { 1, 5 }
});

Console.WriteLine("matrixA:");
Console.WriteLine(matrixA);

Console.WriteLine("matrixB:");
Console.WriteLine(matrixB);

Console.WriteLine("Transposed(matrixB):");
Console.WriteLine(matrixB.GetTransposed());

Console.WriteLine("matrixA * matrixB:");
Console.WriteLine(matrixA.GetProduct(matrixB));

Console.WriteLine("matrixB * matrixA:");
Console.WriteLine(matrixB.GetProduct(matrixA));

var copy1 = matrixA.Clone();
var copy2 = ((Table<int>)matrixA).Clone();
var copy3 = ((ICloneable)matrixA).Clone();

var copy4 = matrixA.Copy();
var copy5 = ((Table<int>)matrixA).Copy();
var copy6 = ((ICopyable<Table<int>>)matrixA).Copy();

Console.WriteLine($"Types of matrixA copies:");
Console.WriteLine($"  copy1: {copy1.GetType().Name}, copy2: {copy2.GetType().Name}, copy3: {copy3.GetType().Name},");
Console.WriteLine($"  copy4: {copy4.GetType().Name}, copy5: {copy5.GetType().Name},  copy6: {copy6.GetType().Name}");

var person = new Person("Вася", "Васечкин", 20, isMale: true);
var passport = new Passport(person, passportNumber: "123 111222333", citizenship: "Россия", registrationAddress: "Москва, Кремль, Колокольня");
var sitizen = new Sitizen(person, passport);
var studentCard = new StudentCard(person, cardNumber: "123-345", faculty: "РЭФ", groupNumber: "РФ-44");
var student = new Student(sitizen, studentCard);

var studentCopy1 = student.Clone();
var studentCopy2 = ((Sitizen)student).Clone();
var studentCopy3 = ((Person)student).Clone();
var studentCopy4 = ((ICloneable)student).Clone();

var studentCopy5 = student.Copy();
var studentCopy6 = ((Sitizen)student).Copy();
var studentCopy7 = ((Person)student).Copy();

var studentCopy8 = ((ICopyable<Student>)student).Copy();
var studentCopy9 = ((ICopyable<Sitizen>)student).Copy();
var studentCopy10 = ((ICopyable<Person>)student).Copy();

Console.WriteLine();
Console.WriteLine($"Types of student copies:");
Console.WriteLine($"  copy1: {studentCopy1.GetType().Name}, copy2: {studentCopy2.GetType().Name}, copy3: {studentCopy3.GetType().Name}, copy4: {studentCopy4.GetType().Name},");
Console.WriteLine($"  copy5: {studentCopy5.GetType().Name}, copy6: {studentCopy6.GetType().Name}, copy7: {studentCopy7.GetType().Name},");
Console.WriteLine($"  copy8: {studentCopy8.GetType().Name}, copy9: {studentCopy9.GetType().Name}, copy10: {studentCopy10.GetType().Name}");