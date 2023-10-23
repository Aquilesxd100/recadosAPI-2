#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./recados-api.csproj" --disable-parallel
RUN dotnet publish "./recados-api.csproj" -c release -o /app --no-restore
#Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "recados-api.dll"]

#ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
#RUN dotnet build /app ./
#CMD ["dotnet run"]
#EXPOSE 3000