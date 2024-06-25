FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# FROM alpine:3.9.4 AS build-env

# # Add some libs required by .NET runtime 
# # https://github.com/dotnet/core/blob/master/Documentation/build-and-install-rhel6-prerequisites.md#troubleshooting
# RUN apk add --no-cache \ 
#     openssh libunwind \
#     nghttp2-libs libidn krb5-libs libuuid lttng-ust zlib \
#     libstdc++ libintl \
#     icu


WORKDIR /App
EXPOSE 80
EXPOSE 3000
EXPOSE 443

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out
# RUN dotnet publish --runtime alpine-x64 -c Release --self-contained true -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "nugsnet6.dll"]
