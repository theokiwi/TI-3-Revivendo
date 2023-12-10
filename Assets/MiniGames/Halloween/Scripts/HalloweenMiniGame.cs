using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HalloweenMiniGame : MonoBehaviour
{
    public static HalloweenMiniGame instance;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject[] candys;
    [SerializeField] GameObject minigame;
    [SerializeField] Text timerText;
    private float timer;

    private void Awake()
    {
        instance = this;
        timer = 30;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = $"{(int)timer}";
        if(timer < 0)
        {
            EndMiniGame();
        }
    }
    private void Start()
    {
        InvokeRepeating("SpawnCandy", 0, 0.3f);
    }
    public void CatchCandy()
    {
        GameController.Instance.AddPoints(10);
    }
    public void EndMiniGame()
    {
        Destroy(minigame);
    }
    public void SpawnCandy()
    {
        Vector2 v = new Vector2(Random.Range(-300f, 420f), 40);
        GameObject c = Instantiate(candys[Random.Range(0, candys.Length)]);
        c.transform.SetParent(spawner.transform);
        c.transform.localPosition = v;
    }
}
