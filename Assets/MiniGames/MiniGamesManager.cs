using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamesManager : MonoBehaviour
{
    [SerializeField] GameObject counterminigame,tableminigame,halloweenminigame,bonfireminigame,valentineminigame;
    public static MiniGamesManager instance;
    private Camera camera;

    private void Awake()
    {
        instance = this;
        camera = Camera.main;
    }
    public void StartMiniGame(string name,Table table = null)
    {
        camera.GetComponent<CameraMovement>().moving = false;
        GameController.Instance._IsPaused = true;
        if(name == "Table")
        {
            GameObject g = Instantiate(tableminigame);
            CrumbMiniGame c = g.GetComponentInChildren<CrumbMiniGame>();
            c.table = table;
        }
        else if (name == "Counter")
        {
            Instantiate(counterminigame);
        }
        else if(name == "Bonfire")
        {
            TimeController.Instance.timeMultiplier = 0;
            Instantiate(bonfireminigame);
        }
        else if (name == "Halloween")
        {
            TimeController.Instance.timeMultiplier = 0;
            Instantiate(halloweenminigame);
        }
        else if (name == "Valentine")
        {
            TimeController.Instance.timeMultiplier = 0;
            Instantiate(valentineminigame);
        }
    }
    public void ExitMiniGame()
    {
        camera.GetComponent<CameraMovement>().moving = true;
        GameController.Instance._IsPaused = false;
    }
}
