using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using GoogleMobileAds.Api;

public class GameLauncher : MonoBehaviour
{
    public static bool StartGame { get; private set; }

    public static float SpawnerScript_SpawnTime { get; private set; }

    public enum Difficulty : int
    {
        Easy = 4,
        Medium = 3,
        Hard = 2,
    };
    public static Difficulty GameDifficulty = Difficulty.Easy;

    public static void LaunchGame(Difficulty difficulty)
    {
        GameDifficulty = difficulty;
        SpawnerScript_SpawnTime = (int)GameDifficulty;
        StartGame = true;
    }

    public static void ResetGame()
    {
        ScoreManagerScript.UpdateScore();
        StartGame = false;
    }
}
