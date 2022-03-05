# Pang

Jest to nasza wersja gry Pang, stworzona w silniku Unity. Gra polega na niszczeniu skaczących piłek. Składa się z trzech modułów, zrealizowanych zgodnie ze specyfikacją.

## Sterowanie

Gra obsługuje wejście z klawiatury oraz kontrolera do gier.

|              Akcja              | Sterowanie na klawiaturze | Sterowanie na padzie |
|:-------------------------------:|:-------------------------:|:--------------------:|
|          Poruszanie się         |      Strzałki / WASD      |      Lewy drążek     |
|              Strzał             |           Spacja          |  Dolny przycisk (A)  |
|          Reset poziomu          |             R             |  Prawy przycisk (B)  |
| Przejście do następnego poziomu |             E             |  Górny przycisk (Y)  |

Sterowanie na padzie testowane było na kontolerze na konsolę Xbox 360, jednak powinno działać na dowolnym kontrolerze.

## Piłki
Celem gry jest zniszczenie wszystkich piłek na planszy. Piłki można zniszczyć trafiając w nie pociskami, niszczą się . Piłki odbijają się od podłogi i sufitu tak, aby zawsze osiągać stałą wysokość skoku. Odbijają się również od ścian, zachowując stałą prędkość w osi X (zmieniając jedynie kierunek ruchu). Każda piłka ma wewnętrzną zmienną *layer*. Od tej zmiennej zależy jej rozmiar, prędkość, wysokość skoku oraz ilość punktów, które gracz otrzyma za jej zniszczenie. Zmienia się również częstotliwość dźwięku odgrywanego przy zniszczeniu piłki. Czym wyższa jest ta zmienna, tym mniejszego rozmiaru jest piłka. Po zniszczeniu piłki, jeśli jej zmienna *layer* nie jest równa stałej *maxLayer* (równej dwa), stworzone zostają dwie nowe piłki poruszające się w przeciwnych kierunkach. Wartość ich zmiennych *layer* ustawiana jest na wartość o jeden wyższą od zmiennej *layer* zniszczonej piłki.

## Bronie

Na każdym z poziomów gracz ma przypisaną jedną z czterech broni. Poszczególne bronie różnią się jedynie maksymalną ilością pocisków, które mogą jednocześnie znajdować się na scenie oraz charakterystyką pocisku wystrzeliwanej z broni. Każdy z pocisków niszczy piłkę przy zderzeniu się z nią, jednocześnie samemu ulegając zniszczeniu. Gracz nie jest w stanie wystrzelić, gdy znajduje się w powietrzu lub wspina się po drabinie.

### Domyślna broń  
Z domyślnej broni gracz jest w stanie wystrzelić maksymalnie jeden pocisk jednocześnie. Pocisk ten porusza się wtedy pionowo do góry dopóki nie uderzy w przeszkodę bądź wyleci poza ekran (wtedy ulega zniszczeniu). Pocisk pozostawia za sobą "ślad", który również powoduje zniszczenie się piłki przy zderzeniu.

### Broń druga
Z tej broni gracz może wystrzelić maksymalnie pięć pocisków jednocześnie. Pociski są mniejsze i poruszają się szybciej niż w przypadku broni domyślnej. Nie pozostawiają za sobą również "śladu".

### Broń trzecia
Limit pocisków broni trzeciej wynosi jeden. Działa ona podobnie do broni domyślnej, jednak przenika przez przeszkody i ulega zniszczeniu jedynie przy wyleceniu poza ekran lub zderzeniu się z piłką.

### Broń czwarta
Z broni czwartej możliwym jest wystrzelenie trzech pocisków jednocześnie. Pocisk broni czwartej ma na sobie komponent fizyczny, więc odbija się od przeszkód oraz innych pocisków. Odbija się również od bocznych krawędzi poziomu. Przy uderzeniu w przeszkodę od dołu, niszczy się. Pocisk ulega autodestrukcji po pięciu sekundach.

## Punkty
W module drugim i trzecim, na końcu każdego poziomu, graczowi przyznawane są punkty. Gracz może zdobywać punkty w różne sposoby:
 - za każdą zniszczoną pociskiem piłkę gracz uzyskuje określoną liczbę punktów (3000 punktów za największą piłkę, za każdą mniejszą piłkę - dwa razy mniej punktów niż za piłkę od niej większą)
 - za każdy punkt życia poza jednym gracz zdobywa 500 punktów
 - za każdą sekundę pozostałego czasu na ukończenie poziomu gracz zdobywa 500 punktów
 - za zebranie artefaktu gracz zdobywa ustaloną liczbę punktów (zależną od poziomu)

## Ulepszenia

W module trzecim po zniszczeniu piłki istnieje szansa na wypadnięcie z niej ulepszenia.

Zaimplementowaliśmy trzy rodzaje ulepszeń:
 - ulepszenie życia (4% szansy na wypadnięcie)
 - ulepszenie prędkości gracza (10% szansy na wypadnięcie)
 - ulepszenie ilości pocisków (6% szansy na wypadnięcie)

Najprostszym z nich jest ulepszenie życia. Po zebraniu tego ulepszenia ilość żyć gracza zwiększa się o jeden.

Ulepszenie prędkości gracza zwiększa prędkość gracza o 50%. Ulepszenie to pozostaje aktywne przez 10 sekund. Jeśli kilka ulepszeń prędkości będzie aktywnych jednocześnie, to ich działanie kumuluje się.

Ulepszenie ilości pocisków dwukrotnie zwiększa maksymalną ilość pocisków, które mogą jednocześnie znajdować się na scenie. Ulepszenie to również działa przez 10 sekund. Jeśli gracz podniesie ulepszenie podczas jego działania, to nie kumuluje się ono, a jedynie resetuje czas działania efektu z powrotem do dziesięciu sekund.

## Teren

### Drabiny
Gdy gracz wchodzi na drabinę, włącza się animacja wspinania się oraz gracz uzyskuje możliwość poruszania się w osi Y. Animacja odtwarzana jest jedynie wtedy, gdy gracz rzeczywiście się wspina.

### Poruszające się platformy
Jako dodatkowe ułatwienie/utrudnienie w module trzecim zaimplentowane zostały poruszające się platformy. Poruszają się one z określoną prędkością pomiędzy podanymi punktami na mapie. Pociski (poza pociskiem z broni trzeciej) niszczą się, gdy uderzą w platformę. Jeśli jednak platforma wejdzie w kolizję z pociskiem "od boku", to przenika przez niego.

### Popękane bloki
Niektóre z bloków budujących poziom są popękane. Po uderzeniu pociskiem w popękany blok, zostaje on zniszczony.

### Oblodzone bloki
Po wejściu na oblodzony teren gracz porusza się w tym samym kierunku dopóki nie opuści śliskiej powierzchni.

## System zapisu gry

W każdym momencie gry gracz ma możliwość zapisania swojego postępu. Postęp zapisywany jest w zaprojektowanym przez nas binarnym formacie plików. W pierwszej linijce pliku znajduje się wersja formatu (aktualna wersja to 1.0). Przydatnym mogłoby się to okazać w przypadku zmiany algorytmu zapisującego (zapewniałoby, że nie będziemy próbować odczytać pliku zakodowanego w starym formacie za pomocą nowego algorytmu dekodującego). W drugiej zapisywane są dane dotyczące wszystkich zapisywalnych obiektów w scenie. Obiekty, które mają zostać zapisane posiadają komponent dziedziczący z abstrakcyjnej klasy *Saveable* (Unity korzysta z modelu entity component). Każda implementacja klasy *Saveable* musi implementować trzy metody:

 - *MemoryStream Save()*
 - *void Load(MemoryStream)*
 - *void OnLoad()*

### Zapis

Metoda *Save* serializuje wszystkie wartości, które mają zostać zapisane, do obiektu *MemoryStream* i zwraca go. Jest wywoływana podczas zapisywania gry.

### Odczyt

Metoda *Load* otrzymuje dane zapisane w metodzie *Save* w obiekcie *MemoryStream*. Wywoływana jest po wczytaniu zapisanej sceny (ale nie jest przypisana do żadnego z obiektów na scenie). Metoda jest odpowiedzialna za deseriakuzację danych oraz ich zinterpretowanie. Niektóre implementacje metody *Load* (np. w przypadku wczytywania pocisków i ulepszeń) tworzą nowa obiekty już istniejącego na nowo wczytanej scenie.

Metoda *OnLoad* wywoływana jest na każdym obiekcie na wczytanej scenie (z wyłączeniem obiektów wczytanych). Służy przede wszystkim do usuwania obiektów, które wczytywane są przez tworzenie nowych instancji.