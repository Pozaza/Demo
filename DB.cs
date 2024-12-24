using LinqToDB;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Demo;


public class DB
{
	public static DataContext? db { get => GetConnection(); }

	private static DataContext? GetConnection()
	{
		if (!File.Exists("Files\\config.ini"))
			return null;

		IConfiguration config = new ConfigurationBuilder()
		.AddIniFile("Files\\config.ini")
		.Build();

		IConfigurationSection section = config.GetSection("Settings");

		MySqlConnectionStringBuilder builder = new()
		{
			Server = section["Server"],
			Database = section["Database"],
			UserID = section["User"],
			Password = section["Password"],
			CharacterSet = "utf8"
		};

		DataContext connection = new(ProviderName.MySql, builder.ConnectionString)
		{
			CloseAfterUse = true,
		};

		return connection;
	}
}