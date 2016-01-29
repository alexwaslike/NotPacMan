using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HopeObj : MonoBehaviour {

    public GameController controller;
    
    private Light highlight;

    private bool mouseDown = false;

    void Start()
    {
        Light[] results = GetComponentsInChildren<Light>();
        for(int i=0; i<results.Length; i++)
        {
            if (results[i].name.Equals("Highlight"))
                highlight = results[i];
        }
    }

    void OnMouseEnter()
    {
        if(highlight != null)
            highlight.enabled = true;
    }

    void OnMouseExit()
    {
        if (highlight != null)
            highlight.enabled = false;
    }

    void OnMouseDown()
    {
        mouseDown = true;
    }

    void OnMouseUp()
    {
        if (mouseDown)
        {
            if(gameObject.tag.Equals("Endgame"))
                controller.ClickedHopeObj(isEndGameHope: true);
            else
                controller.ClickedHopeObj(isEndGameHope: false);
            Instantiate(controller.hopeUsedAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
