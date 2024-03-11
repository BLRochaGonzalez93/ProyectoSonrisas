using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RailType
{
    TYPE_S, TYPE_A, TYPE_B, TYPE_C
}

[CreateAssetMenu(menuName = "Rail")]
public class Rail : ScriptableObject
{
    public int ID;
    public string railName;
    public RailType type;
    public bool canBeSelected = true;
    public bool countForCD = true;
    public int cooldown = 3;
    public GameObject prefab;

    public static int SortByID(Rail r1, Rail r2)
    {
        return r1.ID.CompareTo(r2.ID);
    }
}
