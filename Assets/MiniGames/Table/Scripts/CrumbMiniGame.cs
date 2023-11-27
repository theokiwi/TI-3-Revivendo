using UnityEngine;
using UnityEngine.UI;

public class CrumbMiniGame : MonoBehaviour
{
    [SerializeField] GameObject crumb;
    [SerializeField] GameObject canvas;
    [SerializeField] Slider slider;
    [SerializeField] GameObject minigame;
    public static CrumbMiniGame instance;
    public Table table;
    
    
    private void Awake()
    {
        instance = this;
        for(int i = 0; i < 20; i++)
        {
            Vector3 pos = new Vector3(Random.RandomRange(-128f,128f),Random.RandomRange(-128f,128f),0);
            GameObject img = Instantiate(crumb);
            img.transform.SetParent(canvas.transform);
            img.transform.localPosition = pos;
        }
    }
    public void MinusCrumb()
    {
        slider.value += 1;
        if(slider.value >= 20)
        {
            Destroy(minigame,0.5f);
            table.isDirty = false;
            MiniGamesManager.instance.ExitMiniGame();
        }
    }
}
