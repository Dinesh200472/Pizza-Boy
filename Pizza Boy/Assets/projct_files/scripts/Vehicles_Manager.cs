using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles_Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Vehicle;
    int len;
    void Start()
    {
        
        for (int i = 0; i < Vehicle.Length;i++)
        {
            Vehicle[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        len = Player_Data.level;
        if(len > 0 && len < 6 )
        {
            Stage1();
        }
        if (len > 5 && len < 11)
        {
            Stage2();
        }
        if (len > 11 && len < 16)
        {
            Stage3();
        }
        if (len > 16 && len < 21)
        {
            Stage4();
        }
    }

    public void Stage1()
    {
        
        for(int i = 0;i<35;i++)
        {
            Vehicle[i].SetActive(true);
        }
    }

    public void Stage2()
    {
        for (int i = 35; i < 70; i++)
        {
            Vehicle[i].SetActive(true);
        }
    }
    public void Stage3()
    {
        for (int i = 70; i < 105; i++)
        {
            Vehicle[i].SetActive(true);
        }
    }
    public void Stage4()
    {
        for (int i = 105; i < 140; i++)
        {
            Vehicle[i].SetActive(true);
        }
    }
}
