using System;

namespace FlippyTileGame.Settings
{
    public static class FlippyTileGameSettings
    {
        public static string GameDataFolder { get; set; } = @"C:\ProgramData\FlippyTileGame\";
        public static string RegistryPath { get; set; } = @"C:\ProgramData\FlippyTileGame\Registry.txt";
        public static TimeSpan MaxGameTime { get; set; } = TimeSpan.FromSeconds(60);
        public static string LeaderBoardPath { get; set; } = @"C:\ProgramData\FlippyTileLeaderBoard\FlippyTileLeaderBoard.txt";
    }
}
