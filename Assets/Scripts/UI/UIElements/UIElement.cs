using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Elemento de interface dinâmico.
public abstract class UIElement : MonoBehaviour
{
    //se estiver habilitado, e atualizado todo frame pelo UIManager
    public bool _constantUpdate;

    //e chamado pelo UIManager quando habilitado ou em outros casos determinandos pelo elemento especifico.
    public abstract void UpdateUI();
}
