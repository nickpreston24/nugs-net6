MATCH (build:Build)
WHERE build.name contains $buildName
RETURN build
