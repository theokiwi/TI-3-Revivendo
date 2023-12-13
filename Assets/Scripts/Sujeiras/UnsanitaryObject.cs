using UnityEngine;

//todos os "objetos sujos" herdarao dessa classe
public class UnsanitaryObject : MonoBehaviour
{
    //intensidade do impacto desse objeto no saneamento
    public float dirtiness;

    public void Start()
    {
        SanitationController.Instance.AddUnsanitaryObject(this);
        OnStart();
    }
    
    public virtual void OnStart(){}

    //o que acontece quando o objeto � limpo (por padr�o s� se remove da lista e se destr�i)
    public virtual void Clean()
    {
        SanitationController.Instance.RemoveUnsanitaryObject(this);
        Destroy(gameObject);
    }
}
