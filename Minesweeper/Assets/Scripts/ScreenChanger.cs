using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenChanger : MonoBehaviour {

    //Changes scene to the game screen.
	public void toGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    //Changes scene to the title screen.
    public void toTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
