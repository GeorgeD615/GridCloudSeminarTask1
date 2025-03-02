using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(logging => {
    logging.LoggingFields = HttpLoggingFields.All;
});
var app = builder.Build();
app.UseHttpLogging();

var connectionString = "Data Source=/data/app.db";

// Главная страница с описанием API
app.MapGet("/", () => """
Minimal API - User Management

Available endpoints:
- /users            -> Get all users
- /users/create?name=JohnDoe  -> Add user
- /users/delete-all -> Delete all users
""");

// Получение всех пользователей
app.MapGet("/users", () => {
    try {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT id, name FROM users";
        var users = new List<object>();
        using var reader = command.ExecuteReader();
        while (reader.Read()) {
            users.Add(new { id = reader.GetInt32(0), name = reader.GetString(1) });
        }
        return Results.Json(users);
    } catch (Exception ex) {
        return Results.Problem($"Ошибка: {ex.Message}");
    }
});

// Добавление нового пользователя
app.MapGet("/users/create", (string name) => {
    try {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO users (name) VALUES (@name)";
        command.Parameters.AddWithValue("@name", name);
        command.ExecuteNonQuery();
        return Results.Ok(new { message = "User added successfully" });
    } catch (Exception ex) {
        return Results.Problem($"Ошибка: {ex.Message}");
    }
});

// Удаление всех пользователей
app.MapGet("/users/delete-all", () => {
    try {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM users";
        command.ExecuteNonQuery();
        return Results.Ok(new { message = "All users deleted successfully" });
    } catch (Exception ex) {
        return Results.Problem($"Ошибка: {ex.Message}");
    }
});

app.Run();
