MERGE (person1:Person { name: $person1Name })
MERGE (person2:Person { name: $person2Name })
MERGE (person1)-[:FOLLOWS]->(person2)
RETURN person1, person2
