# SoftCircuits.Wordify

This library provides...

`Wordify` is a static class, which contains static methods and extension methods.

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

