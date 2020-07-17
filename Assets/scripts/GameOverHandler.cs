using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject overPanel,score,timer,glassArray,DrunkMeter,instruction,revivePanel;

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

    public void ReviveAccepted()
    {
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
        glassArray.SetActive(false); 
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerTap>().enabled = true;
        
    }
}
