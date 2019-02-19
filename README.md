# Hackerkegeln Vortrag F#

## Ankündigung im Chat:

* schneller Überblick über die wichtigsten Datentypen
* Kommandozeilen auslesen einfach gemacht
* CSV, HTML oder JSON als Datenquelle für typisierten Output – völlig egal, es ist in allen Fällen nur eine Zeile Quelltext
* Active Patterns als clevere Hilfe für Parser oder Pattern Matching
* rekursives Parsen von Strings, um Merkmale aus Texten zu gewinnen und in einem Record-Typ abzulegen
* Producer - Consumer Probleme mittels Threads und MailboxProcessor umsetzen
* das "M-Wort" existiert für F#'ler nicht – sie nennen es "Computation Expression"

## Vortrag Gliederung

* Paradigmen Strukturiert / OO / FP / Concurrent
  https://twitter.com/unclebobmartin/status/983498470232330240
  gleichzeitig möglich

* Paradigmen: Verzicht auf ...
  - Struktutiert: Kein Goto
  - Objektorientiert: Keine Funktions-Zeiger
  - Funktional: Keine Zuweisung
  - Nebenläufig: Kein Bezug zur Zeit
  - Reaktiv: Kein Return

* Fsharp: Functional-First
  - OO
  - Funktional
  - Concurrent
  - Reactive

* Datentypen
  - simpel: int, float, decimal, string
  - Kollektionen: Array, Liste, Sequenz, Hashset, ...
  - Tupel
  - Records
  - Discriminated Unions

* Type Provider mit Beispiel

* Pattern Matching
  - Discriminated Union
  - Listen
  - Regex, Active Patterns
  - Parser als Beispiel

* 