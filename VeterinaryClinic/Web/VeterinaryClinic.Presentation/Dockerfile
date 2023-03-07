#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/VeterinaryClinic.Presentation/VeterinaryClinic.Presentation.csproj", "Web/VeterinaryClinic.Presentation/"]
COPY ["Services/AppointmentService/VeterinaryClinic.AppointmentModule.Shared/VeterinaryClinic.AppointmentModule.Shared.csproj", "Services/AppointmentService/VeterinaryClinic.AppointmentModule.Shared/"]
COPY ["Services/ManagementService/VeterinaryClinic.ManagementModule.Shared/VeterinaryClinic.ManagementModule.Shared.csproj", "Services/ManagementService/VeterinaryClinic.ManagementModule.Shared/"]
COPY ["SharedKernel/VeterinaryClinic.SharedKernel/VeterinaryClinic.SharedKernel.csproj", "SharedKernel/VeterinaryClinic.SharedKernel/"]
RUN dotnet restore "Web/VeterinaryClinic.Presentation/VeterinaryClinic.Presentation.csproj"
COPY . .
WORKDIR "/src/Web/VeterinaryClinic.Presentation"
RUN dotnet build "VeterinaryClinic.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeterinaryClinic.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeterinaryClinic.Presentation.dll"]