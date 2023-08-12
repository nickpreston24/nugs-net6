# Regex Patterns

#### Env reader

Gets right and left parts of an `.env` files contents.

```php
(?<Left>\w+) # Match alphas and underscore
        =?                  # Match only the first equals sign
(?<Right>.*)        # Match anything
```

#### Weather Forecast

Gets the Microsoft dotnet API Weather Forecast DTO

```php
(?<Date>\d+\/\d+\/\d+\s*(\d+:\d+(:\d+)?\s*(PM|AM)?)?)
\s*,                        # Extracts each piece of the date. 
                            # ? - means one or none of the preceding symbol (e.g. we can only have either AM or PM, or neither one).

(?<TemperatureC>-?\d{1,3}(\.\d+)?)  # Extracts 1-3 digits as the temperature.

\s*,

(?<TemperatureF>-?\d{1,3}(\.\d+)?)  # Extracts 1-3 digits as the temperature.

\s*,                        # matches space & comma

(?<Summary>\w+)             # Extracts any word char or space
```


```php
# https://regex101.com/r/3EkgmM/1
^\|(?<name>\s*`?[\s\w]+`?\s*)\|(?<pattern>\s*`{3}.*`{3}\s*)\|(?<description>.*)\|$
```

Desired output...:

| Name         | Pattern | Description |
|--------------|:-----:|-----------:|
| Juicy Apples |  1.99 |        739 |
| Bananas      |  1.89 |          6 |
| `non-sargable SQL`      |  `(ON|JOIN|WHERE)\s+(AND|OR)?(\s*?.*)(LIKE\s*'%.*%'|([a-zA-Z]*?\([\w\s\.@]+\)))` |          Finds any non-SARGable phrases in a SQL query |
| `name`      |  ```(?<Summary>\w+) ``` |          `Extracts any word char or space` |



