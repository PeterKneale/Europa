# BUILD
FROM microsoft/aspnetcore-build:2 AS builder

# caches restore result by copying csproj file separately
COPY . /src
WORKDIR /src/Europa.Web
RUN dotnet restore
RUN dotnet publish --output /app/ --configuration Release

# RUN
FROM microsoft/aspnetcore:2
WORKDIR /app
COPY --from=builder /app .
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "Europa.Web.dll"]
