@echo off
echo ========================================
echo  MEMORIAS DEL VACIO - BUILD SCRIPT
echo  MEMORIES OF THE VOID - BUILD SCRIPT
echo ========================================
echo.

echo Generando assets...
echo Generating assets...
dotnet run --project MemoriesOfTheVoid.Tools.csproj
if %errorlevel% neq 0 (
    echo Error generando assets / Error generating assets
    pause
    exit /b 1
)

echo.
echo Assets generados exitosamente / Assets generated successfully
echo.

echo Compilando version Windows...
echo Building Windows version...
dotnet build MemoriesOfTheVoid.Windows.csproj --configuration Release
if %errorlevel% neq 0 (
    echo Error compilando Windows / Error building Windows
    pause
    exit /b 1
)

echo.
echo Compilando version Android...
echo Building Android version...
dotnet build MemoriesOfTheVoid.Android.csproj --configuration Release
if %errorlevel% neq 0 (
    echo Error compilando Android / Error building Android
    pause
    exit /b 1
)

echo.
echo ========================================
echo  COMPILACION COMPLETADA EXITOSAMENTE
echo  BUILD COMPLETED SUCCESSFULLY
echo ========================================
echo.
echo Para ejecutar Windows: dotnet run --project MemoriesOfTheVoid.Windows.csproj
echo To run Windows: dotnet run --project MemoriesOfTheVoid.Windows.csproj
echo.
pause