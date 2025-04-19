using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AppDbContext
{
    public DynamoDBContext Context { get; }
    private readonly string _tableName;

    public AppDbContext(IAmazonDynamoDB dynamoDBClient, IConfiguration configuration)
    {
        Context = new DynamoDBContext(dynamoDBClient);
        
        // Get table name from environment variables
        _tableName = Environment.GetEnvironmentVariable("DYNAMODB_TABLE") ?? "TravelBookingApp-Reviews";
    }

    public async Task<List<Review>> GetAllReviewsAsync()
    {
        try
        {
            Console.WriteLine($"Scanning table: {_tableName}");
            var scanConditions = new List<ScanCondition>();
            var reviews = await Context.ScanAsync<Review>(scanConditions).GetRemainingAsync();
            Console.WriteLine($"Found {reviews.Count} reviews");
            return reviews;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetAllReviewsAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task AddReviewAsync(Review review)
    {
        try
        {
            Console.WriteLine($"Adding review with ID: {review.Id}");
            await Context.SaveAsync(review);
            Console.WriteLine("Review added successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddReviewAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<Review> GetReviewAsync(string id)
    {
        try
        {
            Console.WriteLine($"Getting review with ID: {id}");
            var review = await Context.LoadAsync<Review>(id);
            Console.WriteLine(review != null ? "Review found" : "Review not found");
            return review;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetReviewAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task UpdateReviewAsync(Review review)
    {
        try
        {
            Console.WriteLine($"Updating review with ID: {review.Id}");
            await Context.SaveAsync(review);
            Console.WriteLine("Review updated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateReviewAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task DeleteReviewAsync(string id)
    {
        try
        {
            Console.WriteLine($"Deleting review with ID: {id}");
            await Context.DeleteAsync<Review>(id);
            Console.WriteLine("Review deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteReviewAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}






