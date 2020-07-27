# Using .NET Core Tools to Create Reusable and Shareable Tools & Apps

![](images/banner.png)

Starting with .NET Core 2.1 Microsoft introduced the **Dotnet Tools** platform as part of the .NET Core SDK and since then these tools have become a vital although underutilized part of the .NET Eco system.

Dotnet Tools are a simple way to create, publish and consume what are essentially .NET Core ~~Console~~ applications that can be published and shared using the existing NuGet infrastructure for packaging and distribution. This means it's really quick and easy to build tools that you can share either publicly or privately. 

## What is a Dotnet Tool?
When you break down a Dotnet Tool to its bare bones, it all resolves to this simple statement:

> **“A Dotnet Tool is a glorified.NET Core ~~Console~~ Application that can be easily shared and installed via NuGet.”**
>
> *Me, 2020*

The idea of a Dotnet Tool is to make it easy to build, publish and consume executable tools in the same way you've been able create NuGet packages for .NET Core components in the past.

And while the original idea was to build tools to aid as part of the build and development process, this platform really offers much more scope because essentially **you can publish and share any .NET Core executable application**. This includes servers that run full ASP.NET Core applications or services as well as .NET Core desktop applications.

### Why use Dotnet Tools?
The big selling points of Dotnet Tools are:

* You can **build** a tool with a simple flag in your .NET Project file
* Tools are **cross platform** without requiring platform specific binaries    
<small>(Console applications only, althogh you can also deploy WinForms/WPF apps as Dotnet Tools that are Windows specific)</small>
* You can **publish** the tool to NuGet like any other NuGet Package
* There's **no package approval process**
* You can **install** a tool via `Dotnet Tool install`
* You can **update** and **uninstall** Dotnet Tools quickly

On the flip side there is the pre-requisite:

* The **.NET Core SDK is required** to install a Dotnet Tool

The SDK dependence is both a blessing and a curse: Because it's guaranteed that the .NET Core Runtime is installed, binaries can be very small and only consist of application specific files. 

But the .NET SDK is something that has to be installed and the SDK install is neither small, nor something that a typical, non development user will have installed. So either it has to be explicitly installed or you are explicitly targeting (.NET) developers with your tool.

While the idea behind Dotnet Tools isn't anything new, this tooling does provide a number of benefits to developers and the entire .NET Core eco system.

**Easy to build**

* Uses standard .NET Core Projects
* Uses existing NuGet Infrastructure
* Nothing new to learn - works with existing tech

**Sharing**

* Can be easily shared
* Can reach a large number of users
* Quickly published without extensive validation

**Community**

* Ease of use and shareability promotes creation of tools
* Shared content helps build community

### Security: Caveat Emptor
Since Dotnet Tools are executables, are installed from a remote source and there is no validation process, it's important to understand that there is a potentially substantial security risk to your machine. The code that comes down can execute locally on your system and s**o has access to your local resources when you run a tool**.   
  
> Be sure you trust the publisher of the component, and/or verify that the source code is available somewhere to review. 

Also check the repository for comments. I wouldn't advise running tools that don't publish their source code or aren't from a publisher you otherwise highly trust. 

**Be careful and know the risks!**

### There's nothing new under the Sun!

> ***“Did you just describe NPM?”***  
> Yup: Dotnet Tool is very much like NPM  for .NET Core.

If all this looks familiar from NodeJs and NPM - you're right. The idea is not new by any means, but certainly for .NET to be able to easily publish and share binary executable tools in a cross-platform manner is something new and exciting. 





### What can you use Dotnet Tools For?
Although .NET Tools were initially designed to provide development time tools, compilation helpers, build tools and other things that you might need for the development process, it turns out that Dotnet Tools are basically just .NET Core executables and can do - well anything that a .NET Core executable can do. This means you can use it for all sorts of things that might not be directly developer related.

Built a small utility? Need to have an easy way for people to install and run it? Or simply share it amongst your team or maybe even with yourself on multiple machines? A Dotnet Tool requires minimum effort and no long-winded, store-like validation, so once you publish your tool will be avaible in a few minutes to consume.

Here are a few general use cases addressed by Dotnet Tools:

* **Build and Dev Time Tools**  
There are many tools that follow the original design goal for creating project helpers that make development tasks easier or facilitate external but related development operations. For example, tools like EF migration commands in `dotnet ef`, `dotnet watch run`, and the user secrets manager are all Dotnet Tools that fit this bill. There are many tools available in this category.

* **Generic Command Line Tools**  
If you need to build some complex commandline helpers that work on scenarios that are maybe somewhat more complex than what you reasonably want to do in Powershell or Bash, a tool can fit that niche nicely. Because these tools can be shared and installed easily and are generally very small they make a good fit for *'beyond scripting'* scenarios.

* **Local Servers**  
.NET Core makes it easy to build server applications and it's easy to create self-contained Web Server or Services applications. Whether it's running a Web application locally for testing, or whether you have some internal application that maybe is a hybrid that uses both a Web interface and a desktop application in mixed mode, a Dotnet Tool makes it easy to provide this. It's very powerful to be able to create **small** and easily sharable, self-contained Web and server applications. I'll show a couple of examples of this later.

* **Desktop Applications**  
Although Microsoft's official documentation claims that `Dotnet Tool` is meant for Console Applications, it turns out that you **can** also create desktop applications and share them as a tool. You can create .NET Core WinForms and WPF applications and package them as tools too. Keep in mind though that WinForms and WPF applications are platform dependent and have to run on Windows, unlike pure Console applications which can run on any supported .NET Core platform assuming they are not using platform specific features.

#### Finding Dotnet Tools
Unfortunately, finding all .NET Tools that are available is not so obvious. Puzzlingly the [NuGet Web site](https://nuget.org) doesn't allow any filtering in searches to return just Dotnet Tools.

There are a few other ways that you can check though:

* Nate McMaster has a GitHub repo with a [list of many Dotnet Tools](https://github.com/natemcmaster/dotnet-tools)
* The [ToolGet Site](https://www.toolget.net/tools?q=LiveReloadServer) searches NuGet with a Dotnet Tool filter!

### Dotnet Tool or Platform Specific Binary?
As you probably know .NET Core supports creating binaries for every platform that it supports. You can create standalone executables for each platforms using either runtime dependent installation that requires that a specific version of .NET Core pre-exists, or a fully self-contained executable that can contain all the required runtime files in addition to the files that your application needs to run.

The advantage of building a .NET Tool over building a full .NET binary (EXE on Windows for example) is that  a Dotnet Tool is **not platform specific**. Like a NuGet component package, the executable is actually just the compiled binary .NET assemblies packaged into a Nuget zip file. This file contains the .NET executable code, that is not tied to any platform - ie. there's no platform specific binary loader as you would have with an EXE file on Windows for example.

This means you can build a single, relatively small NuGet package as a Dotnet Tool and it will work on each supported .NET Core platform - assuming your code is written to otherwise use platform agnostic features.

That's cool: It provides you cross platform functionality without having to build and maintain any platform specific loaders and it keeps the deployment size very lean.

That said, having a full self-contained application that doesn't require a .NET Core SDK install is also useful and sometimes required, especially for non-developer related tools. So if you're building developer tools the `Dotnet Tool` experience is just fine and probably preferable. But if you're building some sort of end user tool or application, then a self-contained application might be a better call.

## Creating and using Dotnet Tools
Ok, time to look and see how we can:

* Build a Dotnet Tool package for distribution
* Publish a Dotnet Tool package
* Consume a Dotnet Tool package

### Creating a Dotnet Tool Package
A Dotnet Tool is created by creating an executable .NET Core Project. You can create a Console or WPF/WinForms application and as long as the project produces an executable, it can be delivered as a Dotnet Tool.

To mark a project as a Dotnet Tool you use a special `<PackAsTool>` element in the project file:

```xml
<PropertyGroup>
  <PackAsTool>true</PackAsTool>
  <PackageId>dotnet-htmlpackager</PackageId>
  <ToolCommandName>htmlpackager</ToolCommandName>
  
  <PackageOutputPath>./nupkg</PackageOutputPath>
  <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
</PropertyGroup>
```

The key item is the `<PackAsTool>` element which marks the NuGet package as a Dotnet Tool. When it shows up in the NuGet package store it'll show up as a Dotnet Tool:

![](images/DotnetToolOnNuGet.png)

### Publishing a Dotnet Tool
.NET Tools are published as NuGet packages and use the same exact mechanism you might use to publish a NuGet package for a regular .NET Component. 

If you're new to publishing I recommend you grab a copy of the the [NuGet Package Explorer](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer) which is a UI application that lets you review a package:

![](images/NugetPackageExplorer.png)

The Explorer lets you see exactly what your package contains, lets you edit the package attributes and includes functionality to let you sign a package and publish it to NuGet.

If you'd rather use some sort of build script to do it you can use `Nuget.exe` with mostly generic script:

```powershell
$signingCertificateSubject = "West Wind Technologies"

if (test-path ./nupkg) {
    remove-item ./nupkg -Force -Recurse
}   

dotnet build -c Release

# find latest .nupkg file
$filename = gci "./nupkg" | sort LastWriteTime | select -last 1 | select -ExpandProperty "Name"
Write-host $filename

$len = $filename.length
if ($len -gt 0) {
    Write-Host "signing..."
    nuget sign  ".\nupkg\$filename"  `
                -CertificateSubject $signingCertificateSubject `
                -timestamper "http://timestamp.comodoca.com"
    nuget push  ".\nupkg\$filename" -source nuget.org
}
```

For the `nuget push` to work you'll need to set the active NuGet publishing Id which you can do by once running:

```ps
nuget setApiKey <your_API_key>   
```

Once published, NuGet packages become live relatively quickly - typically it just takes a few minutes before you can access a new package. However the feed listings tend to take a bit longer, so you may have to explicitly specify a version number using the `--version` flag.

### Installing a Dotnet Tool
To install Dotnet Tool you use a built-in .NET SDK command: `Dotnet Tool`. To install a tool use its package Id:

```ps
Dotnet Tool install -g LiveReloadServer
```

The `-g` makes the component **global**, which means it will be available on the OS path and can be executed without specifying a location. Most tools are globally installed, but you can also create local tools that become specific to your project folder.

You can also use the `update` command which works both to update a package to the latest version (or a specific version) as well as installing the tool if it's not already installed. Because it handles both **I usually use `update`** instead of `install`:

```ps
Dotnet Tool update -g LiveReloadServer
```

### Running the Dotnet Tool
To then run the installed Dotnet Tool you use the `toolname` that is defined for that server:

```ps
LiveReloadServer --webroot /webconnectionprojects/vue/web `
                 --UseRazor --UseMarkdown
```

Here's what this looks like:

![](images/LiveReloadServerCommandLineInstallAndRun.png)

> **@icon-info-circle Note:**  
If you're running on a non-Windows environment the tool name you use from the command line **is case sensitive** and has to match what you see for the *Command Name* when you do `Dotnet Tool list`.

### How a Tool gets Executed
Dotnet Tools are deployed as .NET NuGet packages that don't include an OS executable binary. Rather, when a tool is installed,the tooling creates a small proxy loader executable that lives in a folder mapped in the OS path:

![](images/DotnetToolsProxyFolder.png)

The `.store` path below this folder holds the actual unpacked NuGet package content for each tool. When you run the proxy via the dotnet net tool command, the proxy starts and loads up the actual .NET startup assembly and the `static void Main()` entry point. The `.exe` you see in the screen shot **is only a loader** not the actual binary - if you try to hook up an IL decompiler you'll find that the exe is a native binary, not a .NET assembly.

![](images/NotADotnetAssembly.png)

Because this wrapper is on the path, it can be executed and each custom loader knows where it can find the appropriate component. This approach is common with package managers - you'll find a similar mechanism for the [Chocolatey Package Manager](https://chocolatey.org) for Windows for example.

### Running on another Platform: Linux with WSL
Dotnet Tools are platform agnostic. Assuming your application doesn't use any platform specific features in its internal code you can run the same Dotnet Tool that works on Windows on a Mac or on Linux.



### Listing and Managing Installed Tools
You can check what tools you have installed by using the `Dotnet Tool list -g` command:

![](images/DotnetToolList.png)

This lists both the **Package Id** which is the Install name used with NuGet and the **Command** name which is used to invoke the package. Note that these can be different. Again keep in mind that the command name is **case sensitive** on case sensitive operating systems.

You can keep tools up to date using the `update` command:

```ps
Dotnet Tool update -g LiveReloadServer
```

and you can easily uninstall tools with `uninstall`:

```ps
Dotnet Tool uninstall -g LiveReloadServer
```

As you can see it's pretty easy to create, publish, consume and manage Dotnet Tools...

## Example Components
In this section I'd like to briefly describe a few of the Dotnet Tools I've created and discuss the how's and more pertinently the why's. I've been huge fan of these tools because it is so frictionless to get a tool published. If it wasn't for the easy of sharing via NuGet I probably wouldn't have bothered sharing these tools at all.

Some of these tools I built mainly for myself, but since I've made them public they ended up getting used by quite a few other people. Maybe you'll find some of these useful as well. But more importantly I hope it inspires you to share your own tools no matter how silly or simple - somebody might find them useful too!

At this point if I need to build something that requires a Console application that isn't part of some other bigger product, I tend to default to package and ship it as a .NET tool.

### Simple Tools 
The first couple of examples are your typical utility tools that under normal circumstances I would have distributed as a downloadable installer or just a standalone binary. If a tool is popular enough I still go that route **in addition to the Dotnet Tool**, but for me personally I tend to use the .NET tool rather than a standalone installed application.


#### Visual Studio Snippet Converter
This tool is a narrow use case utility tool that converts Visual Studio Code Snippets (code expansions) into:

* Visual Studio Code Snippets
* JetBrains Rider Snippets

This is obviously very targeted and specifically targeted at developers who are already using Visual Studio and so are **very likely** to have the .NET SDK installed just by virtue of using Visual Studio. So as a tool this is perfect and it fits perfect into the **Dotnet Project Tooling** use case I pointed out as one of the original design goals by Microsoft.

The background behind this tool is that I have a crap load of Visual Studio code snippets that help me quickly format blocks of code - from inserting properties, but creating entire blocks of classes that have complex signatures. Snippets are incredibly useful and unfortunately another very much underutilized feature in Visual Studio (and other tools).

Now, I use a number of different tools - in addition to Visual Studio I use Rider for .NET development especially on other platforms and also Visual Studio Code especially for front end development. 

So this tool lets me export my previous snippets into these other platforms and take advantage of them there as well. In addition - I've taken to using Visual Studio as my Master Snippet repository when I create or update snippets and then export them back out to the other tools which allows me to keep the snippets in sync.

To use this tool:

```ps
Dotnet Tool install -g snippetconverter
```

For options just run it:

![](images/SnippetConverterSyntax.png)

Then to run the converter you can specify a source snippet or folder and an output path:


```powershell
snippetconverter "~\Visual C#\My Code Snippets" -o "~\ww-csharp.code-snippets" -r -d
```

To make the snippet location easier to use the tool lets you use `~` for the default snippet folders. For Visual Studio you can specify 

![](images/SnippetConverterExportedSnippets.png)

#### HtmlPackager
HtmlPackager is another very use case specific tool that I built some time ago to package up HTML content from a URL and package it into either a fully self-contained 'package' bringing all the content offline. The 'package' output can be either a single very large HTML file internalizes all dependencies inline of the document, an HTML document with a folder full of dependencies, or a Zip file an HTML plus it's dependencies.

I needed this functionality at the time for my [Markdown Monster](https://markdownmonster.west-wind.com) editor, and was looking for a command line tool, but came up with nothing that worked and had a small footprint. As a result I ended up building my own .NET library that I used in Markdown Monster, but I also decided to provide it as a Dotnet Tool so it can be used in script files.

This tool is not directly related to Dotnet projects or the development process, but rather it's more of a general purpose tool and `Dotnet Tool` functionality just provides a very easy way to publish this tool.

The `dotnet-htmlpackager` tool can be installed with:

```ps
Dotnet Tool install -g dotnet-htmlpackager
```

Once installed you can run using the `help` command to see command line options:

![](images/HtmlPackagerHelp.png)


Then to run it you can do something like this:

```ps
htmlpackager  https://markdownmonster.west-wind.com -o /temp/captured/MarkdownMonster.html -v 
```

which creates a single, very large but fully self-contained HTML file:

![](images/HtmlPackagerRun.png)

There are other options for creating:

* A folder of loose HTML and resource files
* A zip file of the folder of loose HTML and resources

It's a very special use tool but I've found this to be very useful in a few scenarios where I needed to automate the process of capturing an HTML page.

Because this tool is very general purpose, it's also published via Chocolatey as a standalone tool:

```ps
choco install htmlpackager
```
### Servers
Because a .NET tool is basically just a .NET Core executable you can take advantage of just about any .NET Core feature - including the ability to create Web server applications and services that can run locally and be fully self contained. There are many use cases for this and here are two of them of how I've built and implemented as server.

#### A Generic Web Server
I frequently run local static Web sites on my machine where I need to access some client side development site or if I need to do some quick fixes to some HTML or client side content and just push it back up to a server.

In order to do this efficiently I can spin up a full development environment or I can just quickly run a local development server and run the application. The `LiveReloadServer` tool I created is just that - a quick and dirty .NET Core based static Web Server that can serve local content that includes:

* Local Static Web Content
* Loose Razor Pages (optionally)
* Markdown Pages

In addition it also provides what for me is the most useful feature:

* Live Reload Functionality (optionally)

The live reload functionality is built-in and enabled by default and automatically refreshes any active HTML pages loaded through the server when an HTML page or any other related resources like CSS, JS etc. plus any extensions you've added are changed. 

The server is quick and easy to install and run.

This is not a new idea obviously. There have been NodeJs based servers like [http-server](https://github.com/http-party/http-server#readme) or [browser-sync](https://www.browsersync.io/) forever, but these tools require NodeJs and - for browser-sync at least - it doesn't work as quickly or nicely as I would like. LiveReloadServer is an alternative and it's .NET Core based.


To use Live Reload Server:

```ps
Dotnet Tool install -g LiveReloadServer
```

To run it you simply point it at a `--WebRoot` folder on your local machine and that site then becomes a Web Site

![](images/LiveReloadServerRunning.png)

Live Reload is enabled by default so you can edit static text files and if you have Razor Pages and/or Markdown Page processing enabled you can also edit Razor and Markdown pages and see those pages refreshed.

This is very useful Dotnet Tool and I use it almost daily. It's also useful to have easy access to when you need to quickly launch a local Web server to check out a folder of files.

The server can run both in local publishing mode but can also be accessed externally.

#### A Legacy Web and Application Server 
The final example is for a legacy application server tool that is ancient and has recently been updated to .NET Core. I've been involved with - wait for it - a FoxPro legacy product that I maintain and still have a hundreds of users on - for over nearly 25 years now. This is ultra legacy, but because the technology I created many years ago that extended FoxPro functionality to build Web applications there are still a large number of existing applications and even to date new applications that use huge FoxPro code bases to hook up to the Web.

The tool is called Web Connection and it has for many years used .NET to provide the interface between a Web Server (IIS in the past) to FoxPro.

When .NET Core came out it offered a number of new opportunities to extend the middleware tooling by:

* Providing a fully self-contained development server
* Providing a nearly zero configuration environment
* Ability to run the server component on non-Windows machines
* Ability to run a local machine or network server without requiring IIS

Long story short - a Dotnet Tool turned out to be great delivery mechanism for someone who for example, needs to run an old application that was built with Web Connection, but doesn't have access or can't install IIS on a machine and most of all doesn't want to set up and configure IIS

This sounds weird, but this is actually a very common scenario for me: A user calls and says they have a Web Connection application that was developed 15 years ago, the developer left and they need to run the application and perhaps make a few changes. Setting up an environment in the past was not the easiest thing in the world. With this new component I can whittle that down to:

* Install the .NET Core SDK
* Install  `Dotnet Tool install -g WebConnection`
* Point at a Web Connection folder and go

![](images/WebConnectionServer.png)

Again, a very specific use case, but a Dotnet Tool here offers a smooth solution to distribute a server that can then be used to serve an ancient legacy application without complex configuration or special installations required.

To be clear though - this is not the only distribution mechanism for this server. A more recent installation of this tool will install a local .NET Core server with each application that travels with the application. This is not a .NET Tool but is essentially **the very same server implementation**  but just packaged as a .NET Core shared runtime application. Besides being local to the project it also offers the ability to host the server in IIS or on a Linux box.

Legacy technology is always an eye roller, and I doubt any of you will use this technology :smile:, but it makes for an interesting use case of the Dotnet Tool functionality.

## Summary
Dotnet Tools provide a great way to share executable code from .NET Core in an easy and  platform independent way. If you've built a NuGet package and published it before you already know how to build and share a Dotnet Tool.

They are easy to consume which gives access to a variety existing tools and because they are so easy to share promote a community of tools to be created. 

Hopefully this session gives you some ideas of tools that you might want to use, or better yet - create and share with the communicate or even just within your organization.

Rock on!

## Resources
* [Nate McMaster's Tool List](https://github.com/natemcmaster/dotnet-tools)
* [ToolGet Tool Package Search](https://www.toolget.net/)
* [NuGet Package Explorer](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer)

## Dotnet Tool Projects
* [LiveReloadServer](https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload/blob/master/LiveReloadServer%2FREADME.md)
* [HtmlPackager](https://github.com/RickStrahl/Westwind.HtmlPackager)
* [Visual Studio Snippet Converter](https://github.com/RickStrahl/VisualStudioSnippetConverter)



