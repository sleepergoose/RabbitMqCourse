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


## Some notes about RabbitMQ

1. It's always the `PRODUCER` who calls the `EXCHANGE`. The `EXCHANGE` may be considered like a mailbox.
2. The `EXCHANGE` is bound with FIFO queues via Routing Keys.
3. On the other side of FIFO queues are Consumers.
4. Consumers compete for the message. It means that one message is consumed by one only one consumer. In other words: consumers don't get the same message. 


## Exchange

There are four types of exchanges, each type means different types of routing policies.

* `Fanout` - when a message is received, it will be forwarded to every queue it is bounded with (the same email is sent to multiple recipients).
* `Direct` - when a message is received, it will be routed to a queue that has the appropriate routing key (the queue has the same routing key as the message).
* `Topic` - allows patterns for Routing Key. Patterns contain `*` and `#` special symbols (see docs). 
* `Headers` - allows set header and `ANY` or `ALL` conditions to route.

## Best practices

1. One connection to RabbitMQ per one microservice. 
2. Sufficient amount of RAM
3. Channels are not thread safe. Don't share channnels between threads.


## Resources