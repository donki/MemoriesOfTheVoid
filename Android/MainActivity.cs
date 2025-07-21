using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using MemoriesOfTheVoid.Core;

namespace MemoriesOfTheVoid.Android
{
    [Activity(
        Label = "Memories of the Void",
        MainLauncher = true,
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class MainActivity : Activity
    {
        private Core.Game1 _game;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Hide the status bar and navigation bar for full immersion
            if (Window != null)
            {
                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            }
            
            _game = new Core.Game1();
            _game.RunOneFrame();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _game?.RunOneFrame();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _game?.RunOneFrame();
        }

        protected override void OnDestroy()
        {
            _game?.Exit();
            base.OnDestroy();
        }
    }
}