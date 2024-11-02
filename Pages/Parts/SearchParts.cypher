MATCH (n1:Part)-[r]->(n2:Part)
  WHERE n1.is_deleted <> true
  AND n2.is_deleted <> true
RETURN r, n1, n2
  LIMIT 25