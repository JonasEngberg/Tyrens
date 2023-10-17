# Tyr�ns testuppgift

## Krav

I de muntliga kraven framgick att testuppgiften ska simulera ett system som anropar Mahirs ber�kningsmodul. Ber�kningarn kan ta l�ng tid att f�rdigst�lla och anv�ndren ska f� feedback att jobbet mottagits och en notifikation n�r jobbet �r f�rdigt.

Antagande
- inparametrar: User (f�r att authentisera, ), InputFolder (inneh�llande json-filer, excel mm) och WebhookUrl (g�r en httppost mot denna url med jobId n�r jobbet �r f�rdigt).
- utparametrar: outputFolder (med pdf mm)
- en endpoint, mahirs ber�kningsmodul sk�ter orkestreringen.

> Mahir:
>  
> Jag vill ha en en json fil. Sen levererar jag typiskt en rapport (pdf).
> Men i f�rl�ngningen kommer det att bli olika kombinationer av:
>
> In:
> - En json fil
> - En mapp med en eller flera json eller excel filer och en eller flera figurer som beskriver f�ruts�ttningar och indata till ber�kningen
> 
> Ut:
> - N�got som kan visas via en URL med en presentation av delar av eller hela resultatet
> - En mapp med filer
> - En komplett rapport (pdf).
>
> Troligen kommer olika applikationer att f� lite olika behov och olika processer kommer att v�xa fram. Ofta �r det praktiskt att jag orkestrerar. Speciellt f�r fall d�r allt finns tillg�ngligt i RAM. Andra fall kanske f�ruts�tter kommunikation mellan olika k�llor av data och d� kan det vara mer effektivt att ha skilda processer och en orkestrering �utifr�n��

## L�nkar

[Blog Asynchronous Messaging](https://blog.stephencleary.com/2021/01/asynchronous-messaging-1-basic-distributed-architecture.html)

[Blog Async processing of long-running tasks in ASP.NET Core](https://blog.elmah.io/async-processing-of-long-running-tasks-in-asp-net-core/)

## Framtida teknisk utveckling

- Anv�nd en permanent message queue som RabbitMQ
- Anv�nd en Consumer med en Mediator f�r att bryta ut k�rningen av ber�kningar till egen Handler-klass. Separerar aff�rslogiken p� ett tydligt s�tt.