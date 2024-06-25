using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public  class Game_Manager : MonoBehaviour
{
    public GameObject gps;
    
    public static bool isretry;
    public GameObject Temp_Target;
    public GameObject player;
    public TextMeshProUGUI timetext;
    public TextMeshProUGUI cashtext;
    public GameObject timeup;
    public GameObject finish;
    public bool isTimerRunning;
    public static  float timeRemaining;
    public bool isfinished;
    private GameObject prefabToSpawn;
    private Level_Manager levelManager;
    public GameObject next_level_ui;
    [SerializeField] private TextMeshProUGUI nop;
    
   
    private void Awake()
    {
        DisplayCash();
        display_nop();
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
        //spawnT();
    }

    
    private void Update()
    {
        extratime();
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
        if(Car_Controller.isfinished&&isfinished || CycleController.  isfinished && isfinished || ScootyController.isfinished && isfinished)
        {
            finishfn();
            isfinished = false;

        }
        DisplayCash();
        

        
    }
    IEnumerator SpawnTargets()
    {
        for (int i = 0; i < Level_Data.NoOfPizzas; i++)
        {
            Temp_Target = Instantiate(Level_Data.Finishs[i], Level_Data.Finishs[i].transform.position, Level_Data.Finishs[i].transform.rotation);
            yield return StartCoroutine(WaitForPlayerMsg());
            
        }
    }
    IEnumerator WaitForPlayerMsg()
    {
        while (!PlayerMsg())
        {
            yield return null; 
        }
    }
    bool PlayerMsg()
    {
        GameObject Temp = GameObject.FindWithTag("Finish");
        IVehicleController GetCurrentVehicle = player.GetComponent<IVehicleController>();
        if(GetCurrentVehicle.OnTarget() == true)
        {
            Destroy(Temp);//Destroy Current Target
            
            return true;
        }
        return false;
    }
    public void level_load()
    {
        try
        {
            player = Instantiate(Level_Data.Vehicle, Level_Data.Spawm.transform.position, Level_Data.Spawm.transform.rotation);
            StartCoroutine(SpawnTargets());
            gps.SetActive(true);
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
       // AudioManager.instance.OnTimeUp();
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
        Button_Data.nextlevel(Level_Data.Level);
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
        display_nop();
    }
    public  void Reset_Gps()
    {
        gps.SetActive(true);

    }
    public void display_nop()
    {
        nop.text = Level_Data.NoOfPizzas + "/";

    }
    public void extratime()
    {
        if(_RewardedAds.adcomplete())
        {
            Debug.Log("  -------------------------extratime-------------------------");
            timeRemaining = Level_Data.time / 2;
            isTimerRunning = true;
            return;
        }
        _RewardedAds.isrewarded = false;
       
    }
  
   
    
    

}
