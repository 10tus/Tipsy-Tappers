using UnityEngine;

public abstract class ReduceTimer : MonoBehaviour
{
    public float reduceRate;
    protected float timeLeft;

    public abstract void TimerEnd();
}
