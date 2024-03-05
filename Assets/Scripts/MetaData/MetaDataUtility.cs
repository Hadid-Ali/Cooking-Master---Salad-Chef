using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MetaDataUtility : MonoBehaviour
{
    [SerializeField] private List<CombinationData> combinations;
    [SerializeField] private MetaData metaDataS;

    private static List<CombinationData> _staticCombinations;
    public static MetaData metaData;
    private void Awake()
    {
        _staticCombinations = combinations;
        metaData = metaDataS;
        
        DontDestroyOnLoad(this);
        SortLists();
    }

    public static void SortLists()
    {
        foreach (var v in _staticCombinations)
        {
            v.Combination.Sort();
        }
    }

    public static CombinationName CheckCombination(List<VegetableName> vegetables)
    {
        vegetables.Sort();
        foreach (var v in _staticCombinations)
        {
            if (IsSame(v.Combination, vegetables))
                return v.Name;
        }

        return CombinationName.None;
    } 
    private static bool IsSame(List<VegetableName> A, List<VegetableName> B)
    {
        if (A.Count != B.Count)
        {
            return false;
        }

        for (int i = 0; i < A.Count; i++)
        {
            if(A[i] != B[i])
            {
                return false;
            }
        }

        return true;
    }
    
}
