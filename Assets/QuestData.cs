using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcID;

    public QuestData(string name, int[] npc)//»ý¼ºÀÚ
    {
        questName = name;
        npcID = npc;
    }

}
