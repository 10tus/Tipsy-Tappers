using UnityEngine;
using UnityEngine.UI;
public class Timer : ReduceTimer
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private float rateAdd = 0.1f;

    [SerializeField]
    private int DivisibleBy;
    private PlayerSystem playerSystem;

    private GameManagerScript gameManager;
    private float addTime = .1f;

    // Update is called once per frame

    private void Start() {
        gameManager = GameManagerScript.instance;
        playerSystem = PlayerSystem.instance;
    }
    public void StartTimer()
    {

        slider.value -= reduceRate*Time.deltaTime;
        TimerEnd();
    }

    public void AddTime()
    {
        slider.value += addTime;      
    }

    public void rateOfDecreaseChange(int score)
    {
        if(reduceRate < 0.4f)
        {
            if(score != 0 && score % DivisibleBy == 0)
            {
                
                reduceRate += rateAdd;
            }
            
        }

    }

    public override void TimerEnd()
    {
        if(slider.value <= 0 && !playerSystem._over.revivedOnce)
        {
            gameManager.ShowRevive();
        }
        else if(slider.value<=0 && playerSystem._over.revivedOnce)
            gameManager.GameOver();
    }
}
