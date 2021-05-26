using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  public void StartFirstLevel()
  {
    SceneManager.LoadScene(1);
  }

  public void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex + 1);
  }

  public void ReloadCurrentScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
