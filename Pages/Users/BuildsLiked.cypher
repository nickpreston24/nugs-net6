MERGE (p1:User { name: $userName })
MERGE (p2:Build { name: $buildName })
MERGE (p1)-[:LIKES]->(p2)
RETURN p1, p2