#!/bin/bash

docker rm $(docker stop $(docker ps -a -q --filter ancestor=apprunner --format="{{.ID}}"))

DEPLOYCONTAINER=$(docker ps -a -q --filter ancestor=appbuilder --format="{{.ID}}")

docker run -d -p 80:5000 --volumes-from $DEPLOYCONTAINER apprunner




