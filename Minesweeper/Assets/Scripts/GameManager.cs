using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public Button[,] buttons = new Button[10,10];
    public int[] minedButtons = new int[10];
    public int clearedButtons = 0;
	
    public void mine()
    {
        for (int i = 0; i < 10; i++ )
        {
            System.Random ran = new System.Random();
            int index = ran.Next(100);
            int pos = Array.IndexOf(minedButtons, index);
            while (pos > -1)
            {
                index = ran.Next(100);
                pos = Array.IndexOf(minedButtons, index);
            }
            //index is not in list, add to list
            minedButtons[i] = index;
            Button buttonToMine = buttons[index/10,index%10];
            Debug.Log(index / 10 + " + " + index % 10);
            buttonToMine.mined = true;
        }
    }
}
