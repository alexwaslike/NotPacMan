using UnityEngine;

public class MainCharacterObj : MonoBehaviour {

    public GameController controller;

    // percent from 0-100
    private float maxHopeLevel = 1.0f;
    
    public float hopeDecrease = 0.01f;
    public float hopeIncrease = 0.5f;

    private float hopeLevel = 1.0f;
    public float HopeLevel
    {
        get { return hopeLevel; }
    }

    public void IncreaseHope()
    {
        if (hopeLevel + hopeIncrease > hopeLevel)
            hopeLevel = maxHopeLevel;
        else
            hopeLevel += hopeIncrease;
    }

    public void DecreaseHope()
    {
        if (hopeLevel - hopeDecrease < 0)
        {
            hopeLevel = 0;
            controller.GameEnd(wonGame: false);
        }  
        else
            hopeLevel -= hopeDecrease;
    }
}
