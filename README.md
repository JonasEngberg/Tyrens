# Tyréns testuppgift

## Krav

I de muntliga kraven framgick att testuppgiften ska simulera ett system som anropar Mahirs beräkningsmodul. Beräkningarn kan ta lång tid att färdigställa och användren ska få feedback att jobbet mottagits och en notifikation när jobbet är färdigt.

Antagande
- inparametrar: User (för att authentisera, ), InputFolder (innehållande json-filer, excel mm) och WebhookUrl (gör en httppost mot denna url med jobId när jobbet är färdigt).
- utparametrar: outputFolder (med pdf mm)
- en endpoint, mahirs beräkningsmodul sköter orkestreringen.

> Mahir:
>  
> Jag vill ha en en json fil. Sen levererar jag typiskt en rapport (pdf).
> Men i förlängningen kommer det att bli olika kombinationer av:
>
> In:
> - En json fil
> - En mapp med en eller flera json eller excel filer och en eller flera figurer som beskriver förutsättningar och indata till beräkningen
> 
> Ut:
> - Något som kan visas via en URL med en presentation av delar av eller hela resultatet
> - En mapp med filer
> - En komplett rapport (pdf).
>
> Troligen kommer olika applikationer att få lite olika behov och olika processer kommer att växa fram. Ofta är det praktiskt att jag orkestrerar. Speciellt för fall där allt finns tillgängligt i RAM. Andra fall kanske förutsätter kommunikation mellan olika källor av data och då kan det vara mer effektivt att ha skilda processer och en orkestrering ”utifrån”…

## Länkar

[Blog Asynchronous Messaging](https://blog.stephencleary.com/2021/01/asynchronous-messaging-1-basic-distributed-architecture.html)

[Blog Async processing of long-running tasks in ASP.NET Core](https://blog.elmah.io/async-processing-of-long-running-tasks-in-asp-net-core/)

## Framtida teknisk utveckling

- Använd en permanent message queue som RabbitMQ
- Använd en Consumer med en Mediator för att bryta ut körningen av beräkningar till egen Handler-klass. Separerar affärslogiken på ett tydligt sätt.