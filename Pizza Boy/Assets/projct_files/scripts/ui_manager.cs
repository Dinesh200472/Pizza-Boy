using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_manager : MonoBehaviour
{
    public GameObject pause_panel;
    [SerializeField] GameObject hats;
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
            Player_Data.update_hats(-1);
            Button_Data.retry(Player_Data.level);
        }

    }
}
