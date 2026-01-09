# --- Stage 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file from the Api folder and restore
# Updated path: Api/api.csproj
COPY ["Api/api.csproj", "Api/"]
RUN dotnet restore "Api/api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "api.csproj" -c Release -o /app/build

# --- Stage 2: Publish ---
FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Stage 3: Final Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]