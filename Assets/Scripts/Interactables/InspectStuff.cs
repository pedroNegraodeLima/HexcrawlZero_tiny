using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectStuff : MonoBehaviour
{
    public Sprite icon;

    public Dialogue dialogue;

    [ContextMenu("CopyDialogueFromTrigger")]
    public void CopyDialogueFromTrigger()
    {
        var trigger = GetComponentInChildren<DialogueTrigger>();
        //dialogue = new Dialogue(trigger.dialogue);
    }
}
