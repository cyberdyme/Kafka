KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://broker:9092,EXTERNAL://localhost:9094
KAFKA_LISTENERS: PLAINTEXT://:9092,CONTROLLER://:9093,EXTERNAL://:9094
KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://localhost:9092
KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: CONTROLLER:PLAINTEXT,PLAINTEXT:PLAINTEXT,EXTERNAL:PLAINTEXT


To run Kafka type the following:
docker-compose up -d

Use the following to see processes:
docker ps --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"

Will give the following output:
NAMES      STATUS                    PORTS
kafka-ui   Up 9 seconds              0.0.0.0:8088->8080/tcp
broker     Up 32 seconds (healthy)   9092/tcp, 0.0.0.0:9094->9094/tcp


Kafa ka UI =>
http://localhost:8088/



# Run docker terminal (so we can find the valid kafka scripts)
docker exec -it broker  /bin/bash

cd /opt/kafka/bin
./kafka-topics.sh --create --topic test-topic --partitions 1 --replication-factor 1 --bootstrap-server kafka:9092
./kafka-console-consumer.sh --topic test-topic --from-beginning --bootstrap-server localhost:9092



docker exec -it broker netstat -tuln

## turn of kafka
docker-compose down -v

## switch on Kafka
docker-compose up -d



