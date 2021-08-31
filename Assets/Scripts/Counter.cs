using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Counter : MonoBehaviour
{
    public Field.FieldColor fieldColor;
    public int number;
    public bool? isFinnish = null;
    private ConnectionManager connectionManager;

    private void Start()
    {
        connectionManager = GameObject.Find("GameController").GetComponent<ConnectionManager>();
    }

    public void SetCounterDetails(int number, bool isFinnish)
    {
        this.number = number;
        this.isFinnish = isFinnish;
        
    }

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

    private void OnMouseDown()
    {
        switch (isFinnish)
        {
            case null:
                connectionManager.CounterClick(false, true, fieldColor, number);
                break;
            case true:
                connectionManager.CounterClick(true, false, fieldColor, number);
                break;
            case false:
                connectionManager.CounterClick(false, false, fieldColor, number);
                break;
        }
    }

}