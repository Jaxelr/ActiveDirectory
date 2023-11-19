FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL name="ActiveDirectory"
COPY src/ActiveDirectory/bin/Release/net8.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "ActiveDirectory.dll"]
