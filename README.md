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
* dotnet new razorcomponent -n <page_name>

## IndexedDB
* https://www.syncfusion.com/faq/blazor/general/how-do-i-use-indexeddb-in-blazor-webassembly
* https://blog.stevensanderson.com/2019/08/03/blazor-indexeddb/
* https://getbootstrap.com/docs/4.1/layout/grid/
* https://blazorschool.com/tutorial/blazor-wasm/dotnet6/indexeddb-storage-749869
* https://web.dev/indexeddb/

## Stuff
* https://icons.getbootstrap.com/
