dotnet publish -c Release -r alpine-x64 --self-contained true /p:PublishTrimmed=true -o ./publish
docker build -t nugs -f Dockerfile .
docker images
docker run -e DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 -p 8080:80 nugs