# kwik-log-pwa

* dotnet new blazorwasm -o pwa --pwa
* dotnet new classlib -o common
* Add common project to other projects:
```xml
  <ItemGroup>
    <ProjectReference Include="..\common\common.csproj" />
  </ItemGroup>
```
* add ==> "url": "http://localhost:5xxx" ==> launch.json
* 