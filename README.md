# SoftCircuits.Wordify

[![NuGet version (SoftCircuits.Wordify)](https://img.shields.io/nuget/v/SoftCircuits.Wordify.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.Wordify/)

```
Install-Package SoftCircuits.Wordify
```

`Wordify` is a static class that contains extension methods to create and modify text. It includes methods to convert numbers and dates to text, insert spaces into camel-case strings, pluralize strings, truncate strings, convert Roman numerals, create memory size strings and much more.

Note: `Wordify` methods that accept a `string` parameter always correctly handle when that parameter is null. And all methods that return a `string` ensure the return value is never null.

## Numbers

The library can be used to convert numbers to words.

| Code | Output |
|---|---|
| `1.Wordify();` | one |
| `123.Wordify();` | one hundred twenty-three |
| `12345.Wordify();` | twelve thousand three hundred forty-five |

The `Wordify()` method has many overloads. The one that accepts floating point values also takes a `FractionOption` argument that specifies how to format the fractional part.

| Code | Output |
|---|---|
| `345.67.Wordify(FractionOption.Round);` | three hundred forty-six |
| `345.67.Wordify(FractionOption.Truncate);` | three hundred forty-five |
| `345.67.Wordify(FractionOption.Decimal);` | three hundred forty-five and .7 |
| `345.67.Wordify(FractionOption.Fraction);` | three hundred forty-five and 67/100 |
| `345.67.Wordify(FractionOption.Check);` | three hundred forty-five and 67/100 |
| `345.67.Wordify(FractionOption.Words);` | three hundred forty-five and sixty-seven one hundredths |
| `345.67.Wordify(FractionOption.UsCurrency);` | three hundred forty-five dollars and sixty-seven cents |

Note: Because most of these methods return strings, it's easy to chain extension method calls. For example: `123.67.Wordify(FractionOption.Decimal).Capitalize();`.

## Ordinals

The library also has support for converting numbers to ordinals using the `MakeOrdinal()` extention method.

| Code | Output |
|---|---|
| `1.MakeOrdinal();` | first |
| `123.MakeOrdinal();` | one hundred twenty-third |

The `MakeOrdinalDigits()` extension method works similarly but outputs digits instead of words.

| Code | Output |
|---|---|
| `1.MakeOrdinalDigits();` | 1st |
| `123.MakeOrdinalDigits();` | 123rd |

## Dates and TimeSpans

The library also provides support for describing the differences between two `DateTime` values. This version of `Wordify()` describes the relationship between the specified date and time with the current date and time. Or, you can supply your own date and time that the description should be relative to.

The following examples assume a `DateTime` variable with the name `now`. (Note that when using the version that automatically gets the current date and time, that it's possible to have a couple of milliseconds pass before it does.)

| Code | Output |
|---|---|
| `now.Wordify();` | now |
| `now.Wordify(now);` | now |
| `now.AddDays(0).Wordify(now);` | now |
| `now.AddHours(2).AddMinutes(24).Wordify();` | 2 hours from now |
| `now.AddDays(4).Wordify(now);` | 4 days from now |
| `now.AddHours(-2).AddMinutes(-24).Wordify();` | 2 hours ago |
| `now.AddHours(-2).AddMinutes(-24).Wordify(DateTimeOption.UseWords);` | two hours ago |

Here are examples using the version of `Wordify()` for `TimeSpan` values. This method also takes a `precision` argument. By default, the precision is 1, and only one part of the time span will be described. Pass a larger number to include additional parts.
 
| Code | Output |
|---|---|
| `TimeSpan.Zero.Wordify();` | 0 milliseconds |
| `new TimeSpan(4, 7, 44).Wordify();` | 4 hours |
| `new TimeSpan(4, 7, 44).Wordify(1);` | 4 hours |
| `new TimeSpan(4, 7, 44).Wordify(2);` | 4 hours and 7 minutes |
| `new TimeSpan(4, 7, 44).Wordify(3);` | 4 hours, 7 minutes and 44 seconds |
| `new TimeSpan(4, 7, 44).Wordify(3, DateTimeOption.UseWords);` | four hours, seven minutes and forty-four seconds |

## Wordifying Strings

`SoftCircuits.Wordify` has several methods to help convert symbol names like `TotalCount`, `total_count` and `total-count` to text like `total count`. The examples below use the `Wordify()` extension method. If you know what method should be used for your string, you can achieve a small performance gain by passing the appropriate `WordifyOption` option. Otherwise, you can pass `WordifyOption.AutoDetect` and `Wordify()` will attempt to automatically detect the type of transformation needed.

| Code | Output |
|---|---|
| `"abcDef".Wordify();` | abc Def |
| `"abc_def".Wordify(WordifyOption.AutoDetect);` | abc def |
| `"abc-def".Wordify(WordifyOption.AutoDetect);` | abc def |
| `"abc-def".Wordify(WordifyOption.ReplaceHypens);` | abc def |

If you know your string contains camel case, you can call the `InsertCamelCaseSpaces()` extension method directly.

| Code | Output |
|---|---|
| `"ThisIsATest".InsertCamelCaseSpaces();` | This Is A Test |
| `"TheHTTPProtocol".InsertCamelCaseSpaces();` | The HTTP Protocol |
| `"IBoughtAnOldIBMXT".InsertCamelCaseSpaces();` | I Bought An Old IBMXT |

Notice in the last example that there is no way to detect if *IBM* and *XT* should be separate words. That's just a limitation of word detection from camel case.

Finally, there is a variation of the `Wordify()` method specifically for `enum`s. This extension method takes an `enum`. If the `enum` value has a `DescriptionAttribute` attribute, the method returns the description from that attribute. Otherwise, the name of the `enum` is passed to the `Wordify()` extension method described above.

This `Wordify()` extension method takes an optional `bool` argument that, when set to `true`, will prevent this method from checking for the presence of a `DescriptionAttribute`.

The examples below assuming the following `enum`.

```cs
enum MyEnums
{
    [Description("First enum")]
    One,
    [Description("Second enum")]
    Two,
    [Description("Third enum")]
    Three,
    OnTheGo,
    ReadHTMLPage,
}
```

| Code | Output |
|---|---|
| `MyEnums.One.Wordify();` | First enum |
| `MyEnums.Two.Wordify();` | Second enum |
| `MyEnums.Two.Wordify(true);` | Two |
| `MyEnums.Three.Wordify(false);` | Third enum |
| `MyEnums.OnTheGo.Wordify();` | On The Go |
| `MyEnums.ReadHTMLPage.Wordify();` | Read HTML Page |

## Pluralization

The library also provides support for making words plural and then back to singular again. Use the `Pluralize()` extension method to make a word plural.

| Code | Output |
|---|---|
| `"cat".Pluralize();` | cats |
| `"boy".Pluralize();` | boys |
| `"cross".Pluralize();` | crosses |
| `"party".Pluralize();` | parties |
| `"goose".Pluralize();` | geese |
| `"sheep".Pluralize();` | sheep |
| `" dog! ".Pluralize();` | dogs! |

And use the `Singularize()` extension method to make a plural word singular.

| Code | Output |
|---|---|
| `"cats".Singularize();` | cat |
| `"boys".Singularize();` | boy |
| `"crosses".Singularize();` | cross |
| `"parties".Singularize();` | party |
| `"geese".Singularize();` | goose |
| `"sheep".Singularize();` | sheep |
| `" dogs! ".Singularize();` | dog! |

Note that the English language is complex. It is just not possible for the library to handle every word perfectly. You can use the `Wordify.AddIrregularNoun()` and `Wordify.AddDefectiveNoun()` methods to add additional words that require special handling by the pluralizer.

## Truncating

The library has several methods that help in formatting strings that are too long. The `Truncate()` method wil shorten a string according to the options you specify.

| Code | Output |
|---|---|
| `"Another test string".Truncate(16);` | Another test str |
| `"Another test string".Truncate(16, TruncateOption.None);` | Another test str |
| `"Another test string".Truncate(16, TruncateOption.AppendEllipsis);` | Another test ... |
| `"Another test string".Truncate(16, TruncateOption.TrimPartialWords);` | Another test |
| `"Another test string".Truncate(16, TruncateOption.TrimPartialWords \| TruncateOption.AppendEllipsis);` | Another test... |

## Converting Case

`Wordify` contains several extension methods for setting case of a string. You can use any of the individual methods `SetUpperCase()`, `SetLowerCase()`, `Capitalize()` or `SetTitleCase()`. Or you can pass a `CaseOption` parameter to `SetCase()`.

| Code | Output |
|---|---|
| `"this is a test".SetUpperCase();` | THIS IS A TEST |
| `"THIS IS A TEST".SetLowerCase();` | this is a test |
| `"this is a test".Capitalize();` | This is a test |
| `"this is a test".SetTitleCase();` | This is a Test |
| `"this is a test".SetCase(CaseOption.Capitalize);` | This is a test |

## Quoting Strings

Use the `WrapInQuotes()` and `WrapInSingleQuotes()` methods to wrap a string or character in double or single quotes.

| Code | Output |
|---|---|
| `"abc".WrapInQuotes();` | "abc" |
| `'a'.WrapInQuotes();` | "a" |
| `"abc".WrapInSingleQuotes();` | 'abc' |
| `'a'.WrapInSingleQuotes();` | 'a' |

## Formatting Collections

Use the `Wordify()` extension method to combine a collection of items into a string.

| Code | Output |
|---|---|
| `(new[] { 1, 2, 3 }).Wordify();` | 1, 2 and 3 |
| `(new[] { 1, 2, 3 }).Wordify(CollectionOption.AndConjunction);` | 1, 2 and 3 |
| `(new[] { 1, 2, 3 }).Wordify(CollectionOption.OrConjunction);` | 1, 2 or 3 |
| `(new[] { 1, 2, 3 }).Wordify(CollectionOption.OxfordComma);` | 1, 2, and 3 |
| `(new[] { 1, 2, 3 }).Wordify(CollectionOption.OrConjunction \| CollectionOption.OxfordComma);` | 1, 2, or 3 |

## Displaying Memory Size

The `ToMemorySize()` extension method is handy when displaying a number of bytes, such as a file size. This method is overloaded to accept a parameter of type `int` or type `ulong`.

| Code | Output |
|---|---|
| `0.ToMemorySize();` | 0 B |
| `1.ToMemorySize();` | 1 B |
| `1000UL.ToMemorySize(MemorySizeOption.Decimal);` | 1 KB |
| `1024UL.ToMemorySize(MemorySizeOption.Binary);` | 1 KiB |
| `1124UL.ToMemorySize(MemorySizeOption.Binary);` | 1.1 KiB |

You can use the `FromMemorySize()` extension method to convert a memory size string back to a `ulong`. This method does not throw any exceptions. It simply parses the string as best it can. If it is unable to parse anything meaningful, this method returns 0.

| Code | Output |
|---|---|
| `"0 B".FromMemorySize();` | 0UL |
| `"1b".FromMemorySize();` | 1UL |
| `"1 kb".FromMemorySize();` | 1000UL |
| `"1.1 KIB".FromMemorySize();` | 1124UL |

## Roman Numerals

The `ToRomanNumerals()` extension method converts a number to Roman numerals.

| Code | Output |
|---|---|
| `0.ToRomanNumerals();` | N |
| `1.ToRomanNumerals();` | I |
| `2.ToRomanNumerals();` | II |
| `3.ToRomanNumerals();` | III |
| `4.ToRomanNumerals();` | IV |
| `1900.ToRomanNumerals();` | MCM |
| `1912.ToRomanNumerals();` | MCMXII |
| `2000.ToRomanNumerals();` | MM |
| `2022.ToRomanNumerals();` | MMXXII |

Use the `ParseRomanNumerals()` extension method to convert a string of Roman numerals back to an integer. This method throws an exception if the string cannot be converted. You can also use the `TryParseRomanNumerals()` method to instead return false when a string cannot be converted.

| Code | Output |
|---|---|
| `"N".ParseRomanNumerals();` | 0 |
| `"I".ParseRomanNumerals();` | 1 |
| `"II".ParseRomanNumerals();` | 2 |
| `"III".ParseRomanNumerals();` | 3 |
| `"IV".ParseRomanNumerals();` | 4 |
| `"MCM".ParseRomanNumerals();` | 1900 |
| `"MCMXII".ParseRomanNumerals();` | 1912 |
| `"MM".ParseRomanNumerals();` | 2000 |
| `"MMXXII".ParseRomanNumerals();` | 2022 |
| `"  V  ".ParseRomanNumerals();` | 5 |

## Spreadsheet Column Names

Support is also provided for generating and parsing spreadsheet column names.

| Code | Output |
|---|---|
| `1.ToSpreadsheetColumn();` | A |
| `2.ToSpreadsheetColumn();` | B |
| `3.ToSpreadsheetColumn();` | C |
| `26.ToSpreadsheetColumn();` | Z |
| `27.ToSpreadsheetColumn();` | AA |
| `28.ToSpreadsheetColumn();` | AB |

Use the `ParseSpreadsheetColumn()` extension method to convert a spreadsheet column name back to an integer. This method throws an exception if the string cannot be converted. You can also use the `TryParseSpreadsheetColumn()` method to instead return false when a string cannot be converted.

| Code | Output |
|---|---|
| `"A".ParseSpreadsheetColumn();` | 1 |
| `"B".ParseSpreadsheetColumn();` | 2 |
| `" C ".ParseSpreadsheetColumn();` | 3 |
| `" z ".ParseSpreadsheetColumn();` | 26 |
| `" aa ".ParseSpreadsheetColumn();` | 27 |
| `"ab".ParseSpreadsheetColumn();` | 28 |

## Formatting Data

The `FormatPhoneNumber()` extension method can be used to convert a string of digits as a phone number.

| Code | Output |
|---|---|
| `"1234567".FormatPhoneNumber();` | 123-4567 |
| `"1234567890".FormatPhoneNumber();` | 123-456-7890 |
| `"1234567890".FormatPhoneNumber(PhoneOption.AreaCodeParentheses);` | (123) 456-7890 |
| `"12345678901".FormatPhoneNumber();` | 1-234-567-8901 |
| `"12345678901".FormatPhoneNumber(PhoneOption.InternationalPlusSign);` | +1-234-567-8901 |
| `"  1 2 3 4 5 6 7  ".FormatPhoneNumber();` | 123-4567 |

The library includes several other methods that are not extension methods that make it easy to format other data.

| Code | Output |
|---|---|
| `Wordify.FormatName("John");` | John |
| `Wordify.FormatName("John", "Van Dyk");` | John Van Dyk |
| `Wordify.FormatName("John", "Van Dyk", "W.", "Dr.", "III");` | Dr. John W. Van Dyk III |

| Code | Output |
|---|---|
| `Wordify.FormatAddress("123 Elm");` | 123 Elm |
| `Wordify.FormatAddress("123 Elm", "Apt 3", delimiter: "-");` | 123 Elm-Apt 3 |
| `Wordify.FormatAddress("123 Elm", "Apt 3", "Small Town", "UT", "84084", "United States", delimiter: "-");` | 123 Elm-Apt 3-Small Town, UT 84084-United States |


| Code | Output |
|---|---|
| `Wordify.FormatCityStateZip("Small Town");` | Small Town |
| `Wordify.FormatCityStateZip("Small Town", "UT");` | Small Town, UT |
| `Wordify.FormatCityStateZip("Small Town", "UT", "84084");` | Small Town, UT 84084 |

## Miscellaneous

The library also includes a number of helper extension methods that don't really fall into any other category. The examples below assume a variable `s` of type `string`.

| Extension Method | Description |
|---|---|
| `s.CountWords();` | Counts the number of words in this string. Words are separated by one or more whitespace character. |
| `s.NormalizeWhiteSpace();` | Returns a copy of `s` with all whitespace sequences replaced with a single space character and all leading and trailing whitespace removed. |
| `s.EmptyIfNull();` | If `s` is `null`, then an empty string is returned. Otherwise, `s` is returned. |
| `s.NullIfEmpty();` | If `s` is an empty string, then `null` is returned. Otherwise, `s` is returned. |
| `s.EmptyIfNullOrWhiteSpace();` | If `s` is `null`, an empty string or only contains whitespace, then an empty string is returned. Otherwise, `s` is returned. |
| `s.NullIfEmptyOrWhiteSpace();` | If `s` is `null`, an empty string or only contains whitespace, then `null` is returned. Otherwise, `s` is returned. |
