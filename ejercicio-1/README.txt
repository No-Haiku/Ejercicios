# Palabras Mágicas

## Descripción

**Palabras Mágicas** es una aplicación de consola desarrollada en **.NET Core 8** que permite al usuario ingresar una palabra y realizar las siguientes funciones:

1. **Inversión de Palabra**: Devuelve la palabra ingresada al revés.
2. **Determinación de Capicúa**: Verifica si la palabra ingresada es un capicúa (palabra que se lee igual de izquierda a derecha que de derecha a izquierda).
3. **Contar Vocales**: Cuenta el número de vocales en la palabra ingresada.

## Requisitos del sistema

- .NET Core 8.0 SDK o superior
- Sistema operativo compatible con .NET Core (Windows, macOS, Linux)

## Cómo compilar y ejecutar la aplicación

### Clonar el repositorio

Primero, clona el repositorio en tu máquina local:

```bash
https://github.com/No-Haiku/Ejercicios.git


Aquí tienes una estructura básica para el archivo README.md de tu proyecto "Palabras Mágicas". Puedes ajustarlo según sea necesario.

markdown
Copiar código
# Palabras Mágicas

## Descripción

**Palabras Mágicas** es una aplicación de consola desarrollada en **.NET Core 8** que permite al usuario ingresar una palabra y realizar las siguientes funciones:

1. **Inversión de Palabra**: Devuelve la palabra ingresada al revés.
2. **Determinación de Capicúa**: Verifica si la palabra ingresada es un capicúa (palabra que se lee igual de izquierda a derecha que de derecha a izquierda).
3. **Contar Vocales**: Cuenta el número de vocales en la palabra ingresada.

## Requisitos del sistema

- .NET Core 8.0 SDK o superior
- Sistema operativo compatible con .NET Core (Windows, macOS, Linux)

## Cómo compilar y ejecutar la aplicación

### Clonar el repositorio

Primero, clona el repositorio en tu máquina local:

```bash
git clone https://github.com/tuusuario/PalabrasMagicas.git

Compilar la aplicación
Navega al directorio del proyecto y ejecuta el siguiente comando para compilar la aplicación:

dotnet build


Ejecutar la aplicación
Para ejecutar la aplicación de consola, usa el siguiente comando desde la carpeta raíz del proyecto:

dotnet run --project PalabrasMagicas

Interacción en consola
Una vez que la aplicación esté corriendo, se te pedirá ingresar una palabra. Dependiendo de las opciones que elijas, la aplicación te mostrará la palabra invertida, te dirá si es capicúa y contará las vocales presentes en la palabra.

Ejecutar pruebas
Para ejecutar las pruebas unitarias incluidas en el proyecto, utiliza el siguiente comando:

dotnet test

PalabrasMagicas/
│
├── PalabrasMagicas/                # Proyecto principal de la aplicación
│   ├── Program.cs                  # Contiene la lógica principal de la aplicación
│   └── PalabrasMagicas.csproj      # Archivo de configuración del proyecto
│
└── PalabrasMagicas.Tests/          # Proyecto de pruebas
    ├── PalabrasMagicasTests.cs     # Contiene los casos de pruebas unitarias
    └── PalabrasMagicas.Tests.csproj # Archivo de configuración del proyecto de pruebas

Funcionalidades

Invertir palabra:

El usuario ingresa una palabra y la aplicación devuelve la palabra invertida.
Ejemplo: Si el usuario ingresa "Hola", la aplicación devolverá "aloH".

Determinación de capicúa:

La aplicación verifica si la palabra ingresada es un capicúa.
Ejemplo: Si el usuario ingresa "anilina", la aplicación devolverá "La palabra es capicúa".

Contar vocales:

La aplicación cuenta cuántas vocales hay en la palabra ingresada.
Ejemplo: Si el usuario ingresa "Hola", la aplicación devolverá "Número de vocales: 2".

Buenas prácticas aplicadas

Se utiliza manejo de excepciones para evitar errores ante entradas inválidas.
Pruebas unitarias incluidas con xUnit.
Código legible y estructurado siguiendo buenas prácticas de programación.

Tecnologías utilizadas

.NET Core 8.0
xUnit para las pruebas unitarias
