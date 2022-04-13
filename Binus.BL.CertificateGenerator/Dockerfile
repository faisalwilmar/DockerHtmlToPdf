#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev libxrender1 libxext6 libx11-6 libfreetype6 fontconfig zlib1g
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Binus.BL.CertificateGenerator.csproj"
RUN dotnet build "Binus.BL.CertificateGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Binus.BL.CertificateGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
#COPY libwkhtmltox.dylib .
COPY libwkhtmltox.dylib /usr/lib
#COPY libwkhtmltox.so .
COPY libwkhtmltox.so /usr/lib
#COPY libwkhtmltox.dll .
COPY libwkhtmltox.dll /usr/lib
ENTRYPOINT ["dotnet", "Binus.BL.CertificateGenerator.dll"]