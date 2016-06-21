Gorilla Geek Mailer
===================

Install from nuget: https://www.nuget.org/packages/Gorilla.Mailer/

```bash
Install-Package Gorilla.Mailer
```

How to use
----------

```cs
var mailer = new Mailer(enEmailProvider.SendGrid, 'your-api-key');
```