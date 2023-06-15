using UnityEngine;
using TMPro;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] TMP_Text money;
    [SerializeField] TMP_Text clientsLost;
    [SerializeField] TMP_Text finalTime;
    [SerializeField] TMP_Text finalScore;
    [SerializeField] TMP_Text rank;
    private float score;


    private void OnEnable()
    {
        score = (GameController.Instance.money) + (-GameController.Instance.lostClients * 5) + (Time.time * 0.5f);

        money.text = $"{GameController.Instance.money}";
        clientsLost.text = $"Clientes insatisfeitos: {GameController.Instance.lostClients}";
        finalTime.text = $"Tempo total: {Time.time}";
        finalScore.text = $"Pontuação final: {score}";
        rank.text = $"{FinalRank()}";
    }

    private char FinalRank()
    {   
        if(score <= 0)
        {
            return 'F';
        }
        else if(score <= 20)
        {
            return 'E';
        }
        else if(score <= 40)
        {
            return 'D';
        }
        else if(score <= 60)
        {
            return 'C';
        }
        else if(score <= 80)
        {
            return 'B';
        }
        else if(score <= 99)
        {
            return 'A';
        }
        else
        {
            return 'S';
        }
    }
}
