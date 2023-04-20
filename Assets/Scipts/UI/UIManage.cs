
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManage : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScene, pauseGame;
    [SerializeField] private AudioClip gameOverSound;
    private void Awake()
    {
        gameOverScene.SetActive(false);
        pauseGame.SetActive(false);
    }
    public void HandlePauseGame()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (pauseGame.activeInHierarchy) PauseGame(false);
        else PauseGame(true);
    }
    private void Update()
    {
        HandlePauseGame();
    }
    public void GameOver()
    {
        gameOverScene.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PauseGame(bool status)
    {
        if (status) Time.timeScale = 0;
        else Time.timeScale = 1;
        pauseGame.SetActive(status);
    }
    public void OnChangeSound()
    {
        SoundManager.instance.ChangeVolume(0.2f);
    }
}
