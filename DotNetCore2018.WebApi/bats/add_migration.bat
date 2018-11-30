@echo off
cd ..\DotNetCore2018.Data\
dotnet ef migrations add %1 --startup-project ..\DotNetCore2018.WebApi\
cd ..\DotNetCore2018.WebApi\
echo on
