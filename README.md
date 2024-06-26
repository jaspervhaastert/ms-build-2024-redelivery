# Workshop Semantic Kernel

## Doel van de Workshop

In deze workshop leer je hoe je een Large Language Model (LLM), specifiek GPT-4o, kunt integreren in je software met behulp van Semantic Kernel. We gebruiken Azure OpenAI voor toegang tot het taalmodel. Aan het eind van de workshop heb je een werkende applicatie die interacteert met gebruikers en de resultaten via een API deelt.

## Voorbereiding

Zorg dat je de benodigde credentials voor Azure OpenAI beschikbaar hebt. Deze worden gedeeld in het Teams-kanaal.


## Opdracht
Ontwikkel een ordersysteem waarin gebruikers items kunnen toevoegen aan een order. Zodra de gebruiker klaar is met het toevoegen van items, moet de lijst met orders naar een API worden gestuurd. Of bedenkt zelf een leuke opdracht!


## Quickstart
Om snel van start te kunnen gaan kan je gebruik maken van deze quickstart.

### Stap 1: Clone het project van GitHub
Op de main-branch staat een project klaar waar je zo mee aan de slag kunt gaan. Kom je er niet helemaal uit dan kan je gebruik maken van de antwoorden in de antwoorden-branch.

### Stap 2: Chatgeschiedenis Opbouwen

Voor een natuurlijk gesprek tussen de gebruiker en het taalmodel is het essentieel om een geschiedenis op te bouwen van de gehele chat. Deze geschiedenis wordt doorgegeven aan het taalmodel om context en continuïteit in het gesprek te waarborgen. In het project is alvast een chathistory opgezet in de ChatService.

### Stap 3: Doel Systeem Definiëren

Informeer GPT-4o over het doel van de applicatie door een opdracht in de chatgeschiedenis te plaatsen. Denk aan de volgende opzet:

````
("Jij bent een vriendelijk systeem die items moet toevoegen aan een order. Vraag aan de gebruiker welke items toegevoegd moeten worden.");
````

### Stap 4: Plugins Maken
Maak een Semantic Kernel Plugin in C#. Een plugin is een klasse met methoden die voorzien zijn van annotaties om duidelijk te maken wat de methoden doen en welke parameters ze accepteren. Met een plugin is het mogelijk om C#-code te laten uitvoeren door het taalmodel. Bijvoorbeeld het bijhouden van items in een order. Als voorbeeld kan je kijken naar de AppearancePlugin, let erop dat de plugin ook in de Program.cs geregistreerd moet staan.

### Stap 5: API integreren
Door gebruik te maken van de HttpPlugin is het mogelijk om API calls te doen vanuit het taalmodel. Door aan de chatgeschiedenis de URL en de opzet van de body mee te geven is het mogelijk om de API-call door het taalmodel uit te laten voeren.

````
To complete the order, call the API at '{logicAppOptions.Value.Url}' with a post method. In the body there should be an object with a property called order, use a concatted list of items as the value.
````

## Afronding

Na het voltooien van deze workshop heb je een goed begrip van hoe je Semantic Kernel en GPT-4o kunt gebruiken om een interactieve applicatie te bouwen. Je hebt geleerd hoe je chatgeschiedenis kunt beheren, plugins kunt maken en resultaten kunt verzenden naar een API.
