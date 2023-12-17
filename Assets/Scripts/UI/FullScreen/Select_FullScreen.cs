using UnityEngine;

public class Select_FullScreen : MonoBehaviour
{
    public void Activate()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
