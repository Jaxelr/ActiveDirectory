FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
LABEL name="ActiveDirectory"
COPY src/bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "ActiveDirectory.dll"]
