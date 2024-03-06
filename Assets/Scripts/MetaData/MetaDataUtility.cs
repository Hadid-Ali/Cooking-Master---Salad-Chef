using System.Collections.Generic;
using UnityEngine;


public class MetaDataUtility : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private MetaData metaDataS;

    public static List<Recipe> Recipes;
    public static MetaData MetaData;
    private void Awake()
    {
        Recipes = recipes;
        MetaData = metaDataS;
        
        DontDestroyOnLoad(this);
        SortLists();
    }

    public static void SortLists()
    {
        foreach (var v in Recipes)
        {
            v.vegetables.Sort();
        }
    }

    public static RecipeName CheckCombination(List<VegetableName> vegetables)
    {
        vegetables.Sort();
        foreach (var v in Recipes)
        {
            if (IsSame(v.vegetables, vegetables))
                return v.recipeName;
        }

        return RecipeName.None;
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
