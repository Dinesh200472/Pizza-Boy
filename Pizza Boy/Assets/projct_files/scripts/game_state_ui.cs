using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class game_state_ui : MonoBehaviour
{
    public   TextMeshProUGUI cashtext;
    public TextMeshProUGUI hats;
    public void Start()
    {
        game_start();
    }
    public  void game_start()
    {
        int cash= Player_Data.cash;
        int not = Player_Data.hats;
       
        cashtext.text = cash.ToString();
        hats.text = not.ToString();
    }
    private void OnApplicationQuit()
    {
        Player_Data.save_vehicle();
    }

}
