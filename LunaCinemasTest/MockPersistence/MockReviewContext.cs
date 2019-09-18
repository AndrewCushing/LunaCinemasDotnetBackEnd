using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasTest.MockPersistence
{
    public class MockReviewContext : IReviewContext
    {
        private List<Review> reviews = new List<Review>();

        public List<Review> FindByFilmId(string filmId)
        {
            return reviews.FindAll(review => review.FilmId.Equals(filmId));
        }

        public void AddReview(Review review)
        {
            reviews.Add(review);
        }

        public List<Review> FindById(string reviewId)
        {
            return reviews.FindAll(review => review.Id.Equals(reviewId));
        }
    }
}