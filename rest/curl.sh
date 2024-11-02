#!/bin/bash

QUERY=query.json

time curl -i -XPOST \
    -o output.log \
    --data "@$QUERY" \
    --user neo4j:neo4j \
    -H "Accept: application/json" \
    -H "Content-Type: application/json" \
    http://127.0.0.1:7474/db/data/transaction/commit