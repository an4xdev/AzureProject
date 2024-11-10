# Etap 1: Kompilacja aplikacji Blazor
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiowanie plików projektu i przywracanie zależności
COPY ["Project.Client/Project.Client.csproj", "Project.Client/"]
COPY ["Project.Shared/Project.Shared.csproj", "Project.Shared/"]
RUN dotnet restore "Project.Client/Project.Client.csproj"

# Skopiowanie pozostałych plików i zbudowanie aplikacji
COPY . .
WORKDIR "/src/Project.Client"
RUN dotnet publish "Project.Client.csproj" -c Release -o /app/publish

# Etap 2: Konfiguracja serwera NGINX do hostowania plików aplikacji
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Usunięcie domyślnej strony NGINX i skopiowanie opublikowanych plików Blazor
RUN rm -rf ./*
COPY --from=build /app/publish/wwwroot .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]