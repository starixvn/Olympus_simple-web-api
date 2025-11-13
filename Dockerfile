FROM hub.nexdev.net/base/dotnet/core/sdk:9.0-alpine AS builder

WORKDIR /src

COPY ./SimpleWebApi/*.csproj ./SimpleWebApi/

RUN dotnet nuget add source https://nuget.nexdev.net/api/v2
RUN --mount=type=cache,target=/root/.nuget/packages \
	dotnet restore -v n -r linux-x64 SimpleWebApi/SimpleWebApi.csproj

COPY ./SimpleWebApi/. ./SimpleWebApi

WORKDIR /src/SimpleWebApi
RUN --mount=type=cache,target=/root/.nuget/packages \
	dotnet publish -r linux-x64 --self-contained false -o /app

FROM hub.nexdev.net/base/dotnet/core/aspnet:9.0-alpine AS base

WORKDIR /app
COPY --from=builder /app .
ENV COMPlus_EnableDiagnostics=0

ENTRYPOINT ["dotnet", "SimpleWebApi.dll"]
