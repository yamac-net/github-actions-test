FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /Build
COPY . /Build

FROM base as test
RUN dotnet build && dotnet test

FROM base as build
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as package
WORKDIR /App
COPY --from=build /Build/src/App/bin/Release/net5.0/publish/ /App
ENTRYPOINT [ "dotnet", "App.dll" ]
