using UnityEngine;

public class Timer_CountDown : ITimer
{
    public bool Count(float value, float duration){
        return value <= 0 ? true : (value -= Time.fixedDeltaTime/duration) <= 0;
    }
}
