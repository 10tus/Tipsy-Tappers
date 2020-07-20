using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject overPanel,score,timer,glassArray,DrunkMeter,instruction,revivePanel;
    public bool revivedOnce;
    private AdsController ads;
    PlayerActions player;

<<<<<<< HEAD
    void Awake(){
        ServiceLocator.Register<GameOverHandler>(this);
    }

    public void GameOverPanel()
    {
=======
    private void Awake() {
        ServiceLocator.Register<GameOverHandler>(this);
        
    }
    private void Start() {
        ads = ServiceLocator.Resolve<AdsController>();
        player = ServiceLocator.Resolve<PlayerActions>();
    }
    public void GameOverPanel()
    {
        TogglePlayerTap(false);
        revivePanel.SetActive(false);
>>>>>>> develop
        overPanel.SetActive(true);
        score.SetActive(false);
        timer.SetActive(false);


    }

    void TogglePlayerTap(bool val)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTap>().enabled = val;
    }

    public void ShowRevivePanel()
    {
        TogglePlayerTap(false);
        revivePanel.SetActive(true);
        DrunkMeter.SetActive(false);
<<<<<<< HEAD
=======
        timer.SetActive(false);
        player.flagTap = false;

    }


    //To be Reworked
    public void ReviveAccepted()
    {
        timer.SetActive(true);
        timer.GetComponent<Timer>().ResetTimer();
        revivedOnce = true;
        revivePanel.SetActive(false);
        DrunkMeter.SetActive(true);
        TogglePlayerTap(true);
        //Play Ad or pay 30 tipsy coins
>>>>>>> develop
    }

    public void HideGameOverPanel()
    {
        overPanel.SetActive(false);
        score.SetActive(false);
        StartCoroutine(tapStart());
        
    }

    private IEnumerator tapStart()
    {
        ads.CheckPremiumUser();
        if(!PlayerPrefs.HasKey("Premium"))
        {
            Debug.Log(PlayerPrefs.GetInt("gCtr") + ":: Games Ctr");
            ads.ShowAdsEveryFiveGames();
        }      
        glassArray.SetActive(false); 
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
<<<<<<< HEAD
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerTap>().enabled = true;
=======
        TogglePlayerTap(true);

>>>>>>> develop
    }

    
}
