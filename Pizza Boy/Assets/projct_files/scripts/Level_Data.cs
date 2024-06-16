using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class  Level_Data 

{
    public static int Level;
    public static int Cash;
   
    public static int[] pizzas = new int[0];
    public static GameObject Spawm;
    
    public static GameObject[] Finishs = new GameObject[0];
    public static float time;
    public static GameObject Vehicle;
    public static int NoOfPizzas;
   
    
    public static void update_vehicle(GameObject v)
    {
        Vehicle = v;
    }
    public static void update_data(int l,int newcash, int[] newpizza, GameObject spawm, GameObject[] finsh, float newtime,int no_pizza)
    {
        Level = l;
        Cash = newcash;
        pizzas = newpizza;
        Spawm = spawm;
        Finishs = finsh;
        time = newtime;
        NoOfPizzas = no_pizza;  
    }


}
