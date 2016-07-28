using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    //Array to hold all the button objects.
    public Button[,] buttons = new Button[10,10];
    //The list of buttons that have mines under them.
    public int[] minedButtons = new int[10];
    //The number of buttons that have been clicked.
    public int clearedButtons = 0;
	
    //Add mines to 10 of the buttons.
    public void mine()
    {
        for (int i = 0; i < 10; i++ )
        {
            //Generate a random index in which to put the mine.
            System.Random ran = new System.Random();
            int index = ran.Next(100);
            //Check if the index is already mined.
            int pos = Array.IndexOf(minedButtons, index);
            while (pos > -1)
            {
                index = ran.Next(100);
                pos = Array.IndexOf(minedButtons, index);
            }
            //index is not in list, add to list
            minedButtons[i] = index;
            Button buttonToMine = buttons[index/10,index%10];
            buttonToMine.mined = true;
        }
    }
}