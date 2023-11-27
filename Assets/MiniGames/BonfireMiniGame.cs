using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireMiniGame : MonoBehaviour
{
    [SerializeField] GameObject bonfire;
    [SerializeField] GameObject minigame;
    public static BonfireMiniGame Instance;
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
    }
    private void Start()
    {
        InvokeRepeating("SpawnBonfire",0,1.8f);
    }
    public void SpawnBonfire()
    {
        Vector2 spawnPos = new Vector2(363,-360);
        GameObject g = Instantiate(bonfire);
        g.transform.SetParent(minigame.transform);
        g.transform.localPosition = spawnPos;
        g.GetComponent<LinearMove>().speed += IncreaseCounter();
    }
    public float IncreaseCounter()
    {
        return counter += 0.2f;
    }
    public void EndMinigame()
    {
        Destroy(minigame);
    }
}
