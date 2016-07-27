using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenChanger : MonoBehaviour {

	public void toGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void toTitle()
    {
        Debug.Log("AH");
        SceneManager.LoadScene("Title");
    }
}
