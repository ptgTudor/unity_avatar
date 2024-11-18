using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkinColor : MonoBehaviour
{
    [Header("Color Values")]
    public float redAmount;
    public float greenAmount;
    public float blueAmount;

    [Header("Color Sliders")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private Color currentSkinColor;
    
    public List<SkinnedMeshRenderer> rendererList = new List<SkinnedMeshRenderer>();

    public void UpdateSliders()
    {
        redAmount = redSlider.value;
        greenAmount = greenSlider.value;
        blueAmount = blueSlider.value;
        SetSkinColor();
    }

    public void SetSkinColor()
    {
        currentSkinColor = new Color(redAmount, greenAmount, blueAmount);

        for (int i = 0; i < rendererList.Count; i++)
        {
            rendererList[i].sharedMaterial.SetColor("_Color", currentSkinColor);
        }
    }
}
