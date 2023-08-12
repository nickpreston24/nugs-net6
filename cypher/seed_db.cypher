LOAD CSV FROM '../SeedData/Loadouts-Grid view.csv' AS line
//CREATE (:Artist {name: line[1], year: toInteger(line[2])})
CREATE (:Loadout {id: line[1], Name: line[2]})