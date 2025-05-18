using UnityEngine;

public enum ScoreType
{
    Attack,
    Dodge,
    Jump
}

public class PointType : MonoBehaviour
{
    public ScoreType scoreType;
}