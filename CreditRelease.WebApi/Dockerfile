#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CreditRelease.WebApi/CreditRelease.WebApi.csproj", "CreditRelease.WebApi/"]
COPY ["CreditRelease.Application/CreditRelease.Application.csproj", "CreditRelease.Application/"]
COPY ["CreditRelease.Infra.Data/CreditRelease.Infra.Data.csproj", "CreditRelease.Infra.Data/"]
COPY ["CreditRelease.Domain/CreditRelease.Domain.csproj", "CreditRelease.Domain/"]
COPY ["CreditRelease.Utility/CreditRelease.Utility.csproj", "CreditRelease.Utility/"]
COPY ["CreditRelease.Infra.Ioc/CreditRelease.Infra.Ioc.csproj", "CreditRelease.Infra.Ioc/"]
RUN dotnet restore "CreditRelease.WebApi/CreditRelease.WebApi.csproj"
COPY . .
WORKDIR "/src/CreditRelease.WebApi"
RUN dotnet build "CreditRelease.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditRelease.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditRelease.WebApi.dll"]