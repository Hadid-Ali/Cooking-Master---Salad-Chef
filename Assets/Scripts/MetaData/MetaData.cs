using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MetaData", menuName = "ScriptableObjects/MetaData", order = 3)]
public class MetaData : ScriptableObject
{
    public int totalVegetables ;
    public int totalCombinations ;
    public int scoreForEachIngredient;
    
    public float powerUpPercentage;
    public float playerLiveTime;
    public float timeForEachIngredient;
    public float timeToChop;
    public int customerLeaveScoreSubtractAmount;
    public int powerUpAddScore;
    public float powerUpAddSpeed;
    public float powerUpAddSpeedDuration;
    public float powerUpAddTime;
    
}
