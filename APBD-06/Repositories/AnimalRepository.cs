using System.Data.SqlClient;
using APBD_06.DTOs;
using APBD_06.Enums;

namespace APBD_06.Repositories;

public class AnimalRepository(IConfiguration configuration) : IAnimalRepository
{
    private IConfiguration _configuration = configuration;
    
    public IEnumerable<GetAnimalsResponse> GetAnimals(OrderBy? orderBy)
    {
        var response = new List<GetAnimalsResponse>();
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        var strOrderBy = orderBy switch
        {
            OrderBy.Description => "description;",
            OrderBy.Category => "category;",
            OrderBy.Area => "area;",
            _ => "name;"
        };
        using var cmd = new SqlCommand("SELECT * FROM Animal ORDER BY " + strOrderBy, con);

        cmd.Connection.Open();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            response.Add(new GetAnimalsResponse(
                reader.GetInt32(0),
                reader.GetString(1),
                !reader.IsDBNull(2) ? reader.GetString(2) : null,
                reader.GetString(3),
                reader.GetString(4)
            ));
        }

        return response;
    }

    public int AddAnimal(CreateAnimalRequest animalRequest)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        using var cmd = new SqlCommand();
        if (animalRequest.Description is null)
        {
            cmd.CommandText = "INSERT INTO Animal (Name, Category, Area) values (@Name, @Category, @Area);";
            cmd.Connection = con;
        }
        else
        {
            cmd.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) values (@Name, @Description, @Category, @Area);";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Description", animalRequest.Description);
        }
        cmd.Parameters.AddWithValue("@Name", animalRequest.Name);
        cmd.Parameters.AddWithValue("@Category", animalRequest.Category);
        cmd.Parameters.AddWithValue("@Area", animalRequest.Area);
        cmd.Connection.Open();

        var rowsAffected = cmd.ExecuteNonQuery();
        return rowsAffected;
    }

    public int UpdateAnimal(int id, ReplaceAnimalRequest animalRequest)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        using var cmd = new SqlCommand();
        if (animalRequest.Description is null)
        {
            cmd.CommandText = "UPDATE Animal SET name = @Name, category = @Category, area = @Area WHERE idAnimal = @idAnimal;";
            cmd.Connection = con;
        }
        else
        {
            cmd.CommandText = "UPDATE Animal SET name = @Name, description = @Description, category = @Category, area = @Area WHERE idAnimal = @idAnimal;";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Description", animalRequest.Description);
        }
        cmd.Parameters.AddWithValue("@Name", animalRequest.Name);
        cmd.Parameters.AddWithValue("@Category", animalRequest.Category);
        cmd.Parameters.AddWithValue("@Area", animalRequest.Area);
        cmd.Parameters.AddWithValue("@idAnimal", id);
        cmd.Connection.Open();

        var rowsAffected = cmd.ExecuteNonQuery();
        return rowsAffected;
    }

    public int DeleteAnimal(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        using var cmd = new SqlCommand();
        cmd.CommandText = "DELETE FROM Animal WHERE idAnimal = @idAnimal;";
        cmd.Connection = con;
        
        cmd.Parameters.AddWithValue("@idAnimal", id);
        cmd.Connection.Open();

        var rowsAffected = cmd.ExecuteNonQuery();
        return rowsAffected;
    }
}