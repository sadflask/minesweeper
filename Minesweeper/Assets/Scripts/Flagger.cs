using UnityEngine;
using UnityEngine.UI;

public class Flagger : MonoBehaviour {

    public GameManager gm;
    public Sprite flagged;
    public Sprite notFlagged;
    public void Clicked() {
        //Toggles the flagged variable
        gm.flagged = !gm.flagged;
        if (gm.flagged)
        {
            GetComponent<Image>().sprite = flagged;
        } else
        {
            GetComponent<Image>().sprite = notFlagged;
        }
    }
}
