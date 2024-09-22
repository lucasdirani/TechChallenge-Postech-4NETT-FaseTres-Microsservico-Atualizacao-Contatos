@echo off

:: Verifica se a rede externa existe
docker network ls | findstr "monitoring" > nul
if %errorlevel% neq 0 (
    echo Criando a rede externa monitoring...
    docker network create monitoring
) else (
    echo Rede monitoring já existe.
)

:: Executa o Docker Compose
docker-compose up -d --build