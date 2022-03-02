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

## Ulepszenia

W module trzecim po zniszczeniu piłki istnieje szansa na wypadnięcie z niej ulepszenia.

Zaimplementowaliśmy trzy rodzaje ulepszeń:
 - ulepszenie życia (4% szansy na wypadnięcie)
 - ulepszenie prędkości gracza (10% szansy na wypadnięcie)
 - ulepszenie ilości pocisków (6% szansy na wypadnięcie)

Najprostszym z nich jest ulepszenie życia. Po zebraniu tego ulepszenia ilość żyć gracza zwiększa się o jeden.

Ulepszenie prędkości gracza zwiększa prędkość gracza o 50%. Ulepszenie to pozostaje aktywne przez 10 sekund. Jeśli kilka ulepszeń prędkości będzie aktywnych jednocześnie, to ich działanie kumuluje się.

Ulepszenie ilości pocisków dwukrotnie zwiększa maksymalną ilość pocisków, które może wystrzelić gracz. Ulepszenie to również działa przez 10 sekund. Jeśli gracz podniesie ulepszenie podczas jego działania, to nie kumuluje się ono, a jedynie resetuje czas działania efektu z powrotem do dziesięciu sekund.

## Teren (drabiny, ruszające się platformy, zniszczalne bloki, oblodzone bloki)

## System zapisu gry