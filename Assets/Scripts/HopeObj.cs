using UnityEngine;
using System.Collections;

public class HopeObj : MonoBehaviour {

    public GameController controller;
    
    private bool mouseDown = false;

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
