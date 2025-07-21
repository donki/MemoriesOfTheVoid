# Guía del Desarrollador - Memorias del Vacío
# Developer Guide - Memories of the Void

---

## 🏗️ Arquitectura del Proyecto / Project Architecture

### Estructura de Carpetas / Folder Structure

```
MemoriesOfTheVoid/
├── src/                        # Código fuente / Source code
│   ├── Core/                   # Lógica principal / Core logic
│   ├── Systems/               # Sistemas modulares / Modular systems
│   ├── Platforms/             # Código específico de plataforma / Platform-specific code
│   └── Tools/                 # Herramientas de desarrollo / Development tools
├── Content/                   # Assets del juego / Game assets
├── Documentation/             # Documentación / Documentation
├── Build/                    # Archivos de compilación / Build files
└── README.md                 # Documentación principal / Main documentation
```

### Sistemas Principales / Main Systems

#### 🎮 Core System
- **Game1.cs**: Clase principal del juego, maneja el loop principal y estados
- **ClickableObject.cs**: Objetos interactivos del mundo del juego

#### 🔧 Game Systems
- **InventorySystem.cs**: Gestión de inventario del jugador
- **DialogSystem.cs**: Motor de diálogos y conversaciones
- **SaveSystem.cs**: Persistencia de datos del juego
- **LocalizationSystem.cs**: Sistema de traducción multiidioma

#### 🖥️ Platform Systems
- **Windows/Program.cs**: Punto de entrada para Windows
- **Android/MainActivity.cs**: Actividad principal para Android

#### 🛠️ Development Tools
- **SpriteGenerator.cs**: Generación procedural de sprites
- **AudioGenerator.cs**: Síntesis de efectos de sonido
- **AssetGenerator.cs**: Herramienta unificada de generación

---

## 🔄 Flujo de Estados del Juego / Game State Flow

```
Splash Screen (3s) → Main Menu → Tutorial → Playing ⇄ Dialog
                         ↓           ↓         ↓
                    Load Game → Options → Inventory
```

### Estados Disponibles / Available States
- **Splash**: Pantalla de carga inicial
- **MainMenu**: Menú principal con opciones
- **Tutorial**: Introducción paso a paso
- **Playing**: Juego principal point-and-click
- **Dialog**: Sistema de conversaciones
- **Inventory**: Gestión de objetos
- **Options**: Configuración del juego
- **SaveLoad**: Guardado y carga de partidas

---

## 🎨 Sistema de Assets / Asset System

### Generación Procedural / Procedural Generation

Los assets se generan automáticamente usando código C# y System.Drawing:

```csharp
// Ejemplo de generación de sprite
using (var bitmap = new Bitmap(width, height))
using (var graphics = Graphics.FromImage(bitmap))
{
    // Dibujar sprite
    graphics.Clear(backgroundColor);
    graphics.FillRectangle(brush, rectangle);
    bitmap.Save("Content/sprite.png", ImageFormat.Png);
}
```

### Pipeline de Contenido / Content Pipeline

MonoGame Content Pipeline procesa los assets:
- **Texturas**: PNG → XNB (comprimido)
- **Audio**: WAV → XNB (optimizado)
- **Fuentes**: SpriteFont → XNB (renderizable)

---

## 🌍 Sistema de Localización / Localization System

### Estructura de Traducciones / Translation Structure

```csharp
private Dictionary<string, Dictionary<string, string>> _localization = new()
{
    ["es"] = new Dictionary<string, string>
    {
        ["key"] = "Texto en español"
    },
    ["en"] = new Dictionary<string, string>
    {
        ["key"] = "English text"
    }
};
```

### Agregar Nuevo Idioma / Adding New Language

1. Editar `src/Systems/LocalizationSystem.cs`
2. Añadir nuevo diccionario con código de idioma
3. Traducir todas las claves existentes
4. Actualizar lista de idiomas disponibles

```csharp
["fr"] = new Dictionary<string, string>
{
    ["game_title"] = "Mémoires du Vide",
    // ... más traducciones
}
```

---

## 💾 Sistema de Guardado / Save System

### Estructura de Datos / Data Structure

```csharp
public class SaveData
{
    public Vector2 PlayerPosition { get; set; }
    public List<string> InventoryItems { get; set; }
    public Dictionary<string, bool> GameFlags { get; set; }
    public string CurrentScene { get; set; }
    public string CurrentLanguage { get; set; }
    public DateTime SaveTime { get; set; }
    public int ChapterProgress { get; set; }
}
```

### Uso del Sistema / System Usage

```csharp
// Guardar partida
var saveData = new SaveData
{
    PlayerPosition = playerPos,
    InventoryItems = inventory.GetItemNames(),
    CurrentScene = "Chapter1"
};
_saveSystem.SaveGame(saveData);

// Cargar partida
var loadedData = _saveSystem.LoadGame();
```

---

## 🎵 Sistema de Audio / Audio System

### Generación de Sonidos / Sound Generation

Los efectos de sonido se generan matemáticamente:

```csharp
// Ejemplo: Generar beep electrónico
for (int i = 0; i < samples; i++)
{
    float t = (float)i / SampleRate;
    float frequency = 800f; // 800Hz
    float amplitude = 0.4f * (float)Math.Exp(-t * 8); // Decay
    float sample = amplitude * (float)Math.Sin(2 * Math.PI * frequency * t);
    audioData[i] = (short)(sample * short.MaxValue);
}
```

### Tipos de Sonidos Generados / Generated Sound Types
- **door_open.wav**: Sonido mecánico con sweep de frecuencia
- **panel_beep.wav**: Beep electrónico con decay exponencial
- **step_hallway.wav**: Pasos con ruido filtrado
- **whisper_loop.wav**: Susurros atmosféricos con modulación

---

## 🔧 Compilación y Desarrollo / Building and Development

### Requisitos de Desarrollo / Development Requirements

**Windows:**
- .NET 6.0 SDK
- Visual Studio 2022 (recomendado) o VS Code
- MonoGame Templates

**Android:**
- Todo lo anterior +
- Android SDK (API Level 21+)
- Java Development Kit (JDK)

### Comandos de Compilación / Build Commands

```bash
# Generar assets
cd Build
dotnet run --project MemoriesOfTheVoid.Tools.csproj

# Compilar Windows
dotnet build MemoriesOfTheVoid.Windows.csproj

# Compilar Android
dotnet build MemoriesOfTheVoid.Android.csproj

# Ejecutar Windows
dotnet run --project MemoriesOfTheVoid.Windows.csproj
```

### Scripts Automatizados / Automated Scripts

- **build.bat** (Windows): Script de compilación completa
- **build.sh** (Linux/Mac): Script de compilación multiplataforma

---

## 🧪 Testing y Debugging / Testing and Debugging

### Debugging en Visual Studio / Visual Studio Debugging

1. Establecer breakpoints en código crítico
2. Usar Debug.WriteLine() para logging
3. Monitorear performance con GameTime
4. Verificar estados de juego en tiempo real

### Testing de Assets / Asset Testing

```csharp
// Verificar que los assets se carguen correctamente
try
{
    var texture = Content.Load<Texture2D>("sprite_name");
    var sound = Content.Load<SoundEffect>("sound_name");
}
catch (ContentLoadException ex)
{
    Debug.WriteLine($"Error loading asset: {ex.Message}");
}
```

---

## 📈 Optimización / Optimization

### Performance Tips

1. **Gestión de Memoria**:
   ```csharp
   // Reutilizar objetos cuando sea posible
   private static readonly Color TransparentBlack = Color.Black * 0.8f;
   ```

2. **Rendering Eficiente**:
   ```csharp
   // Agrupar draws en SpriteBatch.Begin/End
   _spriteBatch.Begin();
   // ... múltiples draws
   _spriteBatch.End();
   ```

3. **Carga de Assets**:
   ```csharp
   // Cargar assets una sola vez en LoadContent()
   // No en Update() o Draw()
   ```

### Profiling

- Usar Visual Studio Diagnostic Tools
- Monitorear FPS y memoria
- Optimizar loops críticos en Update()

---

## 🔄 Extensibilidad / Extensibility

### Agregar Nuevos Capítulos / Adding New Chapters

1. **Crear nuevos assets** en `Content/`
2. **Añadir diálogos** en `LocalizationSystem.cs`
3. **Implementar mecánicas** en nuevos archivos de `Systems/`
4. **Actualizar Game1.cs** con nuevos estados si es necesario

### Agregar Nuevas Mecánicas / Adding New Mechanics

```csharp
// Ejemplo: Sistema de salud
public class HealthSystem
{
    public int CurrentHealth { get; private set; } = 100;
    public int MaxHealth { get; private set; } = 100;
    
    public void TakeDamage(int damage)
    {
        CurrentHealth = Math.Max(0, CurrentHealth - damage);
    }
    
    public void Heal(int amount)
    {
        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
    }
}
```

---

## 🐛 Solución de Problemas Comunes / Common Troubleshooting

### Error: "Content not found"
- Verificar que el asset esté en `Content/`
- Comprobar que esté listado en `Content.mgcb`
- Regenerar assets con el generador

### Error: "MonoGame not found"
- Instalar MonoGame templates: `dotnet new --install MonoGame.Templates.CSharp`
- Verificar referencias de paquetes en .csproj

### Error de compilación Android
- Instalar Android workload: `dotnet workload install android`
- Verificar Android SDK path
- Comprobar API Level compatibility

### Performance Issues
- Reducir resolución de sprites
- Optimizar frequency de Update()
- Usar object pooling para objetos frecuentes

---

## 📚 Recursos Adicionales / Additional Resources

### Documentación Oficial / Official Documentation
- [MonoGame Documentation](https://docs.monogame.net/)
- [.NET 6 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Android Development](https://developer.android.com/)

### Herramientas Recomendadas / Recommended Tools
- **Visual Studio 2022**: IDE principal
- **VS Code**: Editor ligero alternativo
- **GIMP/Photoshop**: Edición de sprites (opcional)
- **Audacity**: Edición de audio (opcional)

### Comunidad / Community
- [MonoGame Community](https://community.monogame.net/)
- [r/gamedev](https://reddit.com/r/gamedev)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/monogame)

---

*Esta guía se actualiza continuamente. Para contribuir o reportar errores, por favor abre un issue en el repositorio del proyecto.*