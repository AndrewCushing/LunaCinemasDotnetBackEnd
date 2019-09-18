using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockShowingContext : IShowingContext
    {
        private readonly List<Showing> _showings = new List<Showing>();
        public List<Showing> GetByFilmId(string filmId)
        {
            return _showings.FindAll(showing => showing.FilmId.Equals(filmId));
        }

        public Showing GetById(string id)
        {
            List<Showing> searchResult = _showings.FindAll(showing => showing.Id.Equals(id));
            if (searchResult.Count < 1)
            {
                return null;
            }
            return searchResult[0];
        }

        public bool UpdateShowing(Showing oldShowing, Showing newShowing)
        {
            try
            {
                int ind = _showings.IndexOf(oldShowing);
                _showings.RemoveAt(ind);
                _showings.Insert(ind, newShowing);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}