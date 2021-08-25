using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public Field.FieldColor fieldColor;
    public int number;
    public bool isIdle;
    public bool isFinnish;
    public void SetColorRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 48, 77, 255);
        fieldColor = Field.FieldColor.Red;
    }

    public void SetColorBlue()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 191, 191, 255);
        fieldColor = Field.FieldColor.Blue;
    }

    public void SetColorGreen()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(46, 166, 87, 255);
        fieldColor = Field.FieldColor.Green;
    }

    public void SetColorYellow()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 178, 74, 255);
        fieldColor = Field.FieldColor.Yellow;
    }
}