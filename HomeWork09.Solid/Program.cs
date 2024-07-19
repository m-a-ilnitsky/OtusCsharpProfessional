using HomeWork09.Solid.Model;
using HomeWork09.Solid.Model.RandomNumber;
using HomeWork09.Solid.Model.Ranges;
using HomeWork09.Solid.View;

var numbersRange = new IntRange(1, 100);
var numberGenerator = new RandomIntNumberGenerator(numbersRange);
//var view = new ConsoleView();
var view = new ConsoleViewWithHint();
var model = new GuessNumberGameModel(view, numbersRange, numberGenerator);

view.Run();
