using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text;
using System.IO;

namespace Demo;

public class Helper
{
	[DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
	public static extern int GetValue(string strSection,
								  string strKeyName,
								  string strEmpty,
								  StringBuilder RetVal,
								  int nSize,
								  string strFilePath);

	[DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
	public static extern long WriteValue(string strSection,
									   string strKeyName,
									   string strValue,
									   string strFilePath);

	public static string GenerateHash(string text)
	{
		string salt = "ag16Q^#@)h09h09^#hjoia@(^G!hj";
		byte[] bytes = MD5.HashData(Encoding.ASCII.GetBytes(salt + text));

		return BitConverter.ToString(bytes).Replace("-", "");
	}

	public static Tuple<string, string, string, string> GetSettings()
	{
		if (!File.Exists("Files\\config.ini"))
			File.WriteAllText("Files\\config.ini", "[Settings]\r\nServer=\r\nDatabase=\r\nUser=\r\nPassword=");

		StringBuilder server = new(75);
		StringBuilder database = new(75);
		StringBuilder user = new(75);
		StringBuilder password = new(75);

		_ = GetValue("Settings", "Server", string.Empty, server, 75, "Files\\config.ini");
		_ = GetValue("Settings", "Database", string.Empty, database, 75, "Files\\config.ini");
		_ = GetValue("Settings", "User", string.Empty, user, 75, "Files\\config.ini");
		_ = GetValue("Settings", "Password", string.Empty, password, 75, "Files\\config.ini");

		return new(server.ToString(), database.ToString(), user.ToString(), password.ToString());
	}

	public static void SaveSettings(string server, string database, string user, string password)
	{
		if (!File.Exists("Files\\config.ini"))
			File.WriteAllText("Files\\config.ini", "[Settings]\r\nServer=\r\nDatabase=\r\nUser=\r\nPassword=");

		WriteValue("Settings", "Server", server, "Files\\config.ini");
		WriteValue("Settings", "Database", database, "Files\\config.ini");
		WriteValue("Settings", "User", user, "Files\\config.ini");
		WriteValue("Settings", "Password", password, "Files\\config.ini");
	}

	public static void AddColumn(DataGridView list, string header, string bindingPath, bool isVisible = true)
	{
		var column = new DataGridTextColumn
		{
			Header = header,
			IsReadOnly = true,
			FontSize = list.FontSize,
			Binding = new Binding(bindingPath),
			Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed
		};

		list.Columns.Add(column);
	}

	public static string ExtractPhone(string input) => Regex.Replace(input, @"\D", "");
	public static string FormatPhone(string input) => Regex.Replace(input, @"(\d{1})(\d{3})(\d{3})(\d{2})(\d{2})", "+$1 ($2) $3-$4-$5");
	public static bool IsValidNumber(string input) => Regex.IsMatch(input, @"^79\d{9}$");
}