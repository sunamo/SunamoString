# SunamoString

## Short description

Rozsáhlá sbírka metod pro práci s řetězci. Obsahuje Runner a testy.


A large number of methods for string operations

## Overview

SunamoString is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development.

## Main Components

### Key Classes

- **BalancedBrackets**
- **stack**
- **StringDelegates**
- **StringExtensions**
- **LoremIpsumGenerator**
- **SH**
- **SHNotTranslateAble**
- **TT**

### Key Methods

- `areBracketsBalanced()`
- `push()`
- `Main()`
- `Contains()`
- `RemoveInvisibleChars()`
- `RemoveWhitespaceChars()`
- `FromSpace160To32()`
- `SplitAndKeep()`
- `Generate()`
- `ContainsCl()`

## Installation

```bash
dotnet add package SunamoString
```

## Dependencies

- **Microsoft.Extensions.Logging.Abstractions** (v9.0.3)
- **Diacritics** (v3.3.29)

## Package Information

- **Package Name**: SunamoString
- **Version**: 25.6.7.1
- **Target Framework**: net9.0
- **Category**: Platform-Independent NuGet Package
- **Source Files**: 49

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.

<!-- sunamo:test-coverage:start -->
## Test coverage

| Metric | Value |
|---|---|
| Method | static |
| Test projects | 1 |
| Public types referenced by tests | 2/22 (9.1 %) |
| Public methods referenced by tests | 6/199 (3.0 %) |
| Line coverage | n/a |
| Branch coverage | n/a |
| Generated (UTC) | 2026-11-22 21:24 |

<!-- sunamo:test-coverage:data {"package":"SunamoString","container":"pinp","testProjects":1,"publicTypes":22,"typesReferencedByTests":2,"publicMethods":199,"methodsReferencedByTests":6,"lineRate":null,"branchRate":null,"method":"static","generatedUtc":"2026-11-22 21:24"} -->
<!-- sunamo:test-coverage:end -->
