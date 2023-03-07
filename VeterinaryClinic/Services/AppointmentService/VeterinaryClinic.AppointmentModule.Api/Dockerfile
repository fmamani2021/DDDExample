#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/AppointmentService/VeterinaryClinic.AppointmentModule.Api/VeterinaryClinic.AppointmentModule.Api.csproj", "Services/AppointmentService/VeterinaryClinic.AppointmentModule.Api/"]
COPY ["SharedKernel/VeterinaryClinic.SharedKernel/VeterinaryClinic.SharedKernel.csproj", "SharedKernel/VeterinaryClinic.SharedKernel/"]
COPY ["Services/AppointmentService/VeterinaryClinic.AppointmentModule.Domain/VeterinaryClinic.AppointmentModule.Domain.csproj", "Services/AppointmentService/VeterinaryClinic.AppointmentModule.Domain/"]
COPY ["Services/AppointmentService/VeterinaryClinic.AppointmentModule.Infrastructure/VeterinaryClinic.AppointmentModule.Infrastructure.csproj", "Services/AppointmentService/VeterinaryClinic.AppointmentModule.Infrastructure/"]
COPY ["Services/AppointmentService/VeterinaryClinic.AppointmentModule.Shared/VeterinaryClinic.AppointmentModule.Shared.csproj", "Services/AppointmentService/VeterinaryClinic.AppointmentModule.Shared/"]
RUN dotnet restore "Services/AppointmentService/VeterinaryClinic.AppointmentModule.Api/VeterinaryClinic.AppointmentModule.Api.csproj"
COPY . .
WORKDIR "/src/Services/AppointmentService/VeterinaryClinic.AppointmentModule.Api"
RUN dotnet build "VeterinaryClinic.AppointmentModule.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeterinaryClinic.AppointmentModule.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeterinaryClinic.AppointmentModule.Api.dll"]