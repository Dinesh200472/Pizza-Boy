using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS_Selection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gps_n, JetGps;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(JetController.isJet)
        {
            JetGps.SetActive(true);
            Gps_n.SetActive(false);
        }
        else
        {
            Gps_n.SetActive(true);
            JetGps.SetActive(false);
        }
    }
}
