@echo off
cd ..\DotNetCore2018.Data\
dotnet ef database update %1 --startup-project ..\DotNetCore2018.WebApi\
if errorlevel 0 if not [%2] == "" if [%2] == "remove" (
    dotnet ef migrations remove --startup-project ..\DotNetCore2018.WebApi\
)
cd ..\DotNetCore2018.WebApi\
echo on
