using System;

namespace MemoriesOfTheVoid.Tools
{
    /// <summary>
    /// Utilidad principal para generar todos los assets del juego
    /// </summary>
    public static class AssetGenerator
    {
        public static void GenerateAllAssets()
        {
            Console.WriteLine("=== GENERADOR DE ASSETS - MEMORIAS DEL VACÍO ===");
            Console.WriteLine("=== ASSET GENERATOR - MEMORIES OF THE VOID ===");
            Console.WriteLine();
            
            try
            {
                Console.WriteLine("Iniciando generación de assets...");
                Console.WriteLine("Starting asset generation...");
                Console.WriteLine();
                
                // Generar sprites
                SpriteGenerator.GenerateAllSprites();
                Console.WriteLine();
                
                // Generar audio
                AudioGenerator.GenerateAllAudio();
                Console.WriteLine();
                
                Console.WriteLine("=== GENERACIÓN COMPLETADA ===");
                Console.WriteLine("=== GENERATION COMPLETED ===");
                Console.WriteLine();
                Console.WriteLine("Todos los assets han sido generados exitosamente en la carpeta Content/");
                Console.WriteLine("All assets have been successfully generated in the Content/ folder");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error durante la generación: {ex.Message}");
                Console.WriteLine($"❌ Error during generation: {ex.Message}");
                throw;
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                GenerateAllAssets();
                
                Console.WriteLine();
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fatal: {ex.Message}");
                Console.WriteLine("Presiona cualquier tecla para salir...");
                Console.ReadKey();
            }
        }
    }
}