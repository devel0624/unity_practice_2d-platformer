
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStageRule : MonoBehaviour
{

    public GameObject ruleTarget;
    public String stageRuleMessage;

    public bool IsFinish()
    {
        bool isDeactivated = ruleTarget.GetComponent<CloudMovement>().isDeactivated();

        return isDeactivated;
    }

    public String GetRuleMessage()
    {
        // "구름이 캐릭터를 따라다니지 않아야 클리어 가능합니다."
        return stageRuleMessage;
    }
    
}
