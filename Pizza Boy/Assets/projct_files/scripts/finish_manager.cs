
using System;
using UnityEngine;

public  class finish_manager 
{
    public  static GameObject finish;
    public static GameObject next_level;
    public static GameObject gps;
    public  static  int count=1;
    
  
    public static void update(GameObject x,GameObject Y,GameObject z)
    {
        finish = x;
        next_level = Y;
        gps = z;
    }
    public static void finish_fn()
    {
        Debug.Log("------COUNT == "+ count);

        Debug.Log("finished");
        if(count  == Level_Data.NoOfPizzas)
        {
            Time.timeScale = 0;
            count = 1;
            Player_Data.update_data(Level_Data.Level, Level_Data.Cash);
            finish.SetActive(true);
           
           

        }
        else
        
        {
            Debug.Log(Level_Data.NoOfPizzas);
          
           
         
            

           
          
            count += 1;
        }
       

    }

  

}
