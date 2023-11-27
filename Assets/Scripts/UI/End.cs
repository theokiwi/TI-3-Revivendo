using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class End : MonoBehaviour
{
    [SerializeField] TMP_Text points, lostClients, timeLeft, money;
    public static End Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void EndUpdate()
    {
        money.text = $"{GameController.Instance.money}";
        points.text = $"pontuação final:{GameController.Instance.CalculateScore()}";
        lostClients.text = $"Clientes insatisfeitos:{GameController.Instance.lostClients}";
        timeLeft.text = $"Tempo Restante:{TimeController.Instance.timeLeft}";
    }
}
