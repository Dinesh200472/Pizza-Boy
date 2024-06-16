using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_manager : MonoBehaviour
{
    public GameObject pause_panel;
    [SerializeField] GameObject hats;
    public GameObject crash_panel;
    public  static GameObject crash;

    private void Start()
    {
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
        SceneManager.LoadScene(0);
    }
    public void retry_level()
    {
        Game_Manager.isretry = true;
        if (Player_Data.hats <= 0)
        {

            hats.SetActive(true);

        }
        else

        {
            finish_manager.count = 1;
            Player_Data.update_hats(-1);
            Button_Data.retry(Level_Data.Level);
        }

    }
    public  static void crashed()
    {
        Time.timeScale = 0;
        crash.SetActive(true);
    }
    public void crash_ad()
    {
        
        Time.timeScale = 1;
        crash.SetActive(false);
        _AdsManager.instance._interstitialAds.ShowInteerstitialAd();
    }
    public void extracash()
    {
        _AdsManager.instance._rewardedAds.ShowRewadedAd();
    }
}
