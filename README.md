# interview-labs

A monorepo of interview prep across the .NET stack — pure C#, ASP.NET Core Web APIs, Azure Functions, and an Angular front-end. Mostly a place to drill specific patterns under one solution.

## Layout

| Folder | What it is |
|---|---|
| `csharp/` | Pure-C# katas / problems |
| `dotnet-api/` | ASP.NET Core Web API examples (TodoApi + a "Fast" minimal-API variant) |
| `azure-function/` | Azure Functions (.NET 8 isolated) — `todo-func` |
| `mugMaker/` | ASP.NET Core backend + Angular (Vite) front-end (`mugmaker-ui`) |
| `docs/` | Notes |

## Running individual pieces

```bash
# C# katas
dotnet run --project csharp/<project>

# ASP.NET Core API
dotnet run --project dotnet-api/TodoApi

# Azure Function locally (requires Azure Functions Core Tools)
cd azure-function/todo-func && func start

# Angular UI
cd mugMaker/mugmaker-ui && npm install && npm run dev
```

## Stack

C# / .NET 8, ASP.NET Core, Azure Functions, Angular + Vite.
