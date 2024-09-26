@echo off

:: Verifica se a rede externa existe
docker network ls | findstr "app-network" > nul
if %errorlevel% neq 0 (
    echo Criando a rede externa app-network...
    docker network create app-network
) else (
    echo Rede app-network já existe.
)

:: Executa o Docker Compose
docker-compose up -d --build