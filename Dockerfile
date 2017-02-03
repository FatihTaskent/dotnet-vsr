FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5000/tcp

RUN ["dotnet", "ef", "database", "update"]

CMD ["dotnet", "run", "--server.urls", "http://*:5000"]
