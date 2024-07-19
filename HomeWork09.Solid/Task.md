# SOLID

## S) Single-responsibility principle / Принцип единственной ответственности
Класс IntRange отвечает только за хранение диапазона.
Класс RandomIntNumberGenerator отвечает только за генерацию числа.
Класс GuessNumberGameModel отвечает только за внутреннюю логику игры.
Класс ConsoleView отвечает только за интерфейс игры.
	
## O) Open–closed principle / Принцип открытости-закрытости
Можно поменять интерфейс игры, сделав еще один класс, реализующий интерфейс IGameView.
Например, вместо ConsoleView сделать класс WpfView
	
## L) Liskov substitution principle / Принцип подстановки Лисков
Можно скорректировать интерфейс игры сделав для класс ConsoleView наследника с исправленным текстом или логикой.
Например, класс ConsoleViewWithHint переопределяет один метод ConsoleView.
В результате в интерфейсе появляется возможность попросить подсказку.
Но класс ConsoleViewWithHint можно использовать вместо ConsoleView
	
## I) Interface segregation principle / Принцип разделения интерфейсов
Разделяем интерфейс IGameView на отдельные интерфейсы IModelSettable и IRunable, которые можно использовать где-то еще
	
## D) Dependency inversion principle / Принцип инверсии зависимостей
Создаем все объекты в классе Program, и передаем остальным классам нужные зависимости