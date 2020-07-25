using UnityEngine;
using UnityEngine.Advertisements;

public abstract class Ads:MonoBehaviour,IUnityAdsListener 
{
    protected string gameId="3719115"; //id for google playstore
    protected string rewardAd = "rewardedVideo";
    protected bool testMode = true;

    public abstract void Reward();
    public abstract void InitialSetup();

    void Start () {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    } 
    public void ShowRewardedVideo() {
        InitialSetup();
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(rewardAd)) {
            Advertisement.Show(rewardAd);
        } 
        else {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            Reward();
            // Reward the user for watching the ad to completion.
        } else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }
    
    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == rewardAd) {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() {
        Advertisement.RemoveListener(this);
    }

}

public class AdsController : Ads
{
    
    

     private void Awake() 
     {
        CheckGamesCount();
        ServiceLocator.Register<AdsController>(this);
    }

    public void AddToGamesCtr()
    {
        PlayerPrefs.SetInt("gCtr", PlayerPrefs.GetInt("gCtr") + 1); //adds 1 to gCtr
    }

    private void CheckGamesCount()
    {
        if(!PlayerPrefs.HasKey("gCtr"))
        {
            PlayerPrefs.SetInt("gCtr", 0);
        }
    }
    public void ShowAdsEveryFiveGames()
    {
        
        AddToGamesCtr();
        if(PlayerPrefs.GetInt("gCtr") == 5)
        {
            PlayerPrefs.SetInt("gCtr", 0);
            ShowRewardedVideo();
        }
        

    }
    
    public override void Reward()
    {
        Debug.Log("No reward");

    }
    public void BuyGameAccepted()
    {
        //PlayerPrefs.DeleteKey("Premium");// uncomment dis if u want to test unskippable ads every 5 games
        PlayerPrefs.SetInt("Premium",1); //comment dis shit to set game to premium
        Debug.Log("Player bought d game? :: " + PlayerPrefs.HasKey("Premium"));
    }

    public void CheckPremiumUser()
    {
        if(!PlayerPrefs.HasKey("Premium"))
        {
            Debug.Log(PlayerPrefs.GetInt("gCtr") + ":: Games Ctr");
            ShowAdsEveryFiveGames();
        }

    }
    public override void InitialSetup()
    {

    }
    
    
}
