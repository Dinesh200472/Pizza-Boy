using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ui_manager : MonoBehaviour
{
    public GameObject pause_panel;
    public bool isCrashed = false;
    public GameObject crash_panel;
    public GameObject time_up;
    public  static GameObject crash;
    public GameObject finish;
    public GameObject Collect;
    public TextMeshProUGUI cash_text;
    public TextMeshProUGUI cashtext;
    public Button[] button;

    public static ui_manager instance;

    private void Awake()
    {
        instance = this;
    }

    private bool addcash = false;
    

    private void Start()
    {
        cash_text.text = Level_Data.Cash.ToString();
        crash = crash_panel;
    }


    private void Update()
    {
        if(CheckInternet())
        {
            for(int i = 0;i < button.Length;i++)
            {
                button[i].interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < button.Length; i++)
            {
                button[i].interactable = false;
            }
        }
    }

    public bool CheckInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    public void pause_button()
    {
        pause_panel.SetActive(true);
        Time.timeScale = 0;

    }
    public void resume_button()
    {
        pause_panel.SetActive(false);
        Time.timeScale = 1;
    }
    public void mainmenu_button()
    {
        Level_Manager.instance.OnDestroy_Duplicate();
        SceneManager.LoadScene(0);
        Player_Data.save_vehicle();
        Player_Data.load();
        Level_Manager.instance.unlock_button();
        isCrashed = false;
    }
    public void retry_level()
    {
        Game_Manager.isretry = true;
        finish_manager.count = 1;
        Player_Data.update_hats(-1);
        Button_Data.retry(Level_Data.Level);
        isCrashed = false;
    }
    public  void crashed()
    {
        Time.timeScale = 0;
        isCrashed = true;
        AudioManager.instance.PlaySound(AudioManager.instance.crash);
        crash.SetActive(true);
    }
    public void crash_ad()
    {      
        crash.SetActive(false);
        float time = Game_Manager.timeRemaining;
        _AdsManager.instance._interstitialAds.ShowInteerstitialAd(1,time);
        Time.timeScale = 1;
        isCrashed = false;
    }
    public void extracash()
    {
        _AdsManager.instance._rewardedAds.ShowRewadedAd(0);
        addcash = true;
        DisplayCash();

    }

   

    public void extratime()
    {
        _AdsManager.instance._rewardedAds.ShowRewadedAd(1);
        time_up.SetActive(false);
        Time.timeScale = 1;
        
    }

    public void collect()
    {
        Collect.SetActive(false);
        int extracash = 0;
        if(addcash)
        {
            extracash = Level_Data.Cash * 2;
            addcash = false;
        }
        else
        {
            extracash = Level_Data.Cash;
        }
        AudioManager.instance.PlaySound(AudioManager.instance.cash);
        Player_Data.update_data(Level_Data.Level, extracash);
        finish.SetActive(true);
    }

    void DisplayCash()
    {
            int cash = Player_Data.cash;
            Debug.Log($"Cash: {cash}");
            cashtext.text = cash.ToString();
    }
}
