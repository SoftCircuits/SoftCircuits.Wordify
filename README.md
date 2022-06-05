# SoftCircuits.Wordify

`Wordify` is a static class that contains methods and extension methods to create and modify text. It includes methods to convert numbers and dates to text, insert spaces into camel-case strings, pluralize strings, truncate strings, convert Roman numerals, create memory size strings and much more.

Note: `Wordify` methods that accept a `string` parameter always correctly handle when that parameter is null. And all methods that return a `string` ensure the return value is never null.

## Numbers

The library can be used to convert numbers to words.

| Code | Output |
|---|---|
| `Wordify.Transform(1);` | one |
| `Wordify.Transform(123);` | one hundred twenty-three |
| `Wordify.Transform(12345);` | twelve thousand three hundred forty-five |

The `Transform` has many overloads. The one that accepts floating point values also takes a `FractionOption` argument that specifies how to format the fractional part.

| Code | Output |
|---|---|
| `Wordify.Transform(345.67, FractionOption.Round);` | three hundred forty-six |
| `Wordify.Transform(345.67, FractionOption.Truncate);` | three hundred forty-five |
| `Wordify.Transform(345.67, FractionOption.Decimal);` | three hundred forty-five and .7 |
| `Wordify.Transform(345.67, FractionOption.Fraction);` | three hundred forty-five and 67/100 |
| `Wordify.Transform(345.67, FractionOption.Check);` | three hundred forty-five and 67/100 |
| `Wordify.Transform(345.67, FractionOption.Words);` | three hundred forty-five and sixty-seven one hundredths |
| `Wordify.Transform(345.67, FractionOption.UsCurrency);` | three hundred forty-five dollars and sixty-seven cents |

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


TODO: Should these be combined into one topic? Converting symbols to strings???
## Enums
## Inserting Spaces


## Pluralization

The library also provides support for making words plural and then back to singular again. Use the `Pluralize()` extension method to make a word plural.

| Code | Output |
|---|---|
| `"cat".Pluralize();` | cats |
| `"boy".Pluralize();` | boys |
| `"cross".Pluralize();` | crosses |
| `"party".Pluralize();` | parties |
| `"goose".Pluralize();` | geese |
| `" dog! ".Pluralize();` | dogs! |
| `"sheep".Pluralize();` | sheep |

And use the `Singularize()` extension method to make a plural word singular.

| Code | Output |
|---|---|
| `"cats".Singularize();` | cas |
| `"boys".Singularize();` | boy |
| `"crosses".Singularize();` | cross |
| `"parties".Singularize();` | party |
| `"geese".Singularize();` | goose |
| `" dogs! ".Singularize();` | dog! |
| `"sheep".Singularize();` | sheep |

*Note: The English language is complex. It is not possible for the library to handle every word perfectly. We are looking to make this functionality more extendible in the future.*

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

## Quoting Strings

Use the `WrapInQuotes()` and `WrapInSingleQuotes()` methods to wrap a string or character in double or single quotes.

| Code | Output |
|---|---|
| `"abc".WrapInQuotes();` | "abc" |
| `'a'.WrapInQuotes();` | "a" |
| `"abc".WrapInSingleQuotes();` | 'abc' |
| `'a'.WrapInSingleQuotes();` | 'a' |

## Formatting Collections

## Displaying Memory Size

The `ToMemorySize()` extension method is handy when displaying a number of bytes, such as a file size. This method accept a parameter of type `ulong`.

| Code | Output |
|---|---|
| `0UL.ToMemorySize();` | 0 B |
| `1UL.ToMemorySize();` | 1 B |
| `1024UL.ToMemorySize();` | 1 KB |
| `1124UL.ToMemorySize();` | 1.1 KB |

You can use the `FromMemorySize()` extension method to convert a memory size string back to a `ulong`. This method does not throw any exceptions. It simply parses the string as best it can. If it is unable to parse anything meaningful, this method returns 0.

| Code | Output |
|---|---|
| `"0 B".FromMemorySize();` | 0UL |
| `"1b".FromMemorySize();` | 1UL |
| `"1 kb".FromMemorySize();` | 1024UL |
| `"1.1 KB".FromMemorySize();` | 1124UL |

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
