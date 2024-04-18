using System.Data.SqlClient;
using System.Diagnostics;
using APBD_06.Enums;
using APBD_06.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace APBD_06.Repositories;

public class AnimalRepository(IConfiguration configuration) : IAnimalRepository
{
    private IConfiguration _configuration = configuration;
    
    public IEnumerable<Animal> GetAnimals(OrderBy? orderBy)
    {
        var response = new List<Animal>();
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        var strOrderBy = orderBy switch
        {
            OrderBy.Description => "description",
            OrderBy.Category => "category",
            OrderBy.Area => "area",
            _ => "name"
        };
        using var cmd = new SqlCommand("SELECT * FROM Animal ORDER BY " + strOrderBy, con);

        cmd.Connection.Open();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            response.Add(new Animal(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4)
            ));
        }

        return response;
    }
}