using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Bubble : MonoBehaviour
{
    #region properties
    public enum STATES{
        SLEEPING,
        ACTIVE,
        SUCCEDED,
        FAILED
    }
    [SerializeField] float progress{
        get { return progress;}
        set {
            if (value > 1){
                throw new InvalidOperationException("Progress must be a float value between 0 and 1");
            }
            if (value < 0){
                throw new InvalidOperationException("Progress must be a float value between 0 and 1");
            }
            progress = value;
        }
    }
    [SerializeField] float duration;
    [SerializeField] private STATES state;
    [SerializeField] Image fillImage;
    [SerializeField] Image foodSprite;
    [SerializeField] Material backgroundMat;
    #endregion


    private void Awake(){
        CloneMaterial(fillImage);
    }

    private void Start(){
        fillImage.fillAmount = 1;
        progress =1;
        backgroundMat = fillImage.material;
    }

    private void FixedUpdate(){
    }

    public void SetSPrite(Sprite sprite){
        foodSprite.sprite = sprite;
    }

    private bool CountDown(float value, float duration){
        value -= Time.fixedDeltaTime / duration;
        if(value <= 0){
            return true;
        }
        return false;
    }

    private void Reffil(){
        progress = 1;
        UpdateBubbleFill();
    }

    private void UpdateBubbleFill(){
        ChangeFillAmount(fillImage, progress);
        ChangeColor(backgroundMat, progress);
    }

    private void ChangeFillAmount(Image img, float value){
        img.fillAmount = value;
    }

    private void ChangeColor(Material mat, float value){
        mat.SetFloat("_Alpha", value);
    }

    private void CloneMaterial(Image img){
        img.material = new Material(img.material);
    }
    
}
