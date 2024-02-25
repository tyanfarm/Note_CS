using System.Data;
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
command.CommandText = "DELETE FROM Shippers WHERE Hoten = 'shibal'";

var result = command.ExecuteNonQuery();
Console.WriteLine(result);
// string[] hotenArr = {"Tyan", "Tlyishere", "Scul"};
// string[] sdtArr = {"0387970037", "0966579440", "0956123456"};

// var hoten = command.Parameters.AddWithValue("@hoten", "aaa");
// var sdt = command.Parameters.AddWithValue("@sdt", "0000");


// for (int i = 0; i < 3; i++) {
//     hoten.Value = hotenArr[i];
//     sdt.Value = sdtArr[i];
//     var result = command.ExecuteNonQuery();
//     Console.WriteLine(result);
// }


connection.Close();
