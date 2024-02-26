using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

static void ShowDataTable(DataTable table) {
    Console.WriteLine($"Table Name: {table.TableName}");

    foreach (DataColumn col in table.Columns) {
        Console.Write($"{col.ColumnName, -20}");
    }
    Console.WriteLine();

    int number_cols = table.Columns.Count;

    foreach (DataRow r in table.Rows) {
        for (int i = 0; i < number_cols; i++) {
            Console.Write($"{r[i], -20}");
        }
        Console.WriteLine();
    }
}

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

var adapter = new MySqlDataAdapter();
adapter.TableMappings.Add("Table", "NhanVien");

// SelectCommand
adapter.SelectCommand = new MySqlCommand("SELECT NhanviennID, Ten, Ho, NgaySinh FROM Nhanvien", connection);

var dataSet = new DataSet();
adapter.Fill(dataSet);

DataTable ?table = dataSet.Tables["NhanVien"];
ShowDataTable(table);


connection.Close();
