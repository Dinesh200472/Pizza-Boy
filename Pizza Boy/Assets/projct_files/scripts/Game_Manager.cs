using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public  class Game_Manager : MonoBehaviour
{
    public GameObject gps;
    
    public static bool isretry;
    public GameObject player;
    public TextMeshProUGUI timetext;
    public TextMeshProUGUI cashtext;
    public GameObject timeup;
    public   GameObject finish;
    public bool isTimerRunning;
    public float timeRemaining;
    public bool isfinished;
    private GameObject prefabToSpawn;
    private Level_Manager levelManager;
    public GameObject next_level_ui;
   
    private void Awake()
    {
        DisplayCash();
    }





    public void Start()
    {
       
        Debug.Log("no of pizzas ===="+Level_Data.NoOfPizzas);
        DisplayCash();
        isfinished = true;
        Time.timeScale = 1;
        timeRemaining = Level_Data.time;
        isTimerRunning = true;
        assign();
       
      
        level_load();
       
    }
    private void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);

            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                timeup_fn();
            }
        }
        if(Car_Controller.isfinished&&isfinished || CycleController.  isfinished && isfinished)
        {
            finishfn();
            isfinished = false;

        }
        DisplayCash();
        

        
    }
    public void level_load()
    {
        try
        {
            if (Level_Data.NoOfPizzas == 1)
            {
                Instantiate(Level_Data.Finsh, Level_Data.Finsh.transform.position, Level_Data.Finsh.transform.rotation);
            }
            else
            {
                for (int i = 0; i < Level_Data.Finishs.Length; i++)
                {
                    Instantiate(Level_Data.Finishs[i], Level_Data.Finishs[i].transform.position, Level_Data.Finishs[i].transform.rotation);
                }


                if (Level_Data.Vehicle != null)
                {

                    Instantiate(Level_Data.Vehicle, Level_Data.Spawm.transform.position, Level_Data.Spawm.transform.rotation);
                    gps.SetActive(true);

                }
                else
                    Debug.Log("not availabe");
            }
           
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        
    }
    public void displayui()
    {

    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay > 0)
        {

            int minutes = Mathf.FloorToInt(timeToDisplay / 60);
            int seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timetext.text = string.Format("{00:00}:{01:00}", minutes, seconds);
        }
        else
        {
            return;
        }
        
       
    }
    void timeup_fn()
    {
       

        Time.timeScale = 0;
        timeup.SetActive(true);
    }
    void  DisplayCash()
    {
        int cash = Player_Data.cash;
        Debug.Log($"Cash: {cash}");
        cashtext.text = cash.ToString();

    }
    public void next_level()
    {
        Button_Data.nextlevel(Player_Data.level);
    }
    public void assign()
    {
        finish_manager.update(finish,next_level_ui,gps);
    }
    public void finishfn()
    {
        gps.SetActive(false);
        Invoke("Reset_Gps", 2);
        Debug.Log("------resert-----");
        DisplayCash();
    }
    public  void Reset_Gps()
    {
        gps.SetActive(true);

    }
    
   
    
    

}
