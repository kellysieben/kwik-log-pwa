# kwik-log-pwa

* dotnet new blazorwasm -o app --pwa
* dotnet new classlib -o common
* Add common project to other projects:
```xml
  <ItemGroup>
    <ProjectReference Include="..\common\common.csproj" />
  </ItemGroup>
```
* add ==> "url": "http://localhost:5xxx" ==> launch.json
* dotnet new razorcomponent -n <page_name>

## Stuff
* https://icons.getbootstrap.com/
