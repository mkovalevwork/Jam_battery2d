using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        if (myStats = null)
        {
            myStats = GetComponent<EnemyStats>();
        }
    }
    public void Attack (CharacterStats targetStats)
    {
        targetStats.TakeDamage(myStats.damage.GetValue());
        
    }
}
