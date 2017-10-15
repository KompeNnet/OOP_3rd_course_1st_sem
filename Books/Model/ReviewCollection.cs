using System.Collections.Generic;
using System.Linq;

namespace Books.Model
{
    public class ReviewCollection
    {
        private List<Review> ReviewList;

        public ReviewCollection() { }

        public ReviewCollection(IEnumerable<Review> reviewList)
        {
            ReviewList = reviewList.ToList();
        }

        public bool AddReview(Review review)
        {
            if (ReviewList.Contains(review)) return false;
            ReviewList.Add(review);
            return true;
        }

        public bool RemoveReview(Review review)
        {
            if (!ReviewList.Contains(review)) return false;
            ReviewList.Remove(review);
            return true;
        }

        public List<Review> GetReviewList()
        {
            return ReviewList;
        }
    }
}
