using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;
using System.Data;
using Unity.VisualScripting;

public class Menu_Manager : MonoBehaviour
{

    public GameObject Main_menu;
    public GameObject vehicle_selection;
    public TextMeshProUGUI cashtext;
    [SerializeField] Camera CAM;
    [SerializeField] GameObject point;
    [SerializeField] Transform vsa;
  
    private void Start()
    {

        Time.timeScale = 1;


    }
    public void PLAY()
    {
       
        Main_menu.SetActive(false);
        try
        {


            CAM.transform.DOMove   (point.transform.position,2);
            CAM.transform.rotation.SetLookRotation(vsa.transform.position);
        }
        catch(Exception e)
        {
            Debug.LogException(e);  
        }

        Invoke("level_menu", 2);
    }
    public void level_menu()
    {
      vehicle_selection.SetActive(true);
    

    }
    public void quit()
    {
        Application.Quit();
        
    }
    public void reset1()
    {
        Player_Data.update_data(0, 0);
        Player_Data.no_of_owned_vehicle = 0;
        Player_Data.level = 0;
        int[] mpty = new int[8];
        SaveSystem.SaveIntArray(mpty);
        Player_Data.owned_vehicle =mpty;
    }
    public void noreset()
    {
        try
        {
            Player_Data.load();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

       
        for (int i = 0; i < Player_Data.no_of_owned_vehicle; i++)
        {
           Debug.Log( Player_Data.owned_vehicle[i]);
        }
        
        Button_Data.level_sd();
        cashtext.text =Player_Data.cash.ToString();
        Debug.Log("OWNED_VEHICL"+Player_Data.no_of_owned_vehicle);
        Debug.Log("level" + Player_Data.level);
        Debug.Log("cash" + Player_Data.cash);
    }
    private void OnApplicationQuit()
    {
        Player_Data.save_vehicle();
    }
}
