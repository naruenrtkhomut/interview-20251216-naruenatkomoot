FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443
#ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Interview-Test.Api/Interview-Test.Api.csproj", "Interview-Test.Api/"]
COPY ["Interview-Test.Infrastructure/Interview-Test.Infrastructure.csproj", "Interview-Test.Infrastructure/"]
RUN dotnet restore "Interview-Test.Api/Interview-Test.Api.csproj"
COPY . .
WORKDIR "/src/Interview-Test.Api"
RUN dotnet build "Interview-Test.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Interview-Test.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Interview-Test.Api.dll"]

#docker build -f Interview-Test/Interview-Test.API/Dockerfile -t interview-test.api .
#docker run --rm -it -p 8000:8080 -e ASPNETCORE_HTTP_PORTS=8080 interview-test.api