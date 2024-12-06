using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public MessageUI messageUI;

    public void RestartMainUI()
    {
        ShowMessage("YOU'VE GOT 10 MIN TO GET THE SERUM!", 3, .3f);
    }
    public void ShowMessage(string msg, float duration, float delay = 0f)
    {
        messageUI.ShowMessage(msg, duration, delay);
    }
}
