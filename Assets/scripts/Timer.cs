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
    GameOverHandler _over;

    private GameManagerScript gameManager;
    private float addTime = .1f;
    

    // Update is called once per frame
    private void Awake() {
        ServiceLocator.Register<Timer>(this);
    }

    private void Start() {
        timeLeft = .5f;
        gameManager = ServiceLocator.Resolve<GameManagerScript>();
        _over = ServiceLocator.Resolve<GameOverHandler>();
        //playerSystem = PlayerSystem.instance;
    }
    public void StartTimer()
    {
        timeLeft -= reduceRate * Time.deltaTime;
        slider.value = timeLeft;
        TimerEnd();
    }

    public void ResetTimer()
    {
        this.timeLeft = 0.5f;
        slider.value = timeLeft;

    }

    public void AddTime()
    {
        this.timeLeft += addTime;      
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
        if(slider.value <= 0 && !_over.revivedOnce)
        {
            gameManager.ShowRevive();
        }
        else if(slider.value<=0 && _over.revivedOnce)
            gameManager.GameOver();
    }
}
