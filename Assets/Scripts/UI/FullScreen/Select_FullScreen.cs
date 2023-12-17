using UnityEngine;

public class Select_FullScreen : MonoBehaviour
{
    public void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
