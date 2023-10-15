using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    //lista de telas ativas
    private List<UIScreen> activeScreens = new List<UIScreen>();

    //referencias de telas importantes
    [SerializeField] private UIScreen endGameScreen;
    [SerializeField] private UIScreen pauseMenu;
    [SerializeField] private UIScreen kitchenMenu;

    //so pra economizar processamento no enablePopup sla
    private int activePriorityLevel;

    //lista de telas importantes
    public enum ScreenEnum
    {
        EndGameScreen,
        PauseMenu,
        KitchenMenu
    }

    //update pra todo frame
    public Action UIUpdate = () => { };

    private void Update()
    {
        UIUpdate();
    }

    //habilita a tela de acordo com regras de prioridade
    public void EnablePopup(UIScreen screen)
    {
        //se o nivel de prioridade ativo for maior que o da tela, nao ativa,
        if (activePriorityLevel > screen.PriorityLevel)
            return;

        //nivel de prioridade vira o nivel de prioridade da tela nova se for maior que a atual
        activePriorityLevel = screen.PriorityLevel;

        //cada tela com nivel de prioridade menor que o atual � fechada.
        foreach(UIScreen activeScreen in activeScreens)
        {
            if(activePriorityLevel > Math.Abs(activeScreen.PriorityLevel))
            {
                DisablePopup(activeScreen);
            }
        }
        screen.Popup();
    }

    //recebe um valor do enum e manda a referencia pro enable normal.
    public void EnablePopup(ScreenEnum screen)
    {
        switch (screen)
        {
            case ScreenEnum.EndGameScreen:
                EnablePopup(endGameScreen);
                break;
                
            case ScreenEnum.PauseMenu:
                EnablePopup(pauseMenu);
                break; 

            case ScreenEnum.KitchenMenu:
                EnablePopup(kitchenMenu);
                break;
            
            default: break;
        }
    }

    //desabilita a tela
    public void DisablePopup(UIScreen screen)
    {
        screen.Hide();
    }

    //recebe um valor do enum e manda a referencia pro disable normal.
    public void DisablePopup(ScreenEnum screen)
    {
        switch (screen)
        {
            case ScreenEnum.EndGameScreen:
                DisablePopup(endGameScreen);
                break;

            case ScreenEnum.PauseMenu:
                DisablePopup(pauseMenu);
                break;

            case ScreenEnum.KitchenMenu:
                DisablePopup(kitchenMenu);
                break;

            default: break;
        }
    }
}
