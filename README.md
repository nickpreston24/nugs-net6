# Goals

1. Aid the average civilian consumer in making decisions for their Loadouts.
2. Make sourcing hard to find materials easier by scanning TacSwap, Gunbroker and others
3. Prevent accidental "violations" to keep civilians safe as possible from tyranny.
4. ...

## Upgrading the app to a Desktop App

For Issues with SDKs or Runtimes not running, try this:

https://github.com/theolivenbaum/Electron.NET/

Or, try this:

https://github.com/ElectronNET/Electron.NET/issues/704

## Knowledge Transfer

> This custom web Stack aims to do two things: Maximize the use of HTMX and make UI much easier and more fun to work with in the emerging `dotnet core` Linux era.

I.  Essentially, I'm supporting the generation of HTML directly from as many backends or API's as possible, without needing Javascript Framework or complicated squeaky toys like Microsoft's Blazor.

Just simple, old-school HTML, CSS and the occasional line of Javascript or C#.

II. On the backend side of things, I've added support for `.sql` and `.cypher` files as first class citizens.  They can be easily imported by name, or even mapped to Razor CRUD functions by name.  This makes the following 3 sample codefiles incredibly easy to maintain for all team members of all types:

``` sql
-- Gets all Parts with Colt as the manufacturer:
SELECT * FROM mynugsdatabase.Parts part
WHERE part.Manufacturer LIKE '%Colt%'
-- .. etc.

```

``` cypher
# A specific User (node) is updated to "Like" a particular Build (another node), thereby forming a direct relationship which can be queried.  

# Returns BOTH the build liked and the User as JSON.
MERGE (user1:User { name: $userName })
MERGE (build1:Build { name: $buildName })
MERGE (user1)-[:LIKES]->(build1)
RETURN user1, build1
```

## TODOS / Ideas

https://github.com/chartjs/awesome

## Issues
