using System;

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
    }

    protected override void OnStatic(){
        //idle
    }

    protected override void OnCompletion(){
        Complete.Invoke();
    }

}
