using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_manager : MonoBehaviour
{
    public GameObject pause_panel;
    
    public GameObject crash_panel;
    public GameObject time_up;
    public  static GameObject crash;
    public GameObject finish;
    public GameObject Collect;
    public TextMeshProUGUI cash_text;
    public TextMeshProUGUI cashtext;

    private void Start()
    {
        cash_text.text = Level_Data.Cash.ToString();
        crash = crash_panel;
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
        Level_Manager.instance.unlock_button();
    }
    public void retry_level()
    {
        Game_Manager.isretry = true;
       
            finish_manager.count = 1;
            Player_Data.update_hats(-1);
            Button_Data.retry(Level_Data.Level);
        

    }
    public  static void crashed()
    {
        Time.timeScale = 0;
        //AudioManager.instance.OnCrash();
        crash.SetActive(true);
    }
    public void crash_ad()
    {      
        crash.SetActive(false);
        _AdsManager.instance._interstitialAds.ShowInteerstitialAd();
        Time.timeScale = 1;
    }
    public void extracash()
    {
        _AdsManager.instance._rewardedAds.ShowRewadedAd();
        float extracash = Level_Data.Cash * 2;
        Player_Data.update_data(Level_Data.Level, Level_Data.Cash);
        cash_text.text = extracash.ToString();
        DisplayCash();

    }
    public void extratime()
    {
        _AdsManager.instance._rewardedAds.ShowRewadedAd();
        time_up.SetActive(false);
        Time.timeScale = 1;
    }

    public void collect()
    {
        Collect.SetActive(false);
        finish.SetActive(true);
    }

    void DisplayCash()

    {
            int cash = Player_Data.cash;
            Debug.Log($"Cash: {cash}");
            cashtext.text = cash.ToString();

    }
}
