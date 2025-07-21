using System;
using MemoriesOfTheVoid.Core;
using Microsoft.Xna.Framework;

namespace MemoriesOfTheVoid.Windows
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("=== Memorias del Vacío - Windows Edition ===");
            Console.WriteLine("=== Memories of the Void - Windows Edition ===");
            Console.WriteLine();
            Console.WriteLine("Iniciando juego... / Starting game...");

            try
            {
                Console.WriteLine("Creando instancia del juego...");
                using (var game = new Core.Game1())
                {
                    Console.WriteLine("Ejecutando juego...");
                    game.Run();
                    Console.WriteLine("Juego terminado normalmente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar el juego / Error starting game: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner exception stack trace: {ex.InnerException.StackTrace}");
                }
                Console.WriteLine("Presiona cualquier tecla para salir / Press any key to exit...");
                Console.ReadKey();
            }
            
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}