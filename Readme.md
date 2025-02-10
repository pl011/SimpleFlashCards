This is a simple WPF app for displaying flash cards. It relies on an SQLite database named "Cards.db" being present in the same folder as the executable.

The database should contain a 'card' table, which can be created with the following SQL: 
```
CREATE TABLE "card" (
	"id"	INTEGER UNIQUE,
	"front"	TEXT NOT NULL,
	"back"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
)
```

The cards should be defined in the "card" table. The "front" field contains the text to display on the front of the card (which is first shown when a new random card is drawn). The "back" field contains the text to be displayed on the back of the card.


