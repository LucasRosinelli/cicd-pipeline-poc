FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/LucasRosinelli.Pipeline.Api/LucasRosinelli.Pipeline.Api.csproj", "src/LucasRosinelli.Pipeline.Api/"]
RUN dotnet restore "src/LucasRosinelli.Pipeline.Api/LucasRosinelli.Pipeline.Api.csproj"
COPY . .
WORKDIR "/src/src/LucasRosinelli.Pipeline.Api"
RUN dotnet build "LucasRosinelli.Pipeline.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LucasRosinelli.Pipeline.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LucasRosinelli.Pipeline.Api.dll"]