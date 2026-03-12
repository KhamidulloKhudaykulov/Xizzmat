# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# hamma kodni copy qilish
COPY . .

# API papkaga o'tish
WORKDIR /src/Modules/Xizzmat.Api

ENV ASPNETCORE_URLS=http://+:80

# restore
RUN dotnet restore

# publish
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Xizzmat.Api.dll"]