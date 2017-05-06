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
    <add verb="GET" path="/AppsInfo/*/*" type="AppsInfo.HttpHandlers.AppsInfoHttpHandler, AppsInfo.HttpHandlers" />
</httpHandlers>
```

NuGet
--------
PM> Install-Package AppsInfo


Configuration
--------
AppsInfo will try to identify your web app assembly, but in some case this is impossible, so just help it adding this key on AppSettings:
```xml
<add key="AppsInfo:AssemblyName" value="your web app assembly name" />
```


Using
========
* Access the the address below in your web app:
* Json: http://&lt;your web app url&gt;/AppsInfo/Version/json
* Png: http://&lt;&gt;your web app url&gt;/AppsInfo/Version/png
* Html: http://&lt;your web app url&gt;/AppsInfo/Version/html


Improvements
------------

Create a fork of [AppsInfo](https://github.com/giacomelli/AppsInfo/fork). 
Did you change it? [Submit a pull request](https://github.com/giacomelli/AppsInfo/pull/new/master).

License
-------

Licensed under the Apache License, Version 2.0 (the "License").
