using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MemoriesOfTheVoid.Core
{
    public class ClickableObject
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, 
            Texture.Width, Texture.Height);
        public string Name { get; private set; }
        public Action OnClickAction { get; private set; }

        public ClickableObject(Texture2D texture, Vector2 position, string name, Action onClickAction)
        {
            Texture = texture;
            Position = position;
            Name = name;
            OnClickAction = onClickAction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void OnClick()
        {
            OnClickAction?.Invoke();
        }
    }
}