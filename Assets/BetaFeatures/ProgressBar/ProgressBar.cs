using UnityEngine.UI;
using UnityEngine;
using System;

public class ProgressBar : MonoBehaviour
{
    #region properties

    //can only be a float value between 0.0 and 1.0, hardcoded
    [SerializeField] private float progress{
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
    [SerializeField] private float duration;
    [SerializeField] private Image fillImage;
    [SerializeField] Material mat;
    [SerializeField] ITimer timer;
    [SerializeField] _States state;

    public enum _States{STATIC, COUNTING, COMPLETED}

    #endregion

    #region methods

    private void Start(){
        fillImage = GetComponent<Image>();
        mat = CloneMaterial(fillImage);
        fillImage.fillAmount = 1;
    }

    private void FixedUpdate(){
        switch(state){
            case _States.COUNTING:
                
            break;
            case _States.STATIC:
            
            break;
            case _States.COMPLETED:
                
            break;
        }
    }

    private void ChangeAlpha(Material mat, float value){
        mat.SetFloat("_Alpha", value);
    }

    private Material CloneMaterial(Image img){
        img.material = new Material(img.material);
        return img.material;
    }

    #endregion

}
