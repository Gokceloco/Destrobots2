using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public GameDirector gameDirector;
    public List<VideoPlayer> videos;
    public void Show()
    {
        gameDirector.gameState = GameState.GameMenu;
        gameDirector.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameDirector.mainUI.levelUI.Hide();
        gameDirector.mainUI.timerUI.Hide();
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, .2f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void RestartGameButtonPressed()
    {
        Invoke(nameof(Hide), .25f);
        gameDirector.levelManager.ResetCurLevel();
        var levelNo = PlayerPrefs.GetInt("CurrentLevel");
        PlayCinematic(levelNo - 1);
        var startDelay = 0;
        if (levelNo == 1)
        {
            startDelay = 9;
            gameDirector.mainUI.inventoryUI.Hide();
        }
        gameDirector.RestartLevelDelayed(startDelay);
        gameDirector.audioManager.PlayButtonPressedSFX();
    }
    public void ResumeGameButtonPressed()
    {
        Hide();
        var levelNo = PlayerPrefs.GetInt("CurrentLevel");
        var startDelay = 0;
        if (levelNo == 1)
        {
            startDelay = 9;
            gameDirector.mainUI.inventoryUI.Hide();
        }
        gameDirector.RestartLevelDelayed(startDelay);
        gameDirector.audioManager.PlayButtonPressedSFX();

    }
    public void PlayCinematic(int i)
    {
        videos[i].gameObject.SetActive(true);
        videos[i].Play();
    }
    public void HideCinematics()
    {
        foreach (var v in videos)
        {
            v.gameObject.SetActive(false);
        }
    }
    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
