using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todos os "objetos sujos" herdarao dessa classe
public abstract class UnsanitaryObject : MonoBehaviour
{
    //intensidade do impacto desse objeto no saneamento
    public float dirtiness;

    public void Start()
    {
        SanitationController.Instance.AddUnsanitaryObject(this);
    }

    //o que acontece quando o objeto é limpo (por padrão só se remove da lista e se destrói)
    public virtual void Clean()
    {
        SanitationController.Instance.RemoveUnsanitaryObject(this);
        Destroy(gameObject);
    }
}
