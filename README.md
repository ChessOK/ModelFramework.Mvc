Getting Started
================

ModelFramework.Mvc contains ASP.NET MVC 3-4 extensions for [ModelFramework](https://github.com/ChessOK/ModelFramework):
* ModelController
* AutoFac modules that support ASP.NET autoloading feature
* Tools for ViewModels

Installation
-------------
ModelFramework.Mvc is available as a [NuGet Package](http://nuget.org/packages/ModelFramework.Mvc). 
To install it, open your Package Manager Console and type:
```
PM> Install-Package ModelFramework.Mvc
```

It will install all dependencies, including [AutoFac](http://code.google.com/p/autofac/), register AutoFac
as default Dependency Resolver and set request-based lifetime for `ModelContext` 
(see [MvcModule.cs](https://github.com/ChessOK/ModelFramework.Mvc/blob/master/ModelFramework.Mvc/MvcModule.cs)).

Usage
------

### ModelController

TBD.

```csharp
public class UserController : ModelController
{
    [HttpPost]
    public ActionResult Register(RegisterModel model)
    {
        if (Bus.TrySend<RegisterCommand>(x => x.Data = model))
        {
            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }
}
```

### Autoloading modules

TBD. See automatically-created class `WebModule` in your project for example.

### View models

TBD.
