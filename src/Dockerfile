#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Dlvr.SixtySeconds.Api/Dlvr.SixtySeconds.Api.csproj", "Dlvr.SixtySeconds.Api/"]
COPY ["Dlvr.SixtySeconds.DataAccessLayer.Library/Dlvr.SixtySeconds.DataAccessLayer.Library.csproj", "Dlvr.SixtySeconds.DataAccessLayer.Library/"]
RUN dotnet restore "Dlvr.SixtySeconds.Api/Dlvr.SixtySeconds.Api.csproj"
COPY . .
WORKDIR "/src/Dlvr.SixtySeconds.Api"
RUN dotnet build "Dlvr.SixtySeconds.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dlvr.SixtySeconds.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dlvr.SixtySeconds.Api.dll"]