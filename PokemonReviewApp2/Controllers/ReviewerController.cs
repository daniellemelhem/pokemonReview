using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp2.Dto;
using PokemonReviewApp2.Interfaces;
using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _revierwerRepository;
        
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository revierwerRepository, IMapper mapper)
        {
            _revierwerRepository = revierwerRepository;
            
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>
                (_revierwerRepository.GetReviewers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviewers);
        }
        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_revierwerRepository.ReviewerExists(reviewerId))
                return NotFound();
            var reviewer = _mapper.Map<ReviewerDto>
                (_revierwerRepository.GetReviewer(reviewerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviewer);
        }
        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByAReviewer(int reviewerId)
        {
            if (!_revierwerRepository.ReviewerExists(reviewerId))
                return NotFound();
            var reviews = _mapper.Map<List<ReviewDto>>(_revierwerRepository.GetReviewsByReviewer(reviewerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);
            var reviewer = _revierwerRepository.GetReviewers().Where(r => r.LastName.Trim().ToUpper()
              == reviewerCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();
            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);
            
           

            if (!_revierwerRepository.CreateReviewer(reviewerMap))
            {

                ModelState.AddModelError("", "Something went wrong while Saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");


        }
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int reviewerId, [FromBody] ReviewerDto updatedReviewer)
        {
            if (updatedReviewer == null)
                return BadRequest(ModelState);
            if (reviewerId != updatedReviewer.Id)
                return BadRequest(ModelState);
            if (!_revierwerRepository.ReviewerExists(reviewerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);
            if (!_revierwerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);

            }
            return NoContent();


        }
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ReviewerOwner(int reviewerId)
        {
            if (!_revierwerRepository.ReviewerExists(reviewerId))
                return NotFound();
            var reviewerToDelete = _revierwerRepository.GetReviewer(reviewerId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_revierwerRepository.DeleteReviewer(reviewerToDelete))

                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            return NoContent();
        }
    }
}
