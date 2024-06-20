using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager instance;
    public GameObject Hats;
    public static  int currentlevel;
    public GameObject[] stages = new GameObject[0];
    [SerializeField] private int CurrentStage=1;
    public GameObject Spawn;
    [Header("buttons")]
    public   Button[] l = new Button[20];
    public GameObject Level_menu;
    
   
   
    private void Awake()
    {
        int s1 = SceneManager.GetActiveScene().buildIndex;
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
        if(s1 ==1)
        {
            Destroy(gameObject);
        }
        
       
    }

    public void OnDestroy_Duplicate()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
       
        CurrentStage = 0;
        stages[0].SetActive(true);
     
       
        assign_buttons();
        //Button_Data.level_sd();
 
       
    }
    public void Update()
    {
        
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
    public void Level(LeveL_Creation L)
    {
        if (check_hats() || Game_Manager.isretry)
        {
            
            Level_Data.update_data(L.Level,L.Cash, L.pizza, Spawn, L.target, L.timer, L.no_of_target);
            Debug.Log(Level_Data.NoOfPizzas);
            Level_menu.SetActive(false);
            SceneManager.LoadScene(1);
        }
        else
        {
            not_enough_hats();
        }

    }
    public void Next_Stage()
    {
        
        if(CurrentStage==stages.Length-1)
        {
            
            return;
        }
        else
        {
            stages[CurrentStage].SetActive(false);
            stages[CurrentStage+1].SetActive(true);
            CurrentStage += 1;
           
        }
       
       
    }
    public void Prev_Stage()
    {
        
        if(CurrentStage ==0)
        {
           
            return;
        }
        else
        {
            stages[CurrentStage].SetActive(false);
            stages[CurrentStage - 1].SetActive(true);
            CurrentStage -= 1;
         
        }
      
        
    }
    public void unlock_button( )
    {
        int Unlocked_levels = Player_Data.level;
        Debug.Log("-------------Playeer_Level" + Player_Data.level);

        for (int i = 0; i < Unlocked_levels; i++)
        {
           if(i==0)
            {
                l[i].interactable = true;
            }
            else if(i>0)
            {
                l[i].interactable = true;
                l[i+1].interactable = true;
            }
        }
       
       // Button_Data.level_sd();
    }

}
