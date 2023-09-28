using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCanvas : MonoBehaviour
{
    [SerializeField] Canvas[] currentCanvas;
    public int currentCanvasIndex;
    public void LoadCanvas(int i)
    {
        AudioManager.instance.Click();
        currentCanvas[currentCanvasIndex].gameObject.SetActive(false);
        currentCanvasIndex = i;
        currentCanvas[i].gameObject.SetActive(true);
    }
}
