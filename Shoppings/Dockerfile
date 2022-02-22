#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Shoppings/Shoppings.csproj", "Shoppings/"]
RUN dotnet restore "Shoppings/Shoppings.csproj"
COPY . .
WORKDIR "/src/Shoppings"
RUN dotnet build "Shoppings.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shoppings.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shoppings.dll"]
