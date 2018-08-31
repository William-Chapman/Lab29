using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lab29.Models;

namespace Lab29.Controllers
{
    public class FilmController : ApiController
    {
        // Get all films
        public List<film> GetAllFilms()
        {
            // URL
            // https://localhost:port/api/Film/GetAllFilms

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Return all films
            return ORM.films.ToList();
        }

        // Get list of films in a genre(userinput)
        public List<film> GetFilmsInGenre(string genre)
        {
            // URL
            // https://localhost:port/api/Film/GetFilmsInGenre?genre={genre}

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Return films in genre
            return ORM.films.Where(x => x.genre.ToLower() == genre.ToLower()).ToList();
        }

        // Get a random film
        public film GetRandomFilm()
        {
            // URL
            // https://localhost:port/api/Film/GetRandomFilm

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Create random and list of films
            Random rand = new Random();
            List<film> filmList = ORM.films.ToList();

            // Return random film
            return filmList[rand.Next(0, filmList.Count - 1)];
        }

        // Get a random film from a genre(userinput)
        public film GetRandomFilmInGenre(string genre)
        {
            // URL
            // https://localshost:port/api/Film/GetRandomFilmInGenre?genre={genre}

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Create random and list of films in genre
            Random rand = new Random();
            List<film> filmsInGenre = ORM.films.Where(x => x.genre.ToLower() == genre.ToLower()).ToList();

            // Return random film in genre
            return filmsInGenre[rand.Next(0, filmsInGenre.Count - 1)];
        }

        // Get a list of random films (quantity=userinput)
        public List<film> GetRandomFilmList(int quantity)
        {
            // URL
            // https://localhost:port/api/Film/GetRandomFilmList?quantity={quantity}

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Create film list and random film list
            List<film> filmList = ORM.films.ToList();
            List<film> randomFilmList = new List<film> { };

            // For loop to create random film list
            for (int i = 0; i < quantity; i++)
            {
                Random rand = new Random();
                int index = rand.Next(0, filmList.Count - 1);
                int lastIndex = -1;
                while (index != lastIndex)
                {
                    randomFilmList.Add(filmList[index]);
                    lastIndex = index;
                }
            }

            // Return random film list
            return randomFilmList;
        }

        // Get a list of all genres
        public List<string> GetAllGenres()
        {
            // URL
            // https://localhost:port/api/Film/GetAllGenres

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Return all genres
            return ORM.films.Where(x => x.genre != null).Select(x => x.genre).Distinct().ToList();
        }

        // Get info of a specific film(userinput)
        public string GetFilmInfo(string title)
        {
            // URL
            // https://localhost:port/api/Film/GetFilmInfo?title={title}

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Find film and seperate info
            List<film> chosenFilm = ORM.films.Where(x => x.title.ToLower() == title.ToLower()).ToList();
            string filmID = chosenFilm[0].id.ToString();
            string filmFilePath = chosenFilm[0].filepath;
            string filmTitle = chosenFilm[0].title;
            string filmGenre = chosenFilm[0].genre;

            // Return film info
            return $"ID: {filmID} Title: {filmTitle} Genre: {filmGenre} File Path: {filmFilePath}";
        }

        // Get list of films that contain a keyword(userinput)
        public List<film> GetFilmListByKeyword(string keyword)
        {
            // URL
            // https://localhost:port/api/Film/GetFilimListByKeyword?keyword={keyword}

            // ORM
            mediaEntities ORM = new mediaEntities();

            // Find whatever titles contain the keyword
            List<film> filmList = ORM.films.ToList();
            List<film> chosenFilmList = new List<film> { };
            foreach (film item in filmList)
            {
                if(item.title.ToLower().Contains(keyword.ToLower()))
                    chosenFilmList.Add(item);                
            }

            // Return films where the titles contain the keyword
            return chosenFilmList;
        }
    }
}