using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Models
{
  /// <summary>
  /// Card Player Container
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class CardPlayer<T>
  {
    public CardPlayer(int id, T player, Func<string> getName)
    {
      Id = id;
      Player = player;
      Cards = new List<GameCard>();
      PlayerName = getName;
    }

    /// <summary>
    /// Player number
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Generic Support For multiple model entities
    /// </summary>
    private T Player { get; }

    /// <summary>
    /// Assigned deck
    /// </summary>
    public List<GameCard> Cards { get; }

    /// <summary>
    /// Current player name
    /// </summary>
    private Func<string> PlayerName { get; }

    /// <summary>
    /// Get current player name
    /// </summary>
    public string GetPlayerName => PlayerName();

    /// <summary>
    /// Add new card to player's deck
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(GameCard card)
    {
      Cards.Add(card);
    }

    /// <summary>
    /// Total sum of player's deck
    /// </summary>
    /// <returns></returns>
    public int CardValueSummary()
    {
      return Cards.Sum(a => a.Value);
    }
  }
}