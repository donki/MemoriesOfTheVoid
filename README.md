# 🌌 Memorias del Vacío / Memories of the Void

Un juego de aventura gráfica point-and-click de terror psicológico y ciencia ficción desarrollado con MonoGame para Windows y Android.

## 🎮 Descripción del Juego

**Español:**
Despierta en una estación de investigación abandonada en Europa, una luna helada de Júpiter. Sin recuerdos, solo con una nota ensangrentada: "No enciendas la antena. Ellos escuchan." Explora, resuelve puzzles y descubre la verdad en este thriller de terror psicológico.

**English:**
Wake up in an abandoned research station on Europa, Jupiter's frozen moon. No memories, just a blood-stained note: "Don't power up the antenna. They listen." Explore, solve puzzles, and uncover the truth in this psychological horror thriller.

## 🚀 Características

- ✅ Sistema point-and-click completo
- ✅ Splash screen y menú principal
- ✅ Sistema de inventario visual
- ✅ Motor de diálogos interactivo
- ✅ Sistema de guardado/carga en JSON
- ✅ Tutorial integrado
- ✅ Soporte multiidioma (Español/Inglés)
- ✅ Audio atmosférico generado proceduralmente
- ✅ Sprites pixel-art generados automáticamente
- ✅ Compatible con Windows y Android
- ✅ Arquitectura modular y escalable

## 📁 Estructura del Proyecto

```
MemoriesOfTheVoid/
├── src/                        # Código fuente
│   ├── Core/                   # Lógica principal del juego
│   │   ├── Game1.cs           # Clase principal del juego
│   │   └── ClickableObject.cs # Objetos interactivos
│   ├── Systems/               # Sistemas del juego
│   │   ├── InventorySystem.cs # Sistema de inventario
│   │   ├── DialogSystem.cs    # Sistema de diálogos
│   │   ├── SaveSystem.cs      # Sistema de guardado
│   │   └── LocalizationSystem.cs # Sistema de localización
│   ├── Platforms/             # Código específico de plataforma
│   │   ├── Android/           # Implementación Android
│   │   │   └── MainActivity.cs
│   │   └── Windows/           # Implementación Windows
│   │       └── Program.cs
│   └── Tools/                 # Herramientas y generadores
│       ├── SpriteGenerator.cs # Generador de sprites
│       ├── AudioGenerator.cs  # Generador de audio
│       └── AssetGenerator.cs  # Generador principal
├── Content/                   # Assets del juego
│   ├── Images/               # 🎨 Sprites e imágenes
│   │   ├── splashscreen.png  # Pantalla de carga
│   │   ├── menu_background.png # Fondo del menú
│   │   ├── cursor.png        # Cursor del juego
│   │   ├── character.png     # Sprite del protagonista
│   │   ├── door.png          # Puerta interactiva
│   │   ├── panel.png         # Panel de control
│   │   └── inventory_icon.png # Icono de inventario
│   ├── Audio/                # 🔊 Efectos de sonido
│   │   ├── door_open.wav     # Sonido de puerta
│   │   ├── panel_beep.wav    # Beep electrónico
│   │   ├── step_hallway.wav  # Pasos en pasillo
│   │   └── whisper_loop.wav  # Susurros atmosféricos
│   ├── Content.mgcb          # Pipeline de MonoGame
│   └── DefaultFont.spritefont # Fuente del juego
├── Documentation/             # Documentación del juego
│   ├── GameStory.md          # Historia completa
│   └── Characters.md         # Descripción de personajes
├── Build/                    # Archivos de compilación
│   ├── MemoriesOfTheVoid.sln # Solución principal
│   ├── *.csproj             # Archivos de proyecto
└── README.md                # Este archivo
```

## 📋 Requisitos

### Para Windows:
- .NET 6.0 SDK
- Visual Studio 2022 o Visual Studio Code
- MonoGame Framework

### Para Android:
- .NET 6.0 SDK
- Visual Studio 2022 con cargas de trabajo de Android
- Android SDK (API Level 21+)

## 🛠️ Instalación y Compilación

### 1. Clonar/Descargar el proyecto
```bash
# Si tienes git
git clone [url-del-repositorio]
cd MemoriesOfTheVoid
```

### 2. Generar Assets
```bash
# Navegar a la carpeta Build
cd Build

# Generar todos los assets (sprites y audio)
dotnet run --project MemoriesOfTheVoid.Tools.csproj
```

### 3. Compilar para Windows
```bash
# Desde la carpeta Build
dotnet restore MemoriesOfTheVoid.Windows.csproj
dotnet build MemoriesOfTheVoid.Windows.csproj
dotnet run --project MemoriesOfTheVoid.Windows.csproj
```

### 4. Compilar para Android
```bash
# Desde la carpeta Build
dotnet restore MemoriesOfTheVoid.Android.csproj
dotnet build MemoriesOfTheVoid.Android.csproj

# Para instalar en dispositivo conectado
dotnet build MemoriesOfTheVoid.Android.csproj -t:Install
```

### 5. Abrir en Visual Studio
```bash
# Abrir la solución completa
start MemoriesOfTheVoid.sln
```

## 🎯 Controles

### PC (Windows):
- **Ratón**: Interactuar con objetos y navegación
- **I**: Abrir/cerrar inventario
- **Espacio**: Continuar diálogos, saltar splash screen
- **Enter**: Confirmar en menús
- **Escape**: Volver/salir

### Android:
- **Toque**: Interactuar con objetos y navegación
- **Toque prolongado**: Abrir inventario
- **Toque en diálogos**: Continuar conversación

## 🎨 Assets Incluidos

### Sprites Generados Proceduralmente:

**Elementos del Juego:**
- `splashscreen.png` - Pantalla de carga espacial con Europa
- `menu_background.png` - Fondo tecnológico del menú
- `cursor.png` - Cursor futurista biónico
- `character.png` - Protagonista con traje espacial
- `door.png` - Puerta sci-fi con indicadores
- `panel.png` - Panel de control dañado
- `inventory_icon.png` - Icono de inventario tecnológico

**Personajes:**
- `eve.png` - EVE (IA principal) - Holograma pulsante
- `sprites_eve.png` - Animaciones de EVE (4 frames)
- `dra_vega_sprites.png` - Dra. Vega (Médica) - 4 poses
- `teniente_aran_sprites.png` - Teniente Aran (Seguridad) - 4 poses
- `tecnico_lau_sprites.png` - Técnico Lau (Mantenimiento) - 4 poses

### Audio Generado Proceduralmente:
- `door_open.wav` - Sonido mecánico de puerta
- `panel_beep.wav` - Beep electrónico
- `step_hallway.wav` - Pasos en pasillo metálico
- `whisper_loop.wav` - Susurros atmosféricos inquietantes

## 🎮 Cómo Jugar

1. **Splash Screen**: Espera 3 segundos o presiona Espacio
2. **Menú Principal**: Presiona Enter para "Nueva Partida"
3. **Tutorial**: Sigue las instrucciones paso a paso
4. **Juego Principal**: 
   - Haz clic en objetos para interactuar
   - Presiona I para abrir inventario
   - Lee los diálogos para entender la historia
   - Resuelve puzzles ambientales para avanzar

## 🌍 Sistema de Localización

El juego incluye soporte completo para múltiples idiomas:
- **Español**: Textos, diálogos y UI completamente traducidos
- **English**: Full English translation for all content

El idioma se puede cambiar modificando el sistema de localización en `src/Systems/LocalizationSystem.cs`.

## 🏗️ Arquitectura del Código

### Sistemas Modulares:
- **Core**: Lógica principal del juego y objetos base
- **Systems**: Sistemas independientes (inventario, diálogos, guardado, localización)
- **Platforms**: Código específico para cada plataforma
- **Tools**: Herramientas de desarrollo y generación de assets

### Patrones de Diseño Utilizados:
- **State Machine**: Para gestión de estados del juego
- **Observer Pattern**: Para eventos del sistema
- **Factory Pattern**: Para generación de objetos
- **Singleton Pattern**: Para sistemas globales

## 🐛 Solución de Problemas

### Error de compilación:
- Asegúrate de estar en la carpeta `Build/` para compilar
- Verifica que .NET 6.0 SDK esté instalado
- Ejecuta `dotnet restore` antes de compilar

### Assets no se generan:
- Ejecuta el generador de assets: `dotnet run --project MemoriesOfTheVoid.Tools.csproj`
- Verifica permisos de escritura en la carpeta `Content/`

### Problemas con Android:
- Instala Android SDK y herramientas
- Ejecuta `dotnet workload install android`
- Verifica que el dispositivo esté en modo desarrollador

## 📝 Desarrollo y Extensión

### Agregar Nuevos Capítulos:
1. Crear nuevos assets en `Content/`
2. Añadir diálogos en `LocalizationSystem.cs`
3. Implementar nuevas mecánicas en `Systems/`
4. Actualizar la lógica principal en `Core/Game1.cs`

### Agregar Nuevos Idiomas:
1. Editar `src/Systems/LocalizationSystem.cs`
2. Añadir diccionario con traducciones
3. Actualizar lista de idiomas disponibles

### Crear Nuevos Assets:
1. Modificar generadores en `src/Tools/`
2. Ejecutar `dotnet run --project MemoriesOfTheVoid.Tools.csproj`
3. Actualizar `Content/Content.mgcb` si es necesario

## 🎯 Roadmap Futuro

### Capítulo 2: "Señales en la Oscuridad"
- [ ] Nuevas áreas explorables
- [ ] Sistema de cordura/sanidad mental
- [ ] Más puzzles complejos
- [ ] Múltiples finales

### Mejoras Técnicas:
- [ ] Sistema de logros
- [ ] Efectos visuales avanzados
- [ ] Música dinámica
- [ ] Soporte para más idiomas
- [ ] Modo historia extendido

## 🤝 Contribuir

Este proyecto está abierto a contribuciones:

1. Fork el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -am 'Añadir nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto es de código abierto para fines educativos y de demostración.

## 🎯 Créditos

- **Desarrollo**: MonoGame Framework
- **Sprites**: Generación procedural con System.Drawing
- **Audio**: Síntesis de audio programática
- **Historia**: Terror psicológico original
- **Arquitectura**: Diseño modular escalable
- **Plataformas**: Windows & Android

---

## 🚀 ¡Comienza tu Aventura!

```bash
cd Build
dotnet run --project MemoriesOfTheVoid.Tools.csproj  # Generar assets
dotnet run --project MemoriesOfTheVoid.Windows.csproj  # Jugar en Windows
```

**¡Descubre los misterios ocultos en las profundidades heladas de Europa! 🌌👽**