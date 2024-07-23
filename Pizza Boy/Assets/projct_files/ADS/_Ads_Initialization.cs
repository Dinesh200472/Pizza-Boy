using UnityEngine;
using UnityEngine.Advertisements;

public class _Ads_Initialization : MonoBehaviour,IUnityAdsInitializationListener
{
    [SerializeField] private string _AndroidGameId;
    [SerializeField] private string _IosGameID;
    [SerializeField] private bool IsTesting;
    private string GameId;
    public static _Ads_Initialization instance;

   

    private void Awake()
    {

        instance = this;
#if UNITY_iOS
GameId = _IosGameID;
#elif UNITY_ANDROID
GameId =  _AndroidGameId;
#elif UNITY_EDITOR
        GameId = _AndroidGameId;
#endif

        AddLoad(); 
    }

    public  void AddLoad()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(GameId, IsTesting, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("---------ADS_Initialition_complete--------------");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("---------ADS_Falied----------------");
    }

}
