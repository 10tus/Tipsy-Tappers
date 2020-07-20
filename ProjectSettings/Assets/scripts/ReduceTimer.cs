using UnityEngine;

public abstract class ReduceTimer : MonoBehaviour
{
    public float reduceRate;

    public abstract void TimerEnd();
}
