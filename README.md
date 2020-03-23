# OTR

Style guide and checklist for developers.

- [OTR](#OTR)
  - [Coding Style](#Coding-Style)
    - [Namespaces](#Namespaces)
    - [Classes & Interfaces](#Classes--Interfaces)
    - [Methods](#Methods)
    - [Fields](#Fields)
    - [Parameters](#Parameters)
    - [Acronyms/Initialisms](#AcronymsInitialisms)
  - [Declarations](#Declarations)
    - [Access Level Modifiers](#Access-Level-Modifiers)
    - [Classes](#Classes)
    - [Interfaces](#Interfaces)
  - [Variables](#Variables)
    - [Type Declaration](#Type-Declaration)
    - [Default Initialisation](#Default-Initialisation)
    - [Strings](#Strings)
  - [Brace Style](#Brace-Style)
  - [Functions](#Functions)
    - [Return Statements](#Return-Statements)
  - [Language](#Language)
  - [Async/Await](#AsyncAwait)
  - [MVVM & Architecture Guidelines](#MVVM--Architecture-Guidelines)
  - [Styles & Theming](#Styles--Theming)
  - [Localisation (I18N) 🌏](#Localisation-I18N-)
  - [Accessibility (A11Y) ♿](#Accessibility-A11Y-)

## Coding Style

### Namespaces

Namespaces are all **PascalCase**, multiple words concatenated together, without hyphens ( - ) or underscores ( \_ ).

😡 **BAD**

```csharp
peregrine.otrapp.core
```

😀 **GOOD**

```csharp
Peregrine.OtrApp.Core
```

### Classes & Interfaces

Written in **PascalCase**. For example `MyAwesomeClass`.

### Methods

Methods are written in **PascalCase**. For example `DoSomethingAwesome()`.

### Fields

All static and constant fields are written in **PascalCase**, including private/protected fields.

For example:

```csharp
public class MyClass
{
    public const int PublicConstField = 0;
    public static int PublicStaticField;

    private const int PrivateConstField = 0;
    private static int PrivateStaticField;

    protected const int PrivateConstField = 0;
    protected static int PrivateStaticField;
}
```

All public/protected fields are written **PascalCase**, private fields are written in **camelCase** prefixed with an underscore ( \_ ).

😡 **BAD**

```csharp
public int publicField;
int myPrivateInt;
private string myPrivateStr;
protected int myProtected;
```

😀 **GOOD**

```csharp
public int PublicField;
private int _myPrivateInt;
private string _myPrivateStr;
protected int MyProtected;
```

### Parameters

Parameters are written in **camelCase**.

😡 **BAD**

```csharp
void EngageWarpCore(double Factor)
```

😀 **GOOD**

```csharp
void EngageWarpCore(double factor)
```

Single character values are to be avoided except for temporary looping variables.

### Acronyms/Initialisms

In code, acronyms/initialisms should be treated as words. For example:

😡 **BAD**

```csharp
OTRApp
HTTPClient
URL
findByID
```  

😀 **GOOD**

```csharp
OtrApp
HttpClient
Url
findById
```

## Declarations

### Access Level Modifiers

Access level modifiers should be explicitly defined for classes, methods and member variables.

😡 **BAD**

```csharp
public class MyClass
{
    int _meaningOfLife;

    void DoWork()
    {
        _myPrivateInt = 42;
    }
}
```  

😀 **GOOD**

```csharp
public class MyClass
{
    private int _meaningOfLife;

    private void DoWork()
    {
        _myPrivateInt = 42;
    }
}
```

### Classes

Exactly one class per source file, although inner classes are encouraged where scoping appropriate.

### Interfaces

All interfaces should be prefaced with the letter **I**.

😡 **BAD**

```csharp
public interface DataService
{
}
```

😀 **GOOD**

```csharp
public interface IDataService
{
}
```

## Variables

### Type Declaration

Prefer `var` where ever possible.

😡 **BAD**

```csharp
float myFloat = 3.142;
IDictionary<int, IEnumerable<string>> myDictionary = new Dictionary<int, IEnumerable<string>>();
```  

😀 **GOOD**

```csharp
var myFloat = 3.142f;
var myDictionary = new Dictionary<int, IEnumerable<string>>();
```

### Default Initialisation

Prefer `default` where ever possible.

😡 **BAD**

```csharp
var myInt = (int?)null;
IList<string> myList = null;
string empty = null;
```  

😀 **GOOD**

```csharp
var myInt = default(int?);
var myList = default(IList<string>);
var empty = string.Empty;
```

### Strings

Always initialise strings to a value, they should never be null. Use `string.Empty` where ever possible;

😡 **BAD**

```csharp
string firstName = "Bob";
string middleName = (string)null;
var lastName = "";
```  

😀 **GOOD**

```csharp
var firstName = "Bob";
var middleName = string.Empty;
var lastName = string.Empty;
```

## Brace Style

All braces get their own line as it is a C# convention:

😡 **BAD**

```csharp
public class MyClass {
    private void DoSomething() {
        if (someTest) {
          // ...
        } else {
          // ...
        }
    }
}
```

😀 **GOOD**

```csharp
public class MyClass
{
    private void DoSomething()
    {
        if (someTest)
        {
          // ...
        }
        else
        {
          // ...
        }
    }
}
```

## Functions

### Return Statements

Functions should have only one `return` statement.

😡 **BAD**

```csharp
public string GetProductName(int id)
{
    if (id <= 0) return string.Empty;

    try
    {
        return DataService.GetProduct(id);
    }
    catch (Exception ex)
    {
        Logger.Log(ex);
    }

    return string.Empty;
}
```

😀 **GOOD**

```csharp
public string GetProductName(int id)
{
    var productName = string.Empty;

    if (id <= 0)
    {
        try
        {
            productName = DataService.GetProduct(id);
        }
        catch (Exception ex)
        {
            Logger.Log(ex);
        }
    }

    return productName;
}
```

## Language

Use US English spelling. 🗽

😡 **BAD**

```csharp
var colour = "red";
```

😀 **GOOD**

```csharp
var color = "red";
```

## Async/Await

If you feel a little uneasy with async/await and Tasks, it might be helpful to read some of Stephen Cleary's blog posts, [Async and Await](https://blog.stephencleary.com/2012/02/async-and-await.html) is a good introduction.

Async functions should always return a `Task`, using the generic if applicable.

😡 **BAD**

```csharp
public async void PopulateDatabaseTables()
{
    await DropAndCreateTables();
    await PopulateTables();
}
```

😀 **GOOD**

```csharp
public async Task PopulateDatabaseTables()
{
    await DropAndCreateTables();
    await PopulateTables();
}
```

‼ **EXCEPTION** ‼

There are two exceptions to this rule.

- Async event handlers

```csharp
public async void OnButtonClick(object sender, EventArgs args)
{
    await DoWork();
}
```

- Async overrides for events

```csharp
// Android
protected override async void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);

    await DoWork();
}

// iOS
public override async void ViewDidLoad()
{
    base.ViewDidLoad();

    await DoWork();
}

// XForms
 protected override async void OnAppearing()
{
    base.OnAppearing();

    await DoWork();
}
```

## MVVM & Architecture Guidelines

Each and every page/view should map to it's own view model (VM). The VMs should not be depend on a concrete implementations, but on interfaces.

No static classes that have state. Helpers and extensions are fine, but they should not have side effect, i.e. stateless.

Make use of MVVM features, such as converters & messaging.

## Styles & Theming

There should be minimal (no) specific element styling. This should all be resourced, so it can be changed in one place and be reflected throughout the UI. Following this approach also allows for multiple themes to be added.

## Localisation (I18N) 🌏

Any static string or format that is shown to user needs to be resourced so it can be accessed by all platforms. Resource keys should be **PascalCase**, with the word `Item` representing placeholders.

```xml
<data name="FirstName" xml:space="preserve">
    <value>First name</value>
</data>
<data name="ItemAddedToFavourites" xml:space="preserve">
    <value>{0} has been added to favourites!</value>
</data>
<data name="ItemColonItem" xml:space="preserve">
    <value>{0}: {1}</value>
</data>
```

## Accessibility (A11Y) ♿

Care needs to be taken to ensure the app is meeting accessibility guidelines. This means taking into account native accessibility properties and frameworks on each platform. For XForms make sure to use `AutomationProperties` where appropriate.

- Do images have alternate text
- Colour contrast (e.g. light text on a dark background, colour blindness, etc)
- Touch target size
  - Is the button easy for the user to tap?
  - Appropriate margin between elements?
- Ensure custom controls work with native accessibility features
