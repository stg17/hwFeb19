using System.Data.SqlClient;

namespace hwFeb19.Models
{
    public class CarManager
    {
        private readonly string _connectionString;
        public CarManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Car> GetCars(string sort)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Cars ";
            if (sort == "desc")
            {
                command.CommandText += "ORDER BY Year DESC";
            }
            else if (sort == "asc")
            {
                command.CommandText += "ORDER BY Year ASC";
            }
            connection.Open();
            List<Car> cars = new();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                cars.Add(new()
                {
                    Make = (string)reader["Make"],
                    Model = (string)reader["Model"],
                    Year = (int)reader["Year"],
                    Price = (decimal)reader["Price"],
                    CarType = (CarType)reader["CarType"],
                    HasLeatherSeats = (bool)reader["HasLeatherSeats"]
                });
            }
            return cars;
        }

        public List<Car> GetCars()
        {
            return GetCars("");
        }

        public void InsertCar(Car car)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Cars 
                                    VALUES (@make, @model, @year, @price, @carType, @hls)";
            command.Parameters.AddWithValue("@make", car.Make);
            command.Parameters.AddWithValue("@model", car.Model);
            command.Parameters.AddWithValue("@year", car.Year);
            command.Parameters.AddWithValue("@price", car.Price);
            command.Parameters.AddWithValue("@carType", car.CarType);
            command.Parameters.AddWithValue("@hls", car.HasLeatherSeats);
            connection.Open();
            command.ExecuteNonQuery();
        }

    }

    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public CarType CarType { get; set; }
        public bool HasLeatherSeats { get; set; }
    }

    public enum CarType
    { 
        SUV,
        Sedan,
        SuperCar
    }
}
