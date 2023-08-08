﻿using Microsoft.EntityFrameworkCore;
using PokemonReviewApp2.Data;
using PokemonReviewApp2.Interfaces;
using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Repository
{
    public class ReviewerRepository : IReviewerRepository

    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
           _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r=>r.Id== reviewerId).Include(r=>r.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviwerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviwerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r=>r.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
           _context.Update(reviewer);
            return Save();

        }
    }
}
