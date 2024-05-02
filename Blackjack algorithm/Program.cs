using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        PlayBlackjack();
    }

    static void PlayBlackjack()
    {
        Console.WriteLine("Welcome to Console Blackjack!");

        List<string> deck = InitializeDeck();
        List<string> playerHand = new List<string>();
        List<string> dealerHand = new List<string>();

        // Deal initial cards
        DealCard(deck, playerHand);
        DealCard(deck, dealerHand);
        DealCard(deck, playerHand);
        DealCard(deck, dealerHand);

        // Player's turn
        while (true)
        {
            Console.WriteLine($"Your hand: {string.Join(", ", playerHand)} (Value: {CalculateHandValue(playerHand)})");
            Console.WriteLine($"Dealer's face-up card: {dealerHand[0]}");

            // Check if the player has Blackjack
            if (CalculateHandValue(playerHand) == 21)
            {
                Console.WriteLine("Blackjack! You win!");
                break;
            }

            // Ask the player to hit or stand
            Console.Write("Do you want to hit or stand? ");
            string action = Console.ReadLine().ToLower();

            if (action == "hit")
            {
                DealCard(deck, playerHand);

                // Check if the player busts
                if (CalculateHandValue(playerHand) > 21)
                {
                    Console.WriteLine("Bust! You lose.");
                    break;
                }
            }
            else if (action == "stand")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid action. Please enter 'hit' or 'stand'.");
            }
        }

        // Dealer's turn
        while (CalculateHandValue(dealerHand) < 17)
        {
            DealCard(deck, dealerHand);
        }

        // Determine the winner
        Console.WriteLine($"\nYour hand: {string.Join(", ", playerHand)} (Value: {CalculateHandValue(playerHand)})");
        Console.WriteLine($"Dealer's hand: {string.Join(", ", dealerHand)} (Value: {CalculateHandValue(dealerHand)})");

        if (CalculateHandValue(dealerHand) > 21 || CalculateHandValue(playerHand) > CalculateHandValue(dealerHand))
        {
            Console.WriteLine("You win!");
        }
        else if (CalculateHandValue(playerHand) < CalculateHandValue(dealerHand))
        {
            Console.WriteLine("You lose.");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }

    static List<string> InitializeDeck()
    {
        List<string> deck = new List<string>
        {
            "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A",
            "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A",
            "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A",
            "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"
        };

        // Shuffle the deck
        Random rng = new Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }

        return deck;
    }

    static void DealCard(List<string> deck, List<string> hand)
    {
        string card = deck[0];
        deck.RemoveAt(0);
        hand.Add(card);
    }

    static int CalculateHandValue(List<string> hand)
    {
        Dictionary<string, int> values = new Dictionary<string, int>
        {
            {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6}, {"7", 7},
            {"8", 8}, {"9", 9}, {"10", 10}, {"J", 10}, {"Q", 10}, {"K", 10}, {"A", 11}
        };

        int value = 0;
        int numAces = 0;

        foreach (var card in hand)
        {
            value += values[card];
            if (card == "A")
                numAces++;
        }

        while (value > 21 && numAces > 0)
        {
            value -= 10;
            numAces--;
        }

        return value;
        Console.ReadLine();
    }
}
