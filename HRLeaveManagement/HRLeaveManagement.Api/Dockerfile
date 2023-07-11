#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HRLeaveManagement.Api/HRLeaveManagement.Api.csproj", "HRLeaveManagement.Api/"]
COPY ["HRLeaveManagement.Identity/HRLeaveManagement.Identity.csproj", "HRLeaveManagement.Identity/"]
COPY ["HRLeaveManagment.Application/HRLeaveManagment.Application.csproj", "HRLeaveManagment.Application/"]
COPY ["HRLeaveManagement.Clean.Domain/HRLeaveManagement.Clean.Domain.csproj", "HRLeaveManagement.Clean.Domain/"]
COPY ["HRLeaveManagement.Persistence/HRLeaveManagement.Persistence.csproj", "HRLeaveManagement.Persistence/"]
COPY ["HRLeaveManagement.Infrastructure/HRLeaveManagement.Infrastructure.csproj", "HRLeaveManagement.Infrastructure/"]
RUN dotnet restore "HRLeaveManagement.Api/HRLeaveManagement.Api.csproj"
COPY . .
WORKDIR "/src/HRLeaveManagement.Api"
RUN dotnet build "HRLeaveManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRLeaveManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:PrecompileBeforePublish=true /p:UseMerge=true /p:SingleAssemblyName=AppCode

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRLeaveManagement.Api.dll"]