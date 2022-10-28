FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR nuclio

RUN dotnet tool install -g dotnet-format

ADD . .

# ENTRYPOINT [ "dotnet" ]

CMD [ "dotnet", "test" , "tests/" ]
