namespace DataAccess.Migrations
{
    using DatabaseStructure.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.DbEntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.DbEntitiesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Directors.AddOrUpdate(new Director[] {
                new Director() { FirstName = "Steven", LastName="Spielberg",City="Cincinnati"},
        new Director() {  FirstName = " George", LastName="Lucas",City="California" },
        new Director() { FirstName="Peter", LastName="Jackson", City="Pukerua Bay"},
        new Director() { FirstName="Woody", LastName="Allen", City="Brooklyn" }
        });

            context.Genres.AddOrUpdate(new Genre[]
            {
                new Genre(){Name = "Thriller"},
                new Genre() {Name = "Adventure"},
                new Genre(){Name = "Fantasy"},
                new Genre(){Name = "Mystery"},
                new Genre(){Name = "Comedy"},
                new Genre(){Name = "Romance"},
                new Genre(){Name = "Sci-fi"}
            });

            context.Movies.AddOrUpdate(new Movie[] {
        new Movie() { Title= "Jurassic Park", GenreID = 1,
            ReleaseDate = new DateTime(1993, 09, 17), DirectorID = 1, Description =
        "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok."},

        new Movie() { Title = "Indiana Jones and the Last Crusade", GenreID = 2,
            ReleaseDate = new DateTime(1989, 05, 24), DirectorID = 1, Description =
            "In 1938, after his father Professor Henry Jones, Sr. goes missing while pursuing the Holy Grail, Indiana Jones finds himself up against Adolf Hitler's Nazis again to stop them obtaining its powers."},

        new Movie() { Title = "Star Wars: Episode IV - A New Hope", GenreID = 7,
            ReleaseDate = new DateTime(1977, 09, 10), DirectorID = 2, Description =
            "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader."},

        new Movie() {  Title = "The Lord of the Rings: The Fellowship of the Ring", GenreID = 3,
            ReleaseDate = new DateTime(2001, 03, 05), DirectorID = 3, Description =
            "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron."},

        new Movie() { Title = "Manhattan", GenreID = 6,
            ReleaseDate = new DateTime(1979, 11, 02), DirectorID = 4, Description =
            "The life of a divorced television writer dating a teenage girl is further complicated when he falls in love with his best friend's mistress."},
    });
        }
    }
}
