using UnityEngine;

public class ScoresHandler : MonoBehaviour
{
    public GameObject hi, score,panelScore;
    public int currScore {get;private set;}

    private void Awake() {
        ServiceLocator.Register<ScoresHandler>(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currScore = 0;
        score.GetComponent<TMPro.TextMeshProUGUI>().text = currScore.ToString();
        panelScore.GetComponent<TMPro.TextMeshProUGUI>().text = currScore.ToString();
        CheckHiScore();

    }
    private void Update() {
        CheckHiScore();
    }

    public void UpdateScore()
    {
        
        currScore++;
        score.GetComponent<TMPro.TextMeshProUGUI>().text = currScore.ToString();
        panelScore.GetComponent<TMPro.TextMeshProUGUI>().text = currScore.ToString();
        
    }
    private void CheckHiScore()
    {
        int hiVal = PlayerPrefs.GetInt("hi");
        if (hiVal > currScore)
        {         
            PlayerPrefs.SetInt("hi", hiVal);
             
        }
        else
        {
            PlayerPrefs.SetInt("hi", currScore);
            
        }
        hi.GetComponent<TMPro.TextMeshProUGUI>().text = hiVal.ToString();
        
        
        
    }
    public void RestartScore()
    {
        currScore = 0;
    }
    
}
