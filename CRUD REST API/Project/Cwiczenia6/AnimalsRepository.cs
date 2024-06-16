

using System.Data.SqlClient;


namespace Cwiczenia6;

public class AnimalsRepository : IAnimalRepository
{
    private readonly string _connectionString = "Server=localhost;Database=master;User Id=xPawelskix;Password=Francuz1244;";

    public IEnumerable<Animal> GetAnimalsFromDatabase(String orderBy)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY " + orderBy;
        
        var dr = cmd.ExecuteReader();
        var animals = new List<Animal>();

        while (dr.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = Convert.ToInt32(dr["IdAnimal"]),
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Category = dr["Category"].ToString(),
                Area = dr["Area"].ToString()
            };
            
            animals.Add(animal);
        }
        
        connection.Close();
        return animals;
    }
    
    public Animal GetAnimalByIdFromDatabase(int idAnimal)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);

        var dr = cmd.ExecuteReader();
        Animal animal = null;

        while (dr.Read())
        {
            animal = new Animal()
            {
                IdAnimal = Convert.ToInt32(dr["IdAnimal"]),
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Category = dr["Category"].ToString(),
                Area = dr["Area"].ToString()
            };
        }

        connection.Close();
        return animal;
    }
    
    public void AddAnimalToDatabase(Animal animal)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdateAnimalInDatabase(Animal animal)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public void DeleteAnimalFromDatabase(int idAnimal)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}