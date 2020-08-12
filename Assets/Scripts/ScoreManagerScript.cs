using UnityEngine;
using System.Collections;

public class ScoreManagerScript : MonoBehaviour {

    public static int Score { get; set; }

	// Use this for initialization
	void Start ()
    {
        (Tens.gameObject as GameObject).SetActive(false);
        (Hundreds.gameObject as GameObject).SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameStateManager.GameState != GameState.Intro && !this.Units.gameObject.activeInHierarchy)
            this.Units.gameObject.SetActive(true);

        if (previousScore != Score) //save perf from non needed calculations
        { 
            if(Score < 10)
            {
                //just draw units
                Units.sprite = numberSprites[Score];
            }
            else if(Score >= 10 && Score < 100)
            {
                (Tens.gameObject as GameObject).SetActive(true);
                Tens.sprite = numberSprites[Score / 10];
                Units.sprite = numberSprites[Score % 10];
            }
            else if(Score >= 100)
            {
                (Hundreds.gameObject as GameObject).SetActive(true);
                Hundreds.sprite = numberSprites[Score / 100];
                int rest = Score % 100;
                Tens.sprite = numberSprites[rest / 10];
                Units.sprite = numberSprites[rest % 10];
            }
        }

	}


    int previousScore = -1;
    public Sprite[] numberSprites;
    public SpriteRenderer Units, Tens, Hundreds;
    

    #region SCORE_STORE_RESTORE

    public static int GetScoreByDifficulty(GameLauncher.Difficulty difficulty)
    {
        if (difficulty == GameLauncher.Difficulty.Hard)
            return PlayerPrefs.GetInt("HSCORE_ONE", 0);

        if (difficulty == GameLauncher.Difficulty.Medium)
            return PlayerPrefs.GetInt("HSCORE_TWO", 0);

        return PlayerPrefs.GetInt("HSCORE_THREE", 0);
    }

    public static void SetScoreByDifficulty(GameLauncher.Difficulty difficulty, int score)
    {
        if (difficulty == GameLauncher.Difficulty.Hard)
            PlayerPrefs.SetInt("HSCORE_ONE", score);

        else if (difficulty == GameLauncher.Difficulty.Medium)
            PlayerPrefs.SetInt("HSCORE_TWO", score);

        else PlayerPrefs.SetInt("HSCORE_THREE", score);
    }
    
    public static void SaveNewHighScore(int score)
    {
        if (GameLauncher.GameDifficulty == GameLauncher.Difficulty.Hard)
        {
            if (score > GetScoreByDifficulty(GameLauncher.Difficulty.Hard))
                SetScoreByDifficulty(GameLauncher.Difficulty.Hard, score);
        }
        else if (GameLauncher.GameDifficulty == GameLauncher.Difficulty.Medium)
        {
            if (score > GetScoreByDifficulty(GameLauncher.Difficulty.Medium))
                SetScoreByDifficulty(GameLauncher.Difficulty.Medium, score);
        }
        else if (GameLauncher.GameDifficulty == GameLauncher.Difficulty.Easy)
        {
            if (score > GetScoreByDifficulty(GameLauncher.Difficulty.Easy))
                SetScoreByDifficulty(GameLauncher.Difficulty.Easy, score);
        }
    }
    public static void UpdateScore()
    {
        SaveNewHighScore(Score);
    }
    #endregion
}
