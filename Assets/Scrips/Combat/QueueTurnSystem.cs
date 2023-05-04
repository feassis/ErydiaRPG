using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTurnSystem : MonoBehaviour
{
    public static QueueTurnSystem Instance;
    private List<CharaterInQueue> queue = new List<CharaterInQueue>();
    public Transform queuePortraitHolder;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    public void AddCharacterToQueue(CombatUnit unit)
    {
        queue.Add(new CharaterInQueue(unit, unit.GetSpeed(),
            unit.GetCharaterPortraitInQueue()));
        SortQueue();
        GenerateQueue();
    }

    //change it later for better performance
    private void GenerateQueue()
    {
        foreach (Transform child in queuePortraitHolder)
        {
            Destroy(child.gameObject);
        }

        foreach (CharaterInQueue charater in queue)
        {
            Instantiate(charater.QueuePortrait, queuePortraitHolder);
        }
    }

    private void SortQueue()
    {
        queue.Sort((CharaterInQueue a, CharaterInQueue b) => (b.CurrentSpeed - a.CurrentSpeed) >= 0 ? 1 : -1);
    }
}

[Serializable]
public struct CharaterInQueue
{
    public CombatUnit Character;
    public float CurrentSpeed;
    public GameObject QueuePortrait;

    public CharaterInQueue(CombatUnit character, float currentSpeed, GameObject queuePortrait)
    {
        Character = character;
        CurrentSpeed = currentSpeed;
        QueuePortrait = queuePortrait;
    }
}
