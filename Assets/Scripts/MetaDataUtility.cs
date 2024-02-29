using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaDataUtility : MonoBehaviour
{
    [SerializeField] private List<CombinationData> Combinations;
    private static List<CombinationData> combinatonsStatic; 

    private void Start()
    {
        combinatonsStatic = Combinations;
        
        DontDestroyOnLoad(this);
        SortLists();
    }

    public static void SortLists()
    {
        foreach (var v in combinatonsStatic)
        {
            v.Combination.Sort();
        }
    }

    public static CombinationName CheckCombination(List<VegetableName> vegetables)
    {
        vegetables.Sort();
        foreach (var v in combinatonsStatic)
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
