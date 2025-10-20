# Personal Budget Tracker

En komplett C#-konsolapplikation f�r att hantera din privatekonomi. Programmet har ett fokus p� tydlig struktur, pedagogik och god kodpraxis.

---

## Projektinformation

- **L�gg till transaktioner** (inkomst eller utgift): Ange beskrivning, belopp, kategori, datum. Inkomster �r alltid gr�na, utgifter visas med r�d f�rg i terminalen.
- **Visa alla transaktioner** med full info och f�rgkodning.
- **Visa total balans** (inkomster minus utgifter).
- **Ta bort enskild transaktion** eller **alla i vald kategori**.
- **Filtrera och sortera** poster p� kategori eller datum.
- **Visa statistik:** antal transaktioner, summa inkomster, summa utgifter.
- **Pedagogiskt menysystem**.
- **All data hanteras i minnet via C#-Listor.**

---

## Klassdiagram (UML)

Diagrammet visar de tv� huvudklasserna i programmet:

- **Transaction**  
  Representerar en enskild inkomst eller utgift.  
  Egenskaper:  
  - Description (string)
  - Amount (decimal)
  - Category (string)
  - Date (string)  
  Metod:  
  - ShowInfo(): Skriver ut rad med f�rg.

- **BudgetManager**  
  Ansvarar f�r all logik som r�r hantering av transaktioner:
  - List<Transaction>: lagrar alla poster.
  - Metoder: AddTransaction, ShowAll, CalculateBalance, DeleteTransaction, DeleteByCategory, ShowByCategory, FilterByCategory, SortByDate, ShowStatistics  
  BudgetManager har en **aggregation** till Transaction (dvs. hanterar m�nga Transaction-objekt).

**Diagrammet visualiserar egenskaper och metodsignaturer tydligt, samt relationen och rollbeskrivning f�r varje klass.**

![UML klassdiagram](budget_tracker_uml.png)

---

## Fl�desschema (Flowchart)

Fl�desschemat visar programmets logik och anv�ndarfl�de:

- Startpunkt
- Anv�ndaren v�ljer menyval:  
  - L�gg till transaktion
  - Visa alla transaktioner
  - Radera transaktion(er)
  - Filtrera, sortera
  - Visa statistik
  - Visa per kategori
  - Avsluta
- Varje val anropar r�tt metod i BudgetManager.
- Efter �tg�rd �terg�r programmet till huvudmenyn tills avslut v�ljs.

**Flowcharten visualiserar hur anv�ndaren interagerar med applikationen samt hur logiken �r uppdelad och �terkopplad till kodens metoder.**

![Flowchart](budget_tracker_flow.png)

---

## Installation

1. Klona repository fr�n GitHub:
2. �ppna projektet i Visual Studio (eller annan C#-IDE).
3. Bygg och k�r programmet.

---

## Anv�ndning

- K�r programmet och v�lj funktion via meny.
- F� direkt feedback och f�rgkodning.
- All hantering sker i minnet.

--- 

## Reflektion

- Hur hj�lpte klasser och metoder dig att organisera programmet?

 Att anv�nda klasser och metoder gjorde det mycket enklare att organisera mitt projekt. Genom att dela upp koden i Transaction och BudgetManager blev allt b�de tydligare och l�ttare att �ndra eller bygga vidare p�. 
 Med metoder kunde jag �teranv�nda kod och snabbt l�gga till nya funktioner utan att skapa on�dig r�ra.
 
- Vilken del av projektet var mest utmanande?
 
Det knepigaste var helt klart att h�lla ordning p� all information � jag tappade n�stan greppet ibland! Mycket blev l�ttare med hj�lp av flowchart och UML-diagram, d� kunde jag se hur allt h�nger ihop. 
F�rgkodningen var ocks� en utmaning; att f� inkomster till gr�nt och utgifter till r�tt tog lite testande innan det blev r�tt. Men n�r allt v�l satt funkade menysystemet och f�rgerna precis som jag ville.


--- 

## Made By:
- BenjiW
- Developer Student at NBI
- 2025/19/10
