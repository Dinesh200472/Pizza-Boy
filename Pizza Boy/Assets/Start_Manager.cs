using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Manager : MonoBehaviour
{
    private void Awake()
    {
        Invoke("banneraddelay",1);
    }

    public void banneraddelay()
    {

        _AdsManager.instance._bannerAds.ShowBannerAds();
    }
    
}
