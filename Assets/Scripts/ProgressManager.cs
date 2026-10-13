using System;
using UnityEngine;

public enum ProgressUnlocked
{
    None,           // This means it's not supposed to unlock anything
    ClownTalked1,   // first clown conversation finished -> unlocks door 1
    Lvl1Beaten,     // final door of level 1 entered -> swaps the clowns
    ClownTalked2    // second clown conversation finished -> playerballoondropping IsUnlocked
}

public static class ProgressManager
{
    // fires whenever anything that changes the player's progress happens.
    // anything that needs to know when the player has unlocked something needs to subscribe
    public static event Action onChanged;

    // reads the saved int (using 0 if nothing is saved yet) and returns true if it's 1.
    public static bool Get(ProgressUnlocked unlock)
    {
        // if the unlock isn't unlocking anything, then you don't get it
        if (unlock == ProgressUnlocked.None)
        {
            return false;
        }
        
        // unlock.ToString() turns an enum value like ClownTalked1 into the text “ClownTalked1”,
        // which is used as the save key.
        int savedValue = PlayerPrefs.GetInt(unlock.ToString(), 0);

        if (savedValue == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // writes 1 or 0, saves, then announces the change
    public static void Set(ProgressUnlocked unlocked, bool value = true)
    {
        // if it's none, ignore it
        if (unlocked == ProgressUnlocked.None)
        {
            return;
        }

        // if it's true, then save it as true, otherwise, save it as false
        if (value == true)
        {
            PlayerPrefs.SetInt(unlocked.ToString(), 1);
        }
        else
        {
            PlayerPrefs.SetInt(unlocked.ToString(), 0);
        }

        PlayerPrefs.Save();

        // if one subscriber exists, then invoke for them.
        if (onChanged != null)
        {
            onChanged.Invoke();
        }
    }
}