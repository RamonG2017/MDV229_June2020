namespace CardGame.Constants
{
  public static class AppQueries
  {
    public static string QUERY_RESTAURANTS_PROFILES => @"SELECT 
                                                                id, 
                                                                RestaurantName, 
                                                                Address,
                                                                Phone,
                                                                HoursOfOperation,
                                                                Price,
                                                                USACityLocation,
                                                                Cuisine,
                                                                FoodRating,
                                                                ServiceRating,
                                                                AmbienceRating,
                                                                ValueRating,
                                                                OverallRating,
                                                                OverallPossibleRating
                                                            FROM RestaurantProfiles";

    public static string QUERY_RESTAURANTS_REVIEWS => @"SELECT
                                                                id,
                                                                RestaurantId,
                                                                ReviewScore,
                                                                PossibleReviewScore,
                                                                ReviewText,
                                                                ReviewColor
                                                             FROM RestaurantReviews 
                                                           ";

    public static string QUERY_RESTAURANT_REVIEWER => @"SELECT Id, First, last FROM RestaurantReviewers";
  }
}