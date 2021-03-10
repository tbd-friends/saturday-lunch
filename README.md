# saturday-lunch

A project to help understand more about how Kafka, ElasticSearch, Mongo and
an application can all be friends

from install directory;

docker compose up -d 

When that's started, use your favorite REST Client to execute the following statements;

#Add MongoDB Sink
```
curl --request POST \
  --url http://localhost:28082/connectors \
  --header 'Content-Type: application/json' \
  --data '{
	"name": "sink-mongodb-pizzas",
	"config": {
        "tasks.max":1,
        "connection.uri": "mongodb://mongosearch:27017",
        "connector.class":"com.mongodb.kafka.connect.MongoSinkConnector",
        "key.converter":"org.apache.kafka.connect.storage.StringConverter",
        "value.converter":"org.apache.kafka.connect.json.JsonConverter",
        "database":"PizzaStore",
        "collection":"pizzas",
        "topics": "pizzas"
    }
}'
```

#Add ElasticSearch Sink
```
curl --request POST \
  --url http://localhost:28082/connectors \
  --header 'Content-Type: application/json' \
  --data '{
    "name": "pizza-search-connector",
    "config": {
            "connector.class": "io.confluent.connect.elasticsearch.ElasticsearchSinkConnector",
            "key.converter": "org.apache.kafka.connect.storage.StringConverter",
            "value.converter": "org.apache.kafka.connect.json.JsonConverter",
            "topics": "pizzas",
            "connection.url": "http://elasticsearch:9200",
            "max.buffered.records": "20001",
            "schema.ignore": "true",
            "value.converter.schemas.enable": "false"
		}
}'
```

Now, run the registration application, and;


```
curl -X POST "http://localhost:5000/Pizza" -H  "accept: */*" -H  "Content-Type: application/json" -d "{\"name\":\"Pizza!\",\"ingredients\":[{\"name\":\"cheese\",\"quantity\":1}],\"price\":1.99}"
```

You should be able to navigate to http://localhost:5601 and add an index pattern for pizzas, then be able to search pizzas

You should be able to connect to mongo db on 27018 (default is 27017) and view the pizza in the PizzaStore collection.

Have Fun!