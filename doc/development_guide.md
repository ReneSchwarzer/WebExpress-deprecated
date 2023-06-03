![WebExpress logo](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/icon.png)

# WebExpress
WebExpress is a lightweight web server that has been optimized for use in low-performance 
environments. Even on small systems, such as the Raspberry PI, web applications can be 
operated efficiently. This is achieved through a small footprint with a low resource burden. 
Furthermore, WebExpress has a powerful and optimized plugin system, with a comprehensive API 
and application templates. This allows web applications to be easily and quickly integrated 
into a .Net language (e.g. C#).

# License
The software is freely available as open source (MIT). The software sources can be obtained 
from https://github.com/ReneSchwarzer/WebExpress. WebExpress is based on components that are 
available as open source:

- https://github.com/dotnet/core (MIT)
- https://getbootstrap.com/ (MIT)
- https://www.chartjs.org (MIT)
- https://jquery.com/ (MIT)
- https://summernote.org/ (MIT)
- https://popper.js.org/ (MIT)
- https://github.com/kurtobando/simple-tags (MIT)
- https://github.com/uxsolutions/bootstrap-datepicker (Apache 2.0)

```
The MIT License (MIT)

Copyright (c) 2023 René Schwarzer

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

# Architecture
WebExpress is deliberately kept very simple. It consists only of basic functionalities 
for processing HTTP and HTTPS requests, an API and a plugin system for extending the 
functionalities. This means that WebExpress itself is not able to generate content. 
The plugin system is required for this. Plugins are .Net assemblies, which create 
content based on the WebExpress API. The plugins are loaded and executed by WebExpress. 
WebExpress controls the plugins and distributes the http(s) requests to the responsible 
plugin. The plugins answer the requests, create the content and transfer it to WebExpress. 
Finally, the content is delivered as an HTTP response via WebExpress. WebExpress uses 
Kestrel to process http(s) requests.

![WebExpress bigpicture](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/bigpicture.svg)

In order to be able to easily extend WebExpress, it is split into several program libraries. 
The ```WebExpress.dll``` program library is global and is used as a basis in other projects. It 
provides basic functions for creating content and additional functions (e.g. logging). The 
```WebExpress.UI.dll``` and ```WebExpress.WebApp.dll``` packages provide controls and templates that 
facilitate the development of (business) applications. The ```WebExpress.App.exe``` program library 
represents the application that takes over the control of the individual functions and 
components. The ```WebExpress.App.exe``` program library is generic and can be replaced by its 
own program library.

![WebExpress packages](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/assets/packages.svg)

In the context of WebExpress, (web) applications are deployed. An application is the logical 
combination of modules. Modules, in turn, are amalgamations of (web) elements. Elements reflect 
content (e.g. web pages). The relationships between WebExpress, packages, applications, modules, 
and elements are illustrated in the following figure: 

![WebExpress architecture](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/architecture.svg)

## Component model
The components of WebExpress and its applications are centrally managed in the ```ComponentManager```.
The following components are available in WebExpress:

|Component                   |Description
|----------------------------|-----------------------
|PackageManager              |Management of packages that extend the functionality of WebExpress.
|PluginManager               |Management of extension modules that extend the functionality of WebExpress.
|ApplicationManager          |An application is the logical combination of functionalities into an application system.
|InternationalizationManager |Provides language packs for the internationalization of applications.
|ModuleManager               |Modules encapsulate (web) elements and make them available for one or more applications.
|IdentityManager             |Users or technical objects that are used for identity and access management.
|ResourceManager             |Resources are contents that are delivered by WebExpress. These include, for example, websites that consist of HTML source code, arbitrary files (e.g. css, JavaScript, images) and REST interfaces, which are mainly used for communication via HTTP(S) with (other) systems.
|ResponseManagers            |Represent HTML pages that are returned with a StatusCode other than 200.
|LayoutManager               |Provides color and layout schemes for customizing applications.
|FragmentManagers            |Are program parts that are integrated into defined areas of pages. The components extend the functionality or appearance of the page.
|SchedulerManager            |Jobs can be used for cyclic processing of tasks.  
|TaskManager                 |Management of ad-hoc tasks.  

In addition, you can create your own components and register them in the ```ComponentManager```.

![WebExpress componemtmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/componemtmodel.svg)

## Package model
WebExpress is designed by its open and modular plugin system, which supports many usage scenarios. The 
distribution of the plugins and other software components (e.g. Entity Framework) takes place as 
WebExpress packages. WebExpress is able to read these packets and execute the code in them. Packages 
can contain both managed code and native libraries (e.g. for Linux) and be dependent on other packages. 
The recursive resolution of the dependencies is done by WebExpress. 
The WebExpress packages are ZIP-compressed files that can provide libraries for multiple platforms. They 
have the `wxp` file extension. A WebExpress package has the following structure:

![WebExpress packagestructure](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/packagestructure.svg)

|Directory/ File  |Description
|-----------------|-------------------
|assets           |Media files, which are needed for the description of the package.
|lib              |This directory contains the libraries.
|runtimes         |Contains the platform-dependent libraries.
|rid              |A runtime identifier (RID) of the supported runtime (see .NET Runtime Identifier (RID) catalog). Each supported runtime is created in its own directory.
|licences         |Storage location of all third-party licenses and your own license.
|readme.md        |The description of the package contents for the user.
|packagename.spec |The specification of the package.

The packages are versioned and can assume the following states:

![WebExpress packagestate](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/packagestate.svg)

- **Available** - The package is available, but not yet loaded by the WebExpress. 
- **Active** - The package has been loaded and is ready for use. 
- **Disable** - The package has been disabled. The use of the package is not possible.

The ```PackageManager``` is responsible for provisioning the packages. This has the task of loading all 
packages and deactivating or removing them if desired. The following directories are used to 
store the packages and libraries: 

![WebExpress packagedirectories](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/packagedirectories.svg)

|Directory/ File |Description
|----------------|-----------------------
|packages        |The home directory that contains the catalog and packages.
|package         |Each active package is unpacked in a separate directory. This directory contains the libraries of the WebExpress packages for the installed framework and platform.
|catalog.xml     |The catalog.xml file collects all metadata (including the package state) of the installed packages.
|package.wxp     |Each installed package is saved unpacked for future actions.

New packages can be installed on the fly by copying them into the packages directory by the user. The provisioning 
service cyclically scans the directory for new packets and loads them. 
If a package is to be deactivated without removing it, the `PackageManager` notes it in the catalog (state ```Disable```). 
In addition package, the directory of the deactivated package is deleted and all contents (applications, modules, elements) 
are removed from the running WebExpress. When WebExpress boots up and initializes, the catalog is read and the 
disabled packages are excluded. A disabled package is activated by changing the state in the catalog and unpacking and 
loading the package into the package directory. When a package is deleted, it is removed from the package directory and 
from the catalog. The `PackageManager` manages the catalog. This can be accessed at runtime via the following classes.

![WebExpress packagemodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/packagemodel.svg)

## Plugin model
The plugin system can be used to extend both WebExpress and application functionalities. Each plugin can provide content in 
different forms. A distinction is made between the following types of content:

|Content                      |Managed by                  |Description
|-----------------------------|----------------------------|-----------------------
|Applications                 |ApplicationManager          |An application is the logical combination of functionalities into an application system.
|Internationalization schemas |InternationalizationManager |Provides language packs for the internationalization of applications.
|Module                       |ModuleManager               |Modules encapsulate (web) elements and make them available for one or more applications.
|Identities                   |IdentityManager             |Users or technical objects that are used for identity and access management.
|Resources                    |ResourceManager             |Resources are contents that are delivered by WebExpress. These include, for example, websites that consist of HTML source code, arbitrary files (e.g. css, JavaScript, images) and REST interfaces, which are mainly used for communication via HTTP(S) with (other) systems.
|Status pages                 |ResponseManager             |Represent HTML pages that are returned with a StatusCode other than 200.
|Layout schemes               |LayoutManager               |Provides color and layout schemes for customizing applications.
|Components                   |ComponentManager            |Are program parts that are integrated into defined areas of pages. The components extend the functionality or appearance of the page.
|Jobs                         |SchedulerManager            |Jobs can be used for cyclic processing of tasks. 
|Tasks                        |TaskManager                 |Management of ad-hoc tasks. 

Each plugin must have a class `Plugin` that implements ```IPlugin```.

``` c#
[WebExName("myplugin")]
[WebExDescription("description")]
[WebExIcon("/assets/img/Logo.png")]
[WebExDependency("webexpress.webapp")]
public sealed class MyPlugin : IPlugin
{
}
```

The following attributes are available:

|Attribute        |Type   |Multiplicity |Optional |Description
|-----------------|-------|-------------|---------|--------------
|WebExId          |String |1            |Yes      |The unique identification key. If no ID is specified, the namespace is used. An ìd should only be specified in exceptional cases.
|WebExName        |String |1            |Yes      |The name of the plugin. This can be a key to internationalization.
|WebExDescription |String |1            |Yes      |The description of the plugin. This can be a key to internationalization.
|WebExIcon        |String |1            |Yes      |The icon that represents the plugin graphically.
|WebExDependency  |String |n            |Yes      |Defines a dependency on another plugin and is specified via the PluginId.

The implemented methods from the interface cover the life cycle of the plugin. Meta information about the plugin is 
stored in the `PluginContext` and is available globally via the ```PluginManager```.

![WebExpress pluginmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/pluginmodel.svg)

## Application model
Each plugin can provide one or more applications. To define an application, a class must be defined that implements the 
`IApplication` interface. The application's metadata is appended as attributes of the class.

``` c#
[WebExName("Application")]
[WebExDescription("example")]
[WebExIcon("/app.svg")]
[WebExContextPath("/app")]
[WebExAssetPath("/app")]
public sealed class MyApplication : IApplication
{
}
```

The following attributes are available:

|Attribute        |Type       |Multiplicity |Optional |Description
|-----------------|-----------|-------------|---------|------------
|WebExId          |String     |1            |Yes      |The unique identification key. If no ID is specified, the class name is used. An ID should only be specified in exceptional cases.
|WebExName        |String     |1            |Yes      |The name of the application. This can be a key to internationalization.
|WebExDescription |String     |1            |Yes      |The description of the application. This can be a key to internationalization.
|WebExIcon        |String     |1            |Yes      |The icon that represents the application graphically.
|WebExAssetPath   |String     |1            |Yes      |The path where the assets are stored. This file path is mounted in the asset path of the web server.
|WebExDataPath    |String     |1            |Yes      |The path where the data is stored. This file path is mounted in the data path of the web server.
|WebExContextPath |String     |1            |Yes      |The context path where the resources are stored. This path is mounted in the context path of the web server.
|WebOption        |String     |n            |Yes      |Includes resources that are marked as optional and are otherwise not directly integrated into the application. The name of the option is the ModuleId and the ResourceId (e.g. webexpress.webapp.settinglog) or webexpress.webapp.* if all options of a module are to be included. A regular expression can also be used.
| ^^              |Type       | ^^          | ^^      |The class of the module. All options from the module will be activated.
| ^^              |Type, Type | ^^          | ^^      |The class of the module and resource to be activated.

The methods implemented from the interface cover the life cycle of the application. When the plugin is loaded, all the 
applications it contains are instantiated. These remain in place until the plugin is unloaded. Meta information about 
the application is stored in the ```ApplicationContext``` and managed by the ```ApplicationManager```.

![WebExpress applicationmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/applicationmodel.svg)

## Module model
Each application can consist of one or more modules. To define a module, a class must be defined that implements the ```IModule``` 
interface. The module's metadata is appended as attributes of the class. A module has the task of organizing (web) elements 
for the application and making them accessible.

``` c#
[WebExName("MyModule")]
[WebExDescription("example")]
[WebExIcon("/mod.svg")]
[WebExContextPath("/mod")]
[WebExApplication<MyApplication>]
public sealed class MyModule : IModule
{
}
```

The following attributes are available:

|Attribute           |Type               |Multiplicity |Optional |Description
|--------------------|-------------------|-------------|---------|----------------
|WebExId             |String             |1            |Yes      |The unique identification key. If no ID is specified, the class name is used. An id should only be specified in exceptional cases.
|WebExName           |String             |1            |Yes      |The name of the module. This can be a key to internationalization.
|WebExDescription    |String             |1            |Yes      |The description of the module. This can be a key to internationalization.
|WebExIcon           |String             |1            |Yes      |The icon that represents the module graphically.
|WebExAssetPath      |String             |1            |Yes      |The path where the assets are stored. This path is mounted in the application's asset path.
|WebExDataPath       |String             |1            |Yes      |The path where the data is stored. This file path is mounted in the data path of the application.
|WebExContextPath    |String             |1            |Yes      |The context path where the resources are stored. This path is mounted in the context path of the application.
|WebExIdentityDomain |None, Local, Share |1            |Yes      |Determines the identity domain of the application. `None` - The application does not provide an identity domain. `Local` - The application has its own identity domain. `Share` - The application shares the identity domain with other applications.
|WebExApplication    |String             |n            |No       |A specific `ApplicationId`, regular expression, or * for any application. 
|                    |Type               |             |         |The class of the application.

The instance of the module is created when the plugin is loaded and persists until the application is unloaded. The methods 
implemented from the interface cover the life cycle of the module. Meta information about the module is stored in the ```ModuleContext``` 
and is available globally. The ```ModuleManager``` manages the modules. 

![WebExpress modulemodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/modulemodel.svg)

## Resource model
Resources are (web) elements that can be accessed with a URI (Uniform Resource Identifier). When a plugin is loaded, all classes marked 
as resources are automatically determined from the assembly and included in a sitemap. For this purpose, the affected classes are 
provided with attributes.

``` c#
[WebExSegment("E")]
[WebExContextPath("/C/D")]
[WebExModule<MyModule>]
[WebExScope<ScopeGeneral>]
[WebExAuthorization(Permission.RWX, IdentityRoleDefault.SystemAdministrator)]
[WebExAuthorization(Permission.R, IdentityRoleDefault.Everyone)]
public sealed class G : ResourcePage
{
}
```

The following attributes are available:

|Attribute            |Type              |Multiplicity |Optional |Description
|---------------------|------------------|-------------|---------|----------------
|WebExId              |String            |1            |Yes      |The unique identification key. If no id is specified, the class name is used. An id should only be specified in exceptional cases.
|WebExTitle           |String            |1            |Yes      |The name of the page. This can be an internationalization key.
|WebExSegment         |String, String    |1            |Yes      |The path segment of the resource. The first argument is the path segment. The second argument is the display string.
|WebExSegmentInt      |Parameter, String |1            |Yes      |A variable path segment of type `Int`.
|WebExSegmentGuid     |Parameter, String |1            |Yes      |A variable path segment of type `Guid`.
|WebExContextPath     |String            |1            |Yes      |The URI path from the module to the resource. The URI of the RSresource is composed of the `ContextPath` of the web server, the application, the module, the resource, and the segment.
|WebExParent          |IResource         |1            |Yes      |The resource is included below a parent resource. The context path is derived from that of the parent and the resource.
|WebExIncludeSubPaths |Bool              |1            |Yes      |Determines whether all resources below the specified path (including segment) are processed.
|WebExScope           |IScope            |n            |Yes      |The scope of the resource
|WebExModule          |IModule           |1            |No       |The class of the module. The module must be defined in the same plugin as the resource.
|WebExAuthorization   |Int, String       |n            |Yes      |Grants authority to a role (specifying the id) (see section notification model).
|WebExCondition       |ICondition        |n            |Yes      |Condition that must be met for the resource to be available.
|WebExCache           |-                 |1            |Yes      |Determines whether the resource is created once and reused each time it is called.
|WebExOptional        |-                 |1            |Yes      |Marks a resource as optional. It only becomes active if the option has been activated in the application.

Resources that are not identified by attributes can be registered manually in the sitemap.

``` c#
ResourceManager.Register<T>(id: "G", path: "/B/E") where T : IResource;
```

A cached resource is created on the first call and persists until the associated module is unloaded. The ```Initialize``` method is called once 
at instantiation, while the `Process` method is called each time the resource is requested. For non-cached resources, a new instance is 
created each time they are called.

![WebExpress sequencediagram](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/sequencediagram.svg)

The ```ResourceManager``` manages all resources. However, these are only accessible through the ```SitemapManager```. The interaction of the classes involved is illustrated 
in the following figure.

![WebExpress resourcemodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/resourcemodel.svg)

Resources, such as pages or assets, can be uniquely addressed with the help of URIs. The following resource types are supported:

|Resource type |Description
|--------------|-------------------------
|Page          |Dynamic web pages that consist of HTML.
|File          |Files from the file system.
|Asset         |Files from the assembly.

## Sitemap model
In a sitemap, all resources are listed with their URI. When a WebClient calls a resource, the associated resource is determined from the sitemap and returned to the 
caller. Only one resource can be associated with a URI. Multiple URIs, on the other hand, can point to a common resource. This comes into play, among other things, 
when the segment of the resource has dynamic components (e.g. described by regular expressions). Furthermore, a partial URI can refer to a resource.

![WebExpress sitemap](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/sitemap.svg)

The sitemap is implemented as a tree. Multiple paths to the same resource are resolved by creating a copy of the affected resource. For example, the URIs ```/B/E/G```, 
```/B/X/G```, and ```/C/D/G``` point to the same resource ```G```, where ```G = G'```.

Context paths can be specified in the configuration of WebExpress, the applications and the modules. The context paths are prefixed to the URIs. The following 
possible combinations exist:

|WebExpress |Application |Module | Resource | URI
|-----------|------------|-------|----------|----
|-          |-           |-      |/         |/
|-          |-           |-      |/a/b/c    |/a/b/c
|-          |-           |/      |/         |/
|-          |-           |/z     |/         |/z
|-          |-           |/z     |/a/b/c    |/z/a/b/c
|-          |/           |/      |/         |/
|-          |/           |/z     |/         |/z
|-          |/y          |/      |/         |/y
|-          |/y          |/z     |/         |/y/z
|-          |/y          |/      |/a/b/c    |/y/a/b/c
|-          |/y          |/z     |/a/b/c    |/y/z/a/b/c
|/          |/           |/      |/         |/
|/          |/           |/z     |/         |/z
|/          |/y          |/      |/         |/y
|/          |/y          |/z     |/         |/y/z
|/          |/           |/      |/a/b/c    |/a/b/c
|/          |/           |/z     |/a/b/c    |/z/a/b/c
|/          |/y          |/      |/a/b/c    |/y/a/b/c
|/          |/y          |/z     |/a/b/c    |/y/z/a/b/c
|/x         |/           |/      |/         |/x
|/x         |/           |/z     |/         |/x/z
|/x         |/y          |/      |/         |/x/y
|/x         |/y          |/z     |/         |/x/y/z
|/x         |/           |/      |/a/b/c    |/x/a/b/c
|/x         |/           |/z     |/a/b/c    |/x/z/a/b/c
|/x         |/y          |/      |/a/b/c    |/x/y/a/b/c
|/x         |/y          |/z     |/a/b/c    |/x/y/z/a/b/c

The insertion into the sitemap is done by sorting the number of URI segments in ascending order. Only one resource can be assigned per sitemap node. In a competing 
situation, the first resource is used. All other resources are not processed. This is indicated in the log by a warning message. 

Finding a resource starts at the root of the sitemap tree and follows the path of the URI. If no resource can be found, a 404 jam page is returned.

Parameters can be transferred to the resource to be executed in a URI or through form inputs. Furthermore, it is possible to store parameters in the session environment 
in order to make values available across pages. The parameters in the session are valid until the web server is restarted or the session is destroyed. The following 
parameters are supported:

|Origin       |Scope     |Description
|-------------|----------|-------------------------
|GET, DELETE  |Parameter |Parameter from the URI. Example: http://www.example.com?id=d9869404-6628-464b-8286-9685d4c4ff8b
|POST, PATCH  |Parameter |Parameter from the content part of the request. 
|Path segment |URI       |Parameters that are part of the URI path. Example: http://www.example.com/d9869404-6628-464b-8286-9685d4c4ff8b/edit
|Session      |Session   |Parameters, which are stored in the session. 

## Page modell
Web pages are resources that are rendered in an HTML tree before delivery. The ```ViualTree``` class, which is available in the ```RenderContext```, is responsible for the display of the page.

![WebExpress pagemodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/pagemodel.svg)

## Response modell
Web queries can be answered with different status responses (see RFC 2616). If successful, a status code of ```200``` is returned with the invoked resource. In the ```ResponseManager```, generally 
valid status pages for the various status codes can be stored. When returning a response that differs from ```200```, the stored status page is used. 

Status pages are primarily used from the plugin in which the associated application is implemented. Status pages implement the ```IStatusPage``` interface and derive from ```ResourcePage```. 

``` c#
[WebExStatusCode(500)]
public sealed class S : ResourcePage, IStatusPage
{
}
```

The following attributes are available:

|Attribute       |Type |Multiplicity |Optional |Description
|----------------|-----|-------------|---------|-------------
|WebExStatusCode |int  |1            |No       |The status code (see RFC 2616 para. 6). 

When creating a response that differs from status 200, the corresponding status page is determined from the ResponseManger and an instance is created. To do this, the following order 
is used to determine the status page:

- Search in the plugin of the called resource.
- Search in the plugin of the module of the called resource.
- Search in the plugin of the application of the called resource.
- Use the status pages from the plugin "webexpress.webapp".
- Use the system status pages.

![WebExpress statuspagemodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/statuspagemodel.svg)

If no status page is found in the current application, a default page is created and delivered by WebExpress.

## Internationalization model
The provision of multilingual applications for different cultures is supported by WebExpress. In addition, the following text formatting is also adapted to the corresponding culture:

|Text formatting |Description
|---------------|-----------------
|Date formats   |Use of the calendar format of the selected culture.
|Time formats   |Support between 24 and 12 hour counting.
|Time zones     |Support for time zones when displaying times.
|Number formats |Support the different representation of decimal and thousands separators, as well as different currencies, weights and measurements.

For the translation of texts, language translation files are used, which are stored in the packages under ```Internationalization```. 

![WebExpress internationalizationfiles](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/internationalizationfiles.png)

The data must be stored as embedded resources in the project file.

![WebExpress internationalizationproject](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/internationalizationproject.png)

The name of the language translation file must match the country code from ISO 3166 ALPHA-2. Each language translation file is structured as follows:

``` c#
# Comment
key=text fragment

e.g.
inventoryexpress.inventory.name.discription=The name of the inventory item
```

The translation of a text is done with the help of the InternationalizationManager, which provides the I18N function. 

``` c#
using static WebExpress.Internationalization.InternationalizationManager;

var text = I18N("de", "example", "name.discription"); Language, PluginId, Key
var text = I18N(culture, "PlginId:name.discription"); culture, pluginId:key
```

## Controls
Controls are units of the web page that are translated into HTML source code by rendering. A Web page consists of nested controls.

![WebExpress controls](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/controls.png)

### Form
A form takes user input and forwards it to the web server for processing.

![WebExpress formprocess](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/formprocess.png)

Form classes and associated form controls are available for entering data.

![WebExpress form](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/form.png)

The alignment of the form elements can be controlled with the help of the different form classes.

|Class                   |Description
|------------------------|-------------------------
|```ControlForm```       |A form in which the elements are arranged in several rows. ![WebExpress controlform](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/controlform.svg)
|```ControlFormInline``` |A form whose elements are arranged in one row. ![WebExpress controlforminline](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/controlforminline.svg)

### Form controls
Each form can hold multiple form controls. There are two different types of form controls:

- Controls with an informational or decorative character
- Controls for selecting or entering data

The arrangement of the form contents can be controlled by the ```ControlFormItemGroup``` classes:

|Arrangement      |Class                                      |Example
|-----------------|-------------------------------------------|-------------
|Vertical         |```ControlFormItemGroupVertical```         |![WebExpress groupvertical](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/groupvertical.svg)
|Horizontal       |```ControlFormItemGroupHorizontal```       |![WebExpress grouphorizontal](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/grouphorizontal.svg)
|Mix              |```ControlFormItemGroupMix```              |![WebExpress groupmix](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/groupmix.svg)
|ColumnVertical   |```ControlFormItemGroupColumnVertical```   |![WebExpress groupcolumnvertical](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/groupcolumnvertical.svg)
|ColumnHorizontal |```ControlFormItemGroupColumnHorizontal``` |![WebExpress groupcolumnhorizontal](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/groupcolumnhorizontal.svg)
|ColumnMix        |```ControlFormItemGroupColumnMix```        |![WebExpress groupcolumnmix](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/groupcolumnmix.svg)

## Fragment model
Fragments are components that can be integrated into pages to extend functionalities. Fragments can come from different sources (plugins). When a resource is loaded, the fragments 
stored in the sections are determined, instantiated and integrated into the resource. A section is a named area within a page (e.g. ```Property.Primary```).

![WebExpress fragmentmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/fragmentmodel.svg)

Fragments are derived from the ```IFragment``` interface and are identified by attributes:

``` c#
[WebExSection("Sektionsname")]
[WebExOrder(0)]
[WebExModule<MyModule>]
[WebExScope<ScopeGeneral>]
[WebExAuthorization(Permission.RW, IdentityRoleDefault.Authenticated)]
[WebExAuthorization(Permission.R, IdentityRoleDefault.Everyone)]
public sealed class C : IFragment
{
}
```

The following attributes are available:

|Attribute          |Type        |Multiplicity |Optional |Description
|-------------------|------------|-------------|---------|-----------------
|WebExId            |String      |1            |No       |The unique identification key. 
|WebExSection       |String      |1            |No       |The section of the Web page where the fragment is rendered.
|WebExOrder         |Int         |1            |Yes      |The order within the section. If no value is specified, the order "0" is set as the default.
|WebExModule        |IModule     |1            |No       |The class of the module. The module must be defined in the same plugin as the resource.
|WebExScope         |IScope      |n            |Yes      |The scope in which the fragment is valid.
|WebExAuthorization |Int, String |n            |Yes      |Grants authority to a role (specifying the id).       
|WebExCondition     |ICondition  |1            |Yes      |Condition that must be met for the fragment to be available.
|WebExCache         |-           |1            |Yes      |Determines whether the fragment is created once and reused each time it is called. This attribute is active only if the associated page also has the cache attribute. 

If the fragments are to be created dynamically at runtime, it is necessary to create a class that implements ```IFragmentDynamic```.

``` c#
[WebExSection("section name")]
[WebExModule<MyModule>]
[WebExScope<ScopeGeneral>]
public sealed class C : IFragmentDynamic
{
    public IEnumerable<T> Create<T>() where T : IControl
    {
        return …;
    }
}
```

In the ```Create``` method, the fragments are instantiated.

## Session model
A session establishes a state-based connection between the client and WebExpress using the otherwise stateless HTTP(S) protocol. The session is assigned to a cookie and 
is personalized. The cookie consists of a guid. Further data is not stored in the cookie, but on the server side in the ```session``` object. 

![WebExpress sessionmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/sessionmodel.svg)

The session manager delivers the currently used session based on the cookie stored in the request. The session, in turn, stores instances of the ```ISessionProperty``` 
interface in which the information (e.g. parameters) is stored. 

## Job modell
Jobs are tasks that are executed in a time-controlled and repetitive manner. When a plugin is loaded, all jobs containing it are determined by the ScheduleManager and 
instantiated and started at the specified execution time.

![WebExpress jobmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/jobmodel.svg)

A job is created by creating a class that inherits from Job.

``` c#
[WebExJob("30", "0", "1", "*", "*")] // The job starts at 0:30 a.m. on the first day of each month
[WebExModule<MyModule>]
public sealed class MyJob : Job
{
  public override void Initialization(JobContext context)
  {
    base. Initialization(context);
  }

  public override void Process()
  {
    base. Process();
  }
}
```

The following attributes are available:

|Attribute   |Type    |Multiplicity |Optional |Description
|------------|--------|-------------|---------|------------
|WebExJob    |String  |1            |No       |Time information about when the job should be executed. The parameters have the following meanings: Minute (0 - 59), Hour (0 - 23), Day of the month (1 - 31), Month (1 - 12), Weekday (0 - 6) for (Sunday - Saturday). The parameters can consist of single values, comma-separated lists (1, 3, 6, 9, ...), range (from-to) or * for all.
|WebExModule |IModule |1            |No       |The class of the module. The module must be defined in the same plugin as the resource.

## Task model
Tasks are another form of concurrent code execution. In contrast to jobs, tasks are executed ad-hoc (e.g. an export task that was triggered by the user). The result 
may not be available until a later date. However, the web application can still be fully used. If the result is available, information is usually provided (e.g. by means 
of a notification).

![WebExpress taskmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/taskmodel.svg)

Tasks are created dynamically by instantiating a class derived from ```Task``` and starting it from the ```TaskManager```.

The tasks can take the following states:

![WebExpress taskstate](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/taskstate.svg)

## Notification model
Notifications are messages that are displayed to users as pop-up windows. The notifications are globally (visible to all), linked to a session (visible to current users) or 
to specific roles (visible to selected users). The notifications are displayed in the upper right corner and are retained when a page is changed. Notifications are closed 
by the user or at the end of the display period. Notifications that are visible to multiple users are removed by closing a user.

![WebExpress notificationmodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/notificationmodel.svg)

The ```NotificationManager``` is the central class for notifications. The ```AddNotification``` method is used to create notifications.

The following properties can be assigned to notifications:

|Property   |Optional |Description
|-----------|---------|-----------------
|Id         |No       |Is assigned internally. A change is not possible.
|Heading    |Yes      |The heading, or null if you don't want it to be displayed.
|Message    |No       |The body of the message.
|Durability |Yes      |The display time in milliseconds. If the number is less than 0, the notification remains active until it is closed by the user.
|Progress   |Yes      |Instead of the display duration, a progress value from 0 to 100 can be specified. A value less than zero means that no progress is calculated.
|Icon       |Yes      |A URI that contains an icon.
|Type       |Yes      |Is the notification type. The following values are supported: Primary, Secondary, Success, Info, Warning, Danger, Dark, Light, White

The following example illustrates how the NotificationManager works:

``` c#
// Welcome notification
NotificationManager.AddNotification
(
    heading: I18N("inventoryexpress:app.notification.welcome.label"),
    message: I18N("inventoryexpress:app.notification.welcome.description"),
    icon: Context.Icon,
    durability: 30000
);

```

The example creates a notification with a headline, an icon, and a message. The display time is 30 seconds.

![WebExpress notification](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/notification.png)

The NotificationManager must be enabled in the application. For this purpose, webexpress.webapp with the ResourceId or all webexpress.webapp.* must be included.

``` c#
[WebExId("app")]
...
[WebExOption<WebExpress.WebApp.ApiPopupNotificationV1>]
public sealed class MyApplication : IApplication
{
}
```

The functions of the ```NotificationManager``` can also be accessed via the REST API interface 
```{base path}/wxapp/api/v1/popupnotifications```
can be accessed. The following methods are available:

|Method |Parameter             |Description
|-------|----------------------|----------------
|Get    |None                  |Detects all notifications for the current user.
|Post   |A notification object |Stores a notification.
|Delete |The id                |Deletes an existing notification.

## Identity model
A large number of web applications are subject to requirements for access protection, integrity and confidentiality. These requirements can be met through 
identity and access management (IAM). In identity management, identities are managed. In access management, on the other hand, authorized entities are 
enabled to use a service (application). WebExpress supports the following identity management features:
- Provisioning: Provides WebExpress with the basic requirements for the entities to carry out their activities. Deprovisioning is the opposite path, in which the prerequisites are withdrawn (e.g. when leaving).
- Authentication: Handles the identification process of the entities.
- Authorization: Granting permission for a specific entity to use a specific service.

The provisioning service provides WebExpress with the basic requirements for the operation of the identities. This is realized with the help of a user 
account. The following illustration outlines the lifecycle of a user account. A user account can be in one of two states, ```Active``` and ```Deactivated```. If the 
events ```Create```, ```Update```, ```Disable```, ```Enable``` or ```Delete``` occur, the user account changes its state.

![WebExpress identitystate](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/identitystate.svg)

- Create: This event creates a new user account for an entity. As a rule, each entity should have exactly one user account. 
- Update: The update event is triggered in the event of changes (e.g. marriage or relocation). The changes are forwarded to the appropriate user accounts.
- Disable: This event disables the user account. However, allocated resources are retained and can no longer be used.
- Enable: A deactivated user account can be transferred to the activated state with the help of this event.
- Delete: This event is used for deprovisioning and deletes the user account of an entity.

WebExpress supports two methods of identity management:

- On-premises identity management: Each application has its own user management. The cost of setting up the necessary infrastructure is particularly easy here, as identity management is carried out directly by the application. Each application has its own identity domain, which is disadvantageous from a unified identity management perspective.
- Shared identity management: If the identities are outsourced to a central service and retrieved by the applications, there is shared identity management. Shared identity management allows you to reduce the number of identity domains. 

Entities (people, technical objects, etc.) have one or more identities, which distinguishes them from other entities. An identity is used for identification and consists 
of a collection of attributes (properties e.g. name, password), which individualizes an entity. Identities can be grouped according to certain characteristics. Furthermore, 
each group can be assigned one or more roles (e.g. administrator, programmer). The roles determine access to identity resources. In the following figure, the concept of 
identity is defined in terms of a UML model.

![WebExpress identity](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/identity.svg)

The identities and groups must be loaded from a persistent data storage. These can be provided by the application or come from external identity management (e.g. LDAP). The roles and 
identity resources are dictated by the application by hard-implementing them.

![WebExpress identitymodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/identitymodel.svg)

WebExpress provides the following default groups:
![WebExpress identitymodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/identitymodel.svg)

|Group |Description
|------|------------------
|All   | All identities are members of the group.

WebExpress provides the following roles:

|Role                   |Description
|-----------------------|----------------------
|Anonymous              |Without authenticating the entity.
|Authenticates          |All authenticated entities.
|Business administrator |Business configuration of the application. For example, the business administrator can define access rights (except system administration) of the entities.
|System administrator   |Technical configuration of the system. For example, the system administrator can install or update a new application.

In addition to the listed standard roles, self-defined roles from definition classes can be provided. 

``` c#
[WebExId("7f2f1d0c-7ef8-48b8-b513-e9fc12cb2c24")]
[WebExModule<MyModule>]
[WebExName("myRole")]
[WebExRole(IdentityRoleDefault.Authenticated)]
public sealed class MyIdentityRole : IIdentityRole
{
}
````
![WebExpress identitymodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/identitymodel.svg)

The role definition classes have the following attributes:

|Attribute        |Type    |Multiplicity |Optional |Description
|-----------------|--------|-------------|---------|-------------
|WebExId          |String  |1            |No       |The unique identification key.
|WebExModule      |IModule |1            |No       |The class of the module. The module must be defined in the same plugin as the resource.
|WebExName        |String  |1            |No       |The human-readable name of the role or an internationalization key.
|WebExDescription |String  |1            |Yes      |The description of the role. This can be a key to internationalization.
|WebExRole        |String  |1            |Yes      |Inherits the characteristics of the specified role.

Identity resources are usually automatically discovered from the metadata of the web resources and web components and assigned to roles. In addition, identity resources can also be 
created from definition classes.

``` c#
[WebExId("647af2e9-d8a1-4b83-835d-3d7da022fba9")]
[WebExModule<MyModule>]
[WebExName("Passwort zurücksetzen")]
[WebExAuthorization(Permission.RW, IdentityRoleDefault.Authenticated)]
[WebExAuthorization(Permission.R, IdentityRoleDefault.Everyone)]
public sealed class MyIdentityResource : IIdentityResource
{
}
```

The identity resource definition classes have the following attributes:

|Attribute          |Type        |Multiplicity |Optional |Description
|-------------------|------------|-------------|---------|-------------
|WebExId            |String      |1            |No       |The unique identification key.
|WebExModule        |IModule     |1            |No       |The class of the module. The module must be defined in the same plugin as the resource.
|WebExName          |String      |1            |No       |The human-readable name of the role or an internationalization key.
|WebExDescription   |String      |1            |Yes      |The description of the role. This can be a key to internationalization.
|WebExAuthorization |Int, String |1            |Yes      |Grants authority for a role (specifying the id).

In the case of an authorization check (can an identity be accessed by an identity resource (e.g. page)), it must be checked whether there is at least 
one transition (identity -> group -> role -> identity resource). This is done by the function 
```CheckAccess: (Identity, Identity Resource, Right) → Bool ```
of the Identity Manager. A return value of ```true``` means that access can be made.

![WebExpress checkaccess](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/checkaccess.svg)

During the authorization check, a distinction is made between the following types of access:

|Value |Rights |Description
|------|-------|------------------
|7     |RWX    |Read, Write, Delete and Execute (Full Control)
|6     |RW     |Reading and Writing
|5     |RX     |Read and Execute
|4     |R      |Read only
|3     |WX     |Write, Delete and Run
|2     |W      |Write only
|1     |X      |Run only
|0     |None   |None

The rights have the following meanings:

- read - The "read" right means that an identity resource can be opened for reading. The user who has this right can read the content, but cannot modify or delete it.
- write - The "write" right allows the user to modify the content. As a result, he does not have the right to delete.
- execute - The "execute" privilege allows a user to perform an action (e.g. start a process). In combination with the "write" right, the user is allowed to delete elements.

# WebApp template
The ```WebExpress.WebApp.dll``` package provides a template for creating business applications.

## WebApp page
The template determines the layout of a page. The page is divided into a header, a side area, the page content, and a footer. The individual sections (areas) can be accessed 
via the class properties. Furthermore, components can bind to these areas and display their contents.

![WebExpress webapppage](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppage.svg)

### Header
The business application header contains buttons and submenus to navigate the application at the top level. The ```ApplicationNavigator``` refers to other (WebExpress) 
applications. The ```AppTitle``` contains the name of the application. This comes from the name attribute of the application (see Section 3.3). The AppNavigation links 
point to key features of the application. The ```QuickCreate``` button provides functionality for creating records. In the search field, search queries can be passed 
to the application. The ```Help``` shaft panel groups the application's help links. The ```Notification``` button collects all notifications from the application. In 
the ```Avatar``` button, the functions of the user account are provided. The ```Setting``` button contains the functions for configuring the application.

![WebExpress webapppageheader](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppageheader.svg)

### Sidebar
The left side area of the application is responsible for the navigation of a thematically related area/function. Links to sub-functions or data sets can be created and 
displayed here.

![WebExpress webapppagesidebar](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppagesidebar.svg)

### Content
The content area is used to display records (for example, as a table or list) or to display and edit a record.

![WebExpress webapppagecontent](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppagecontent.svg)

### Toolbar

The toolbar contains links or buttons with data-independent functions (e.g. switching between lists and table view).

![WebExpress webapppagetoolbar](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppagetoolbar.svg)

### Headline
The headline displays the title of the displayed data. The title bar also has data-dependent functions (e.g. printing) and a display of metadata (e.g. creation date, creator).

![WebExpress webapppageheadline](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppageheadline.svg)

### Property
The properties pane is used to display metadata and properties of the displayed data (for example, attachments). 

![WebExpress webapppageproperty](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppageproperty.svg)

### Notifications
There are three ways to display notifications in web applications. The first way is to display notifications in the Notification section of the header. Above all, personalized 
notifications are displayed here (e.g. new comments on subscribed content). The second way is to display notifications in an area below the header. This is intended for application-wide 
notifications (e.g. scheduled maintenance windows).

![WebExpress webapppagetoastnotification](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppagetoastnotification.svg)

The third option is to display notifications in a pop-up dialog. This is intended for the display of results (e.g. successful saving).

![WebExpress webapppagenotification](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppagenotification.svg)

### Searchoptions
The search options provide a dialog for filtering records.

![WebExpress webapppagesearchoption](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppagesearchoption.svg)

### Footer
The footer is located at the bottom of the web application and usually contains information about the copyright, imprint and version.

![WebExpress webapppagefooter](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapppagefooter.svg)

## Login Page
The login page is used to authenticate users. 

![WebExpress webapploginpage](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webapploginpage.svg)

## Status page
The status pages are displayed in case of errors. This can have different causes. For example, if a requested page was not found.

![WebExpress webappstatuspage](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webappstatuspage.svg)

## Setting page
Setting page templates are used to administer the web applications. Settings pages must implement the ```IPageSetting``` interface.

![WebExpress webappsettingpagemodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webappsettingpagemodel.svg)

When the settings page is generated, the class is enriched with meta information by attributes.

``` c#
[WebExId("Settings")]
...
[WebExSettingContext("admin")]
[WebExSettingSection(SettingSection.Primary)]
[WebExSettingGroup("Setting")]
[WebExSettingIcon(TypeIcon.InfoCircle)]
public sealed class S : PageTemplateWebAppSetting
{
}
```

The following attributes are available for a settings page:

|Attribute           |Type           |Multiplicity |Optional |Description
|--------------------|---------------|-------------|---------|--------------
|WebExSettingContext |String         |1            |Yes      |Sets the context. Only settings pages that use the same context are included in the Setting menu. In the ```SettingTab```, all contexts are listed and referred to the first settings page.
|WebExSettingSection |SettingSection |1            |Yes      |Determines the section by displaying the entry in the Setting menu.
|WebExSettingGroup   |String         |1            |Yes      |Groups the settings entries within a section.
|WebExSettingIcon    |String         |1            |Yes      |An icon to be displayed in the SettigMenu along with the link to the settings page.
| ^^                 |TypeIcon       | ^^          | ^^      | ^^
|WebExSettingHide    |-              |1            |Yes      |Not displaying the page in the settings

The template is specially adapted to the settings pages. In particular, the side navigation pane and a tab element are automatically populated 
from the meta information.

![WebExpress webappsettingpage](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webappsettingpage.svg)

### Setting menu
The settings menu groups the different settings thematically. The groups are determined from the ```SettingGroup``` attributes of the settings pages.

![WebExpress webappsettingpagemenu](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webappsettingpagemenu.svg)

### Setting tab
The contents of the ```SettingTab``` are fed from the ```SettingSection``` attributes of the settings pages. For each defined section, a tab element is created and linked to the first element 
of the section. The ```SettingTab``` is not displayed if no section or only one section has been defined.

![WebExpress webappsettingpagetab](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webappsettingpagetab.svg)

## Theme model
WebExpress.WebApp offers a ready-made layout (e.g. color scheme, fonts, font sizes). This can be adapted to individual needs by the web applications. The management of the themes is taken over by the 
```ThemeManager```. An individual topic can be assigned to each application. The configuration of the topics can be done via definition classes or via a settings dialog, which is provided by WebExpress.WebApp.

![WebExpress webappthememodel](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/webappthememodel.svg)

A color scheme is defined in a class that implements the ITheme interface and is associated with an application.

``` c#
[WebExId("mytheme")]
[WebExName("MyLayout")]
[WebExDescription("example")]
[WebExImage("/assets/img/mytheme.png")]
[WebExApplication<MyApplication>]
public sealed class MyTheme : ITheme
{
    public static PropertyColorBackground HeaderBackground => new(TypeColorBackground.Dark);
    public static PropertyColorText HeaderTitle => new(TypeColorText.Light);
    public static PropertyColorText HeaderNavigationLink => new(TypeColorText.Light);
    …
}
```

The following attributes are available:

|Attribute        |Type   |Multiplicity |Optional |Description
|-----------------|-------|-------------|---------|---------------------
|WebExId          |String |1            |No       |The unique identification key.
|WebExName        |String |1            |No       |The name of the topic that can be displayed in the interface. This can be a key to internationalization.
|WebExDescription |String |1            |Yes      |The description of the topic. This can be a key to internationalization.
|WebExImage       |String |1            |Yes      |Link to an image that visually represents the topic.
|WebExApplication |String |n            |No       |A specific ApplicationId, regular expression, or * for any application.
|                 |Type   |             |         |The class of the application.

# CRUD
CRUD stands for the four basic operations supported by WebExpress.WebApp in the form of a framework:

- Create -> create dataset
- Read or Retrieve -> read record
- Update -> update record
- Delete or Destroy -> delete record

The CRUD framework consists of HTML and REST API templates that provide a generic view and processing.

![WebExpress crud](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/crud.svg)

CRUD operations are mapped by the REST API by the following operations (RFC 7231 and RFC 5789):

|CRUD operation   |HTML              |REST API
|-----------------|------------------|-----------
|Create           |Form              |POST
|Read (Retrieve)  |List or Table     |GET
|Update           |Form              |PATCH
|Delete (Destroy) |Confirmation form |DELETE

# WQL
The WebExpress Query Language (WQL) is a query language that filters and sorts a given amount of data. A statement of the query language is usually sent from the client to the server, which collects, filters and sorts the data and sends it back to the client.
Example of a WQL:

```
Name ~ "WebExpress" and Create < now(-3d) orderby Create desc take 5
```

The example returns the first five elements of the dataset that contain the value "WebExpress" in the Name attribute and that were created three days ago (Create attribute) or earlier. The result is sorted in descending order by creation date.

The following BNF is used to illustrate the grammar:

```
                 WQL ::= Filter Order Partitioning | ε
              Filter ::= Filter LogicalOperator Filter | „(“Filter„)“ | Condition | ε
           Condition ::= Attribute Operator „"“Value„"“ | Attribute Operator Function 
     LogicalOperator ::= „and“ | „or“
           Attribute ::= Sign Attribute | Sign
            Function ::= Name „(“ ParameterList „)“
                Name ::= Sign Name | Sign
       ParameterList ::= Parameter „,“ Parameter | Parameter | ε
           Parameter ::= „"“Value„"“ | Function
            Operator ::= „=“ | „>“ | „<“ |  „!=“ | „is“ | „is not“ | „in“ | „not in“
               Order ::= „orderby“ OrderOperator | ε
       OrderOperator ::= OrderOperator „,“ OrderOperator | Attribute DescendingOrder
     DescendingOrder ::= „asc“ | „desc“ | ε
         Partitioning::= Partitioning Partitioning | PartitioningOperator Number | ε
PartitioningOperator ::= „take“ | „skip“
                Sign ::= „A“ | … | „Z“ | „a“ | … | „z“ 
              Number ::= „0“ | … | „9“
               Value ::= all characters except „"“ 
```

# Example
The classic example of the Hello World application is intended to show in the simplest possible way which instructions and components are needed for a complete program.

``` c#
using WebExpress.WebAttribute;
using WebExpress.WebApplication;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebResource;

namespace Sample
{
    public sealed class MyPlugin : IPlugin
    {
        public void Initialization(IPluginContext context) { }
        public void Run(){ }
        public void Dispose() { }
    }

    public sealed class MyApplication : IApplication
    {
        public void Initialization(IApplicationContext context) { }
        public void Run() { }
        public void Dispose() { }
    }

    [WebExApplication<MyApplication>]
    public sealed class MyModule : IModule
    {
        public void Initialization(IModuleContext context) { }
        public void Run() { }
        public void Dispose() { }
    }

    [WebExModule<MyModule>]
    public sealed class Home : ResourcePage
    {
        public Home (UriRessource uri, IModuleContext context)
            : base(uri, context)
        {
        }
        
        public override IHtmlNode Render()
        {
            var control = new ControlText(){Text = "Hello World!"};
            return control.Render(new RenderContext(this));
        }
    }
}
```
