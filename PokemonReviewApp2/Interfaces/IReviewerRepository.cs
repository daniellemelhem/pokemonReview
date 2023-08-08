﻿using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        ICollection<Review> GetReviewsByReviewer(int reviwerId);
        bool ReviewerExists (int reviewerId);
        bool CreateReviewer (Reviewer reviewer);
        bool UpdateReviewer (Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();

    }
}
