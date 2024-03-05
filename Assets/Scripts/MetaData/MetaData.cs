using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MetaData", menuName = "ScriptableObjects/MetaData", order = 3)]
public class MetaData : ScriptableObject
{
    public float powerUpPercentage;
    public int vegetableTotalNumber ;
    public float playerLiveTime;
    public float timeForEachIngredient;
    public float TimeToChop;
    public int customerLeaveScoreSubtractAmount;
    public int powerUpAddScore;
    public float powerUpAddSpeed;
    public float powerUpAddSpeedDuration;
    public float powerUpAddTime;
    
}
