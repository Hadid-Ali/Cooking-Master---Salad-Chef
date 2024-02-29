using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/CombinationData", order = 2)]
public class CombinationData : ScriptableObject
{
    public CombinationName Name;
    public List<VegetableName> Combination;
}
