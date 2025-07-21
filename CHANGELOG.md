# Changelog - Memorias del Vacío / Memories of the Void

Todos los cambios notables de este proyecto serán documentados en este archivo.

## [1.0.2] - 2025-07-18

### ✨ Añadido / Added
- **EVE - IA Principal**: Nueva inteligencia artificial como personaje central
  - `eve.png`: Sprite principal de EVE con efectos holográficos
  - `sprites_eve.png`: Animaciones de EVE (4 frames con efectos pulsantes)
  - Integración en splash screen con animación y texto de introducción
  
- **Nuevos Personajes**: Sprites completos del equipo de Europa Station
  - `dra_vega_sprites.png`: Dra. Vega (Médica y Bióloga) - 4 poses
  - `teniente_aran_sprites.png`: Teniente Aran (Oficial de Seguridad) - 4 poses  
  - `tecnico_lau_sprites.png`: Técnico Lau (Técnico de Mantenimiento) - 4 poses
  
- **Sistema de Localización Expandido**: Textos para EVE y nuevos personajes
  - Introducción de EVE en español e inglés
  - Descripciones y diálogos de los nuevos personajes

### 🔄 Cambiado / Changed
- **Splash Screen**: Ahora presenta a EVE como personaje central
  - Fondo espacial mejorado con Europa visible
  - EVE aparece con efectos de pulsación holográfica
  - Texto de introducción de EVE con fade-in
  
- **Content Pipeline**: Agregados todos los nuevos sprites de personajes
- **Game1.cs**: Integración completa de texturas de personajes
- **SpriteGenerator**: Métodos para generar todos los personajes

### 📚 Documentación / Documentation
- **Characters.md**: Perfiles completos de todos los nuevos personajes
  - EVE: IA principal con personalidad única
  - Dra. Vega: Médica con reportes de anomalías fisiológicas
  - Teniente Aran: Oficial de seguridad con reportes de infiltración
  - Técnico Lau: Mantenimiento con experiencias paranormales
- **README.md**: Actualizado con lista completa de sprites de personajes

### 🎨 Arte / Art
- **Sprites Procedurales**: 5 nuevos conjuntos de sprites generados
  - EVE: Diseño holográfico con anillos y partículas de datos
  - Dra. Vega: Apariencia científica con bata de laboratorio y gafas
  - Teniente Aran: Uniforme militar con insignias y equipo táctico
  - Técnico Lau: Overoles de trabajo con cinturón de herramientas
  - Cada personaje tiene 4 poses diferentes para animación

## [1.0.1] - 2025-07-18

### 🗂️ Organización / Organization
- **Separación de assets**: Audio e imágenes ahora en carpetas independientes
  - `Content/Images/`: Todos los sprites e imágenes del juego
  - `Content/Audio/`: Todos los efectos de sonido
- **Documentación específica**: README.md para cada tipo de asset
- **Pipeline actualizado**: Content.mgcb reorganizado con secciones claras

### 🔄 Cambiado / Changed
- **Rutas de assets**: Actualizadas en Game1.cs para usar subcarpetas
- **Generadores**: SpriteGenerator y AudioGenerator usan nuevas rutas
- **Estructura de proyecto**: Mejor organización para escalabilidad

### 📚 Documentación / Documentation
- README.md actualizado con nueva estructura de carpetas
- Documentación técnica detallada para assets de audio e imágenes
- Especificaciones de cada archivo generado

## [1.0.0] - 2025-07-18

### ✨ Añadido / Added
- **Estructura del proyecto completamente reorganizada**
  - Separación en carpetas modulares: `src/`, `Content/`, `Documentation/`, `Build/`
  - Sistemas independientes: Core, Systems, Platforms, Tools
  
- **Sistema de juego completo**
  - Splash screen con efectos de fade
  - Menú principal navegable
  - Tutorial interactivo paso a paso
  - Sistema point-and-click funcional
  - Estados de juego (Splash, Menu, Tutorial, Playing, Dialog)

- **Sistemas modulares**
  - `InventorySystem`: Gestión completa de inventario
  - `DialogSystem`: Motor de diálogos con cola de mensajes
  - `SaveSystem`: Guardado/carga en JSON con metadatos
  - `LocalizationSystem`: Soporte multiidioma completo

- **Generación procedural de assets**
  - `SpriteGenerator`: Genera sprites pixel-art automáticamente
  - `AudioGenerator`: Síntesis de efectos de sonido
  - `AssetGenerator`: Herramienta unificada de generación

- **Soporte multiplataforma**
  - Proyecto Windows con MonoGame.DesktopGL
  - Proyecto Android con MonoGame.Android
  - Configuración específica para cada plataforma

- **Contenido del juego**
  - Historia completa del Capítulo 1: "Ecos de la Ausencia"
  - Personajes detallados con trasfondos
  - Diálogos en español e inglés
  - Objetos interactivos con callbacks

- **Assets generados**
  - 7 sprites únicos (splash, menú, cursor, personaje, objetos)
  - 4 efectos de sonido atmosféricos
  - Fuente de texto con caracteres especiales

- **Herramientas de desarrollo**
  - Scripts de compilación automatizada (build.bat/build.sh)
  - Configuración de .gitignore optimizada
  - Documentación completa del proyecto

### 🔧 Técnico / Technical
- **Arquitectura modular**: Separación clara de responsabilidades
- **Patrones de diseño**: State Machine, Observer, Factory, Singleton
- **Gestión de memoria**: Disposición correcta de recursos
- **Manejo de errores**: Try-catch en operaciones críticas
- **Localización**: Sistema extensible para múltiples idiomas

### 📚 Documentación / Documentation
- README.md completamente actualizado con nueva estructura
- GameStory.md con historia detallada y mecánicas
- Characters.md con descripción completa de personajes
- Comentarios en código para facilitar mantenimiento

### 🎮 Gameplay
- **Controles intuitivos**: Mouse/touch para interacción
- **Feedback visual**: Cursor personalizado, efectos de hover
- **Audio atmosférico**: Sonidos generados proceduralmente
- **Progresión narrativa**: Tutorial → Juego → Diálogos
- **Sistema de guardado**: Persistencia de progreso del jugador

### 🌍 Localización / Localization
- **Español**: Textos, diálogos y UI completamente traducidos
- **English**: Full translation for all game content
- **Sistema extensible**: Fácil adición de nuevos idiomas

## [Próximas versiones / Upcoming versions]

### [1.1.0] - Planificado / Planned
- [ ] Capítulo 2: "Señales en la Oscuridad"
- [ ] Sistema de cordura/sanidad mental
- [ ] Más puzzles ambientales
- [ ] Efectos visuales mejorados
- [ ] Música de fondo dinámica

### [1.2.0] - Planificado / Planned
- [ ] Múltiples finales
- [ ] Sistema de logros
- [ ] Modo historia extendido
- [ ] Soporte para más idiomas (Francés, Alemán)
- [ ] Optimizaciones de rendimiento

---

## Formato del Changelog

Este changelog sigue el formato de [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
y este proyecto adhiere al [Versionado Semántico](https://semver.org/lang/es/).

### Tipos de cambios:
- **✨ Añadido** para nuevas funcionalidades
- **🔄 Cambiado** para cambios en funcionalidades existentes
- **❌ Obsoleto** para funcionalidades que serán eliminadas
- **🗑️ Eliminado** para funcionalidades eliminadas
- **🐛 Corregido** para corrección de errores
- **🔒 Seguridad** para vulnerabilidades