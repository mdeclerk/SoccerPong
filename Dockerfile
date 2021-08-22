# Build environment
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /publish --nologo

# Runtime environment
FROM nginx:alpine AS runtime-env
WORKDIR /usr/share/nginx/html
COPY --from=build-env /publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf