# ✅ ToDoList

Base de una aplicación web de lista de tareas con usuarios.

![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_MVC-5C2D91?style=flat-square&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)

## ✨ Funcionalidades

**Implementado**
- Registro, inicio y cierre de sesión con cookies.
- Roles (`Admin`, `User`) y estados de usuario (`Active`, `Inactive`).
- Modelo de datos de tareas con Entity Framework Core y migraciones.

**Pendiente**
- Pantallas para crear, editar y completar tareas.

## 🚀 Cómo correrlo

1. Requisitos: .NET 8 SDK y SQL Server (LocalDB sirve).
2. Revisá `ToDoListConnection` en `appsettings.json`.
3. Aplicá las migraciones: `dotnet ef database update --project ToDoList`.
4. `dotnet run --project ToDoList`
