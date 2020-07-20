using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ReviveTimer : ReduceTimer
{
    private GameManagerScript gameManager;

    private void Start() {
        timeLeft = 1f;
        gameManager = ServiceLocator.Resolve<GameManagerScript>();
    }
    void Update()
    {
        timeLeft -= reduceRate * Time.deltaTime;
        GetComponent<Image>().fillAmount = timeLeft;
        TimerEnd();
    }
    public override void TimerEnd()
    {
        if(timeLeft<= 0)
        {
            gameManager.GameOver();
        }
    }
}
