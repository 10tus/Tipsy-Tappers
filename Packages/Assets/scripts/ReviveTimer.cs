using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ReviveTimer : ReduceTimer
{
    private GameManagerScript gameManager;
    private float timeLeft;

    private void Start() {
        timeLeft = GetComponent<Image>().fillAmount;
        gameManager = GameManagerScript.instance;
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
