using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour
{

    public GameController controller;
    public RawImage hopeBarImg;
    
    private float height;
    private float maxWidth;
    private float x;
    private float y;

    void Start()
    {
        maxWidth = hopeBarImg.rectTransform.sizeDelta.x;
        height = hopeBarImg.rectTransform.sizeDelta.y;
        x = hopeBarImg.rectTransform.anchoredPosition.x;
        y = hopeBarImg.rectTransform.anchoredPosition.y;
    }

    void Update()
    {

        hopeBarImg.rectTransform.sizeDelta = new Vector2(maxWidth * controller.mainCharacterObj.HopeLevel, height);
        hopeBarImg.rectTransform.anchoredPosition = new Vector2(x + hopeBarImg.rectTransform.sizeDelta.x / 2 - maxWidth/2, y);

    }
}