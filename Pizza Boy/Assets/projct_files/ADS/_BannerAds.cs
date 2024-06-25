using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class _BannerAds : MonoBehaviour
{
    [SerializeField] private string AndroidAdUnit_ID;
    [SerializeField] private string IosAdUnit_ID;
    private string AdUnit_ID;
    private void Awake()
    {
#if UNITY_iOS
AdUnit_ID= IosAdUnit_ID;
#elif UNITY_ANDROID
        AdUnit_ID = AndroidAdUnit_ID;
#endif

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
       
    }
    public void LoadBanner()
    {
        BannerLoadOptions Options = new BannerLoadOptions()
        {
            loadCallback = bannerLoaded,
            errorCallback = bannerLoadedError


        };
        Advertisement.Banner.Load(AdUnit_ID, Options);
       

    }

    private void bannerLoaded()
    {
        Debug.Log(" Banner---Ad----Loaded");
    }

    private void bannerLoadedError(string message)
    {
       Debug.Log(message);
    }
    public void ShowBannerAds()
    {
        BannerOptions Options = new BannerOptions()
        {
            showCallback = bannershown,
            clickCallback = bannerclicked,
            hideCallback = bannerhide

        };
        Advertisement.Banner.Show(AdUnit_ID, Options);
       
    }

    private void bannershown()
    {
        
    }

    private void bannerclicked()
    {
        Debug.Log(" Banner-------Clicked");
    }

    private void bannerhide()
    {
        
    }
    public void HideBannerAds()
    {
        Advertisement.Banner.Hide();
    }
}
