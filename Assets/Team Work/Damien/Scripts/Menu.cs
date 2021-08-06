using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Damien
{
    public class Menu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Options()
        {
            
        }

        public void QuitGame()
        {
            // Doesn't work in editor, only works in the build
            Debug.Log("Game Has Quit");
            Application.Quit();
        }
    }
}
