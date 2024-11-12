using System;
using System.Numerics;
using System.Threading;

namespace Game10003
{
    public class Card
    {
        private float W;
        private float H;
        public float X;
        public float Y;
        public bool Matched = false; // New property to indicate if the card is matched
        public Color FrontColor; // The color displayed on the front of the card

        public Card(float xpos,float ypos, Color color)
        {
            W = 40;
            H = 80;
            X = xpos;
            Y = ypos;
            FrontColor = color; // Assign color to the card
        }

        public Card(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool CardFaceDown { get; private set; } = true;
        public void Update()
        {

            if (Matched) return;
            // Check if the left mouse button is pressed and if the mouse is over this card
            if (Input.IsMouseButtonPressed(MouseInput.Left) && IsMouseTouchingBox())
            {
                // Flip the card when clicked
                CardFaceDown = !CardFaceDown;
            }

            // Draw the card based on its current state
            if (CardFaceDown)
                CardBack();
            else
                CardFront(X, Y);
        }
        public void ToggleFace()
        {
            CardFaceDown = !CardFaceDown;
        }
        private bool IsMouseTouchingBox()
        {
            // Get the current mouse position
            Vector2 mousePos = Input.GetMousePosition();

            // Calculate the edges of the card
            float leftEdge = X;
            float rightEdge = X + W;
            float topEdge = Y;
            float bottomEdge = Y + H;

            // Check if the mouse position is within the card's boundaries
            return mousePos.X >= leftEdge && mousePos.X <= rightEdge &&
                   mousePos.Y >= topEdge && mousePos.Y <= bottomEdge;
        }

        private void CardFront(float Cardx,float Cardy)
        {
            Draw.FillColor = FrontColor;
            Draw.Rectangle(Cardx, Cardy, W, H);
        }
        private void CardBack()
        {
            Draw.FillColor = Color.Red;
            Draw.Rectangle(X, Y, W, H);
        }
    }
}
