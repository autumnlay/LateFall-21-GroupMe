FROM mcr.microsoft.com/dotnet/sdk:5.0
COPY ./GroupMe/bin/Release/net5.0/publish/ App/
WORKDIR /App
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GroupMe.dll
