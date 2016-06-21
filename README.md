Gorilla Geek Mailer
===================

Install from nuget: https://www.nuget.org/packages/Gorilla.Mailer/

```bash
Install-Package Gorilla.Mailer
```

How to use
----------

```cs
// Production
// var mailer = Mailer.Create(enEmailProvider.SendGrid, "your-api-key");

// Development
var mailer = Mailer.CreateForDevelopement("path-to-folder-output");
var providerResultKey = await mailer.Send("Subject", "From", "To", "Body");
```

Template
--------

From String:
```cs
var template = MailerTemplate.CreateFromString("Your template here [TemplateVariable]");
template.Params.Add("TemplateVariable", "with variables");

var body = template.Render();
```

From File:
```cs
var template = MailerTemplate.CreateFromFileSystem("path-to-file");
```

From Namespace:
```cs
var path = "MyApp.Templates.Email.html";
var template = MailerTemplate.CreateFromManifestResourceStream(path);
```

