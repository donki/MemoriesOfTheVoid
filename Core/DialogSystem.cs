using System.Collections.Generic;

namespace MemoriesOfTheVoid.Systems
{
    public class DialogSystem
    {
        private Queue<string> _dialogQueue = new Queue<string>();
        public bool IsActive => _dialogQueue.Count > 0;
        public string CurrentText { get; private set; } = "";

        public void ShowDialog(string text)
        {
            _dialogQueue.Enqueue(text);
            if (string.IsNullOrEmpty(CurrentText))
            {
                NextDialog();
            }
        }

        public void ShowMultipleDialogs(params string[] texts)
        {
            foreach (var text in texts)
            {
                _dialogQueue.Enqueue(text);
            }
            
            if (string.IsNullOrEmpty(CurrentText))
            {
                NextDialog();
            }
        }

        public void NextDialog()
        {
            if (_dialogQueue.Count > 0)
            {
                CurrentText = _dialogQueue.Dequeue();
            }
            else
            {
                CurrentText = "";
            }
        }

        public void ClearDialogs()
        {
            _dialogQueue.Clear();
            CurrentText = "";
        }
    }
}

// Mover a MemoriesOfTheVoid.Core