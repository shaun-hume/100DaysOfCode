
#--- .NET CORE API ---#

FROM mcr.microsoft.com/dotnet/core/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./src ./100DaysAPI/

# copy everything else and build app
WORKDIR /app/100DaysAPI
RUN dotnet restore
RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/100DaysAPI/out ./
ENTRYPOINT ["dotnet", "100DaysAPI.dll"]