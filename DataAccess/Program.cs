using Microsoft.Data.SqlClient;
using System.Text;
using System.Data;
using DataAccess.Models;

char response;
do
{
    List();
    ShowMenuOptions();
    response = GetSelectedOption();
    DoAction(response);
} while (response != 'S');


void ShowMenuOptions()
{
    Console.WriteLine("\n[A]gregar nuevo autor");
    Console.WriteLine("[M]odificar un autor registrado");
    Console.WriteLine("[E]liminar un autor registrado");
    Console.WriteLine("[S]Salir");

}

char GetSelectedOption()
{
    Console.WriteLine();
    Console.Write("Selecione una opcion: ");
    char selectedOption = char.ToUpper(Console.ReadKey().KeyChar);
    Console.WriteLine();
    return selectedOption;
}

void DoAction(char response)
{
    switch (response)
    {
        case 'A':
            Insert();
            break;
        case 'M':
            Modificar();
            break;
        case 'E':
            Eliminar();
            break;
        case 'S':
            Console.WriteLine("Adios");
            break;

        default:
            break;
        

    }
}


void List()
{
    Console.WriteLine($"Autores registrados: {GetTotalofAuthors()}.");

    IEnumerable<Author>authors = GetAllAuthors();

    Console.WriteLine("{0, -12}{1, -30}{2}", "Id", "Nombre", "Apellidos");
    Console.WriteLine("---------------------------------------------------------------");
    foreach(var author in authors)
    {
        Console.WriteLine("{0, -12}{1, -30}{2}", 
            author.Id, 
            author.FirstName, 
            author.LastName);
    }
}

void Insert()
{
    string? firstName;
    string? lastName;

    Console.WriteLine("Modulo de registro de autores.\n");

    Console.Write("Nombre del autor: ");
    firstName = Console.ReadLine();

    Console.Write("Apellidos del autor: ");
    lastName = Console.ReadLine();

if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
    {
        return;
    }

Author author = new Author(firstName, lastName);
Console.WriteLine($"Renglones afectados: {InsertAuthor(author)}");

}

void Eliminar()
{
    int id;

    Console.WriteLine("Modulo de borrado de autores registrados.\n");

    Console.Write("Identidicador del autor: ");
    string idAsString = Console.ReadLine();
    if (string.IsNullOrEmpty(idAsString))
    {
        Console.WriteLine("Debe proporcionar un identicador valido del autor");
        return;
    }
    bool isValid = int.TryParse(idAsString, out id);
    if (isValid)
    {
        DeleteAuthor(id);
        Console.WriteLine($"Se elimino el autor cuyo id. es {id}. ");
    }
    else
    {
        Console.WriteLine("Debe proporcionar un identicador valido del autor");
    }
}

void Modificar()
{
    int id;
    Author author;

    Console.WriteLine("\nModulo de modificacion de datos de autores registrados.\n ");
    Console.WriteLine("id del autor a modificar: ");
    string idAsString = Console.ReadLine();
    bool isValid = int.TryParse(idAsString, out id);
    if (isValid)
    {
        author = GetAuthor(id);
        Console.WriteLine("\nDatos del autor:");
        Console.WriteLine($"Nombre: {author.FirstName}");
        Console.WriteLine($"Nombre: {author.LastName}");

        Console.WriteLine("\nDatos del autor:");
        Console.WriteLine("Nuevos datos.");
        Console.WriteLine("Nuevo nombre: ");
        string nombre = Console.ReadLine();
        Console.WriteLine("Nuevos apellidos: ");
        string apellidos = Console.ReadLine();

        if (!string.IsNullOrEmpty(nombre))
        {
            author.FirstName = nombre;
        }
        if (!string.IsNullOrEmpty(apellidos))
        {
            author.LastName = apellidos;
        }
        updateAuthor(id, author);
    }
}





//string GetConnectionInformation(SqlConnection conexion)
//{
//    StringBuilder s  = new StringBuilder(1024);

//    s.AppendLine($"Informacion de la conexion: ");
//    s.AppendLine($"\tCadena de conexion: {conexion.ConnectionString}");
//    s.AppendLine($"\tEstado: {conexion.State}");
//    s.AppendLine($"\tTiempo de espera: {conexion.ConnectionTimeout.ToString()}");
//    s.AppendLine($"\tBase de datos: {conexion.Database}");
//    s.AppendLine($"\tFuente de datos: {conexion.DataSource}");
//    s.AppendLine($"\tVersion del servidor: {conexion.ServerVersion}");
//    s.AppendLine($"\tId. de la estacion de trabajo: {conexion.WorkstationId}");



//    return s.ToString();
//}
Author GetAuthor(int id)
{
    string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Libros;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


    string sqlCommandString = "SELECT * FROM Authors WHERE AuthorId = @Id";
    SqlDataReader reader;
    Author author = null;

    try
    {
        using (SqlConnection connection = new SqlConnection(connString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sqlCommandString, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@Id", id));
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    reader.Read();
                    author = new Author(
                       reader.GetInt32(0),
                       reader["FirstName"].ToString(),
                       reader["LastName"].ToString()
                       );
                }

            }
        }

    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Se encontro el siguiente error: {ex.Message}");
    }
    return author;
}

int InsertAuthor(Author author)
{
    string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Libros;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    int renglonesAfectados = 0;
    string SqlCommandString = "INSERT INTO Authors (FirstName,LastName) VALUES (@FirstName,@LastName)";

    try
    {
        using (SqlConnection connection = new SqlConnection(connString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(SqlCommandString,connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@FirstName", author.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", author.LastName));
                renglonesAfectados = command.ExecuteNonQuery();
            }
            //Console.WriteLine(GetConnectionInformation(connection));
        }
    }
    catch (Exception ex)
    {

        Console.Error.WriteLine($"Ha ocurrido un erro: {ex.Message}");
        throw;

    }

    return renglonesAfectados;

}

int updateAuthor(int id, Author author)
{
    string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Libros;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    int renglonesAfectados = 0;
string sqlCommandString = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE AuthorId = @AuthorId";

try
{
    using (SqlConnection connection = new SqlConnection(connString))
    {
        connection.Open();
        using (SqlCommand command = new SqlCommand(sqlCommandString, connection))
        {
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@FirstName", author.FirstName));
            command.Parameters.Add(new SqlParameter("@LastName", author.LastName));
            command.Parameters.Add(new SqlParameter("@AuthorId", id));
            renglonesAfectados = command.ExecuteNonQuery();
            Console.WriteLine($"Renglones Afectados: {renglonesAfectados}");
        }
    }

}
catch (Exception ex)
{
    Console.Error.WriteLine($"Se encontro el siguiente error: {ex.Message}");
}
return renglonesAfectados;
}

int DeleteAuthor(int id)
{
    string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Libros;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    int renglonesAfectados = 0;
    string SqlCommandString = "DELETE FROM Authors WHERE AuthorId = @AuthorID";

    try
    {
        using (SqlConnection connection = new SqlConnection(connString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(SqlCommandString, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@AuthorId", id));
                renglonesAfectados = command.ExecuteNonQuery();
            }
            //Console.WriteLine(GetConnectionInformation(connection));
        }
    }
    catch (Exception ex)
    {

        Console.Error.WriteLine($"Ha ocurrido un erro: {ex.Message}");
        throw;

    }

    return renglonesAfectados;

}


IEnumerable<Author> GetAllAuthors()
{
    string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Libros;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

   
    string SqlCommandString = "SELECT * FROM Authors";
    List<Author> authors = new List<Author>();
    SqlDataReader reader = null;
    Author author;

    try
    {
        using (SqlConnection connection = new SqlConnection(connString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(SqlCommandString, connection))
            {
                command.CommandType = CommandType.Text;
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        
                        author = new Author(
                            reader.GetInt32(0), 
                            reader.GetString(1),
                            reader.GetString(2)
                            );
                        //author = new Author(
                        //  (int) reader["AuthorId"],
                        //   reader["FirstName"].ToString(),
                        //   reader["LastName"].ToString()
                        //   );
                        authors.Add(author);
                    }
                }
            }
            //Console.WriteLine(GetConnectionInformation(connection));
        }
    }
    catch (Exception ex)
    {

        Console.Error.WriteLine($"Ha ocurrido un erro: {ex.Message}");
        throw;

    }

    return authors;

}



int GetTotalofAuthors()
{
    string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Libros;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


    string SqlCommandString = "SELECT COUNT(AuthorId) FROM Authors";
    int totalAuthors = 0;
    
    try
    {
        using (SqlConnection connection = new SqlConnection(connString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(SqlCommandString, connection))
            {
                command.CommandType = CommandType.Text;
                totalAuthors = (int)command.ExecuteScalar();
               
            }
            //Console.WriteLine(GetConnectionInformation(connection));
        }
    }
    catch (Exception ex)
    {

        Console.Error.WriteLine($"Ha ocurrido un erro: {ex.Message}");
        throw;

    }

    return totalAuthors;

}