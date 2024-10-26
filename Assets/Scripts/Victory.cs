using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Victory : MonoBehaviour
{
    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
