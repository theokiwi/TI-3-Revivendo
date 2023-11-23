using UnityEngine;

public class Timer_CountUp : ITimer
{  
    public bool Count(float value, float duration){
        return value >= 0.99f ? true : (value += Time.fixedDeltaTime/duration) >= 0.99f;
    }
}
