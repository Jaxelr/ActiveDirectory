FROM mcr.microsoft.com/dotnet/aspnet:10.0
LABEL name="ActiveDirectory"
COPY src/bin/Release/net10.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "ActiveDirectory.dll"]
