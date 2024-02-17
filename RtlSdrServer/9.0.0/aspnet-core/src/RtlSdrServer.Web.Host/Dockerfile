FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/RtlSdrServer.Web.Host/RtlSdrServer.Web.Host.csproj", "src/RtlSdrServer.Web.Host/"]
COPY ["src/RtlSdrServer.Web.Core/RtlSdrServer.Web.Core.csproj", "src/RtlSdrServer.Web.Core/"]
COPY ["src/RtlSdrServer.Application/RtlSdrServer.Application.csproj", "src/RtlSdrServer.Application/"]
COPY ["src/RtlSdrServer.Core/RtlSdrServer.Core.csproj", "src/RtlSdrServer.Core/"]
COPY ["src/RtlSdrServer.EntityFrameworkCore/RtlSdrServer.EntityFrameworkCore.csproj", "src/RtlSdrServer.EntityFrameworkCore/"]
WORKDIR "/src/src/RtlSdrServer.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/RtlSdrServer.Web.Host", "src/RtlSdrServer.Web.Host"]
COPY ["src/RtlSdrServer.Web.Core", "src/RtlSdrServer.Web.Core"]
COPY ["src/RtlSdrServer.Application", "src/RtlSdrServer.Application"]
COPY ["src/RtlSdrServer.Core", "src/RtlSdrServer.Core"]
COPY ["src/RtlSdrServer.EntityFrameworkCore", "src/RtlSdrServer.EntityFrameworkCore"]
WORKDIR "/src/src/RtlSdrServer.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "RtlSdrServer.Web.Host.dll"]
