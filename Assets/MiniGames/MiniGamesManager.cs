using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamesManager : MonoBehaviour
{
    [SerializeField] GameObject counterminigame,tableminigame;
    public static MiniGamesManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void StartMiniGame(string name,Table table = null)
    {
        GameController.Instance._IsPaused = true;
        if(name == "Table")
        {
            GameObject g = Instantiate(tableminigame);
            CrumbMiniGame c = g.GetComponentInChildren<CrumbMiniGame>();
            c.table = table;
        }
        else if(name == "Counter")
        {
            Instantiate(counterminigame);
        }
    }
    public void ExitMiniGame()
    {
        GameController.Instance._IsPaused = false;
    }
}
