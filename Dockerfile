FROM mcr.microsoft.com/dotnet/aspnet:5.0
LABEL name="ActiveDirectory"
COPY src/ActiveDirectory/bin/Release/net5.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "ActiveDirectory.dll"]
