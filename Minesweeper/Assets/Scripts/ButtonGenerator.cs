using UnityEngine;
using System.Collections;

public class ButtonGenerator : MonoBehaviour {

    public GameObject mineButton;
    public Canvas canvas;
    public GameManager gm;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
                float scale = worldSize.y / Screen.height;
                GameObject buttonObject = Instantiate(mineButton, new Vector3(i - worldSize.y + Screen.height/10 * scale / 2,
                                        j - worldSize.y + Screen.height/10 * scale / 2, 0), Quaternion.identity) as GameObject;
                Button button = buttonObject.GetComponent<Button>();
                button.x = i;
                button.y = j;
                button.transform.SetParent(canvas.transform);
                gm.buttons[i, j] = button;
            }
        }
        gm.mine();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                gm.buttons[i, j].getSurroundingButtons();
                gm.buttons[i, j].CalculateMines();
                //Set the text to the number.
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
