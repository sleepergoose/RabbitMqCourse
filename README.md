# Project To Study RabbitMQ message broker

This repository will be used for a project to study RabbitMQ. For now the README file will collect some necessary information (commands, settings, links, etc.) related to RabbitMQ.

Main Video Course: [Microservices communication patterns, messaging basics, RabbitMQ | Messaging in distributed systems](https://www.youtube.com/watch?v=eW4JgrkwWEM).
<br/>
Source Code of the video course on GitHub: [repo](https://github.com/devmentors/Messaging-In-Distributed-Systems).
<br/>
<br/>


## Prerequisites

We need a RabbitMQ Server running in Docker container. 

Page with official [docker image](https://hub.docker.com/_/rabbitmq) on the Docker Hub.

Command to pull the official docker image: 

```
docker pull rabbitmq
```

Command to run RabbitMQ server in the docker container:

```
docker run -d --hostname RabbitMqHostName --name RabbitMqServerName -p 8080:15672 -e RABBITMQ_DEFAULT_USER=adminuser -e RABBITMQ_DEFAULT_PASS=adminpassword123 rabbitmq:3-management
```

where:

* `-d` - Run container in background and print container ID.
* `--hostname RabbitMqHostName`
* `--name RabbitMqServerName`
* `-p 8080:15672` - ports numbers (8080 - outer port, 15672 - inner port inside docker container).
* `-e` - set an environment variable. For example, `RABBITMQ_DEFAULT_USER` and `RABBITMQ_DEFAULT_PASS` are default RabbitMQ environment variables.
* `-e RABBITMQ_DEFAULT_USER=adminuser` - set an environment variable `RABBITMQ_DEFAULT_USER` with value `adminuser`
* `-e RABBITMQ_DEFAULT_PASS=adminpassword123` - set an environment variable `RABBITMQ_DEFAULT_PASS` with value `adminpassword123`
* `rabbitmq:3-management` - docker image to run.
<br/>
<br/>


## Resources

