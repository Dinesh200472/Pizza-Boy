using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class game_state_ui : MonoBehaviour
{
    public   TextMeshProUGUI cashtext;
   
    public void Start()
    {
        game_start();
    }
    public  void game_start()
    {
        int cash= Player_Data.cash;
        int not = Player_Data.hats;
       
        cashtext.text = cash.ToString();
       
    }
    private void OnApplicationQuit()
    {
        Player_Data.save_vehicle();
    }

}
