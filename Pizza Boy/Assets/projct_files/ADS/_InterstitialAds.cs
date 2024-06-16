using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class _InterstitialAds : MonoBehaviour,IUnityAdsLoadListener,IUnityAdsShowListener
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
    }
    public void LoadInteerstitialAd()
    {
        Advertisement.Load(AdUnit_ID, this);
    }
    public void ShowInteerstitialAd()
    {
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
        throw new System.NotImplementedException();
    }
}
