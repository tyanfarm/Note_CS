using System.Data.Common;
using MySql.Data.MySqlClient;

var sqlStringBuilder = new MySqlConnectionStringBuilder
{
    ["Server"] = "localhost",
    ["Database"] = "tyanlab",

    // UserID
    ["UID"] = "root",
    ["PWD"] = "abc123",

    // Port ảo của MySQL trong Docker
    ["Port"] = "3307"
};

var sqlStringConnection = sqlStringBuilder.ToString();
Console.WriteLine(sqlStringConnection);

// using giải phóng `MySqlConnection`
using var connection = new MySqlConnection(sqlStringConnection);

connection.Open();

// QUERY
using var command = new MySqlCommand();
command.Connection = connection;
command.CommandText = "SELECT DanhmucID, TenDanhMuc, MoTa FROM Danhmuc WHERE DanhmucID >= @value;";

var danhMucID = command.Parameters.AddWithValue("@value", 6);


var dataReader = command.ExecuteReader();

while(dataReader.Read()) {
    Console.WriteLine($"{dataReader["TenDanhMuc"], -15} {dataReader["MoTa"], -10}");
}

connection.Close();
