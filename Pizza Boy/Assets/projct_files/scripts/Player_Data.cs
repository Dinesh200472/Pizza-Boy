
using System;
using Unity.VisualScripting;
using UnityEngine;

public static class Player_Data
{
    public static string name;
    public static string playerid;
    [Range(0,5)]
    public static int hats=3;
    public static int cash;
    public static int level;
    public static int no_of_owned_vehicle = 0;
    public static int[] owned_vehicle = new int[8];

    public static void update_data(int x, int y)
    {
        level = x;
        cash = y;
        save(level, cash);
    }
    public static void update_data(int x)
    {

        cash += x;
        save(level, cash);
    }
    public static void save(int l, int c)
    {
        

        PlayerPrefs.SetInt("savedLevel", l);
        PlayerPrefs.SetInt("savedCash", c);
        PlayerPrefs.Save();


    }
    public static void load()
    {
        owned_vehicle = SaveSystem.LoadIntArray();
        cash = PlayerPrefs.GetInt("savedCash");
        no_of_owned_vehicle = PlayerPrefs.GetInt("no_vehicle_ownd");
        level = PlayerPrefs.GetInt("savedLevel");

    }
    public static bool isowned(int id)
    {

        bool x = false;
        for (int i = 0; i < owned_vehicle.Length; i++)
        {
            if (owned_vehicle[i] == id)
            {
                x = true;
                break;
            }

        }
        return x;



    }
    public static void update_vehicles(int id)
    {

        if (!isowned(id))
        {

            owned_vehicle[no_of_owned_vehicle] = id;
            Debug.Log("updated successfull" + id);
            no_of_owned_vehicle = no_of_owned_vehicle+ 1;
        }
        else
        {
            Debug.Log("already in");
        }

        return;
    }
    public static void save_vehicle()
    {
        PlayerPrefs.SetInt("savedCash", cash);
        PlayerPrefs.SetInt("no_vehicle_ownd",no_of_owned_vehicle);
        
        SaveSystem.SaveIntArray(owned_vehicle);

    }
    public static void update_hats(int x)
    {
        if (x == 0)
            hats = 0;
        else
            hats = x;
    }
    public static void repleshment()
    {

    }
   
    
   
}
