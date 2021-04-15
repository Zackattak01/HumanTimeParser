<div align="center">
  <h1>HumanTimeParser</h1>
  <p>A library for parsing human time formats into useable objects.</p>
</div>

## Installing
You can download the HumanTimeParser packages from our NuGet feeds:
* [HumanTimeParser.Core](https://www.nuget.org/packages/HumanTimeParser.Core/)
* [HumanTimeParser.English](https://www.nuget.org/packages/HumanTimeParser.English

Any prerelease packages are CI/CD builds.

## Example
A small example to demonstrate how the [EnglishTimeParser](https://github.com/Zackattak01/HumanTimeParser/blob/main/src/HumanTimeParser.English/EnglishTimeParser.cs) works.
```csharp
var result = EnglishTimeParser.Parse("6 minutes from now"); // returns a generic ITimeParsingResult

if (result is ISuccessfulTimeParsingResult<DateTime> successfulResult) // to determine if the result is successful or not we pattern match.  DefaultTimeParsingResult also works.
{
  Console.WriteLine(successfulResult.Value); // sucessfulResult.Value will represent a time 6 minutes from DateTime.Now
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

## Other language implementations
None ;)
