using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Data 
{
    public static Button[] button = new Button[20];


    public static void update_data(Button[] l)
    {
        for(int i = 0; i < button.Length; i++)
        {
            button[i] = l[i];
        }
    }
    public static void nextlevel(int level)
    {
        for (int i = level;i<=level;i++)
        {
            Debug.Log("nextlevel" + i);
            try
            {
                button[i].onClick.Invoke();
            }
            catch(Exception e)
            {
                Debug.Log(e.ToString());

            }
        }
    }
    public static void level_sd()
    {
        int unlock = Player_Data.level;

        for (int i = 0; i <= unlock; i++)
        {
            button[i].interactable = true;
        }
        

    }
    public static void retry(int level)
    {
        button[level-1].onClick.Invoke();
    }
}
