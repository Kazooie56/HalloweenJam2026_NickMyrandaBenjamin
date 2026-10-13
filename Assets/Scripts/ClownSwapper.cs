using UnityEngine;
public class ClownSwapper : MonoBehaviour
{
    [SerializeField] private GameObject beforeLvl1BeatNPC;
    [SerializeField] private GameObject afterLvl1BeatNPC;

    // subscribing to the action
    private void OnEnable()
    {
        ProgressManager.onChanged += Refresh;
    }

    private void OnDisable()
    {
        ProgressManager.onChanged -= Refresh;
    }

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        // just checks if we have lvl1Beaten on this pc
        bool lvl1Beat = ProgressManager.Get(ProgressUnlocked.Lvl1Beaten);
        beforeLvl1BeatNPC.SetActive(!lvl1Beat);
        afterLvl1BeatNPC.SetActive(lvl1Beat);
    }
}