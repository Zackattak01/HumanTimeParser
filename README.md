<div align="center">
  <h1>HumanTimeParser</h1>
  <p>A library for parsing human time formats into useable objects.</p>
</div>


<div align="center">
  <h2>Installing</h2>
  <p>You can download the HumanTimeParser packages from our NuGet feeds:</p>
  <ul>
    <li><a href="https://www.nuget.org/packages/HumanTimeParser.Core">HumanTimeParser.Core</a></li>
    <li><a href="https://www.nuget.org/packages/HumanTimeParser.English">HumanTimeParser.English</a></li>
  </ul>
  Any prerelease packages are CI/CD builds.
</div>




<div align="center">
  <h2>Example</h2>
  <p>A small example to demonstrate how the <a href="https://github.com/Zackattak01/HumanTimeParser/blob/main/src/HumanTimeParser.English/EnglishTimeParser.cs">EnglishTimeParser</a> works.
<div/>
  
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

<div align="center">
  <h2>HumanTimeParser.Core</h2>

  <p>This project contains all the tools required to build a time parser.</p>

  <p>Notable features:</p>
  <ul>
    <li>Default parser</li>
    <li>Default sectionizer (text splitter)</li>
    <li>Tokenizer abstractions</li>
    <li>Default set of tokens</li>
    <li>Time construct abstraction</li>
  </ul>

  <p>This project can be used as a base tool for creating a time parser for other languages.</p>

</div>

<div align="center">
  <h2>HumanTimeParser.English</h2>

  <p>This is the project that contains the implementation of HumanTimeParser.Core for the English language.</>

  <p>Notable features:</p>
  
  <ul>
  <li>Relative time parsing</li>
  <li>Date parsing</li>
  <li>Time of day parsing</li>
  <li>Day of week parsing</li>
  </ul>
  
</div>

<div align="center">
  <h2>Other language implementations</h2>

  <p>None ;)</p>
</div>
