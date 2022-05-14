using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject GameClearUI;
    public GameObject BtnPause;

    private void Start() {
      PlayerController player = FindObjectOfType<PlayerController>();
      player.GameOver += GameOverM;

      JF mentor = FindObjectOfType<JF>();
      mentor.OnClear += ClearM;
    }
    public void GameOverM() {
      GameOverUI.SetActive(true);
    }

    public void ClearM() {
      GameClearUI.SetActive(true);
      BtnPause.SetActive(false);
    }

    public void Pause() {
      Time.timeScale = 0f;
    }

    public void Continue() {
      Time.timeScale = 1f;
    }
}
