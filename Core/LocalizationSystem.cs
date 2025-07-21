using System.Collections.Generic;

namespace MemoriesOfTheVoid.Core
{
    public class LocalizationSystem
    {
        private Dictionary<string, Dictionary<string, string>> _localization;
        private string _currentLanguage = "es";

        public LocalizationSystem()
        {
            InitializeLocalization();
        }

        private void InitializeLocalization()
        {
            _localization = new Dictionary<string, Dictionary<string, string>>
            {
                ["es"] = new Dictionary<string, string>
                {
                    ["POWER_RESTORED"] = "La energía ha sido restaurada.",
                    ["PANEL_REPAIRED"] = "El panel ha sido reparado.",
                    ["ACCESS_GRANTED"] = "Acceso concedido.",
                    ["GAME_OVER_ANTENA"] = "La antena ha sido activada. Fin del juego.",
                    ["MISSING_ITEMS"] = "Te faltan algunos objetos necesarios."
                },
                ["en"] = new Dictionary<string, string>
                {
                    ["POWER_RESTORED"] = "Power has been restored.",
                    ["PANEL_REPAIRED"] = "Panel has been repaired.",
                    ["ACCESS_GRANTED"] = "Access granted.",
                    ["GAME_OVER_ANTENA"] = "Antenna has been activated. Game over.",
                    ["MISSING_ITEMS"] = "You are missing some required items."
                }
            };
        }

        public string GetText(string key)
        {
            if (_localization.ContainsKey(_currentLanguage) &&
                _localization[_currentLanguage].ContainsKey(key))
            {
                return _localization[_currentLanguage][key];
            }
            return key;
        }

        public void SetLanguage(string language)
        {
            if (_localization.ContainsKey(language))
            {
                _currentLanguage = language;
            }
        }
    }
}