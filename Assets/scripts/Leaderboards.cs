using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class Leaderboards : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject image;

    private void Awake() {
        ServiceLocator.Register<Leaderboards>(this);
    }
    // Update is called once per frame
    public void showPic()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = ChangeToSprite(PlayerPrefs.GetString("fbmed"), 100, 100);
    }

    Sprite ChangeToSprite(string txt,int height,int width)
    {
        Texture2D res = new Texture2D(height,width);
        byte[] bytes = Convert.FromBase64String(txt);
        res.LoadImage(bytes);
        return Sprite.Create(res,new Rect(0,0,height,width), new Vector2());
    }

    public void SubmitScoreToPlayfab()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "best score",
                    Value = PlayerPrefs.GetInt("hi")
    }
            }
        }, resultCallback => OnStatisticsUpdated(resultCallback), FailureCallback);
    }

    private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult) 
    {
        Debug.Log("Successfully submitted high score");
    }

    private void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
