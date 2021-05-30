using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
  private int gemsCount = 0;
  [SerializeField] 
  private TMP_Text gemText;

  public static GameSession instance;

  private void Awake()
  {
    SetUpSingleton();
  }

  private void Start()
  {
    UpdateGemsText();
  }

  private void UpdateGemsText()
  {
    gemText.text = gemsCount.ToString();
  }

  public void AddGems(int amount)
  {
    gemsCount += amount;
    UpdateGemsText();
  }

  private void SetUpSingleton()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      gameObject.SetActive(false);
      Destroy(gameObject);
      return;
    }
    DontDestroyOnLoad(gameObject);
  }
}
