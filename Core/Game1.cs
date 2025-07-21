using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

namespace MemoriesOfTheVoid.Core
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

        // Textures
        private Texture2D _splashTexture;
        private Texture2D _menuBackground;
        private Texture2D _cursorTexture;
        private Texture2D _inventoryIcon;
        private Texture2D _characterTexture;
        private Texture2D _doorTexture;
        private Texture2D _panelTexture;

        // Audio
        private SoundEffect _doorOpenSound;
        private SoundEffect _panelBeepSound;
        private SoundEffect _stepSound;
        private SoundEffect _whisperSound;

        // Game Objects
        private List<ClickableObject> _gameObjects = new List<ClickableObject>();
        private Inventory _inventory;
        private DialogSystem _dialogSystem;
        private SaveSystem _saveSystem;
        // private CharacterSystem _characterSystem;

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
            
            // Use the most basic settings possible for maximum compatibility
            _graphics.GraphicsProfile = GraphicsProfile.Reach;
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            _graphics.PreferredDepthStencilFormat = DepthFormat.None;
            _graphics.PreferMultiSampling = false;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.IsFullScreen = false;
            
            // Disable hardware mode switching to force software fallback if needed
            _graphics.HardwareModeSwitch = false;
            
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Force the most compatible graphics settings
            try
            {
                _graphics.GraphicsProfile = GraphicsProfile.Reach;
                _graphics.PreferredBackBufferWidth = 640;
                _graphics.PreferredBackBufferHeight = 480;
                _graphics.IsFullScreen = false;
                _graphics.PreferMultiSampling = false;
                _graphics.SynchronizeWithVerticalRetrace = false;
                
                // Force software rendering if hardware fails
                _graphics.HardwareModeSwitch = false;
                
                _graphics.ApplyChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Graphics initialization warning: {ex.Message}");
                // Try with even more basic settings
                try
                {
                    _graphics.PreferredBackBufferWidth = 320;
                    _graphics.PreferredBackBufferHeight = 240;
                    _graphics.ApplyChanges();
                }
                catch
                {
                    // If all else fails, use default settings
                }
            }

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

            // Create procedural textures instead of loading from files
            CreateProceduralTextures();

            // Create silent sound effects
            CreateSilentSounds();

            // Initialize game objects
            InitializeGameObjects();
        }

        private void CreateProceduralTextures()
        {
            // Create simple colored textures
            _splashTexture = CreateColorTexture(1280, 720, Color.DarkBlue);
            _menuBackground = CreateColorTexture(1280, 720, Color.Black);
            _cursorTexture = CreateColorTexture(32, 32, Color.White);
            _inventoryIcon = CreateColorTexture(64, 64, Color.Gray);
            _characterTexture = CreateColorTexture(64, 64, Color.Blue);
            _doorTexture = CreateColorTexture(128, 256, Color.Brown);
            _panelTexture = CreateColorTexture(128, 128, Color.Silver);
            
            // Create a simple font texture (we'll use a 1x1 white pixel for now)
            _font = CreateSimpleFont();
        }

        private SpriteFont CreateSimpleFont()
        {
            // For now, we'll return null and handle text drawing differently
            // In a real implementation, you'd create a proper SpriteFont
            return null;
        }

        private Texture2D CreateColorTexture(int width, int height, Color color)
        {
            var texture = new Texture2D(GraphicsDevice, width, height);
            var data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            texture.SetData(data);
            return texture;
        }

        private void CreateSilentSounds()
        {
            // Create minimal sound effects (silent)
            var soundData = new byte[44100]; // 1 second of silence at 44.1kHz
            // Note: In a real implementation, you'd need to create proper SoundEffect objects
            // For now, we'll just set them to null and handle null checks in the game logic
            _doorOpenSound = null;
            _panelBeepSound = null;
            _stepSound = null;
            _whisperSound = null;
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
            var door = new ClickableObject(_doorTexture, new Vector2(300, 200), "door", () =>
            {
                _doorOpenSound?.Play();
                _dialogSystem.ShowDialog("La puerta está cerrada. Necesito encontrar una forma de abrirla.");
            });

            var panel = new ClickableObject(_panelTexture, new Vector2(500, 300), "panel", () =>
            {
                _panelBeepSound?.Play();
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

            // Since we don't have a font, we'll just show the splash screen
            // In a real implementation, you'd render text properly
        }

        private void DrawMainMenu()
        {
            _spriteBatch.Draw(_menuBackground, Vector2.Zero, Color.White);

            // Draw simple colored rectangles to represent menu options
            var menuRect1 = new Rectangle(540, 250, 200, 40);
            var menuRect2 = new Rectangle(540, 300, 200, 40);
            var menuRect3 = new Rectangle(540, 350, 200, 40);
            var menuRect4 = new Rectangle(540, 400, 200, 40);

            _spriteBatch.Draw(CreateColorTexture(1, 1, Color.DarkGray), menuRect1, Color.White);
            _spriteBatch.Draw(CreateColorTexture(1, 1, Color.DarkGray), menuRect2, Color.White);
            _spriteBatch.Draw(CreateColorTexture(1, 1, Color.DarkGray), menuRect3, Color.White);
            _spriteBatch.Draw(CreateColorTexture(1, 1, Color.DarkGray), menuRect4, Color.White);
        }

        private void DrawTutorial()
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            // Draw a simple rectangle to represent tutorial text
            var tutorialRect = new Rectangle(200, 300, 880, 120);
            _spriteBatch.Draw(CreateColorTexture(1, 1, Color.DarkGray), tutorialRect, Color.White);
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
            var inventoryBg = CreateColorTexture(1, 1, Color.Black);
            var inventoryRect = new Rectangle(200, 150, 400, 300);
            _spriteBatch.Draw(inventoryBg, inventoryRect, Color.Black * 0.8f);

            // Draw inventory items
            for (int i = 0; i < _inventory.Items.Count; i++)
            {
                var item = _inventory.Items[i];
                var itemPos = new Vector2(
                    inventoryRect.X + 20 + (i % 5) * 70,
                    inventoryRect.Y + 50 + (i / 5) * 70
                );

                _spriteBatch.Draw(_inventoryIcon, itemPos, Color.White);
            }
        }

        private void DrawDialog()
        {
            if (_dialogSystem.IsActive)
            {
                // Dialog background
                var dialogBg = CreateColorTexture(1, 1, Color.Black);
                var dialogRect = new Rectangle(50, _graphics.PreferredBackBufferHeight - 200,
                    _graphics.PreferredBackBufferWidth - 100, 150);

                _spriteBatch.Draw(dialogBg, dialogRect, Color.Black * 0.9f);

                // Draw a simple rectangle to represent dialog text
                var textRect = new Rectangle(dialogRect.X + 20, dialogRect.Y + 20, 
                    dialogRect.Width - 40, dialogRect.Height - 60);
                _spriteBatch.Draw(CreateColorTexture(1, 1, Color.DarkGray), textRect, Color.White);
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