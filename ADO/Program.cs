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
    ["Server"] = "127.0.0.1",
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

// InsertCommand
adapter.InsertCommand = new MySqlCommand("INSERT INTO Nhanvien (Ho, Ten) values (@Ho, @Ten)", connection);
adapter.InsertCommand.Parameters.Add("@Ho", MySqlDbType.VarChar, 255, "Ten");
adapter.InsertCommand.Parameters.Add("@Ten", MySqlDbType.VarChar, 255, "Ho");

// DeleteCommand
adapter.DeleteCommand = new MySqlCommand("DELETE FROM Nhanvien WHERE NhanviennID = @id", connection);
adapter.DeleteCommand.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32, 4, "NhanviennID"));

// UpdateCommand
adapter.UpdateCommand = new MySqlCommand("UPDATE Nhanvien Set Ho = @Ho, Ten = @Ten WHERE NhanviennID = @id", connection);
adapter.UpdateCommand.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32, 4, "NhanviennID"));
adapter.UpdateCommand.Parameters.Add("@Ho", MySqlDbType.VarChar, 255, "Ten");
adapter.UpdateCommand.Parameters.Add("@Ten", MySqlDbType.VarChar, 255, "Ho");


var dataSet = new DataSet();

// Đổ dữ liệu từ Data nguồn đã lấy vào DataSet
adapter.Fill(dataSet);

DataTable ?table = dataSet.Tables["NhanVien"];

ShowDataTable(table);

connection.Close();
