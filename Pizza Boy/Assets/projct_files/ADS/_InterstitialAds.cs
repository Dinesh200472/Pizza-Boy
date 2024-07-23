using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class _InterstitialAds : MonoBehaviour,IUnityAdsLoadListener,IUnityAdsShowListener
{
    [SerializeField] private string AndroidAdUnit_ID;
    [SerializeField] private string IosAdUnit_ID;
    private string AdUnit_ID;
    private int num = -1;
    private float Temp_time;
    private void Awake()
    {
#if UNITY_iOS
AdUnit_ID= IosAdUnit_ID;
#elif UNITY_ANDROID
 AdUnit_ID = AndroidAdUnit_ID;
#endif
    }
    public void LoadInteerstitialAd()
    {
        Advertisement.Load(AdUnit_ID, this);
    }
    public void ShowInteerstitialAd(int n , float time)
    {
        num = n;
        Temp_time = time;
        Advertisement.Show(AdUnit_ID, this);
    }


    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == AdUnit_ID)
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
               if(num==1)
                {
                    Game_Manager.instance.CrashTime(Temp_time);
                    Debug.Log("Crashed Time Updated : " + Temp_time);
                    num = -1;
                    Temp_time = 0;
                    
                }
                else
                {
                    Debug.Log("Nothing will happen num is constant value : " + num);
                }
            }
            
        }
        else
        {
            Debug.Log("Adds Completed Problems");
        }
        _Ads_Initialization.instance.AddLoad();
    }
    
}
