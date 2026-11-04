# Garage Applikation 3.0

### En konsol-baserad garage applikation utvecklat i C# / Visual Studio.
Applikationen tillåter användare att hantera fordon i ett garage, inklusive att lägga till, ta bort, filtrera och lagra fordon genom ett enkelt filbaserat lagringssystem.

### Klasser och Medlemmar
 - Garage: Representerar en samling fordon. Fordon kan parkera om det finns plats och lämna. 
 - Vehicle: En abstrakt klass som representerar ett fordon. Klassen innehåller properties som märke, modell, tillverkningsår och registreringsnummer.
  - Fordon-subklasser: Dessa implementerbara klasser ärver från Vehicle-klassen och representerar olika fordon som kan parkera i ett riktigt garage. Dessa klasser inkluderar; bilar, motorcyklar, plan, båtar och bussar. 

### Metoder och Egenskaper
 - Add vehicles: Registrera nya fordon med olika egenskaper.
 - Remove vehicles: Ta bort fordon från garaget.
 - Filter vehicles: Söka och filtrera fordon baserat på användar-angivna egenskaper.
 - Vehicle management: Stöder flera olika fordonstyper med unika egenskaper.
 - Data persistence: Tillåter sparning och inladdning av data från fil vid uppstart och avstängning.

 ### Teknologier
  - C#/.NET
  - Visual Studio 2026
  - LINQ
  - System.IO

### Arkitektur och Koncept
Applikationen använder principerna bakong OOP för struktur och hantering av data. Detta inkluderar:
 - Inheritance: Alla fordon ärver från en gemensam abstrakt bas-klass (Vehicle) som definierar fordonens gemensamma medlemmar. 
 - Polymorhism: Fordonsspecifika implementationer kan hanteras via pekare av bas-klassens typ. 
 - Generics: Garaget utnyttjar generiska typer för att hantera fordonssamlingar. 
 - LINQ: Används för att filtrera data i både garatets interna lista och andra temporära listor av typ fordon. 
 - Filhantering: Använder System.IO för skriva och läsa in fordons-data till en textfil.
 - Hjälpar-klasser: Dessa statiska klasser är till för att dela upp projektets kod i olika ansvarsområden för bättre läsbarhet, skalbarhet och 'Separation of Concern'.

### Förvaring
Garagets innehåll kan sparas till en lokal text-fil som sedan kan laddas in igen vid andra körningar av programmet, vilket tillåter fortlevande av fordons-data. 

### Enhets tester
Innehåller ett projekt för testning. Testerna är skrivna i xUnit och försäkrar garagets funktionallitet genom att testa dess publika metoder och properties. Fordonens ToString-metoder testas också.

### Framtida förbättringar
 - Tillägg av flera sub-klasser till vehicle som även ärver vidare från de konkreta fordons-klasserna.
 - Implementation av flera garage som hanterar olika typer av fordon. Till exempel kan ett garage användas för markfordon, medan ett separat garage kan agera hangar för båtar eller flygplan. 
 - Implementation av flera tester som testar diverse metoder från hjälparklasserna. 