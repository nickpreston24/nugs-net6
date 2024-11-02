MATCH (part)-[rel]-(build)
  WHERE part.Name =~ '(?i)a.*s'
  OR build.Name =~ '(?i)s.*s'
// or n.BallisticCoefficient >= .3
RETURN part, rel


// Sample merge + match set, for creation and last modified
//
//MERGE (p:Person {name: 'John Doe'})
//  ON CREATE SET p.createdAt = timestamp()
//  ON MATCH SET p.lastLoggedInAt = timestamp()
//RETURN p