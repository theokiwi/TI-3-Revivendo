using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValentineMinigame : MonoBehaviour
{
    public static ValentineMinigame Instance;
    [SerializeField] Text timer;
    [SerializeField] GameObject minigame;
    float time = 20;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        timer.text = "" + (int)time;
        if(time <= 0)
        {
            Destroy(minigame);
        }
    }
    public void AddPoints()
    {
        GameController.Instance.AddPoints(20);
    }
}
