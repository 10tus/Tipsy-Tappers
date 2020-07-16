using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private float rateAdd = 0.1f;

    [SerializeField]
    private int DivisibleBy;

    private GameManagerScript gameManager;
    private float rateOfDecrease = 0.2f, addTime = .1f;

    #region SingletonInstance
    public static Timer instance;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }    
    }

    #endregion
    // Update is called once per frame

    private void Start() {
        gameManager = GameManagerScript.instance;
    }
    public void StartTimer()
    {
        
        slider.value -= rateOfDecrease*Time.deltaTime;
        TimerEnd();
    }

    public void AddTime()
    {
        slider.value += addTime;      
    }

    public void rateOfDecreaseChange(int score)
    {
        if(rateOfDecrease < 0.4f)
        {
            if(score != 0 && score % DivisibleBy == 0)
            {
                
                rateOfDecrease += rateAdd;
            }
            
        }

    }

    private void TimerEnd()
    {
        if(slider.value <= 0)
        {
            gameManager.GameOver();
        }
    }
}
