using UnityEngine;
using System.Collections;

public class ButtonGenerator : MonoBehaviour {

    //The prefab that will be created.
    public GameObject mineButton;
    public Canvas canvas;
    //The global game object that holds state.
    public GameManager gm;

    //Runs on script execution
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //Get the size of the world for scale.
                Vector3 worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
                //Create a button object dependent on the array position.
                GameObject buttonObject = Instantiate(mineButton, new Vector3(-worldSize.y + i * (worldSize.y/5) + worldSize.y/10,
                                        -worldSize.y + j * (worldSize.y/5) + worldSize.y/10, 0), Quaternion.identity) as GameObject;
                //Gets the script attached to the button and sets the x and y values.
                Button button = buttonObject.GetComponent<Button>();
                button.x = i;
                button.y = j;
                button.transform.SetParent(canvas.transform);
                gm.buttons[i, j] = button;
            }
        }
        //Adds mines to 10 of the buttons
        gm.mine();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //Figures out the cells adjacent to each cell.
                gm.buttons[i, j].getSurroundingButtons();
                //Calculates the number of mines in adjacent cells.
                gm.buttons[i, j].CalculateMines();
            }
        }
    }
}
