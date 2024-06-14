FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["sqs/sqs.csproj", "sqs/"]
RUN dotnet restore "sqs/sqs.csproj"
COPY . .
WORKDIR "/src/sqs"
RUN dotnet build "sqs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sqs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sqs.dll"]
