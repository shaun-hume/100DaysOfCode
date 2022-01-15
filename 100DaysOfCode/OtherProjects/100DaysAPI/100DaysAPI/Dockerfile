
#--- .NET CORE API ---#

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./ ./100daysapi/

# copy everything else and build app
WORKDIR /app/100daysapi
RUN dotnet restore
RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
EXPOSE 5000

ENV ASPNETCORE_URLS="http://+:5000"

WORKDIR /app
COPY --from=build /app/100daysapi/out ./
ENTRYPOINT ["dotnet", "100DaysAPI.dll"]