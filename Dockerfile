FROM mcr.microsoft.com/dotnet/aspnet:7.0
LABEL name="ActiveDirectory"
COPY src/ActiveDirectory/bin/Release/net7.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "ActiveDirectory.dll"]
