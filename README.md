# About

ASP.NET Core project for mentoring program

[![Build Status](https://dev.azure.com/alexnav73/DotNetCore2018/_apis/build/status/AlexNav73.DotNetCore2018)](https://dev.azure.com/alexnav73/DotNetCore2018/_build/latest?definitionId=2)

# Building

## ASP.NET Core project building

Follow this steps to build Web application:
1. Download and install latest .NET Core SDK from [the main page](https://www.microsoft.com/net/download)
2. Install [MS Sql Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express) (Project uses local database)
3. `cd DotNetCore2018.WebApi`
4. Issue `dotnet restore` command to restore all nuget packages
5. `dotnet build` to build project
6. Run database migrations using script `update_database.bat`
7. From the Powershell call `dotnet run` to launch Web application

## UI building

UI written mainly in [Rust](https://www.rust-lang.org/en-US/) programming language and compiles to `WebAssembly` module which loaded asynchronously to the main script. To bundle all scripts and `WebAssembly` module together [Webpack](https://webpack.js.org/) was used. To build UI follow this steps:
1. Install [Build Tools for VS2017](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2017). It contains MSVC toolchain, linker etc.
2. Download `Rustup` from [here](https://rustup.rs/).
3. Start following command `rustup default nightly-x86_64-pc-windows-msvc`. It will download and install nightly version of the rust compiler.
4. Next we need to install build target using this command `rustup target add wasm32-unknown-unknown`
5. `cargo +nightly install wasm-bindgen-cli` this command installs `wasm-bindgen-cli` tool which generates Rust-to-JS binding
6. Install [Node.js](https://nodejs.org/en/download/) and [Yarn](https://yarnpkg.com/en/)
7. `cd DotNetCore2018.WebApi`
8. Use `yarn install` to restore all dependencies
9. Launch `build_web.bat` script to build UI

# Deploy

## Docker

In order to deploy web app to container one should build all 3 containers (`mssql`, `dotnetcore2018.webapi` and `nginx`) using `docker-compose`.
After `mssql` will be successfully built one sould make sure that database created and all migrations applied.
Container exposes only http port to access (for now).

Steps to deploy app into docker container:
1. Create `appsettings.Docker.json` file with connection string that points to `mssql` server
2. In repository root execute `docker-compose build` command to build all containers
3. Execute `docker-compose up` command to start containers up (issue the command multiple times until database will be ready)
4. Get machine ip (`docker-machine ip`) and use it to connect to the app

UI *must* be build before deploying to a container.

## IIS

1. Create `appsettings.Production.json` file with appropriate connection string
2. Publish `DotNetCore2018.WebApi` application to a folder
3. Create web api in IIS and point path to publish directory
4. For HTTPS connection one may use developer certificate

# Conclusion

## Pros

1. New fully rewritten design
2. Most of the components implemented as separate NuGet packages that can be easily integrated into application
3. Well implemented default Identity Provider (can be applied on top of existing database)
4. TagsHelpers really improve readability of the cshtml sources
5. Middleware pipeline gives clear understanding of what happened with request
6. Ability to write unit tests for controllers

## Cons

1. Routing. If route defined with `Route("[controller]/[action]")` attribute on controller it didn't recognized by Default route
2. Required methods is not type-checked. (`ViewComponent` requires `InvokeAsync` method and so on)
3. Some methods of the `Sturtup.cs` class can't be injected with services
4. `SignInManager` can't be used with non-standard Authentication scheme (`SignOutAsync` throws exception if Cookie authentication scheme not exists)
5. Not all HtmlHelpers covered by TagHalpers (`Html.Partial` must be written with old style)
6. Not supported `wasm` content type (automatic content type detection)
7. NuGet (may be bug?) link some packages to local folder causing Azure Pipeline to fail

