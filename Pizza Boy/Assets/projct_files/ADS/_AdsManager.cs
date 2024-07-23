using UnityEngine;

public class _AdsManager : MonoBehaviour
{
    public _Ads_Initialization initialization;
    public _RewardedAds _rewardedAds;
    public _BannerAds _bannerAds;
    public _InterstitialAds _interstitialAds;
    public static _AdsManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null && instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        _bannerAds.LoadBanner();
        _interstitialAds.LoadInteerstitialAd();
        _rewardedAds.LoadRewardedAd();
    }
}
