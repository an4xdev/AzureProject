FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Project.Client/Project.Client.csproj", "Project.Client/"]
COPY ["Project.Shared/Project.Shared.csproj", "Project.Shared/"]
RUN dotnet restore "Project.Client/Project.Client.csproj"

COPY . .
WORKDIR "/src/Project.Client"
RUN dotnet publish "Project.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

RUN rm -rf ./*
COPY --from=build /app/publish/wwwroot .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
