using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Ищет сцену с точным названием "Level1". 
        // Убедись, что твоя сцена первого уровня называется именно так!
        SceneManager.LoadScene("Level1"); 
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры нажат!");
        Application.Quit(); // Эта команда закроет игру на готовом телефоне
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}