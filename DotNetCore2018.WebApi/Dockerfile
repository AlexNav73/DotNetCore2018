FROM microsoft/aspnetcore:2

WORKDIR /app
COPY ./bin/Release/netcoreapp2.0/publish/ .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "DotNetCore2018.WebApi.dll"]