using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;



// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public  class GoogleMobileAdsScript : MonoBehaviour
{
    public static GoogleMobileAdsScript Instance;
  //  public static RewardBasedVideoAd rewardBasedVideo = null;
    private static string outputMessage = "";

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    IEnumerator DELAY()
    {
        yield return new WaitForSeconds(1);
        
    }


    void Awake()
    {
        Instance = this;
        RequestBanner();
        RequestInterstitial();
      //  rewardBasedVideo = RewardBasedVideoAd.Instance;
        RequestRewardBasedVideo(0);
      //  RequestRewardBasedVideo(1);
        StartCoroutine(DELAY());
    }
    void Start()
    {
     
    }


   /// <summary>
   /// show the 
   /// </summary>
   public  void showbanermain()
    {
        RequestBanner();
        

    }

    #region c�c h�m ch�nh
    public void showbaner()
    {
        
    }
    public void hidebaner()
    {
        
    }
    public void showfullbaner()
    {
        ShowInterstitial();
        RequestInterstitial();
    }
    
    public void showvideo()
    {
        ShowRewardBasedVideo();
        RequestRewardBasedVideo(0);
    }
    

    int changevideo = 0;
    public void Getadsvideo()
    {
  
         
   
      
    }
    public  string baner_Adr = "ca-app-pub-6102026202830298/6732848768";
    public string fullbaner_Adr = "ca-app-pub-6102026202830298/8209581965";
    public string video_Adr = "ca-app-pub-6102026202830298/8069981161";
    //public string video_Adr = "ca-app-pub-6190532367875222/4694685199";
    public string baner_IOS = "ca-app-pub-6102026202830298/2023447564";
    public string fullbaner_IOS = "ca-app-pub-6102026202830298/3500180763";
    public string video_IOS = "ca-app-pub-6102026202830298/4976913965";
    #endregion

    private void RequestBanner()
    {
#if UNITY_EDITOR
            string adUnitId = "baner_Adr";
#elif UNITY_ANDROID
            string adUnitId = baner_Adr;
#elif UNITY_5 || UNITY_IOS || UNITY_IPHONE
            string adUnitId = baner_IOS;
#else
            string adUnitId = baner_Adr;
#endif

        // Create a 320x50 banner at the top of the screen.
       
    }

    private void RequestInterstitial()
    {
        #if UNITY_EDITOR
            string adUnitId = "fullbaner_Adr";
#elif UNITY_ANDROID
            string adUnitId = fullbaner_Adr;
#elif UNITY_5 || UNITY_IOS || UNITY_IPHONE
            string adUnitId = fullbaner_IOS;
#else
            string adUnitId = fullbaner_Adr;
#endif

        // Create an interstitial.
       
    }

    // Returns an ad request with custom ad targeting.


    private void RequestRewardBasedVideo(int value)
    {
        #if UNITY_EDITOR
            string adUnitId = "video_Adr";
#elif UNITY_ANDROID
            string adUnitId = video_Adr;
#elif UNITY_5 || UNITY_IOS || UNITY_IPHONE
            string adUnitId = video_IOS;
#else
            string adUnitId = video_Adr;
#endif
       

    }

    private void ShowInterstitial()
    {
       
    }
    private void ShowRewardBasedVideo()
    {
       
    }
  

    public bool ADS_Video_GetIsloaded()
    {
       
        return true;
    }
}
