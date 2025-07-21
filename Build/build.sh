#!/bin/bash

echo "========================================"
echo " MEMORIAS DEL VACIO - BUILD SCRIPT"
echo " MEMORIES OF THE VOID - BUILD SCRIPT"
echo "========================================"
echo

echo "Generando assets..."
echo "Generating assets..."
dotnet run --project MemoriesOfTheVoid.Tools.csproj
if [ $? -ne 0 ]; then
    echo "Error generando assets / Error generating assets"
    exit 1
fi

echo
echo "Assets generados exitosamente / Assets generated successfully"
echo

echo "Compilando version Windows..."
echo "Building Windows version..."
dotnet build MemoriesOfTheVoid.Windows.csproj --configuration Release
if [ $? -ne 0 ]; then
    echo "Error compilando Windows / Error building Windows"
    exit 1
fi

echo
echo "Compilando version Android..."
echo "Building Android version..."
dotnet build MemoriesOfTheVoid.Android.csproj --configuration Release
if [ $? -ne 0 ]; then
    echo "Error compilando Android / Error building Android"
    exit 1
fi

echo
echo "========================================"
echo " COMPILACION COMPLETADA EXITOSAMENTE"
echo " BUILD COMPLETED SUCCESSFULLY"
echo "========================================"
echo
echo "Para ejecutar Windows: dotnet run --project MemoriesOfTheVoid.Windows.csproj"
echo "To run Windows: dotnet run --project MemoriesOfTheVoid.Windows.csproj"
echo