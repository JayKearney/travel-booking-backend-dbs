using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;

namespace TravelBooking_Backend_Jesica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ILogger<ReviewController> _logger;
    private readonly AppDbContext _dbContext;

    public ReviewController(ILogger<ReviewController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("GetReviews")]
    public async Task<IActionResult> GetReviews()
    {
        try
        {
            var reviews = await _dbContext.GetAllReviewsAsync();
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting reviews.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview([FromBody] Review newReview)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            newReview.Id = Guid.NewGuid().ToString();
            await _dbContext.AddReviewAsync(newReview);
            return CreatedAtAction(nameof(GetReviews), new { id = newReview.Id }, newReview);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding review.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("UpdateReview/{id}")]
    public async Task<IActionResult> UpdateReview(string id, [FromBody] Review updatedReview)
    {
        if (updatedReview == null || id != updatedReview.Id)
        {
            return BadRequest("Review data is undefined");
        }

        try
        {
            var existingReview = await _dbContext.GetReviewAsync(id);
            if (existingReview == null)
            {
                return NotFound("Review not found");
            }

            existingReview.Name = updatedReview.Name;
            existingReview.Rating = updatedReview.Rating;
            existingReview.Comments = updatedReview.Comments;

            await _dbContext.UpdateReviewAsync(existingReview);
            return Ok("Review updated successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating review.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("DeleteReview/{id}")]
    public async Task<IActionResult> DeleteReview(string id)
    {
        try
        {
            var existingReview = await _dbContext.GetReviewAsync(id);
            if (existingReview == null)
            {
                return NotFound("Review not found");
            }

            await _dbContext.DeleteReviewAsync(id);
            return Ok("Review deleted successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting review.");
            return StatusCode(500, "Internal server error");
        }
    }
}

