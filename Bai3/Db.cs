using Microsoft.Extensions.Configuration;

namespace bai73;

/// <summary>
/// Đọc connection string từ file appsettings.json (thư mục bin\...\net10.0-windows\).
/// </summary>
public static class Db
{
    public static string ConnectionString { get; } =
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("QuanLyDiem")
        ?? throw new InvalidOperationException(
            "Không tìm thấy connection string \"QuanLyDiem\" trong file appsettings.json.");
}
