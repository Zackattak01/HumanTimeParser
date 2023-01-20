<div align="center">
  <h1>HumanTimeParser</h1>
  <img src="https://img.shields.io/github/actions/workflow/status/Zackattak01/HumanTimeParser/publish.yml/?branch=main&style=flat-square"/>
  <img src="https://img.shields.io/nuget/v/HumanTimeParser.Core?style=flat-square"/>
  <img src="https://img.shields.io/tokei/lines/github/Zackattak01/HumanTimeParser?style=flat-square"/>
  <p>An easy to use library for parsing human time formats into useable objects.</p>
</div>

## Installing
You can download the HumanTimeParser packages from our NuGet feeds:
* [HumanTimeParser.Core](https://www.nuget.org/packages/HumanTimeParser.Core/)
* [HumanTimeParser.English](https://www.nuget.org/packages/HumanTimeParser.English)

Any prerelease packages are CI/CD builds.

## Example
A small example to demonstrate how the [EnglishTimeParser](https://github.com/Zackattak01/HumanTimeParser/blob/main/src/HumanTimeParser.English/EnglishTimeParser.cs) works.
```csharp
// instantiate a reusable time parser.
var parser = new EnglishTimeParser();
// returns a generic ITimeParsingResult
var result = parser.Parse("6 minutes from now"); 

// to determine if the result is successful or not we pattern match.  Pattern matching for DefaultTimeParsingResult also works.
if (result is ISuccessfulTimeParsingResult<DateTime> successfulResult) 
{
  // sucessfulResult.Value will represent a time 6 minutes from DateTime.Now
  Console.WriteLine(successfulResult.Value); 
}
else
{
  // handle failed result
}
```

## HumanTimeParser.Core
This project contains all the tools required to build a time parser.

Notable features:
* Default parser
* Default sectionizer (text splitter)
* Tokenizer abstractions
* Default set of tokens
* Time construct abstraction

This project can be used as a base tool for creating a time parser for other languages.

## HumanTimeParser.English
This is the project that contains the implementation of HumanTimeParser.Core for the English language.

Notable features:
* Relative time parsing
* Date parsing
* Time of day parsing
* Day of week parsing

### Performance
The following benchmark was run on:
* AMD Ryzen 3 1200 (4 core)
* Dotnet 6.0.301
* Linux

|    Intensity |  # of Tokens |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
|------------- |--------------|-----------|-----------|-----------|--------|-----------|
|      Toddler |            1 | 2.101 us  | 0.0151 us | 0.0141 us | 0.0114 | 1192 B    |
|       Simple |            2 | 2.562 us  | 0.0164 us | 0.0146 us | 0.0114 | 976 B     |
| Intermediate |            4 | 5.548 us  | 0.0245 us | 0.0217 us | 0.0229 | 1928 B    |
|  Stress Test |           14 | 10.751 us | 0.0475 us | 0.0444 us | 0.0458 | 3840 B    |

## Other language implementations
None ;)
