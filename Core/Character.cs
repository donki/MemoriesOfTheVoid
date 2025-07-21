using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MemoriesOfTheVoid.Core
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public Texture2D Portrait { get; set; }
        public bool IsVisible { get; set; }
        public Vector2 Position { get; set; }

        public Character(string name, string description, string role, string status)
        {
            Name = name;
            Description = description;
            Role = role;
            Status = status;
            IsVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible && Portrait != null)
            {
                spriteBatch.Draw(Portrait, Position, Color.White);
            }
        }
    }

    public class AmbientCharacter : Character
    {
        public string AudioLogKey { get; set; }

        public AmbientCharacter(string name, string description, string role, string audioLogKey)
            : base(name, description, role, "Audio Log Only")
        {
            AudioLogKey = audioLogKey;
        }
    }
}

// Mover a MemoriesOfTheVoid.Core
