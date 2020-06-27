using System;
using CardGame.Constants;

namespace CardGame.Helpers
{
  /// <summary>
  ///   Chart Generator Helper
  /// </summary>
  public class ChartHelper
  {
    /// <summary>
    ///   Set the limit of the base scale to implement
    /// </summary>
    private int BaseScale => ApplicationConfig.BASE_SCALE;

    /// <summary>
    ///   Default foreground color
    /// </summary>
    private ConsoleColor DefaultColor => ConsoleColor.Gray;

    /// <summary>
    ///   Round the average rating to the closest lower int number
    /// </summary>
    /// <param name="rating"></param>
    /// <returns></returns>
    private int roundRating(double rating)
    {
      return (int) Math.Floor(rating);
    }

    /// <summary>
    ///   Get foreground color base on the qualification rating
    /// </summary>
    /// <param name="rating">restaurant average evaluation</param>
    /// <returns>Green | Yellow | Red</returns>
    private ConsoleColor GetColor(double rating)
    {
      var result = DefaultColor;
      // set the rating to the closest lower number
      var roundedRating = roundRating(rating);
      if (roundedRating >= 70)
        result = ConsoleColor.Green;
      if (roundedRating >= 31 && roundedRating <= 69)
        result = ConsoleColor.Yellow;
      if (roundedRating <= 30)
        result = ConsoleColor.Red;
      return result;
    }

    /// <summary>
    ///   Generate Chart for the selected rating
    /// </summary>
    /// <param name="rating"></param>
    public void Generate(double rating)
    {
      // calculate the amount of chars to be print using
      // the corresponding color for the average rating
      // example:
      // Average rating: 90.18 
      // Rounded average: 90
      // Color portion: 9 out 10 (base scale)
      var colorPortion = roundRating(rating) / BaseScale;
      var categoryColor = GetColor(rating);
      var currentColor = Console.BackgroundColor;
      for (var graphPortion = 0; graphPortion < BaseScale; graphPortion++)
      {
        Console.BackgroundColor = graphPortion < colorPortion ? categoryColor : DefaultColor;
        Console.Write(" ");
      }

      // reset console to its default values for encoding and foreground color
      Console.BackgroundColor = currentColor;
      Console.WriteLine("\n");
    }
  }
}