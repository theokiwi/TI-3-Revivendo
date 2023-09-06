using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todos os "objetos sujos" herdar�o dessa classe
public abstract class UnsanitaryObject : MonoBehaviour
{
    //intensidade do impacto desse objeto no saneamento
    public float dirtiness;

    public void Start()
    {
        SanitationController.Instance.AddUnsanitaryObject(this);
    }

    //o que acontece quando o objeto � limpo (por padr�o s� se remove da lista e se destr�i)
    public virtual void Clean()
    {
        SanitationController.Instance.RemoveUnsanitaryObject(this);
        Destroy(gameObject);
    }
}
