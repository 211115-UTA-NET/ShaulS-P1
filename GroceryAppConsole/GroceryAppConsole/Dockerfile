FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
# #Change the images current working directory
COPY . .
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /

COPY  --from=build /bin/Release/net6.0/publish/ .

ENTRYPOINT ["dotnet", "GroceryAppConsole.dll"]
#Entrypoint sets the container to be an executable, and specifies
#what to run on startup.
