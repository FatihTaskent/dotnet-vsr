#!/bin/bash

docker rm $(docker stop $(docker ps -a -q --filter ancestor=apprunner --format="{{.ID}}"))
docker rm $(docker stop $(docker ps -a -q --filter ancestor=appbuilder --format="{{.ID}}"))
docker rmi apprunner
docker rmi appbuilder

docker build -f Dockerfile.build -t appbuilder .
docker build -f Dockerfile.app -t apprunner .

docker run appbuilder
