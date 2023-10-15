using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseButton : ButtonFunction
{
    private void Awake()
    {
        ButtonAction = Unpause;
    }

    private void Unpause()
    {
        GameController.Instance.PauseGame(false);

    }
}
