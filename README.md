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
9. Launch `build.bat` script to build UI