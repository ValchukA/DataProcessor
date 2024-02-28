## Запуск
```
cd docker-compose
docker compose up -d
```
## Тестирование
Для тестирования нужно скопировать XML файл в директорию /data контейнера docker-compose-fileparser-1
```
cd ..
docker cp FileParser/DataSamples/status_1.xml docker-compose-fileparser-1:/data/status_1.xml
```
