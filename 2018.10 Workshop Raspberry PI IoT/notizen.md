
geek@Azure:~$ az extension add --name azure-cli-iot-ext
geek@Azure:~$ az iot hub device-identity create --hub-name IoTExampleHubGeeks --device-id RaspBerryPiGeek
{
  "authentication": {
    "symmetricKey": {
      "primaryKey": "eE67vQJMf2O2NWWQ/ZHvmofkxSUwF/aPjkxDt+nIFeA=",
      "secondaryKey": "QznBw/v/x2H+PGzkLSPCWxLq2cEfKMqgDgbZXdTJutY="
    },
    "type": "sas",
    "x509Thumbprint": {
      "primaryThumbprint": null,
      "secondaryThumbprint": null
    }
  },
  "capabilities": {
    "iotEdge": false
  },
  "cloudToDeviceMessageCount": 0,
  "connectionState": "Disconnected",
  "connectionStateUpdatedTime": "0001-01-01T00:00:00",
  "deviceId": "RaspBerryPiGeek",
  "etag": "MjE1NzQyODU2",
  "generationId": "636764328273144121",
  "lastActivityTime": "0001-01-01T00:00:00",
  "status": "enabled",
  "statusReason": null,
  "statusUpdatedTime": "0001-01-01T00:00:00"
}

geek@Azure:~$ az iot hub show --query properties.eventHubEndpoints.events.endpoint --name IoTExampleHubGeeks
"sb://ihsuprodamres021dednamespace.servicebus.windows.net/"
geek@Azure:~$ az iot hub show --query properties.eventHubEndpoints.events.path --name IoTExampleHubGeeks
"iothub-ehub-iotexample-907907-19e29d3f41"
geek@Azure:~$ az iot hub policy show --name iothubowner --query primaryKey --hub-name IoTExampleHubGeeks
"VOWaNLMMKA9htWQ8tUzmY4WtuKbxf7A4Cz36iPFOU74="