using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
   
    public GameObject Hats;
    public static  int currentlevel;
    public GameObject level_menu;
    public GameObject Spawn;
    [Header("Target")]
    public GameObject[] f1 = new GameObject[5];
    [Header("buttons")]
    public   Button[] l = new Button[5];
    [Header("LeveL")]
    public LeveL_Creation L1;
    public LeveL_Creation L6;
   
    public Level_Manager instance;
    private void Awake()
    {
        // Ensure only one instance of this script exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Debug.Log("null");
        }
       
    }

    private void Start()
    {

        Debug.Log(Level_Data.NoOfPizzas);
        assign_buttons();
 
       
    }
    
    public void  Level_1()
    {
        if (check_hats() || Game_Manager.isretry)
        {
           
            level_menu.SetActive(false);
            Player_Data.level = 1;
            Level_Data.update_data(L6.Cash, L6.pizza, Spawn, L6.target, L6.timer,L6.no_of_target);
            Debug.Log(Level_Data.NoOfPizzas);
            //Level_Data.update_data(100, 1, Spawn, f1[0], 45);
            SceneManager.LoadScene(1);
        }
        else
        {
            not_enough_hats();
        }

    }
    public void Level_2()
    {
        if (check_hats())
        {
            level_menu.SetActive(false);
            Player_Data.level = 2;

            Level_Data.update_data(200, 2, Spawn, f1[1], 60);
            SceneManager.LoadScene(1);
        }
        else
        {
            not_enough_hats();
        }
    }
    public void Level_3()
    {
        if (check_hats())
        {
            level_menu.SetActive(false);
            Player_Data.level = 3;

            Level_Data.update_data(300, 3, Spawn, f1[2], 60);
            SceneManager.LoadScene(1);
        }
        else
        {
            not_enough_hats();
        }
    }
    public void Level_4()
    {
        if (check_hats())
        {
            level_menu.SetActive(false);
            Player_Data.level = 4;

            Level_Data.update_data(400, 4, Spawn, f1[3], 80);
            SceneManager.LoadScene(1);
        }
        else
        {
            not_enough_hats();
        }
    }
    public void Level_5()
    {
        if (check_hats())
        {
            level_menu.SetActive(false);

            Player_Data.level = 5;
            Level_Data.update_data(500, 5, Spawn, f1[4], 90);
            SceneManager.LoadScene(1);
        }
        else
        {
            not_enough_hats();
        }
    }
    
   
    public void assign_buttons()
    {
        Button_Data.update_data(l);
    }
    private void OnApplicationQuit()
    {
        Player_Data.save_vehicle();
    }
    private bool check_hats()
    {
        if (Player_Data.hats > 0)
        {
            return true;
        }
        else return false;
    }
    public void not_enough_hats()
       {
         Hats.SetActive(true);
    }

}
