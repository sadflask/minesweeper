using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public AudioSource source;
    public AudioClip explosion;
    public AudioClip success;

    public GameManager gm;
    public bool mined = false;
    public ArrayList surroundingButtons = new ArrayList();
    public int x;
    public int y;
    public int surroundingMines = 0;
    //Sprites to be used for the buttons.
    public Sprite zero, one, two, three, four, five, six, seven, eight, mine;
    public Sprite flag;
    public Sprite button;
    //Booleans to save the buttons current state.
    public bool clicked = false;
    public bool flagged = false;
    //Variables to hold end of game state.
    private bool passed;
    private bool failed;
    private float timeSinceEnd = 0;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    //Checks for flag clicks and toggles the flag.
    public void ToggleFlag()
    {
        //Only toggle the flag if the button has not been clicked.
        if (!clicked)
        {
            if (flagged)
            {
                GetComponent<Image>().sprite = button;
            }
            else
            {
                GetComponent<Image>().sprite = flag;
            }
            flagged = !flagged;
        }
    }
    //Finds all the adjacent cells for the button.
    public void getSurroundingButtons()
    {
        gm = gameObject.transform.parent.GetComponent<Transform>().GetComponent<ButtonGenerator>().gm;
        if (x > 0)
        {
            //Add square to left
            surroundingButtons.Add(gm.buttons[x - 1, y]);
            if (y > 0)
            {
                //Add bottom left
                surroundingButtons.Add(gm.buttons[x - 1, y - 1]);
            }
            if (y < 9)
            {
                //Add top left
                surroundingButtons.Add(gm.buttons[x - 1, y + 1]);
            }
        }
        if (x < 9)
        {
            //Add right
            surroundingButtons.Add(gm.buttons[x + 1, y]);
            if (y > 0)
            {
                //Add bottom right
                surroundingButtons.Add(gm.buttons[x + 1, y - 1]);
            }
            if (y < 9)
            {
                //Add top right
                surroundingButtons.Add(gm.buttons[x + 1, y + 1]); 
            }
        }
        if (y > 0)
        {
            //Add bottom
            surroundingButtons.Add(gm.buttons[x, y - 1]);
        }
        if (y < 9)
        {
            //Add top
            surroundingButtons.Add(gm.buttons[x, y + 1]);
        }

    }
    //Iterate through the list of surrounding buttons to see how many have mines.
    public void CalculateMines()
    {
        foreach (Button b in surroundingButtons)
        {
            if (b.mined)
            {
                surroundingMines++;
            }
        }
    }
    //Function triggered when button is clicked.
    public void Clicked()
    {
        if (gm.flagged)
        {
            ToggleFlag();
        }
        else {
            if (!flagged)
            {
                if (mined)
                {
                    //Ends game if mine is clicked.
                    GetComponent<Image>().sprite = mine;
                    source.PlayOneShot(explosion);
                    gm.clearedButtons = 0;
                    foreach (Button b in gm.buttons)
                    {
                        if (!b.clicked)
                        {
                            b.Reveal();
                        }
                    }
                }
                else
                {
                    if (!clicked)
                    {
                        gm.clearedButtons++;
                        clicked = true;
                    }
                    if (gm.clearedButtons > 89)
                    {
                        passed = true;
                        source.PlayOneShot(success);
                    }
                    if (surroundingMines == 0)
                    {
                        GetComponent<Image>().sprite = zero;
                        foreach (Button b in surroundingButtons)
                        {
                            if (!b.clicked)
                            {
                                b.Clicked();
                            }
                        }
                    }
                    else
                    {
                        switch (surroundingMines)
                        {
                            case 1:
                                GetComponent<Image>().sprite = one;
                                break;
                            case 2:
                                GetComponent<Image>().sprite = two;
                                break;
                            case 3:
                                GetComponent<Image>().sprite = three;
                                break;
                            case 4:
                                GetComponent<Image>().sprite = four;
                                break;
                            case 5:
                                GetComponent<Image>().sprite = five;
                                break;
                            case 6:
                                GetComponent<Image>().sprite = six;
                                break;
                            case 7:
                                GetComponent<Image>().sprite = seven;
                                break;
                            case 8:
                                GetComponent<Image>().sprite = eight;
                                break;
                        }
                    }
                }
            }
        }
    }
    //Executes at regular intervals.
    void Update()
    {
        //Increment timer if end of game has been reached
        if (passed || failed )
        {
            timeSinceEnd += Time.deltaTime;
        }
        if (timeSinceEnd>1)
        {
            if (passed)
                SceneManager.LoadScene("Pass");
            if (failed)
                SceneManager.LoadScene("Fail");
        }
    }
    //Reveals all the other squares if a mine has been clicked.
    public void Reveal()
    {
        if (mined)
        {
            GetComponent<Image>().sprite = mine;
            source.PlayOneShot(explosion);
            failed = true;
        }
        else
        {
            switch (surroundingMines)
            {
                case 0:
                    GetComponent<Image>().sprite = zero;
                    break;
                case 1:
                    GetComponent<Image>().sprite = one;
                    break;
                case 2:
                    GetComponent<Image>().sprite = two;
                    break;
                case 3:
                    GetComponent<Image>().sprite = three;
                    break;
                case 4:
                    GetComponent<Image>().sprite = four;
                    break;
                case 5:
                    GetComponent<Image>().sprite = five;
                    break;
            }
        }
    }
}
