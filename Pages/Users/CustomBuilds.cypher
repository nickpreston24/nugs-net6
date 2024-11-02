match (user:User)
where user.Name = $username
OPTIONAL MATCH (user)-[has_build:HAS_BUILD]->(build:Build)-[has_part:HAS_PART]->(part)
return user, build, part, has_part, has_build