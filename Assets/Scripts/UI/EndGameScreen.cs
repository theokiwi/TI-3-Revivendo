using UnityEngine;
using TMPro;

public class EndGameScreen : UIScreen
{

    protected override void OnHide()
    {
        //faz nada eu imagino
    }

    protected override void OnPopup()
    {
        foreach(UIElement element in elements)
        {
            element.UpdateUI();
        }
    }


    //private void OnEnable()
    //{
    //    score = (GameController.Instance.money) + (-GameController.Instance.lostClients * 5) + (Time.time * 0.5f);

    //    money.text = $"{GameController.Instance.money}";
    //    clientsLost.text = $"Clientes insatisfeitos: {GameController.Instance.lostClients}";
    //    finalTime.text = $"Tempo total: {Time.time}";
    //    finalScore.text = $"Pontuação final: {score}";
    //    rank.text = $"{FinalRank()}";
    //    if(score > PlayerPrefs.GetFloat("HighScore"))
    //    {
    //        PlayerPrefs.SetFloat("HighScore", score);
    //        PlayerPrefs.SetString("Rank", rank.text);
    //    }
    //}

    private char FinalRank()
    {
        float score = GameController.Instance.CalculateScore();
        if(score <= 0)
        {
            return 'F';
        }
        else if(score < 20)
        {
            return 'E';
        }
        else if(score < 40)
        {
            return 'D';
        }
        else if(score < 60)
        {
            return 'C';
        }
        else if(score < 80)
        {
            return 'B';
        }
        else if(score < 100)
        {
            return 'A';
        }
        else
        {
            return 'S';
        }
    }


}
