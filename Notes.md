### Html Packager

```ps
dotnet tool update -g dotnet-htmlpackager --add-source ./nupkg
```

```ps
htmlpackager  https://hanselman.com -o /temp/captured/captured.html -d -v
```

### Snippet Converter  (C# Snippets)

```ps
snippetconverter "~\Visual C#\My Code Snippets" -o "~\ww-csharp.code-snippets" -r -d
```

```ps
snippetconverter "~" -o "~\ww-all.code-snippets" -r -d
```

### Live Reload Server

```powershell
livereloadserver --webroot /webconnectionprojects/vue/web
```

```ps
livereloadserver --webroot C:\projects\Westwind.AspnetCore.LiveReload\Samples\StandaloneFiles
```

#### Web Connection Web Server

```ps
webconnectionwebserver /webroot /webconnectionprojects/wwthreads/web
```