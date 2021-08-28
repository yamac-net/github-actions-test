FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /Build
COPY . /Build

FROM base as test
RUN dotnet build && dotnet test

FROM test as development
ENTRYPOINT [ "dotnet", "run" ]

FROM test as publish
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as production
WORKDIR /App
COPY --from=publish /Build/src/App/bin/Release/net5.0/publish/ /App
ENTRYPOINT [ "dotnet", "App.dll" ]
