using Amazon.DynamoDBv2.DataModel;
using System;
using System.ComponentModel.DataAnnotations;

[DynamoDBTable("TravelBookingApp-Reviews")]
public class Review
{
    [DynamoDBHashKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();  // Initialize with a new GUID

    [Required(ErrorMessage = "Name is required")]
    [DynamoDBProperty]
    public string? Name { get; set; }  // Nullable string

    [Required(ErrorMessage = "Rating is required")]
    [DynamoDBProperty]
    public string? Rating { get; set; }  // Changed to string to match frontend

    [Required(ErrorMessage = "Comments are required")]
    [DynamoDBProperty]
    public string? Comments { get; set; }  // Nullable string
}

