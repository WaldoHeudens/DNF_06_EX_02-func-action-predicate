// Je hoeft niet voor elke delegate een eigen type te maken. De BCL heeft
// drie kant-en-klare families: Func, Action en Predicate.

// 1. Func<...>: een delegate die iets teruggeeft.
//    Het laatste type-argument is altijd het returntype.
//    Func<int, int, int> = neemt twee int en geeft een int terug.

// Een anonieme method (oude vorm, met het delegate-sleutelwoord):
Func<int, int, int> optellenOud = delegate (int a, int b) { return a + b; };

// Dezelfde bewerking als lambda-expressie (moderne, korte vorm):
Func<int, int, int> optellen = (a, b) => a + b;

Console.WriteLine(optellenOud(2, 3)); // 5
Console.WriteLine(optellen(2, 3));    // 5

// 2. Action<...>: een delegate die niets teruggeeft (void).
Action<string> print = tekst => Console.WriteLine($"> {tekst}");
print("hallo via Action");

// 3. Predicate<T>: een delegate die een bool teruggeeft, voor ja/nee-vragen.
Predicate<int> isEven = getal => getal % 2 == 0;
Console.WriteLine(isEven(4)); // True
Console.WriteLine(isEven(7)); // False

// Predicate wordt veel gebruikt door methods van de BCL, zoals List.FindAll.
List<int> getallen = [1, 2, 3, 4, 5, 6];
List<int> evenGetallen = getallen.FindAll(isEven);
Console.WriteLine(string.Join(", ", evenGetallen)); // 2, 4, 6

// 4. Closures: een lambda mag variabelen uit zijn omgeving gebruiken.
//    De lambda 'onthoudt' die variabele, ook nadat de method is doorlopen.
int drempel = 3;
Predicate<int> groterDanDrempel = getal => getal > drempel;
Console.WriteLine(getallen.FindAll(groterDanDrempel).Count); // 3 (namelijk 4, 5, 6)

// Pas je de opgesloten variabele aan, dan ziet de lambda de nieuwe waarde.
drempel = 5;
Console.WriteLine(getallen.FindAll(groterDanDrempel).Count); // 1 (namelijk 6)

// 5. Een functie die een functie teruggeeft. De teruggegeven lambda sluit
//    'factor' in via een closure.
Func<int, int> maalDrie = MaakVermenigvuldiger(3);
Console.WriteLine(maalDrie(10)); // 30

static Func<int, int> MaakVermenigvuldiger(int factor) => getal => getal * factor;
