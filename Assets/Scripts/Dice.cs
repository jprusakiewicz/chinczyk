using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private List<Sprite> diceSides;

    private void Start()
    {
        diceSides = new List<Sprite>();
        for (int i = 1; i <= 6; i++)
        {
            diceSides.Add(Resources.Load<Sprite>(Path.Combine("Dice", "dice_" + i)));
        }
    }

    public void SetDice(int num)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = diceSides[num - 1];
    }
}