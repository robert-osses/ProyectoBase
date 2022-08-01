# SCRIPTs HABITUALES

# Ejecución (desde la carpeta del proyecto principal [Proyecto.Web])
dotnet run

# Compilar/Limpiar
dotnet build
dotnet clean

# Publicación (Distribución para Linux x64)
dotnet publish -r linux-x64 -o ../Deploy



# AYUDA .NET 6

# crea una nueva solución vacia
dotnet new sln -n "PROYECTO"

# crea un proyecto webapi
dotnet new webapi -o Proyecto.Web
# dotnet new webapi -n "Proyecto.Web"

# crea un proyecto de tipo librería
dotnet new classlib -o Proyecto.Entidades
dotnet new classlib -o Proyecto.Datos

# agrega un proyecto a la solución
dotnet sln Proyecto.sln add ./Proyecto.Web/Proyecto.Web.csproj
dotnet sln Proyecto.sln add ./Proyecto.entidades/Proyecto.Entidades.csproj
dotnet sln Proyecto.sln add ./Proyecto.datos/Proyecto.Datos.csproj

# Añade una referencia de datos a web
# Primero referencias la bilbioteca de entidades a la biblioteca de datos
dotnet add ./Proyecto.Datos/Proyecto.Datos.csproj reference Proyecto.entidades/Proyecto.entidades.csproj

# Segundo referencias las dos bibliotecas al proyecto web
dotnet add ./Proyecto.Web/Proyecto.Web.csproj reference Proyecto.Datos/Proyecto.Datos.csproj
dotnet add ./Proyecto.Web/Proyecto.Web.csproj reference Proyecto.Entidades/Proyecto.Entidades.csproj