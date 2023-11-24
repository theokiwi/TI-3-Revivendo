using UnityEngine;

public class Timer_CountDown : ITimer
{
    public bool Count (ref float value, float duration){
        if(value <= 0){
            value = 0;
            return true;
        }
        value -= Time.fixedDeltaTime/duration;
        return false;
    }
}
