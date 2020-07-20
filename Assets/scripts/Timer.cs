using UnityEngine;
using UnityEngine.UI;
public class Timer : ReduceTimer
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    float rateAdd = 0.1f;

    [SerializeField]
<<<<<<< HEAD
    int DivisibleBy;

    float rateOfDecrease = 0.2f, addTime= .1f;

    GameManagerScript gameManager;

    void Awake() {
        ServiceLocator.Register<Timer>(this);
    }

    void Start(){
        gameManager = ServiceLocator.Resolve<GameManagerScript>();
=======
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
>>>>>>> develop
    }

    public void StartTimer()
    {
<<<<<<< HEAD
        slider.value -= rateOfDecrease*Time.deltaTime;
=======
        timeLeft -= reduceRate * Time.deltaTime;
        slider.value = timeLeft;
>>>>>>> develop
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
<<<<<<< HEAD
                rateOfDecrease += rateAdd;
=======
                
                reduceRate += rateAdd;
>>>>>>> develop
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
