using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnHandler : MonoBehaviour {
  public Animator AMT;

  private int levelToLoad;
  private void Awake() {
    Screen.sleepTimeout = SleepTimeout.NeverSleep;
    Application.targetFrameRate = 60;
  }

  public void Replay() {
    FadeToLevel(SceneManager.GetActiveScene().buildIndex);
  }

  public void MoveToJF() {
    Application.OpenURL("https://github.com/jongfeel");
  }

  public void FadeToLevel(int levelIndex) {
    levelToLoad = levelIndex;
    AMT.SetTrigger("FadeOut");
  }

  public void OnFadeComplete() {
    SceneManager.LoadScene(levelToLoad);
  }

}
