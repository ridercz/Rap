Rap
===

**Rap** (což je méně obvyklý název pro černého koně) je systém určený pro rezervaci sdílených prostředků více uživateli tam, kde není k dispozici nějaký sofistikovanější systém.

Aplikaci jsem napsal pro účely jezdecké stáje, v níž působí moje kamarádka [Sirril](http://www.sirril.cz), ale může se hodit i dalším lidem, je naprosto univerzální.

Rap je navržen tak, aby jednoduše fungoval na jakémkoliv hostingu. Pro ukládání dat proto využívá embedded databázi _Microsoft SQL Server Compact Edition_ (ale lze to snadno změnit na plnohodnotný SQL server, stačí změnit DB providera v souboru ``web.config``). Jedná se o mou první aplikaci, kterou jsem _(značně touto platformou znechucen)_ napsal v ASP.NET MVC (verzi 5.1).

Licence
-------
Tato aplikace je open source a je licencována pod [MIT License](LICENSE.md).