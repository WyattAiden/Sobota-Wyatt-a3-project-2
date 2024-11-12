// Include code libraries you need below (use the namespace).
using Game10003;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

// The namespace your code is in.
namespace Game10003
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Deck PlaceDeck;

        Vector2[] Positions = new Vector2[12];
        Card[] cards = new Card[12];
        private List<Card> flippedCards = new List<Card>();
        private float flipBackTimer = 0;

        public static int Score = 0;
        public static bool EndGame = false;
        public static bool WinGame = false;

        private int incorrectGuesses = 0;
        private const int maxIncorrectGuesses = 5;
        public void Setup()
        {
            Window.SetTitle("Concentration");
            Window.SetSize(400, 400);

            // Define the 6 colors and duplicate them to have pairs
            List<Color> colors = new List<Color>
            {
                Color.Blue, Color.Green, Color.Yellow, Color.Cyan, Color.Magenta, Color.Black
            };

            // Duplicate each color to make pairs
            List<Color> colorPairs = new List<Color>();
            foreach (Color color in colors)
            {
                colorPairs.Add(color);
                colorPairs.Add(color);
            }

            // Shuffle the list of colors
            System.Random rand = new System.Random();
            for (int i = 0; i < colorPairs.Count; i++)
            {
                int randomIndex = rand.Next(i, colorPairs.Count);
                Color temp = colorPairs[i];
                colorPairs[i] = colorPairs[randomIndex];
                colorPairs[randomIndex] = temp;
            }

            //making the aray to make the cards
            for (int i = 0; i < 4; i++)
            {
                int x = i * 70 + 20;
                for (int j = 0; j < 3; j++)
                {
                    int y = j * 100 + 50;
                    int inex = i + j * 4;
                    Positions[inex] = new Vector2(x, y);
                    cards[inex] = new Card(x, y, colorPairs[inex]);
                }
            }

            PlaceDeck = new Deck();
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.White);

            if (flipBackTimer > 0)
            {
                flipBackTimer -= Time.DeltaTime;
                if (flipBackTimer <= 0)
                {
                    ResetFlippedCards();
                }
            }

            foreach (Card card in cards)
            {
                card.Update();

                if (!card.CardFaceDown && !card.Matched && !flippedCards.Contains(card))
                {
                    flippedCards.Add(card);
                }

                if (flippedCards.Count == 2 && flipBackTimer <= 0)
                {
                    CheckForMatch();
                }
                Text.Draw($"Score: {Game.Score}", new Vector2(200, 10));
            }
            if (Score == 60) Game.WinGame = true;

            if (Game.WinGame) Game.EndGame = true;

            if (EndGame)
            {
                // Display win or game over message and stop further updates
                if (WinGame)
                {
                    Text.Draw($"You Win!", new Vector2(150, 200));
                }
                else
                {
                    Text.Draw($"Game Over", new Vector2(150, 200));
                }
                return; // Exit the Update method to stop further execution
            }

            PlaceDeck.Update();
        }

        private void CheckForMatch()
        {
            if (flippedCards.Count == 2)
            {
                if (flippedCards[0].FrontColor.Equals(flippedCards[1].FrontColor))
                {
                    flippedCards[0].Matched = true;
                    flippedCards[1].Matched = true;
                    Score += 10;
                    flippedCards.Clear(); // Clear immediately after matching
                }
                else
                {
                    incorrectGuesses++;
                    if (incorrectGuesses >= maxIncorrectGuesses)
                    {
                        EndGame = true; // Set to true when max incorrect guesses reached
                    }

                    flipBackTimer = 0.5f;
                }
            }
        }

        private void ResetFlippedCards()
        {
            if (flippedCards.Count == 2)
            {
                flippedCards[0].ToggleFace();
                flippedCards[1].ToggleFace();
                flippedCards.Clear();
            }
        }
    }
}