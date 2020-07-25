using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PlayFab;
using PlayFab.ClientModels;
using LoginResult = PlayFab.ClientModels.LoginResult;



public class FacebookControllerScript : MonoBehaviour
{
    [SerializeField] GameObject buttonFb;
    [SerializeField] GameObject fbOutPanel;
    [SerializeField] GameObject profImg;
    [SerializeField] GameObject profName;
    [SerializeField] Sprite fbicon;

    string playfabId = "1CBD2";

    //private PlayFabAuth _auth;

    //public GetPlayerCombinedInfoRequestParams infoRequestParams;

    void Awake() {
        
        PlayFabSettings.TitleId = playfabId;
        ServiceLocator.Register<FacebookControllerScript>(this);
        //_auth = PlayFabAuth.Instance;
        Debug.Log(FB.IsInitialized + ":: Fb initial start");
        if(!FB.IsInitialized)
        {
            FB.Init(SetInit,OnHideUnity);
        }
        else
            if(FB.IsLoggedIn)
            {
                LoadLoggedInData();
            }

    }


    void SetInit()
    {
        GetFbProfile(FB.IsLoggedIn);
        
    }
    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    //button call
    public void FbSignIn()
    {   
        Debug.Log(FB.IsLoggedIn + " : status");
        if(!FB.IsLoggedIn)
        {
            var permissions = new List<string>() {"public_profile","user_friends" };
            FB.LogInWithReadPermissions(permissions,AuthCallBack);
            //GetAccessToken();
        }
        else
        {
            fbOutPanel.SetActive(true);
            LoadLoggedInData();
        }
        

    }

    void LoadLoggedInData()
    {
        profName.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetString("fbname");
        buttonFb.GetComponent<Image>().sprite = ChangeToSprite(PlayerPrefs.GetString("fbsmall"),50,50);
        profImg.GetComponent<Image>().sprite = ChangeToSprite(PlayerPrefs.GetString("fbmed"),100,100);

    }
    Sprite ChangeToSprite(string txt,int height,int width)
    {
        Texture2D res = new Texture2D(height,width);
        byte[] bytes = Convert.FromBase64String(txt);
        res.LoadImage(bytes);
        return Sprite.Create(res,new Rect(0,0,height,width), new Vector2());
    }

    void PersistFbImage(string key,Texture2D texture)
    {
        byte[] texAsByte = texture.EncodeToPNG();
        string texAsString = Convert.ToBase64String(texAsByte);
        PlayerPrefs.SetString(key, texAsString);
        PlayerPrefs.Save();
    }
    void DeletePersistData()
    {
        PlayerPrefs.DeleteKey("fbsmall");
        PlayerPrefs.DeleteKey("fbmed");
        PlayerPrefs.DeleteKey("fbname");
    }
    //button call
    public void FbOut()
    {
        FB.LogOut();
        Debug.Log("FB out");
        ResetProfile(FB.IsLoggedIn);

    }

    public void ShareGame()
    {
        FB.ShareLink(
            contentTitle: "Beat my Highscore: "+PlayerPrefs.GetInt("hi").ToString(),
            contentURL: new System.Uri("https://www.youtube.com/watch?v=BGo6lNU9UMM"), //sample url
            contentDescription: "Yooow what r u waiting forrr?? come and play with me by downloading this game!",
            callback:OnShare
        );
    }

    public void FbGameRequest()
    {
        FB.AppRequest(
            message: "Can you beat my highscore: " + PlayerPrefs.GetInt("hi").ToString()+" ???",
            title:"Challenge your friends and be the best!"
        );

    }
    void OnShare(IShareResult result)
    {
        if(result.Cancelled || !string.IsNullOrEmpty(result.Error))
            Debug.Log("sharelink error:"+result.Error);
        else if(!string.IsNullOrEmpty(result.PostId))
            Debug.Log(result.PostId);
        else
            Debug.Log("Share succeed brodie");
    }

    public void GetGamingFriends()
    {
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
         {
             var dict = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
             var friendsList = (List<object>)dict["data"];

         });
    }

    void GetFbProfile(bool isLoggedIn)
    {

        string query = "/me/picture?type=square&height=50&width=50";
        string query2 = "/me/picture?type=square&height=100&width=100";
        string query3 = "/me?fields=first_name";
        string query4 = "/me?fields=id";
        if(isLoggedIn)
        {   
            FB.API(query,HttpMethod.GET,DisplayPicSmall);
            FB.API(query2,HttpMethod.GET,DisplayPicMedium);
            FB.API(query3,HttpMethod.GET,DisplayFbName);
            FB.API(query4,HttpMethod.GET,DisplayFbId);
        }
        

    }
    void DisplayFbId(IResult result)
    {
        if(result.Error == null)
        {
            string temp = result.ResultDictionary ["id"].ToString();
            Debug.Log(temp + " : id");
        }
        else
            Debug.Log(result.Error);



    }
    void DisplayFbName(IResult result)
    {
        string temp;
        if(result.Error == null)
        {
            temp = result.ResultDictionary ["first_name"].ToString();
            PlayerPrefs.SetString("fbname", temp);
            profName.GetComponent<TMPro.TextMeshProUGUI>().text = temp;

            
            
        }
        else
            Debug.Log(result.Error);



    }

    void ResetProfile(bool isLoggedOut)
    {
        buttonFb.GetComponent<Image>().sprite = fbicon;
        DeletePersistData();
        profName.GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
        profImg.GetComponent<Image>().sprite = null;
        fbOutPanel.SetActive(false);

    }

    void AuthCallBack(ILoginResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }

        else if(result.Cancelled)
        {
            Debug.Log("fb is canceledt");

        }
        else
        {
            if(FB.IsLoggedIn)
            {
                Debug.Log("FB in");
            }
            else
                Debug.Log("FB out");

            GetFbProfile(FB.IsLoggedIn);
            PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString},
                OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
        }

    }
    

    private void OnPlayfabFacebookAuthComplete(LoginResult result)
    {
        Debug.Log("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
    }

    private void OnPlayfabFacebookAuthFailed(PlayFabError error)
    {
         Debug.Log("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport());
    }

    void DisplayPicSmall(IGraphResult result)
    {
        if(result.Texture !=null)
        {
            buttonFb.GetComponent<Image>().sprite = CreateSprite("fbsmall",result.Texture,50,50);

        }

    }

    //handles sprite error idk why it happens xd
    Sprite CreateSprite(String key,Texture2D texture, int height, int width)
    {
        Sprite temp;
        try
        {
           temp = Sprite.Create(texture, new Rect(0, 0,width,height), new Vector2 ());
            
        }
        catch
        {
            temp = fbicon;
        }
        PersistFbImage(key,texture);
        return temp;

    }

    void DisplayPicMedium(IGraphResult result)
    {
        if(result.Texture !=null)
        {
            profImg.GetComponent<Image>().sprite = CreateSprite("fbmed",result.Texture,100,100);
        }
    }

}
    
