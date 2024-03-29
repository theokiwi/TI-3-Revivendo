using System;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : ProgressBar
{
    public Action Complete;
    public bool sleepy;
    public GameObject bubble;
    [SerializeField] private Image displayedImage;

    public _States State {
        get => state;
    }


    protected override void Awake(){
        base.Awake();
        if(duration == 0) duration = 5;
        state =_States.STATIC;
        progress = 1;
        timer = new Timer_CountDown();
    }

    public void Wake(){
        if(!bubble.activeInHierarchy) bubble.SetActive(true);
        if(sleepy) sleepy = false;
        state = _States.COUNTING;
    }

    public void Sleep(bool toggle){
        sleepy = toggle;
        state = _States.STATIC;
    }

    public void Refresh(float duration, Sprite sprite){
        displayedImage.sprite = sprite;
        state = _States.STATIC;
        progress = 1;
        this.duration = duration;
    }

    public void Hide(bool value){
        Sleep(value);
        bubble.SetActive(!value);
    }

    protected override void OnCounting(){
        if(timer.Count (ref progress, duration)){
            state = _States.COMPLETED;
        }
        ChangeAlpha(progress);
        ChangeFill(progress);
    }

    protected override void OnStatic(){
        //idle
    }

    protected override void OnCompletion(){
        Complete.Invoke();
    }

}
