using System;
using MemoriesOfTheVoid.Tools;

namespace MemoriesOfTheVoid.AssetGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generando assets para Memories of the Void...");
            
            try
            {
                Console.WriteLine("Generando sprites...");
                SpriteGenerator.GenerateAllSprites();
                Console.WriteLine("✓ Sprites generados");
                
                Console.WriteLine("Generando audio...");
                AudioGenerator.GenerateAllAudio();
                Console.WriteLine("✓ Audio generado");
                
                Console.WriteLine("¡Todos los assets han sido generados exitosamente!");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando assets: {ex.Message}");
                Console.WriteLine("Presiona cualquier tecla para salir...");
                Console.ReadKey();
            }
        }
    }
}