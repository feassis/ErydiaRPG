using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] private bool isEnemy;
    [SerializeField] private GameObject characterPortraitInQueue;

    public float GetMaxHP() => stats.MaxHP;

    public float GetSpeed() => stats.Speed;

    public GameObject GetCharaterPortraitInQueue() => characterPortraitInQueue;

    private void Awake()
    {
        QueueTurnSystem.Instance.AddCharacterToQueue(this); 
    }
}
