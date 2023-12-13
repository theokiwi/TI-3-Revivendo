using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireMiniGame : MonoBehaviour
{
    [SerializeField] GameObject bonfire;
    [SerializeField] GameObject minigame;
    public static BonfireMiniGame Instance;
    [SerializeField] GameObject border;
    public int bonfiresJumped = 0;
    float counter =  0;

    private void Awake()
    {
        Instance = this;
    }
    public void BonfireJumped()
    {
        bonfiresJumped++;
        GameController.Instance.AddPoints(100);
        if(bonfiresJumped > 4)EndMinigame(1.5f);
    }
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Invoke("SpawnBonfire",1.8f * i);
        }
    }
    public void SpawnBonfire()
    {
        Vector2 spawnPos = new Vector2(363, -300.25f);
        GameObject g = Instantiate(bonfire);
        g.transform.SetParent(border.transform);
        g.transform.localPosition = spawnPos;
        g.GetComponent<LinearMove>().speed += IncreaseCounter();
    }
    public float IncreaseCounter()
    {
        return counter += 0.2f;
    }
    public void EndMinigame(float t)
    {
        MiniGamesManager.instance.ExitMiniGame();
        Destroy(minigame,t);
    }
}
