#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/ManagementService/VeterinaryClinic.ManagementModule.Api/VeterinaryClinic.ManagementModule.Api.csproj", "Services/ManagementService/VeterinaryClinic.ManagementModule.Api/"]
RUN dotnet restore "Services/ManagementService/VeterinaryClinic.ManagementModule.Api/VeterinaryClinic.ManagementModule.Api.csproj"
COPY . .
WORKDIR "/src/Services/ManagementService/VeterinaryClinic.ManagementModule.Api"
RUN dotnet build "VeterinaryClinic.ManagementModule.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeterinaryClinic.ManagementModule.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeterinaryClinic.ManagementModule.Api.dll"]