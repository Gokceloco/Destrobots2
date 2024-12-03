using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public MessageUI messageUI;

    public void RestartMainUI()
    {
        messageUI.Hide();
    }
    public void ShowMessage(string msg, float duration)
    {
        messageUI.ShowMessage(msg, duration);
    }
}
