using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private readonly static string url = "http://localhost:4798/api/movies";

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:4798/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            RunAsync().GetAwaiter().GetResult();

        }


        static async Task<List<Movie>> GetAllMoviesAsync()
        {
            var path = "http://localhost:4798/api/movies";
            List<Movie> movies = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                movies = await response.Content.ReadAsAsync<List<Movie>>();
            }
            return movies;
        }

        static async Task<Movie> GetMovieAsync(string movieID)
        {
            var path = "http://localhost:4798/api/movies" + $"/{movieID}";
            Movie movie = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                movie = await response.Content.ReadAsAsync<Movie>();
            }
            return movie;
        }

        static async Task<HttpStatusCode> CreateMovieAsync(Movie movie)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                url, movie);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        static async Task<HttpStatusCode> DeleteMovieAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                url + $"/{id}");
            return response.StatusCode;
        }

        static void ShowMovie(Movie movie)
        {
            Console.WriteLine($"ID: {movie.ID}\nDirectorID:{movie.DirectorID}\nGenreID: {movie.GenreID}\nTitle: {movie.Title}\nDescription: {movie.Description}\nReleaseDate: {movie.ReleaseDate}");
        }

        static void ShowAllMovies(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine($"ID: {movie.ID}\nDirectorID:{movie.DirectorID}\nGenreID: {movie.GenreID}\nTitle: {movie.Title}\nDescription: {movie.Description}\nReleaseDate: {movie.ReleaseDate}");
                Console.WriteLine();
            }
        }

        static async Task RunAsync()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Movie App Interface!\n\nJust simply enter any of the following commands:\n- get - to find a movie by id \n- getAll - to show all movies\n- del - to delete a movie\n- add - to add a movie\n- q - to exit");
            Console.WriteLine();
            client.BaseAddress = new Uri("http://localhost:4798/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            await Input();
        }

        public static async Task Input()
        {
            for (; ; )
            {
                Console.WriteLine("Enter command...");
                string command = Console.ReadLine();
                string input;
                string getByID = "get";
                string getAll = "getAll";
                string add = "add";
                string del = "del";
                string txt = "txt";
                string q = "q";
                string[] commands = { getByID, getAll, add, del, txt, q };
                if (command == getByID)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Enter Id of movie:");
                        input = Console.ReadLine();
                        Console.WriteLine();
                        var movie = await GetMovieAsync(input);
                        ShowMovie(movie);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (command == getAll)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Showing all movies:");
                        Console.WriteLine();
                        var movies = await GetAllMoviesAsync();
                        ShowAllMovies(movies);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                if (command == add)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Enter data to insert a movie:");
                        Console.WriteLine();
                        Console.WriteLine($"Enter DirectorID:");
                        var directorID = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter GenreID: ");
                        var genreID = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter Title:");
                        var title = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter Description:");
                        var description = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter ReleaseDate:");
                        var releaseDate = Console.ReadLine();
                        
                        var movie = new Movie
                        {
                            DirectorID = int.Parse(directorID),
                            GenreID = int.Parse(genreID),
                            Title = title,
                            Description = description,
                            ReleaseDate = DateTime.Parse(releaseDate)
                        };
                        var statusCode = await CreateMovieAsync(movie);
                        Console.WriteLine($"Created (HTTP Status = {(int)statusCode})");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (command == del)
                {
                    Console.Clear();
                    Console.WriteLine("Enter id of movie to delete:");
                    input = Console.ReadLine();
                    var statusCode = await DeleteMovieAsync(input);
                    Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                }
                if (command == "q")
                {
                    Console.WriteLine();
                    Console.WriteLine("Closing program...");
                    break;
                }
                if (!commands.Contains(command))
                {
                    Console.WriteLine("Wrong command!");
                    Console.WriteLine();
                }
            }
        }
    }
}
