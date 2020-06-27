using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using CardGame.AppData;
using CardGame.Helpers;
using CardGame.Models;

namespace CardGame.Screens
{
  /// <summary>
  /// Card Game Options
  /// </summary>
  public class CardGameScreen : IVisualizationOption
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="goBackToMainMenu"></param>
    public CardGameScreen(Action goBackToMainMenu)
    {
      GoBackToMainMenu = goBackToMainMenu;
      DeckHelper = new DeckHelper();
      DbContext = new DbContext();
      // Get complete deck of cards
      DeckHelper.Get();
      Players = new List<CardPlayer<RestaurantReviewer>>();
      // Game places
      Places = new Hashtable
      {
        {0, "1st"},
        {1, "2nd"},
        {2, "3rd"},
        {3, "4th"}
      };
    }

    /// <summary>
    ///   Menu Options
    /// </summary>
    private IEnumerable<string> Options { get; }

    /// <summary>
    ///   Current Selection
    /// </summary>
    private int Selection { get; set; }

    /// <summary>
    ///   Go Back to Main Menu Screen
    /// </summary>
    private Action GoBackToMainMenu { get; }

    /// <summary>
    /// Card Deck Helper
    /// </summary>
    private DeckHelper DeckHelper { get; }

    /// <summary>
    /// Data Layer Access
    /// </summary>
    private DbContext DbContext { get; }

    /// <summary>
    /// Player's List
    /// </summary>
    private List<CardPlayer<RestaurantReviewer>> Players { get; set; }

    /// <summary>
    /// Game places
    /// </summary>
    private Hashtable Places { get; }

    /// <summary>
    /// Welcome banner for Card Game Screen Options
    /// </summary>
    public void Welcome()
    {
      Players.Clear(); //reset players
      // Get complete list of reviewers from the database
      var reviewers = DbContext.Reviewers();
      // Get random reviewers as player from the database
      var randomPlayer = DeckHelper.GetPlayers(reviewers?.ToList());
      // Add reviewers as random players
      var playerId = 1;
      foreach (var player in randomPlayer)
        Players.Add(new CardPlayer<RestaurantReviewer>(playerId++, player,
          () => player.FirstName + " " + player.LastName));
      // Shuffle all the cards for each player
      ShuffleCards();
      PrintResult();
      Console.Write("Press any key to go back to main menu ...");
      Console.ReadLine();
      GoBackToMainMenu();
    }
    /// <summary>
    /// Print Game Results
    /// </summary>
    private void PrintResult()
    {
      Console.Clear();
      var scoreChecker = 0;
      // sort winners
      IEnumerable<CardPlayer<RestaurantReviewer>> sortedResults = Players.OrderByDescending(p => p.CardValueSummary());
      // print place + player's name + cards + score
      for (int i = 0; i < sortedResults.Count(); i++)
      {
        // get player
        var player = sortedResults.ElementAt(i);
        Console.Write($"{Places[i]} Place: Player {player.Id} - {player.GetPlayerName.PadRight(20, ' ')}");
        ConsoleColor resetForeground = Console.ForegroundColor;
        ConsoleColor resetBackground = Console.BackgroundColor;
        Console.BackgroundColor = ConsoleColor.White;
        // print given cards with format
        foreach (var card in player.Cards)
        {
          Console.ForegroundColor = card.Color;
          Console.Write(card);
          Console.BackgroundColor = resetBackground;
          Console.ForegroundColor = resetForeground;
          Console.Write(" ");
          Console.BackgroundColor = ConsoleColor.White;
        }

        Console.ForegroundColor = resetForeground;
        Console.BackgroundColor = resetBackground;
        // print score
        Console.WriteLine($" Score: {player.CardValueSummary().ToString().PadLeft(3, ' ')}");
        scoreChecker += player.CardValueSummary();
        Console.Write(Environment.NewLine);
      }
      Console.WriteLine($"Score Checker: {scoreChecker}");
    }

    /// <summary>
    /// Shuffle some cards
    /// </summary>
    private void ShuffleCards()
    {
      Console.Clear();
      
      var total = 0; // total of cards for debugging purposes
      foreach (var cardPlayer in Players)
      {
        Console.Write($"Shuffling cards for player for {cardPlayer.GetPlayerName}: \n");
        // remember 13 is the max number of cards a player can get
        for (var i = 0; i < 13; i++)
        {
          var card = DeckHelper.Take();
          cardPlayer.AddCard(card);
          AnimateShuffle(card);
          total++;
        }
        Console.Write("Press any key to continue...");
        Console.ReadLine();
        Debug.WriteLine($"CardGameScreen.Welcome: Deck Value Sum: {cardPlayer.CardValueSummary()}");
        Debug.WriteLine($"CardGameScreen.Welcome: Deck Total Cards: {total}");
      }
    }
    /// <summary>
    /// Animate Card Shuffle for each player
    /// </summary>
    /// <param name="card"></param>
    private void AnimateShuffle(GameCard card)
    {
      var resetForeground = Console.ForegroundColor;
      var resetBackground = Console.BackgroundColor;
      Console.ForegroundColor = card.Color;
      Console.BackgroundColor = ConsoleColor.White;
      Console.WriteLine(card);
      // set cursor position at the line break and clear
      Console.SetCursorPosition(0, Console.CursorTop - 1);
      Thread.Sleep(100);
      Console.BackgroundColor = resetBackground;
      Console.ForegroundColor = resetForeground;
    }
  }
}