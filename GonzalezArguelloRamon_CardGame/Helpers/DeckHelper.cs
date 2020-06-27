using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CardGame.Models;

namespace CardGame.Helpers
{
  /// <summary>
  ///   Card Deck Helper
  /// </summary>
  public class DeckHelper
  {
    /// <summary>
    ///   Initializer
    /// </summary>
    public DeckHelper()
    {
      Suits = new List<KeyValuePair<string, string>>
      {
        new KeyValuePair<string, string>("Spades", "♠"),
        new KeyValuePair<string, string>("Hearts", "♥"),
        new KeyValuePair<string, string>("Clubs", "♣"),
        new KeyValuePair<string, string>("Diamonds", "♦")
      };
      Cards = new List<GameCard>();
      UsedCards = new List<GameCard>();
      Names = new Hashtable
      {
        {1, "A"},
        {2, "2"},
        {3, "3"},
        {4, "4"},
        {5, "5"},
        {6, "6"},
        {7, "7"},
        {8, "8"},
        {9, "9"},
        {10, "10"},
        {11, "J"},
        {12, "Q"},
        {13, "K"}
        
      };
    }

    /// <summary>
    ///   Card Container
    /// </summary>
    private List<GameCard> Cards { get; }

    /// <summary>
    ///   Used Cards
    /// </summary>
    private List<GameCard> UsedCards { get; }

    /// <summary>
    ///   Card Suits
    /// </summary>
    private List<KeyValuePair<string, string>> Suits { get; }

    private Hashtable Names { get; set; }

    /// <summary>
    ///   Get the complete deck
    /// </summary>
    /// <returns></returns>
    public IEnumerable<GameCard> Get()
    {
      if (!Cards.Any())
        Add();

      return Cards;
    }

    /// <summary>
    ///   Add Cards to the deck
    /// </summary>
    private void Add()
    {
      // quick helper
      string[] reds = {"Hearts", "Diamonds"};
      // Add card of each suit type
      foreach (var suit in Suits)
      {
        // add 13 cards of the current suit
        for (var i = 1; i <= 13; i++)
        {
          // print this message to the debug console of visual studio
          Debug.WriteLine($"DeckHelper.Add: Adding Suite: {suit} - Card: {i} - Value: {SetValue(i)}");
          Cards.Add(new GameCard
          {
            Id = i,
            Suit = suit.Key,
            Value = SetValue(i),
            Icon = suit.Value,
            Color = reds.Contains(suit.Key) ? ConsoleColor.Red : ConsoleColor.Black,
            Name = Names[i].ToString(),
          });
        }
      }

      // print this message to the debug console of visual studio
      Debug.WriteLine($"DeckHelper.Add: Total Cards: {Cards.Count()}");
      Debug.WriteLine($"DeckHelper.Add: Total Value: {Cards.Sum(a => a.Value)}");
    }

    /// <summary>
    ///   Set card value
    /// </summary>
    /// <param name="i">Card Identifier</param>
    /// <returns>Card Value</returns>
    private int SetValue(int i)
    {
      var value = i;
      if (i == 1) value = 15;
      if (i > 10 && i <= 13) value = 12;
      return value;
    }

    /// <summary>
    ///   Get random card from the current deck
    /// </summary>
    /// <returns></returns>
    public GameCard Take()
    {
      var random = new Random();
      var index = random.Next(Cards.Count);
      var card = Cards[index];
      Cards.Remove(card);
      UsedCards.Add(card);
      // print this message to the debug console of visual studio
      Debug.WriteLine($"DeckHelper.Take: Random Index: {index}");
      Debug.WriteLine($"DeckHelper.Take: Card: Id {card.Id} - Suit: {card.Suit} - Value: {card.Value}");
      Debug.WriteLine($"DeckHelper.Take: Cards Length: {Cards.Count}");
      Debug.WriteLine($"DeckHelper.Take: Used Cards Length: {UsedCards.Count}");
      return card;
    }

    /// <summary>
    ///   Random player from generic data source
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IEnumerable<T> GetPlayers<T>(List<T> data)
    {
      var random = new Random();
      var result = new List<T>();
      for (var i = 0; i <= 3; i++)
      {
        var index = random.Next(data.Count);
        var player = data[index];
        data.Remove(player);
        result.Add(player);
      }

      return result;
    }
  }
}