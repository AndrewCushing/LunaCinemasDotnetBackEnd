using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class FilmGrabber
    {
        public ResponseObject<Film> GetAll()
        {
            return new ResponseObject<Film>(true, "Got all the films", new List<Film>());
        }

        public ResponseObject<Film> Search(string searchText)
        {
            return new ResponseObject<Film>(true, $"Got all the films using the search query {searchText}", new List<Film>());
        }
    }
}