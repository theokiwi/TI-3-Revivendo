using UnityEngine;

public class Timer_CountDown : ITimer
{
    public bool Count(float value){
        return value <= 0 ? true : false;
    }
}
