FROM mcr.microsoft.com/dotnet/core/sdk:3.0
WORKDIR /app
COPY ./src/BeComfy.Services.Airplanes/bin/Release/netcoreapp3.0 .
ENV ASPNETCORE_URLS http://*:5015
ENV ASPNETCORE_ENVIRONMENT Release
EXPOSE 5015
ENTRYPOINT ["dotnet", "BeComfy.Services.Airplanes.dll"]