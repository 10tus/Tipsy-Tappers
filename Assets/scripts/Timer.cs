using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    float rateAdd = 0.1f;

    [SerializeField]
    int DivisibleBy;

    float rateOfDecrease = 0.2f, addTime= .1f;

    GameManagerScript gameManager;

    void Awake() {
        ServiceLocator.Register<Timer>(this);
    }

    void Start(){
        gameManager = ServiceLocator.Resolve<GameManagerScript>();
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
