match (user:User)-[like:LIKES]-(build:Build )-[p:HAS_PART]->(part:Part)
where part.Name contains $partName
return user, build, like, p, part, count(*) as occurrence
order by occurrence desc
limit 5