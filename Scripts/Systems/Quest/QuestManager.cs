using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();
    public void Start()
    {
        //can be taken from a resource file?
        QuestEvent a = quest.AddQuestEvent("test1", "description1");
        QuestEvent b = quest.AddQuestEvent("test2", "description2");
        QuestEvent c = quest.AddQuestEvent("test3", "description3");
        QuestEvent d = quest.AddQuestEvent("test4", "description4");
        QuestEvent e = quest.AddQuestEvent("test5", "description5");

        quest.AddPath(a.GetId(), b.GetId());
        quest.AddPath(b.GetId(), c.GetId());
        quest.AddPath(b.GetId(), d.GetId());
        quest.AddPath(c.GetId(), e.GetId());
        quest.AddPath(d.GetId(), e.GetId());


        quest.BFS(a.GetId());
        quest.PrintPath();
    }

   
}
