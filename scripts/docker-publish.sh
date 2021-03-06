#!/bin/bash
TAG=latest
VERSION_TAG=$TRAVIS_BUILD_NUMBER
REPOSITORY=$DOCKER_USERNAME/warehouse

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker build -t $REPOSITORY:$TAG -t $REPOSITORY:$VERSION_TAG .
docker push $REPOSITORY:$TAG
docker push $REPOSITORY:$VERSION_TAG
