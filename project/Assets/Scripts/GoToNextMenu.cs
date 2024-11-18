using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSecondMenu : MonoBehaviour
{
    public GameObject previousCanvas, nextCanvas;

    public void LoadNextMenu()
    {
        previousCanvas.SetActive(false);
        nextCanvas.SetActive(true);
    }
}
