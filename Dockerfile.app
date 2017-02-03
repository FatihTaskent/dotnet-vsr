FROM microsoft/dotnet:latest

RUN mkdir /output

WORKDIR /output

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "dotnet-vsr.dll", "--server.urls", "http://*:5000"]

