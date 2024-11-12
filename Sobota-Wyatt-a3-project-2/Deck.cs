using System;
using System.Numerics;

namespace Game10003
{
    public class Deck
    {
        public void Update()
        {
            CardDeck();
        }

        private void CardDeck()
        {
            Draw.FillColor = Color.Red;
            Draw.Rectangle(310, 300, 40, 80);
        }
    }
}
