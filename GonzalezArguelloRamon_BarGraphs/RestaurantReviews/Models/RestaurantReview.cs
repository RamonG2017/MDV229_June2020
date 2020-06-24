namespace RestaurantReviews.Models
{
  public class RestaurantReview
  {
    public RestaurantReview()
    {
      Id = default;
      RestaurantId = default;
      ReviewScore = default;
      PossibleReviewScore = default;
      ReviewText = string.Empty;
      ReviewColor = string.Empty;
    }

    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int ReviewScore { get; set; }
    public int PossibleReviewScore { get; set; }
    public string ReviewText { get; set; }
    public string ReviewColor { get; set; }
  }
}