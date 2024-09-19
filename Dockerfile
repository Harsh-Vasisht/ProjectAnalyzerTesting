FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BackEndAPI/BackEndAPI.csproj", "./BackEndAPI/"]
RUN dotnet restore "./BackEndAPI/BackEndAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./BackEndAPI/BackEndAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./BackEndAPI/BackEndAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "BackEndAPI.dll"]'