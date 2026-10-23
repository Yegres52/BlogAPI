# BlogAPI

REST API для проекта `Blog`. Требуется .NET SDK 10 и соседний каталог `../Blog`: в нём находятся общие проекты `Blog.Core`, `Blog.Data` и база SQLite `app.db`.

## Структура

- `Controllers/` — HTTP-маршруты и преобразование результатов сервисов в ответы API.
- `Contracts/` — модели запросов, которые принадлежат только API.
- `Extensions/` — регистрация зависимостей, Identity и Swagger.
- `Program.cs` — настройка HTTP-конвейера и запуск приложения.
- `../Blog/src/Blog.Core/` — общие модели и контракты сервисов.
- `../Blog/src/Blog.Data/` — EF Core, миграции и реализации сервисов.

## Запуск

```bash
dotnet restore
dotnet run --launch-profile http
```

Swagger: <http://localhost:5188/swagger>. Настройка базы находится в `appsettings.json`.
