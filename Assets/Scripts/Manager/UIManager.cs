using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timeText;
    public Text ruleText;

    // Update is called once per frame
    void Update()
    {
        timeText.text = GameManager.instance.GetGameCountdown().ToString("000");
        ruleText.text = GameManager.instance.GetStageRule();
    }

}