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
        else if(name == "Bonfire")
        {
            Instantiate(bonfireminigame);
        }
        else if (name == "Halloween")
        {
            Instantiate(halloweenminigame);
        }
        else if (name == "Valentine")
        {
            Instantiate(valentineminigame);
        }
    }
    public void ExitMiniGame()
    {
        camera.GetComponent<CameraMovement>().moving = true;
        GameController.Instance._IsPaused = false;
    }
}
