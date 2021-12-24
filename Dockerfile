FROM mcr.microsoft.com/dotnet/aspnet:6.0
LABEL name="ActiveDirectory"
COPY src/ActiveDirectory/bin/Release/net6.0/publish/ App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "ActiveDirectory.dll"]
