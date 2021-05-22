using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent 
{
    public enum EventStatus { WAITING, CURRENT, DONE};
    //WAITING - not yet completed, but can't be worked on because of a pre requisite event
    //CURRENT - the one the player should be trying to achieve
    //DONE - has been achieved

    public string name, id, desc;
    public int order = -1;
    public EventStatus status;

    public List<QuestPath> pathList = new List<QuestPath>();

    public QuestEvent(string n, string d)
    {
        name = n;
        desc = d;
        id = Guid.NewGuid().ToString();
        status = EventStatus.WAITING;
    }

    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
    }

    public string GetId()
    {
        return id;
    }
}
