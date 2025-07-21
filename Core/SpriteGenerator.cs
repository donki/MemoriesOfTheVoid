using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MemoriesOfTheVoid.Tools
{
    public static class SpriteGenerator
    {
        public static void GenerateAllSprites()
        {
            GenerateSplashScreen();
            GenerateMenuBackground();
            GenerateCursor();
            GenerateInventoryIcon();
            GenerateCharacter();
            GenerateDoor();
            GeneratePanel();
        }

        private static void GenerateSplashScreen()
        {
            using (var bitmap = new Bitmap(1280, 720))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Dark space background with stars
                graphics.Clear(Color.FromArgb(10, 10, 30));
                
                var random = new Random(42); // Fixed seed for consistent stars
                var starBrush = new SolidBrush(Color.White);
                
                // Draw stars
                for (int i = 0; i < 200; i++)
                {
                    int x = random.Next(0, 1280);
                    int y = random.Next(0, 720);
                    int size = random.Next(1, 3);
                    graphics.FillEllipse(starBrush, x, y, size, size);
                }
                
                // Draw title area
                var titleBrush = new SolidBrush(Color.FromArgb(100, 150, 200));
                graphics.FillRectangle(titleBrush, 340, 300, 600, 120);
                
                // Add some sci-fi elements
                var linePen = new Pen(Color.Cyan, 2);
                graphics.DrawLine(linePen, 0, 360, 1280, 360);
                graphics.DrawLine(linePen, 640, 0, 640, 720);
                
                bitmap.Save("Content/splashscreen.png", ImageFormat.Png);
            }
        }

        private static void GenerateMenuBackground()
        {
            using (var bitmap = new Bitmap(1280, 720))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Dark gradient background
                var brush = new LinearGradientBrush(
                    new Point(0, 0), new Point(0, 720),
                    Color.FromArgb(20, 20, 40),
                    Color.FromArgb(5, 5, 15)
                );
                graphics.FillRectangle(brush, 0, 0, 1280, 720);
                
                // Add some geometric patterns
                var patternPen = new Pen(Color.FromArgb(50, 100, 150), 1);
                for (int i = 0; i < 1280; i += 40)
                {
                    graphics.DrawLine(patternPen, i, 0, i, 720);
                }
                
                bitmap.Save("Content/menu_background.png", ImageFormat.Png);
            }
        }

        private static void GenerateCursor()
        {
            using (var bitmap = new Bitmap(32, 32))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                
                // Draw a futuristic cursor
                var cursorBrush = new SolidBrush(Color.Cyan);
                var outlinePen = new Pen(Color.White, 1);
                
                // Main cursor shape
                Point[] cursorPoints = {
                    new Point(0, 0),
                    new Point(0, 20),
                    new Point(6, 15),
                    new Point(12, 25),
                    new Point(16, 22),
                    new Point(10, 12),
                    new Point(20, 12),
                    new Point(0, 0)
                };
                
                graphics.FillPolygon(cursorBrush, cursorPoints);
                graphics.DrawPolygon(outlinePen, cursorPoints);
                
                bitmap.Save("Content/cursor.png", ImageFormat.Png);
            }
        }

        private static void GenerateInventoryIcon()
        {
            using (var bitmap = new Bitmap(64, 64))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                
                // Draw a simple inventory slot
                var slotBrush = new SolidBrush(Color.FromArgb(100, 50, 50, 50));
                var borderPen = new Pen(Color.Gray, 2);
                
                graphics.FillRectangle(slotBrush, 2, 2, 60, 60);
                graphics.DrawRectangle(borderPen, 2, 2, 60, 60);
                
                bitmap.Save("Content/inventory_icon.png", ImageFormat.Png);
            }
        }

        private static void GenerateCharacter()
        {
            using (var bitmap = new Bitmap(64, 96))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                
                // Simple character sprite (astronaut)
                var suitBrush = new SolidBrush(Color.LightGray);
                var helmetBrush = new SolidBrush(Color.FromArgb(200, 220, 255));
                var visorBrush = new SolidBrush(Color.FromArgb(50, 50, 100));
                
                // Body
                graphics.FillRectangle(suitBrush, 20, 40, 24, 50);
                
                // Helmet
                graphics.FillEllipse(helmetBrush, 16, 10, 32, 32);
                graphics.FillEllipse(visorBrush, 20, 14, 24, 24);
                
                // Arms
                graphics.FillRectangle(suitBrush, 10, 45, 12, 30);
                graphics.FillRectangle(suitBrush, 42, 45, 12, 30);
                
                // Legs
                graphics.FillRectangle(suitBrush, 22, 85, 8, 10);
                graphics.FillRectangle(suitBrush, 34, 85, 8, 10);
                
                bitmap.Save("Content/character.png", ImageFormat.Png);
            }
        }

        private static void GenerateDoor()
        {
            using (var bitmap = new Bitmap(120, 180))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                
                // Sci-fi door
                var doorBrush = new SolidBrush(Color.FromArgb(80, 80, 100));
                var panelBrush = new SolidBrush(Color.FromArgb(60, 60, 80));
                var lightBrush = new SolidBrush(Color.Red);
                
                // Main door
                graphics.FillRectangle(doorBrush, 10, 10, 100, 160);
                
                // Door panels
                graphics.FillRectangle(panelBrush, 20, 20, 35, 140);
                graphics.FillRectangle(panelBrush, 65, 20, 35, 140);
                
                // Status light
                graphics.FillEllipse(lightBrush, 50, 30, 20, 20);
                
                // Door frame
                var framePen = new Pen(Color.Gray, 3);
                graphics.DrawRectangle(framePen, 10, 10, 100, 160);
                
                bitmap.Save("Content/door.png", ImageFormat.Png);
            }
        }

        private static void GeneratePanel()
        {
            using (var bitmap = new Bitmap(80, 60))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                
                // Control panel
                var panelBrush = new SolidBrush(Color.FromArgb(40, 40, 60));
                var screenBrush = new SolidBrush(Color.FromArgb(20, 100, 20));
                var buttonBrush = new SolidBrush(Color.FromArgb(100, 100, 120));
                
                // Main panel
                graphics.FillRectangle(panelBrush, 0, 0, 80, 60);
                
                // Screen
                graphics.FillRectangle(screenBrush, 10, 10, 30, 20);
                
                // Buttons
                graphics.FillEllipse(buttonBrush, 50, 10, 8, 8);
                graphics.FillEllipse(buttonBrush, 62, 10, 8, 8);
                graphics.FillEllipse(buttonBrush, 50, 22, 8, 8);
                graphics.FillEllipse(buttonBrush, 62, 22, 8, 8);
                
                // Damage effects
                var damagePen = new Pen(Color.FromArgb(150, 100, 50), 2);
                graphics.DrawLine(damagePen, 15, 35, 65, 45);
                graphics.DrawLine(damagePen, 25, 40, 55, 50);
                
                bitmap.Save("Content/panel.png", ImageFormat.Png);
            }
        }
    }
}

// Linear gradient brush implementation for older .NET versions
public class LinearGradientBrush : Brush
{
    private Color _startColor;
    private Color _endColor;
    private Rectangle _rect;

    public LinearGradientBrush(Point start, Point end, Color startColor, Color endColor)
    {
        _startColor = startColor;
        _endColor = endColor;
        _rect = new Rectangle(start.X, start.Y, end.X - start.X, end.Y - start.Y);
    }

    public override object Clone()
    {
        return new LinearGradientBrush(
            new Point(_rect.X, _rect.Y),
            new Point(_rect.Right, _rect.Bottom),
            _startColor, _endColor);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}