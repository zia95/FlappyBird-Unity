using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using GoogleMobileAds.Api;

public class MainMenuScript : MonoBehaviour
{
    public GameObject PanelDifficultySelection;
    public Toggle DifficultyEasy;
    public Toggle DifficultyMedium;
    public Toggle DifficultyHard;

    public GameObject PanelHighScore;
    public Text TextDifficulty;
    public Text TextHighScore;

    public Sprite HighScorePressed;
    public Sprite HighScoreNormal;
    public Sprite OptionsPressed;
    public Sprite OptionsNormal;

    public Button btnHighScores;
    public Button btnOptions;
        
    public string PanelShowClipName = "panel_show";
    public string PanelHideClipName = "panel_hide";
    public float MaxPanelClipTime = 0.1f;

    private bool Locked = false;
    private GameLauncher.Difficulty GameDifficulty = GameLauncher.Difficulty.Easy;

    private Animator pnlDifficultyAnimator = null;
    private Animator pnlHighScoreAnimator = null;

    private static InterstitialAd Interstitial = null;
    private static bool InterstitialShown = false;

    public static void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6600354513005133/3076458405";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public static void RequestInterstitial()
    {
        if (InterstitialShown) return;
        if (Interstitial == null)
        {
            string adUnitId = "ca-app-pub-6600354513005133/6029924804";

            // Initialize an InterstitialAd.
            Interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            Interstitial.LoadAd(request);
        }
        else if (Interstitial.IsLoaded())
        {
            Interstitial.Show();
            InterstitialShown = true;
        }
            
    }

    void Start()
    {
        //RequestBanner();
        RequestInterstitial();
        //-----------Animator-------------------
        pnlDifficultyAnimator = PanelDifficultySelection.GetComponent<Animator>();
        pnlHighScoreAnimator = PanelHighScore.GetComponent<Animator>();

        //-----------DIFFICULTY-------------------
        GameDifficulty = GetDifficulty();
        if (GameDifficulty == GameLauncher.Difficulty.Medium)
        {
            DifficultyMedium.isOn = true;
        }
        else if (GameDifficulty == GameLauncher.Difficulty.Hard)
        {
            DifficultyHard.isOn = true;
        }
        else if (GameDifficulty == GameLauncher.Difficulty.Easy)
        {
            DifficultyEasy.isOn = true;
        }
    }

    GameLauncher.Difficulty GetDifficulty()
    {
        return (GameLauncher.Difficulty) PlayerPrefs.GetInt("Difficulty", 4);
    }

    void SaveDifficulty(GameLauncher.Difficulty difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", (int) difficulty);
    }

    public void DifficulyChanged(int difficulty)
    {
        if (Locked) return;
        Locked = true;

        if (difficulty == 1)
        {
            DifficultyMedium.isOn = false;
            DifficultyHard.isOn = false;
            GameDifficulty = GameLauncher.Difficulty.Easy;
            SaveDifficulty(GameLauncher.Difficulty.Easy);
        }
        else if (difficulty == 2)
        {
            DifficultyEasy.isOn = false;
            DifficultyHard.isOn = false;
            GameDifficulty = GameLauncher.Difficulty.Medium;
            SaveDifficulty(GameLauncher.Difficulty.Medium);
        }
        else if (difficulty == 3)
        {
            DifficultyMedium.isOn = false;
            DifficultyEasy.isOn = false;
            GameDifficulty = GameLauncher.Difficulty.Hard;
            SaveDifficulty(GameLauncher.Difficulty.Hard);
        }

        Locked = false;
    }

    public void StartGameClick()
    {
        GameLauncher.LaunchGame(GameDifficulty);
    }
    public void HighScoreClick()
    {
        var active = !PanelHighScore.activeInHierarchy;

        if (active)
        {
            TextDifficulty.text = "" + GameDifficulty + "";
            TextHighScore.text = ScoreManagerScript.GetScoreByDifficulty(GameDifficulty).ToString();
        }


        HideDifficultyPanel();
        
        if (active) ShowHighScorePanel();
        else HideHighScorePanel();
    }
    public void DifficultySelectClick()
    {
        var active = !PanelDifficultySelection.activeInHierarchy;

        HideHighScorePanel();

        if(active) ShowDifficultyPanel();
        else HideDifficultyPanel();
    }

    #region HIDE_SHOW_PANELS
    private void ShowDifficultyPanel()
    {
        btnOptions.image.sprite = OptionsPressed;

        if (IsInvoking("DeactiveDifficultyPanel"))
            CancelInvoke("DeactiveDifficultyPanel");

        PanelDifficultySelection.SetActive(true);
        pnlDifficultyAnimator.Play(PanelShowClipName);
    }
    private void HideDifficultyPanel()
    {
        btnOptions.image.sprite = OptionsNormal;

        pnlDifficultyAnimator.Play(PanelHideClipName);
        Invoke("DeactiveDifficultyPanel", MaxPanelClipTime);
    }
    private void DeactiveDifficultyPanel()
    {
        PanelDifficultySelection.SetActive(false);
    }

    private void ShowHighScorePanel()
    {
        btnHighScores.image.sprite = HighScorePressed;

        if (IsInvoking("DeactiveHighScorePanel"))
            CancelInvoke("DeactiveHighScorePanel");

        PanelHighScore.SetActive(true);
        pnlHighScoreAnimator.Play(PanelShowClipName);
    }
    private void HideHighScorePanel()
    {
        btnHighScores.image.sprite = HighScoreNormal;

        pnlHighScoreAnimator.Play(PanelHideClipName);
        Invoke("DeactiveHighScorePanel", MaxPanelClipTime);
    }
    private void DeactiveHighScorePanel()
    {
        PanelHighScore.SetActive(false);
    }
    #endregion
}
