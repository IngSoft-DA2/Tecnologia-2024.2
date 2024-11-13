# Usamos la imagen oficial del SDK de .NET 8 como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecemos el directorio de trabajo dentro del contenedor
WORKDIR /src

# Copiamos todo el contenido del directorio actual a /src en el contenedor
COPY . .

# Instala dotnet-ef globalmente
RUN dotnet tool install --global dotnet-ef

# Asegura que el path del dotnet tools esté en el PATH del contenedor
ENV PATH="$PATH:/root/.dotnet/tools"