using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] public int number;

    [System.Serializable] public enum FieldColor
    {
        Green,
        Red,
        Blue,
        Yellow,
        Regular
    };

    public FieldColor fieldColor;
    [SerializeField] public bool isIdle;
    [SerializeField] public bool isFinnish;
    
}
