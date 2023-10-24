using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    
    //elementos de interface que pertencem a essa tela
    [SerializeField] protected UIElement[] elements;

    /* Niveis de prioridade!!!
     * 
     * 0: So pode abrir se nao tiver nenhuma UIScreen habilitada!
     * 1: Fecha toda UIScreen nivel 0 e se abre. Nao abre se tiver telas de prioridade mais alta!
     * 2: Fecha toda UIScreen nivel 0 a 1 e se abre. Nao abre se tiver telas de prioridade mais alta!
     * ... etc
     * 2
     * -1: Abre por cima de UIScreens prioridade 0 a -1. Nao abre se tiverem telas de prioridade >2!
     * -2: Abre por cima de UIScreens prioridade 0 a -2. Nao abre se tiverem telas de prioridade >3!
     * ... etc
     */
    [SerializeField] protected int priorityLevel;
    public int PriorityLevel { get{ return priorityLevel; } }
    //guardam o nível de prioridade mais positivo e o mais negativo
    public static int highestPriority;
    public static int lowestPriority;

    //roda sempre que uma tela e ativada e passa a prioridade dela como parametro.
    private static Action<int> OtherOpened = (int level) =>{};
    //roda sempre que uma tela e desativada e reseta o valor de prioridade. Cada tela aberta quando ouve isso ajeita os valores de prioridade de acordo com a sua.
    private static Action<int> OtherClosed = (int level) =>{ highestPriority = 0; lowestPriority = 0;};
    public bool _isEnabled;
    
    private void Start() {
        Hide();
    }

    //habilita os elementos pertencentes e insere as atualizacoes constantes necessarias.
    //tambem fala pras telas de prioridade baixa se fecharem.
    public void Popup()
    {
        if(highestPriority > Math.Abs(priorityLevel))
            return;
        else if (priorityLevel > highestPriority)
        {
            highestPriority = priorityLevel;
            OtherOpened(priorityLevel);
        }

        OnPopup();
        foreach (UIElement element in elements) 
        {
            if (element._constantUpdate)
            {
                UIManager.Instance.UIUpdate += element.UpdateUI;
            }
            element.gameObject.SetActive(true);
        }
        OtherOpened += OnOtherPopup;
        OtherClosed += OnOtherHide;
        _isEnabled = true;
    }
    //metodo abstrato pra inserir funcionalidade extra pro popup da tela
    protected virtual void OnPopup() { }

    //desabilita os elementos pertencentes e remove as atualizacoes constantes.
    //também fala pras outras telas rodarem seu onOtherHide.
    public void Hide()
    {
        OnHide();
        foreach (UIElement element in elements)
        {
            if (element._constantUpdate)
            {
                UIManager.Instance.UIUpdate -= element.UpdateUI;
            }
            element.gameObject.SetActive(false);
        }
        OtherOpened -= OnOtherPopup;
        OtherClosed -= OnOtherHide;
        OtherClosed(priorityLevel);
        _isEnabled = false;
    }
    //metodo abstrato pra inserir funcionalidade extra pro desabilitar da tela
    protected virtual void OnHide() { }

    //metodo pra ser adicionado a OtherOpened pra se fechar se uma tela grande abrir
    protected virtual void OnOtherPopup(int level)
    {
        if(priorityLevel >= 0 && priorityLevel < level)
            Hide();
    }
    //metodo pra ser adicionado a OtherClosed pra redefinir as prioridades e outras eventualidades
    protected virtual void OnOtherHide(int level)
    {
        if(highestPriority < priorityLevel)
            highestPriority = priorityLevel;
        if(lowestPriority > priorityLevel)
            lowestPriority = priorityLevel;
    }
}
