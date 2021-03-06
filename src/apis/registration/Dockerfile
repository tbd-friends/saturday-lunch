#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/apis/registration/registration.csproj", "src/apis/registration/"]
RUN dotnet restore "src/apis/registration/registration.csproj"
COPY . .
WORKDIR "/src/src/apis/registration"
RUN dotnet build "registration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "registration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "registration.dll"]