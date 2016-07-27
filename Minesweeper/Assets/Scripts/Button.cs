using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Threading;

public class Button : MonoBehaviour, IPointerClickHandler
{
    public GameManager gm;
    public bool mined = false;
    public ArrayList surroundingButtons = new ArrayList();
    public int x;
    public int y;
    public int surroundingMines = 0;
    public Sprite zero, one, two, three, four, five, mine;
    public bool clicked = false;
    public Sprite flag;
    public bool flagged = false;
    public Sprite button;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
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
    public void CalculateMines()
    {
        foreach (Button b in surroundingButtons)
        {
            if (b.mined)
            {
                surroundingMines++;
                Debug.Log(surroundingMines);
            }
        }
    }
    public void Clicked()
    {
        if (!flagged)
        {
            if (mined)
            {
                GetComponent<Image>().sprite = mine;
                gm.clearedButtons = 0;
                foreach (Button b in gm.buttons)
                {
                    if (!b.clicked)
                    {
                        b.Reveal();
                    }
                }
                SceneManager.LoadScene("Fail");
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
                    SceneManager.LoadScene("Pass");
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
                    }
                }
            }
        }
    }
    public void Reveal()
    {
        if (mined)
        {
            GetComponent<Image>().sprite = mine;
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
