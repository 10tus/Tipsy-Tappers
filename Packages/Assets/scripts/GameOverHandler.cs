using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject overPanel,score,timer,glassArray,DrunkMeter,instruction,revivePanel;
    public bool revivedOnce;
    private AdsController ads;

    private void Awake() {
        ads = AdsController.instance;
    }
    public void GameOverPanel()
    {
        revivePanel.SetActive(false);
        overPanel.SetActive(true);
        score.SetActive(false);
        
        
    }

    public void ShowRevivePanel()
    {
        revivePanel.SetActive(true);
        DrunkMeter.SetActive(false);
        timer.SetActive(false);

    }


    //To be Reworked
    public void ReviveAccepted()
    {
        
        revivedOnce = true;
        revivePanel.SetActive(false);
        DrunkMeter.SetActive(true);
        timer.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerTap>().enabled = true;
        //Play Ad or pay 30 tipsy coins
    }
    public void HideGameOverPanel()
    {
        overPanel.SetActive(false);
        score.SetActive(false);
        StartCoroutine(tapStart());
        
    }
    private IEnumerator tapStart()
    {
        if(!PlayerPrefs.HasKey("Premium"))
        {
            Debug.Log(PlayerPrefs.GetInt("gCtr") + ":: Games Ctr");
            ads.ShowAdsEveryFiveGames();
        }      
        glassArray.SetActive(false); 
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerTap>().enabled = true;
        
    }
}
