using System;
using UnityEngine;

public class Bubble : ProgressBar
{
    public static Action Complete;
    protected override void Start(){
        base.Start();
        progress = 1;
        timer = new Timer_CountDown();
    }

    protected override void OnCounting(){
        timer.Count (ref progress, duration);
        ChangeAlpha(progress);
        ChangeFill(progress);
    }

    protected override void OnStatic(){
        //idle
    }

    protected override void OnCompletion(){
        Debug.Log("Completed");
        Complete.Invoke();
    }

}
