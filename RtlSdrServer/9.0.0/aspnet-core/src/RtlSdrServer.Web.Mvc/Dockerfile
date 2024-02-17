FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/RtlSdrServer.Web.Mvc/RtlSdrServer.Web.Mvc.csproj", "src/RtlSdrServer.Web.Mvc/"]
COPY ["src/RtlSdrServer.Web.Core/RtlSdrServer.Web.Core.csproj", "src/RtlSdrServer.Web.Core/"]
COPY ["src/RtlSdrServer.Application/RtlSdrServer.Application.csproj", "src/RtlSdrServer.Application/"]
COPY ["src/RtlSdrServer.Core/RtlSdrServer.Core.csproj", "src/RtlSdrServer.Core/"]
COPY ["src/RtlSdrServer.EntityFrameworkCore/RtlSdrServer.EntityFrameworkCore.csproj", "src/RtlSdrServer.EntityFrameworkCore/"]
WORKDIR "/src/src/RtlSdrServer.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/RtlSdrServer.Web.Mvc", "src/RtlSdrServer.Web.Mvc"]
COPY ["src/RtlSdrServer.Web.Core", "src/RtlSdrServer.Web.Core"]
COPY ["src/RtlSdrServer.Application", "src/RtlSdrServer.Application"]
COPY ["src/RtlSdrServer.Core", "src/RtlSdrServer.Core"]
COPY ["src/RtlSdrServer.EntityFrameworkCore", "src/RtlSdrServer.EntityFrameworkCore"]
WORKDIR "/src/src/RtlSdrServer.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "RtlSdrServer.Web.Mvc.dll"]
