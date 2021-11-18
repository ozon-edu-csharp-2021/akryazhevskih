FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/MerchandiseService.API/MerchandiseService.API.csproj", "src/MerchandiseService.API/"]

RUN dotnet restore "src/MerchandiseService.API/MerchandiseService.API.csproj"
COPY . .

WORKDIR "/src/src/MerchandiseService.API"
RUN dotnet build "MerchandiseService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MerchandiseService.API.csproj" -c Release -o /app/publish

COPY "entrypoint.sh" "/app/publish/."

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app

EXPOSE 80

COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh