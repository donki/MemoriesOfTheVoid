using System.Collections.Generic;
using MemoriesOfTheVoid.Core;

namespace MemoriesOfTheVoid.Core
{
    public class PuzzleSystem
    {
        private Dictionary<string, bool> _puzzleStates;
        private Dictionary<string, List<string>> _puzzleRequirements;
        private Inventory _inventory;
        private DialogSystem _dialogSystem;
        private LocalizationSystem _localization;

        public PuzzleSystem(Inventory inventory, DialogSystem dialogSystem, LocalizationSystem localization)
        {
            _inventory = inventory;
            _dialogSystem = dialogSystem;
            _localization = localization;
            _puzzleStates = new Dictionary<string, bool>();
            _puzzleRequirements = new Dictionary<string, List<string>>();
            InitializePuzzles();
        }

        private void InitializePuzzles()
        {
            // Estado inicial de los puzzles
            _puzzleStates["POWER_RESTORED"] = false;
            _puzzleStates["PANEL_REPAIRED"] = false;
            _puzzleStates["ACCESS_GRANTED"] = false;
            _puzzleStates["ANTENNA_AVOIDED"] = true; // Comienza true, se vuelve false si el jugador la activa

            // Requisitos para cada puzzle
            _puzzleRequirements["POWER_RESTORED"] = new List<string> { "REPAIR_CABLE", "POWER_CELL" };
            _puzzleRequirements["PANEL_REPAIRED"] = new List<string> { "MULTIFUNCTION_TOOL" };
            _puzzleRequirements["ACCESS_GRANTED"] = new List<string> { "ACCESS_CARD" };
        }

        public bool CheckPuzzleRequirements(string puzzleId)
        {
            if (!_puzzleRequirements.ContainsKey(puzzleId))
                return false;

            foreach (var item in _puzzleRequirements[puzzleId])
            {
                if (!_inventory.HasItem(item))
                    return false;
            }

            return true;
        }

        public void TrySolvePuzzle(string puzzleId)
        {
            if (!_puzzleRequirements.ContainsKey(puzzleId))
                return;

            if (CheckPuzzleRequirements(puzzleId))
            {
                _puzzleStates[puzzleId] = true;

                switch (puzzleId)
                {
                    case "POWER_RESTORED":
                        _dialogSystem.ShowDialog(_localization.GetText("POWER_RESTORED"));
                        break;
                    case "PANEL_REPAIRED":
                        _dialogSystem.ShowDialog(_localization.GetText("PANEL_REPAIRED"));
                        break;
                    case "ACCESS_GRANTED":
                        _dialogSystem.ShowDialog(_localization.GetText("ACCESS_GRANTED"));
                        break;
                    case "ANTENNA_AVOIDED":
                        if (!_puzzleStates["ANTENNA_AVOIDED"])
                            _dialogSystem.ShowDialog(_localization.GetText("GAME_OVER_ANTENA"));
                        break;
                }
            }
            else
            {
                // Mensaje de que faltan items
                _dialogSystem.ShowDialog(_localization.GetText("MISSING_ITEMS"));
            }
        }

        public bool IsPuzzleSolved(string puzzleId)
        {
            return _puzzleStates.ContainsKey(puzzleId) && _puzzleStates[puzzleId];
        }

        public void ResetPuzzles()
        {
            foreach (var key in _puzzleStates.Keys)
            {
                _puzzleStates[key] = false;
            }
            _puzzleStates["ANTENNA_AVOIDED"] = true; // Reset to initial state
        }
    }
}
