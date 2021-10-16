using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Rogue Data", menuName = "Character Data/Rogue")]
public class RogueData : CharacterData
{
    public RogueWeaponType _weaponType;
    public RogueStrategyType _strategyType;
}
