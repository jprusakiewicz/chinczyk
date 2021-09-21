using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private GameObject blueArrow;
    [SerializeField] private GameObject redArrow;
    [SerializeField] private GameObject greenArrow;
    [SerializeField] private GameObject yellowArrow;

    void Start()
    {
        DeactivateArrows();
    }

    void DeactivateArrows()
    {
        blueArrow.SetActive(false);
        redArrow.SetActive(false);
        greenArrow.SetActive(false);
        yellowArrow.SetActive(false);
    }

    public void ActivateArrow(string arrowName)
    {
        DeactivateArrows();
        
        switch (arrowName)
        {
            case "Blue":
                blueArrow.SetActive(true);
                break;
            case "Red":
                redArrow.SetActive(true);
                break;
            case "Green":
                greenArrow.SetActive(true);
                break;
            case "Yellow":
                yellowArrow.SetActive(true);
                break;
        }
    }
}