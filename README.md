# Clean Architecture - Template .NET

[![.NET 6.0](https://img.shields.io/badge/.NET-6.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/6.0)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-13+-blue.svg)](https://www.postgresql.org/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

Implementación de Clean Architecture (Arquitectura Limpia) en .NET 6.0, siguiendo los principios de Robert C. Martin (Uncle Bob). Este proyecto demuestra cómo estructurar una aplicación web API con separación clara de responsabilidades y alta mantenibilidad.

## 📋 Tabla de Contenidos

- [Descripción](#-descripción)
- [Arquitectura](#-arquitectura)
- [Tecnologías](#-tecnologías)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Ejecución](#-ejecución)
- [Endpoints de la API](#-endpoints-de-la-api)
- [Principios Aplicados](#-principios-aplicados)
- [Patrones de Diseño](#-patrones-de-diseño)
- [Contribuir](#-contribuir)

## 📖 Descripción

Este proyecto es una plantilla de arquitectura limpia que implementa un sistema básico de gestión de personas (CRUD). Está diseñado para servir como base para aplicaciones empresariales más complejas, demostrando las mejores prácticas en el desarrollo de software.

La arquitectura está organizada en capas concéntricas donde las dependencias apuntan hacia el centro, permitiendo que las reglas de negocio sean independientes de frameworks, bases de datos y otros detalles de implementación.

## 🏗️ Arquitectura

El proyecto sigue la arquitectura de 4 capas propuesta por Robert C. Martin:

```
┌─────────────────────────────────────────────────────────┐
│  1. Frameworks & Drivers (App.API)                      │
│  ┌───────────────────────────────────────────────────┐  │
│  │  2. Interface Adapters                            │  │
│  │  (Controllers, Presenters, Gateways, IoC)         │  │
│  │  ┌─────────────────────────────────────────────┐  │  │
│  │  │  3. Application Business Rules              │  │  │
│  │  │  (Use Cases, Ports, DTOs)                   │  │  │
│  │  │  ┌───────────────────────────────────────┐  │  │  │
│  │  │  │  4. Enterprise Business Rules         │  │  │  │
│  │  │  │  (Entities, Interfaces)               │  │  │  │
│  │  │  └───────────────────────────────────────┘  │  │  │
│  │  └─────────────────────────────────────────────┘  │  │
│  └───────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

### Capas

#### Capa 4: Enterprise Business Rules (Reglas de Negocio Empresariales)
- **App.Entities**: Contiene las entidades del dominio (POCOs) y las interfaces de repositorio
- Independiente de cualquier framework o librería externa
- Define las reglas de negocio más generales y de alto nivel

#### Capa 3: Application Business Rules (Reglas de Negocio de Aplicación)
- **App.UseCases**: Implementa los casos de uso de la aplicación (Interactors)
- **App.UseCasePorts**: Define las interfaces (puertos) para entrada y salida
- **App.DTO**: Objetos de Transferencia de Datos
- Orquesta el flujo de datos hacia y desde las entidades

#### Capa 2: Interface Adapters (Adaptadores de Interfaz)
- **App.Controllers**: Controladores de la API
- **App.Presenters**: Formatea los datos para la presentación
- **App.RepositoryEFCore**: Implementación del repositorio con Entity Framework Core
- **App.IoC**: Configuración de Inyección de Dependencias
- Convierte datos del formato más conveniente para casos de uso y entidades

#### Capa 1: Frameworks & Drivers (Frameworks y Controladores)
- **App.API**: Aplicación ASP.NET Core Web API
- Capa más externa que contiene frameworks y herramientas
- Punto de entrada de la aplicación

## 🛠️ Tecnologías

- **.NET 6.0**: Framework principal
- **ASP.NET Core**: Web API
- **Entity Framework Core**: ORM para acceso a datos
- **PostgreSQL**: Base de datos relacional
- **Npgsql**: Proveedor de PostgreSQL para .NET
- **Swagger/OpenAPI**: Documentación de API

## 📁 Estructura del Proyecto

```
Clean-Architecture/
├── App.API/                          # Capa 1: API Web
│   ├── Program.cs                    # Punto de entrada
│   └── appsettings.json             # Configuración
│
├── App.Controllers/                  # Capa 2: Controladores
│   ├── CreateProductController.cs   # Crear persona
│   └── GetAllPersonController.cs    # Obtener todas las personas
│
├── App.Presenters/                   # Capa 2: Presentadores
│   ├── CreatePersonPresenter.cs
│   ├── GetAllPersonsPresenter.cs
│   └── IPresenter.cs
│
├── App.RepositoryEFCore/            # Capa 2: Repositorio (Gateway)
│   ├── DataContext/
│   │   ├── AppDbContext.cs
│   │   └── AppDbContextFactory.cs
│   ├── Repositories/
│   │   ├── PersonRepository.cs
│   │   └── UnitOfWork.cs
│   └── Migrations/
│
├── App.IoC/                         # Capa 2: Inyección de Dependencias
│   └── DependencyContainer.cs
│
├── App.UseCases/                    # Capa 3: Casos de Uso
│   ├── CreatePerson/
│   │   └── CreatePersonInteractor.cs
│   └── GetAllProducts/
│       └── GetAllPersonsInteractor.cs
│
├── App.UseCasePorts/                # Capa 3: Puertos
│   ├── ICreatePersonInputPort.cs
│   ├── ICreatePersonOutputPort.cs
│   ├── IGetAllPersonInputPort.cs
│   └── IGetAllPersonOutputPort.cs
│
├── App.DTO/                         # Capa 3: DTOs
│   ├── CreatePersonDTO.cs
│   └── PersonDTO.cs
│
├── App.Entities/                    # Capa 4: Entidades
│   ├── POCO/
│   │   └── Person.cs
│   └── Interfaces/
│       ├── IPersonRepository.cs
│       └── IUnitOfWork.cs
│
└── Template.sln                     # Solución
```

## 📋 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) o superior
- [PostgreSQL 13+](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/) (opcional)
- [Git](https://git-scm.com/)

## 🚀 Instalación

1. **Clonar el repositorio**

```bash
git clone https://github.com/Desarrolladores-Net/Clean-Architecture.git
cd Clean-Architecture
```

2. **Restaurar dependencias**

```bash
dotnet restore
```

3. **Compilar la solución**

```bash
dotnet build
```

## ⚙️ Configuración

### Base de Datos

1. **Crear una base de datos en PostgreSQL**

```sql
CREATE DATABASE template;
```

2. **Configurar la cadena de conexión**

Edita el archivo `App.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Template": "Host=127.0.0.1;Port=5432;Database=template;User Id=postgres;Password=tu_contraseña;"
  }
}
```

3. **Aplicar migraciones**

```bash
cd App.API
dotnet ef database update --project ../App.RepositoryEFCore
```

O desde la raíz:

```bash
dotnet ef database update --project App.RepositoryEFCore --startup-project App.API
```

## ▶️ Ejecución

### Ejecutar la API

```bash
cd App.API
dotnet run
```

O desde la raíz:

```bash
dotnet run --project App.API
```

La aplicación estará disponible en:
- **HTTPS**: https://localhost:7001
- **HTTP**: http://localhost:5001
- **Swagger UI**: https://localhost:7001/swagger

## 📡 Endpoints de la API

### Crear una Persona

**POST** `/api/CreateProduct`

Request Body:
```json
{
  "name": "Juan Pérez"
}
```

Response:
```json
{
  "id": 1,
  "name": "Juan Pérez"
}
```

### Obtener Todas las Personas

**GET** `/api/GetAllPerson`

Response:
```json
[
  {
    "id": 1,
    "name": "Juan Pérez"
  },
  {
    "id": 2,
    "name": "María García"
  }
]
```

## 🎯 Principios Aplicados

### SOLID

- **S**ingle Responsibility Principle: Cada clase tiene una única responsabilidad
- **O**pen/Closed Principle: Abierto para extensión, cerrado para modificación
- **L**iskov Substitution Principle: Las abstracciones pueden ser reemplazadas por sus implementaciones
- **I**nterface Segregation Principle: Interfaces específicas en lugar de interfaces generales
- **D**ependency Inversion Principle: Dependencias hacia abstracciones, no hacia implementaciones concretas

### Clean Architecture

- **Independencia de Frameworks**: Las reglas de negocio no dependen de frameworks externos
- **Testeable**: Las reglas de negocio pueden ser probadas sin UI, base de datos, o servicios externos
- **Independencia de la UI**: La UI puede cambiar sin afectar el resto del sistema
- **Independencia de la Base de Datos**: Puedes cambiar PostgreSQL por SQL Server, MongoDB, etc.
- **Independencia de Agentes Externos**: Las reglas de negocio no saben nada del mundo exterior

## 🎨 Patrones de Diseño

### Repository Pattern
Abstrae la lógica de acceso a datos, permitiendo cambiar la implementación sin afectar la lógica de negocio.

### Unit of Work Pattern
Mantiene una lista de objetos afectados por una transacción y coordina la escritura de cambios.

### Dependency Injection
Todas las dependencias se inyectan a través de constructores, facilitando las pruebas y el desacoplamiento.

### Use Case (Interactor) Pattern
Cada caso de uso está encapsulado en su propia clase, facilitando la comprensión y mantenimiento.

### Presenter Pattern
Separa la lógica de presentación de los controladores, permitiendo múltiples formatos de salida.

## 🔄 Flujo de Datos

```
Request (HTTP)
    ↓
Controller (convierte HTTP a DTO)
    ↓
Input Port (interfaz del caso de uso)
    ↓
Use Case Interactor (lógica de aplicación)
    ↓
Repository (acceso a datos)
    ↓
Database
    ↓
Repository (retorna entidades)
    ↓
Use Case Interactor (procesa entidades)
    ↓
Output Port (interfaz de presentación)
    ↓
Presenter (formatea datos)
    ↓
Controller (retorna resultado)
    ↓
Response (HTTP)
```

## 🧪 Extensiones Futuras

Este proyecto puede extenderse con:

- **Validaciones**: FluentValidation para validar DTOs
- **Mapeo de Objetos**: AutoMapper para convertir entre entidades y DTOs
- **Autenticación**: JWT para seguridad de API
- **Logging**: Serilog para registro estructurado
- **Caché**: Redis o Memory Cache
- **Pruebas Unitarias**: xUnit, NUnit o MSTest
- **Documentación Mejorada**: XML comments para Swagger
- **Manejo de Errores**: Middleware de excepciones global
- **CQRS**: Separación de comandos y consultas
- **Event Sourcing**: Para auditoría y trazabilidad

## 🤝 Contribuir

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📚 Referencias

- [Clean Architecture - Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [The Clean Code Blog](https://blog.cleancoder.com/)
- [Microsoft - Clean Architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)

## 👥 Autores

**Desarrolladores.Net**

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo LICENSE para más detalles.

---

⭐️ Si este proyecto te ha sido útil, considera darle una estrella en GitHub!
