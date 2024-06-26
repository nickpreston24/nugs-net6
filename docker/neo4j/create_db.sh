
DBNAME=http://ip-api.com
PASSWORD=""

print_help() {
  cat <<EOF

  intip.sh 👀

  Usage:
    intip.sh --inline <ip>
    intip.sh --prompt

  Options:
    --inline : Execute with mode inline
    --prompt : Execute with mode prompt

EOF
}

prompt_read_ip() {
  echo -n "Input your new password: "
  read NEW_PASSWORD
  
  docker run \
      --restart always \
      --env=NEO4J_AUTH=none \
      --publish=7474:7474 --publish=7687:7687 \
      --volume=$HOME/neo4j/data:/data \
#      --env NEO4J_AUTH=neo4j/${NEW_PASSWORD} \
      neo4j:5.18.0
}

read_inline() {
  NEW_PASSWORD=${1}
  
  docker run \
        --restart always \
        --publish=7474:7474 --publish=7687:7687 \
#        --env NEO4J_AUTH=neo4j/${NEW_PASSWORD} \
        --env=NEO4J_AUTH=none \
        --volume=$HOME/neo4j/data:/data \
        neo4j:5.18.0
    
}

case "$1" in
  --inline)
    read_inline $2
    ;;
  --prompt)
    prompt_read_ip
    ;;
  *)
    print_help
    ;;
esac

if [[ -n "${GET_DATA}" ]]; then
  command printf "\n";
  echo $GET_DATA | jq
fi
