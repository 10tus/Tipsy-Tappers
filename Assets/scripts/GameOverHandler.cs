using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject overPanel,score,timer,glassArray,DrunkMeter,instruction;

    void Awake(){
        ServiceLocator.Register<GameOverHandler>(this);
    }

    public void GameOverPanel()
    {
        
        overPanel.SetActive(true);
        score.SetActive(false);
        timer.SetActive(false);
        DrunkMeter.SetActive(false);
        
        
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
