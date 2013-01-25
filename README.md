AppsInfo
========

Components to show informations about ASP .NET Web Apps (MVC and Web API)

Currently supporting only information about app version. More to come...

Setup
========

Manually
--------
* Reference the AppsInfo.HttpHandlers.dll assembly on your web app.

* Adds the httpHandler to web.config:
```xml
<httpHandlers>
    <add verb="*" path="AppsInfo*" type="AppsInfo.HttpHandlers.AppsInfoHttpHandler, AppsInfo.HttpHandlers" />
</httpHandlers>
```

* AppsInfo will try to identify your web app assembly, but in some case this is impossible, so just help it adding this key on AppSettings:
```xml
<add key="AppsInfo:AssemblyName" value="your web app assembly name" />
```

NuGet
--------
Soon...


Using
========
* Access the the address below in your web app:
* Json: http://<your web app url>/AppsInfo.json
* Png: http://<your web app url>/AppsInfo.png
* Html: http://<your web app url>/AppsInfo


Improvements
------------

Create a fork of [AppsInfo](https://github.com/giacomelli/AppsInfo/fork). 
Did you change it? [Submit a pull request](https://github.com/giacomelli/AppsInfo/pull/new/master).

License
-------

Licensed under the Apache License, Version 2.0 (the "License").


Change Log
----------
0.1 Only supports version information.