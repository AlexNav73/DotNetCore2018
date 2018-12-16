FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY DotNetCore2018.WebApi/DotNetCore2018.WebApi.csproj DotNetCore2018.WebApi/
COPY DotNetCore2018.Data/DotNetCore2018.Data.csproj DotNetCore2018.Data/
COPY DotNetCore2018.Core/DotNetCore2018.Core.csproj DotNetCore2018.Core/
COPY DotNetCore2018.Business/DotNetCore2018.Business.csproj DotNetCore2018.Business/
RUN dotnet restore DotNetCore2018.WebApi/DotNetCore2018.WebApi.csproj
COPY . .
WORKDIR /src/DotNetCore2018.WebApi
RUN dotnet build DotNetCore2018.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DotNetCore2018.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DotNetCore2018.WebApi.dll"]
