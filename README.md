<div align="center">
  <h1>HumanTimeParser</h1>
  <img src="https://img.shields.io/github/workflow/status/Zackattak01/HumanTimeParser/publish/main?style=flat-square"/>
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
* Dotnet 5.0.102
* Linux

|    Intensity |  # of Tokens |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
|------------- |--------------|-----------|-----------|-----------|--------|-----------|
|      Toddler |            1 | 2.628 us  | 0.0118 us | 0.0111 us | 0.0114 | 1216 B    |
|       Simple |            2 | 2.906 us  | 0.0174 us | 0.0146 us | 0.0076 | 928 B     |
| Intermediate |            4 | 6.408 us  | 0.0272 us | 0.0227 us | 0.0153 | 1808 B    |
|  Stress Test |           14 | 12.525 us | 0.0720 us | 0.0638 us | 0.0305 | 3688 B    |

## Other language implementations
None ;)
