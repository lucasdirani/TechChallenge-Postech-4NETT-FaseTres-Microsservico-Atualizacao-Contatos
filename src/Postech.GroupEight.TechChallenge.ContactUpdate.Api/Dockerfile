FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8081
ENV ASPNETCORE_HTTP_PORTS=8081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Postech.GroupEight.TechChallenge.ContactUpdate.Api/Postech.GroupEight.TechChallenge.ContactUpdate.Api.csproj", "src/Postech.GroupEight.TechChallenge.ContactUpdate.Api/"]
RUN dotnet restore "src/Postech.GroupEight.TechChallenge.ContactUpdate.Api/Postech.GroupEight.TechChallenge.ContactUpdate.Api.csproj"
COPY . .
WORKDIR "/src/src/Postech.GroupEight.TechChallenge.ContactUpdate.Api"
RUN dotnet build "Postech.GroupEight.TechChallenge.ContactUpdate.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Postech.GroupEight.TechChallenge.ContactUpdate.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.GroupEight.TechChallenge.ContactUpdate.Api.dll"]