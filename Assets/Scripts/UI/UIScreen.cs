using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    //elementos de interface que pertencem a 
    [SerializeField] protected UIElement[] elements;

    /* Níveis de prioridade!!!
     * 
     * 0: Só pode abrir se não tiver nenhuma UIScreen habilitada!
     * 1: Fecha toda UIScreen nível 0 e se abre. Não abre se tiver telas de prioridade mais alta!
     * 2: Fecha toda UIScreen nível 0 a 1 e se abre. Não abre se tiver telas de prioridade mais alta!
     * ... etc
     * 2
     * -1: Abre por cima de UIScreens prioridade 0 a -1. Não abre se tiverem telas de prioridade >2!
     * -2: Abre por cima de UIScreens prioridade 0 a -2. Não abre se tiverem telas de prioridade >3!
     * ... etc
     */
    protected int priorityLevel;
    public int PriorityLevel { get{ return priorityLevel; } }
    
    //habilita os elementos pertencentes e insere as atualizacoes constantes necessarias.
    public void Popup()
    {
        OnPopup();
        foreach (UIElement element in elements) 
        {
            if (element._constantUpdate)
            {
                UIManager.Instance.UIUpdate += element.UpdateUI;
            }
            element.gameObject.SetActive(true);
        }
    }
    //metodo abstrato pra inserir funcionalidade extra pro popup da tela
    protected virtual void OnPopup() { }

    //desabilita os elementos pertencentes e remove as atualizacoes constantes.
    public void Hide()
    {
        OnHide();
    }
    //metodo abstrato pra inserir funcionalidade extra pro desabilitar da tela
    protected virtual void OnHide() { }
}
