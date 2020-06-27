using System;

namespace CardGame.Models
{
  /// <summary>
  ///   Game Card Model
  /// </summary>
  public class GameCard
  {
    public GameCard()
    {
      Id = default;
      Value = default;
      Suit = string.Empty;
      Icon = string.Empty;
      Color = ConsoleColor.White;
    }

    /// <summary>
    ///   Identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///   Card Value
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    ///   Suit Group
    /// </summary>
    public string Suit { get; set; }

    /// <summary>
    ///   Suit Icon
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Card Color
    /// </summary>
    public ConsoleColor Color { get; set; }

    /// <summary>
    ///   Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Every time a method that wants to print will look for the method to string
    /// we are overriding its behaviour with a more readable one
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"{Name.ToString().PadLeft(3,' ')} {Icon} ";
    }
  }
}