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

## IndexedDB
* https://www.syncfusion.com/faq/blazor/general/how-do-i-use-indexeddb-in-blazor-webassembly
* https://blog.stevensanderson.com/2019/08/03/blazor-indexeddb/
* https://getbootstrap.com/docs/4.1/layout/grid/
* https://blazorschool.com/tutorial/blazor-wasm/dotnet6/indexeddb-storage-749869
* https://web.dev/indexeddb/
* https://developer.mozilla.org/en-US/docs/Web/API/IDBObjectStore/put
* https://developer.mozilla.org/en-US/docs/Web/API/IndexedDB_API

## Stuff
* https://icons.getbootstrap.com/
* https://www.strathweb.com/2019/06/building-a-c-interactive-shell-in-a-browser-with-blazor-webassembly-and-roslyn/
* https://github.com/filipw/Strathweb.Samples.BlazorCSharpInteractive
* https://github.com/AlanLynn/Web-Terminal

## AADB2C
* https://devkimchi.com/2022/09/23/authn-ing-blazor-wasm-with-azure-ad-b2c/
* https://dev.to/425show/secure-asp-net-blazor-wasm-apps-and-apis-with-azure-ad-b2c-474g
* https://stackoverflow.com/questions/64866476/how-to-secure-an-azure-function-accessed-by-a-blazor-wasm-app-with-azure-ad-b2c