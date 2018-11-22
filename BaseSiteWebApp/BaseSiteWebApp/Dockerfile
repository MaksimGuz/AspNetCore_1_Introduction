FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
#setup node
ENV NODE_VERSION 8.9.4
ENV NODE_DOWNLOAD_SHA 21fb4690e349f82d708ae766def01d7fec1b085ce1f5ab30d9bda8ee126ca8fc

RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs
EXPOSE 80
WORKDIR /src
COPY ["BaseSiteWebApp/BaseSiteWebApp.csproj", "BaseSiteWebApp/"]
RUN dotnet restore "BaseSiteWebApp/BaseSiteWebApp.csproj"
COPY . .
WORKDIR "/src/BaseSiteWebApp"
RUN dotnet build "BaseSiteWebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BaseSiteWebApp.csproj" -c Release -o /app

FROM base AS final
# build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
#setup node, this is only needed if you use Node both at runtime and build time. Some people may only need the build part.
ENV NODE_VERSION 8.9.4
ENV NODE_DOWNLOAD_SHA 21fb4690e349f82d708ae766def01d7fec1b085ce1f5ab30d9bda8ee126ca8fc

RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BaseSiteWebApp.dll"]