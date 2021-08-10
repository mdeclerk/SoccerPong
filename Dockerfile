FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /publish --nologo

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build-env /publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf