#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HRLeaveManagement.MVC/HRLeaveManagement.MVC.csproj", "HRLeaveManagement.MVC/"]
COPY ["HRLeaveManagement.Identity/HRLeaveManagement.Identity.csproj", "HRLeaveManagement.Identity/"]
COPY ["HRLeaveManagment.Application/HRLeaveManagment.Application.csproj", "HRLeaveManagment.Application/"]
COPY ["HRLeaveManagement.Clean.Domain/HRLeaveManagement.Clean.Domain.csproj", "HRLeaveManagement.Clean.Domain/"]
RUN dotnet restore "HRLeaveManagement.MVC/HRLeaveManagement.MVC.csproj"
COPY . .
WORKDIR "/src/HRLeaveManagement.MVC"
RUN dotnet build "HRLeaveManagement.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRLeaveManagement.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRLeaveManagement.MVC.dll"]