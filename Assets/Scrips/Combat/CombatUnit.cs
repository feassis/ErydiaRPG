using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] private bool isEnemy;

    public float GetMaxHP() => stats.MaxHP;
}
