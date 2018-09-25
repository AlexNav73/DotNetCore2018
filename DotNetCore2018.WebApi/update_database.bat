@echo off
cd ..\DotNetCore2018.Data\
dotnet ef database update --startup-project ..\DotNetCore2018.WebApi\
cd ..\DotNetCore2018.WebApi\
echo on
