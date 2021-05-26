using System.Collections;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
  [SerializeField]
  float levelLoadDelay = 2f;
  [SerializeField] SceneLoader sceneLoader;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    StartCoroutine(LoadNextLevel());
  }

  private IEnumerator LoadNextLevel()
  {
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(levelLoadDelay);
    Time.timeScale = 1f;
    sceneLoader.LoadNextLevel();
  }
}
