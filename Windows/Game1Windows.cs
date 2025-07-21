using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

namespace MemoriesOfTheVoid.Windows
{
    public enum GameState
    {
        Splash,
        MainMenu,
        Tutorial,
        Playing,
        Inventory,
        Options,
        SaveLoad,
        Dialog
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        
        // Game State Management
        private GameState _currentState = GameState.Splash;
        private float _splashTimer = 0f;
        private const float SPLASH_DURATION = 3f;
        
        // Input
        private MouseState _previousMouseState;
        private KeyboardState _previousKeyboardState;
        
        // Textures (will be created programmatically)
        private Texture2D _splashTexture;
        private Texture2D _menuBackground;
        private Texture2D _cursorTexture;
        private Texture2D _inventoryIcon;
        private Texture2D _characterTexture;
        private Texture2D _doorTexture;
        private Texture2D _panelTexture;
        
        // Game Objects
        private List<ClickableObject> _gameObjects = new List<ClickableObject>();
        private Inventory _inventory;
        private DialogSystem _dialogSystem;
        private SaveSystem _saveSystem;
        
        // UI
        private Vector2 _mousePosition;
        private bool _showInventory = false;
        private string _currentText = "";
        private List<string> _tutorialTexts;
        private int _tutorialStep = 0;
        
        // Language
        private Dictionary<string, Dictionary<string, string>> _localization;
        private string _currentLanguage = "es";

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            
            // Set resolution
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            InitializeLocalization();
            InitializeTutorial();
            
            _inventory = new Inventory();
            _dialogSystem = new DialogSystem();
            _saveSystem = new SaveSystem();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Create simple textures programmatically
            CreateTextures();
            
            // Initialize game objects
            InitializeGameObjects();
        }

        private void CreateTextures()
        {
            try
            {
                // Load actual PNG images from Content/Images folder
                _splashTexture = LoadTextureFromFile("Content/Images/splashscreen.png");
                _menuBackground = LoadTextureFromFile("Content/Images/menu_background.png");
                _cursorTexture = LoadTextureFromFile("Content/Images/cursor.png");
                _inventoryIcon = LoadTextureFromFile("Content/Images/inventory_icon.png");
                _characterTexture = LoadTextureFromFile("Content/Images/eve.png");
                _doorTexture = LoadTextureFromFile("Content/Images/door.png");
                _panelTexture = LoadTextureFromFile("Content/Images/panel.png");
                
                Console.WriteLine("Todas las texturas cargadas correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando texturas: {ex.Message}");
                // Fallback to colored rectangles if images fail to load
                _splashTexture = CreateColorTexture(1280, 720, Color.DarkBlue);
                _menuBackground = CreateColorTexture(1280, 720, Color.Black);
                _cursorTexture = CreateColorTexture(32, 32, Color.White);
                _inventoryIcon = CreateColorTexture(64, 64, Color.Gray);
                _characterTexture = CreateColorTexture(64, 96, Color.LightGray);
                _doorTexture = CreateColorTexture(120, 180, Color.DarkGray);
                _panelTexture = CreateColorTexture(80, 60, Color.DarkSlateGray);
            }
        }

        private Texture2D LoadTextureFromFile(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                return Texture2D.FromStream(GraphicsDevice, fileStream);
            }
        }

        private Texture2D CreateColorTexture(int width, int height, Color color)
        {
            var texture = new Texture2D(GraphicsDevice, width, height);
            var colorData = new Color[width * height];
            for (int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = color;
            }
            texture.SetData(colorData);
            return texture;
        }

        private void InitializeLocalization()
        {
            _localization = new Dictionary<string, Dictionary<string, string>>
            {
                ["es"] = new Dictionary<string, string>
                {
                    ["game_title"] = "Memorias del Vacío",
                    ["new_game"] = "Nueva Partida",
                    ["load_game"] = "Cargar Partida",
                    ["options"] = "Opciones",
                    ["exit"] = "Salir",
                    ["tutorial_1"] = "Bienvenido a Memorias del Vacío",
                    ["tutorial_2"] = "Usa el ratón para interactuar con objetos",
                    ["tutorial_3"] = "Presiona I para abrir el inventario",
                    ["tutorial_4"] = "Explora y descubre la verdad...",
                    ["chapter_1"] = "Capítulo 1: Ecos de la Ausencia"
                },
                ["en"] = new Dictionary<string, string>
                {
                    ["game_title"] = "Memories of the Void",
                    ["new_game"] = "New Game",
                    ["load_game"] = "Load Game",
                    ["options"] = "Options",
                    ["exit"] = "Exit",
                    ["tutorial_1"] = "Welcome to Memories of the Void",
                    ["tutorial_2"] = "Use mouse to interact with objects",
                    ["tutorial_3"] = "Press I to open inventory",
                    ["tutorial_4"] = "Explore and discover the truth...",
                    ["chapter_1"] = "Chapter 1: Echoes of Absence"
                }
            };
        }

        private void InitializeTutorial()
        {
            _tutorialTexts = new List<string>
            {
                GetLocalizedText("tutorial_1"),
                GetLocalizedText("tutorial_2"),
                GetLocalizedText("tutorial_3"),
                GetLocalizedText("tutorial_4")
            };
        }

        private void InitializeGameObjects()
        {
            _gameObjects.Clear();
            
            // Add interactive objects
            var door = new ClickableObject(_doorTexture, new Vector2(300, 200), "door", () => {
                _dialogSystem.ShowDialog("La puerta está cerrada. Necesito encontrar una forma de abrirla.");
            });
            
            var panel = new ClickableObject(_panelTexture, new Vector2(500, 300), "panel", () => {
                _dialogSystem.ShowDialog("El panel parece dañado. Hay cables sueltos por todas partes.");
            });
            
            _gameObjects.Add(door);
            _gameObjects.Add(panel);
        }

        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();
            
            _mousePosition = new Vector2(mouseState.X, mouseState.Y);
            
            switch (_currentState)
            {
                case GameState.Splash:
                    UpdateSplash(gameTime, keyboardState);
                    break;
                case GameState.MainMenu:
                    UpdateMainMenu(mouseState, keyboardState);
                    break;
                case GameState.Tutorial:
                    UpdateTutorial(keyboardState);
                    break;
                case GameState.Playing:
                    UpdatePlaying(mouseState, keyboardState);
                    break;
                case GameState.Dialog:
                    UpdateDialog(keyboardState);
                    break;
            }
            
            _previousMouseState = mouseState;
            _previousKeyboardState = keyboardState;
            
            base.Update(gameTime);
        }

        private void UpdateSplash(GameTime gameTime, KeyboardState keyboardState)
        {
            _splashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (_splashTimer >= SPLASH_DURATION || keyboardState.IsKeyDown(Keys.Space))
            {
                _currentState = GameState.MainMenu;
            }
        }

        private void UpdateMainMenu(MouseState mouseState, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                _currentState = GameState.Tutorial;
            }
            
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        private void UpdateTutorial(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space))
            {
                _tutorialStep++;
                if (_tutorialStep >= _tutorialTexts.Count)
                {
                    _currentState = GameState.Playing;
                    _tutorialStep = 0;
                }
            }
        }

        private void UpdatePlaying(MouseState mouseState, KeyboardState keyboardState)
        {
            // Toggle inventory
            if (keyboardState.IsKeyDown(Keys.I) && !_previousKeyboardState.IsKeyDown(Keys.I))
            {
                _showInventory = !_showInventory;
            }
            
            // Handle object interactions
            if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
            {
                foreach (var obj in _gameObjects)
                {
                    if (obj.Bounds.Contains(_mousePosition))
                    {
                        obj.OnClick();
                        break;
                    }
                }
            }
            
            // Check for dialog state
            if (_dialogSystem.IsActive)
            {
                _currentState = GameState.Dialog;
            }
        }

        private void UpdateDialog(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space))
            {
                if (_dialogSystem.IsActive)
                {
                    _dialogSystem.NextDialog();
                    if (!_dialogSystem.IsActive)
                    {
                        _currentState = GameState.Playing;
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin();
            
            switch (_currentState)
            {
                case GameState.Splash:
                    DrawSplash();
                    break;
                case GameState.MainMenu:
                    DrawMainMenu();
                    break;
                case GameState.Tutorial:
                    DrawTutorial();
                    break;
                case GameState.Playing:
                    DrawPlaying();
                    break;
                case GameState.Dialog:
                    DrawPlaying();
                    DrawDialog();
                    break;
            }
            
            // Always draw cursor last
            _spriteBatch.Draw(_cursorTexture, _mousePosition, Color.White);
            
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void DrawSplash()
        {
            _spriteBatch.Draw(_splashTexture, Vector2.Zero, Color.White);
            
            // Simple text rendering without font
            DrawSimpleText(GetLocalizedText("game_title"), 
                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight - 100), 
                Color.White);
        }

        private void DrawMainMenu()
        {
            _spriteBatch.Draw(_menuBackground, Vector2.Zero, Color.White);
            
            DrawSimpleText(GetLocalizedText("game_title"), 
                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, 100), 
                Color.White);
            
            DrawSimpleText("Presiona ENTER para comenzar", 
                new Vector2(50, _graphics.PreferredBackBufferHeight - 50), 
                Color.Gray);
        }

        private void DrawTutorial()
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            
            if (_tutorialStep < _tutorialTexts.Count)
            {
                DrawSimpleText(_tutorialTexts[_tutorialStep], 
                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - 150, _graphics.PreferredBackBufferHeight / 2), 
                    Color.White);
            }
            
            DrawSimpleText("Presiona ESPACIO para continuar", 
                new Vector2(50, _graphics.PreferredBackBufferHeight - 50), 
                Color.Gray);
        }

        private void DrawPlaying()
        {
            // Draw background (room)
            GraphicsDevice.Clear(Color.DarkSlateGray);
            
            // Draw game objects
            foreach (var obj in _gameObjects)
            {
                obj.Draw(_spriteBatch);
            }
            
            // Draw character
            _spriteBatch.Draw(_characterTexture, new Vector2(100, 400), Color.White);
            
            // Draw inventory if open
            if (_showInventory)
            {
                DrawInventory();
            }
        }

        private void DrawInventory()
        {
            // Semi-transparent background
            var inventoryBg = CreateColorTexture(400, 300, Color.Black);
            var inventoryRect = new Rectangle(200, 150, 400, 300);
            _spriteBatch.Draw(inventoryBg, inventoryRect, Color.Black * 0.8f);
            
            DrawSimpleText("INVENTARIO", new Vector2(inventoryRect.X + 10, inventoryRect.Y + 10), Color.White);
            
            // Draw inventory items
            for (int i = 0; i < _inventory.Items.Count; i++)
            {
                var item = _inventory.Items[i];
                var itemPos = new Vector2(
                    inventoryRect.X + 20 + (i % 5) * 70,
                    inventoryRect.Y + 50 + (i / 5) * 70
                );
                
                _spriteBatch.Draw(_inventoryIcon, itemPos, Color.White);
                DrawSimpleText(item.Name, new Vector2(itemPos.X, itemPos.Y + 50), Color.White);
            }
        }

        private void DrawDialog()
        {
            if (_dialogSystem.IsActive)
            {
                // Dialog background
                var dialogBg = CreateColorTexture(_graphics.PreferredBackBufferWidth - 100, 150, Color.Black);
                var dialogRect = new Rectangle(50, _graphics.PreferredBackBufferHeight - 200, 
                    _graphics.PreferredBackBufferWidth - 100, 150);
                
                _spriteBatch.Draw(dialogBg, dialogRect, Color.Black * 0.9f);
                
                // Dialog text
                DrawSimpleText(_dialogSystem.CurrentText, 
                    new Vector2(dialogRect.X + 20, dialogRect.Y + 20), Color.White);
                
                DrawSimpleText("Presiona ESPACIO para continuar", 
                    new Vector2(dialogRect.X + 20, dialogRect.Y + dialogRect.Height - 30), Color.Gray);
            }
        }

        private void DrawSimpleText(string text, Vector2 position, Color color)
        {
            // Simple text rendering using small rectangles (pixel-style)
            // This is a very basic implementation - in a real game you'd use SpriteFont
            var charTexture = CreateColorTexture(8, 12, color);
            
            for (int i = 0; i < Math.Min(text.Length, 50); i++) // Limit text length
            {
                var charPos = new Vector2(position.X + i * 10, position.Y);
                _spriteBatch.Draw(charTexture, charPos, color);
            }
        }

        private string GetLocalizedText(string key)
        {
            if (_localization.ContainsKey(_currentLanguage) && 
                _localization[_currentLanguage].ContainsKey(key))
            {
                return _localization[_currentLanguage][key];
            }
            return key;
        }
    }

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

    public class InventoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Texture2D Icon { get; set; }
    }

    public class Inventory
    {
        public List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();

        public void AddItem(InventoryItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            Items.Remove(item);
        }
    }

    public class DialogSystem
    {
        private Queue<string> _dialogQueue = new Queue<string>();
        public bool IsActive => _dialogQueue.Count > 0;
        public string CurrentText { get; private set; } = "";

        public void ShowDialog(string text)
        {
            _dialogQueue.Enqueue(text);
            if (string.IsNullOrEmpty(CurrentText))
            {
                NextDialog();
            }
        }

        public void NextDialog()
        {
            if (_dialogQueue.Count > 0)
            {
                CurrentText = _dialogQueue.Dequeue();
            }
            else
            {
                CurrentText = "";
            }
        }
    }

    public class SaveData
    {
        public Vector2 PlayerPosition { get; set; }
        public List<string> InventoryItems { get; set; } = new List<string>();
        public Dictionary<string, bool> GameFlags { get; set; } = new Dictionary<string, bool>();
        public string CurrentScene { get; set; }
    }

    public class SaveSystem
    {
        private const string SAVE_FILE = "savegame.json";

        public void SaveGame(SaveData data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(SAVE_FILE, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public SaveData LoadGame()
        {
            try
            {
                if (File.Exists(SAVE_FILE))
                {
                    string json = File.ReadAllText(SAVE_FILE);
                    return JsonConvert.DeserializeObject<SaveData>(json);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading game: {ex.Message}");
            }
            
            return new SaveData();
        }
    }
}