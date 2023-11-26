using UnityEngine.UI;
using UnityEngine;

public abstract class ProgressBar : MonoBehaviour
{
    #region properties

    [SerializeField] protected float progress;
    [SerializeField] protected float duration;
    [SerializeField] protected Image fillImage;
    [SerializeField] protected Material mat;
    [SerializeField] protected _States state;
    protected ITimer timer;
    public enum _States{STATIC, COUNTING, COMPLETED}


    #endregion

    #region methods

    protected virtual void Start(){
        fillImage.fillAmount = 1;
        mat = CloneMaterial(fillImage);
    }

    protected virtual void FixedUpdate(){
        switch(state){
            case _States.COUNTING:
                OnCounting();
            break;
            case _States.STATIC:
                OnStatic();
            break;
            case _States.COMPLETED:
                OnCompletion();
                state = _States.STATIC;
            break;
        }
    }

    protected virtual void ChangeAlpha(float value){
        mat.SetFloat("_Alpha", value);
    }

    protected virtual void ChangeFill(float value){
        fillImage.fillAmount = value;
    }

    protected virtual Material CloneMaterial(Image img){
        img.material = new Material(img.material);
        return img.material;
    }

    protected abstract void OnCounting();
    protected abstract void OnStatic();
    protected abstract void OnCompletion();

    #endregion

}
