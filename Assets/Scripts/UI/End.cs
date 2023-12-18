using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class End : Singleton<End>
{
    [SerializeField] TMP_Text points, lostClients, timeLeft, money;
    public void EndUpdate()
    {
        money.text = $"{GameController.Instance.money}";
        points.text = $"pontua��o final:{GameController.Instance.CalculateScore()}";
        lostClients.text = $"Clientes insatisfeitos:{GameController.Instance.lostClients}";
        timeLeft.text = $"Tempo Restante:{TimeController.Instance.timeLeft}";
    }
}
