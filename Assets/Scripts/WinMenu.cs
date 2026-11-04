using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); // меню сахнасының дәл атын жаз
    }
}