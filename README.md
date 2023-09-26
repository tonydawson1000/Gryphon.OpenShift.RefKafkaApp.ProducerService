# Introduction

Sample HTTP API that Produces simple Messages to a Kafka.

Part of the 'RefKafkaApp' Reference Architecture Project.

Used to demonstrate containerisation of a Kafka Producer configured and running on OpenShift. 

# Getting Started
1.	Ensure [Podman](https://github.com/containers/podman) is installed
2.	Ensure you have connection details to a Kafka (BootstrapServers, Topic)
3.	Clone the repo

## Setting up a 'Local' Kafka
1.	Ensure [Podman-Compose](https://github.com/containers/podman-compose) is installed
2.	Clone the repo
3.	Start Kafka
4.	From the confluentKafkaAllInOne folder - run `podman-compose up -d`
5.	View the 'Control Centre' Dashboard from -> http://localhost:9021/clusters 

# Build and Test (Podman)
To Build a Container Image using the Containerfile:
- `podman build -t kafka-producer:0.1 .`

To Run a Container instance:
- `podman run -e "Kafka.ProducerConfig:TopicName=<ENTER-TOPIC-NAME-HERE>" -e "Kafka.ProducerConfig:BootstrapServers=<IPS-FOR-BOOTSTRAP-SERVERS>:9092" -e "Kafka.ProducerConfig:ClientId=<CLIENT-ID>" -p 8081:8080 kafka-producer:0.1 .`

e.g. 
- `podman run -e "Kafka.ProducerConfig:TopicName=MyTestTopic" -e "Kafka.ProducerConfig:BootstrapServers=10.25.30.157:9092" -e "Kafka.ProducerConfig:ClientId=MyTestProducer" -p 8081:8080 kafka-producer:0.1 .`

To View Container API:
- http://localhost:8081/swagger/

- The **GET** [Config](http://localhost:8081/config) Endpoint returns details of the loaded Kafka Producer Config

- The **POST** `Messages` Endpoint 'Publishes' a Single Message of `Key` and `Value` to Kafka using the JSON Payload
    ```json
    {
        "key": "string",
        "value": "string"
    }
    ```

# TODO : Setup Kubernetes Manifests for Deployment...