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
        StartCoroutine(AnimateDice(num));
        SetDiceSprite(num);

    }

    void SetDiceSprite(int num)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = diceSides[num - 1];
    }

    public IEnumerator AnimateDice(int num)
    {
        Debug.Log(num);
        var random = Random.Range(3, 7);
        for (int i = 0; i < random; i++)
        {
            var rand_time = Random.Range(0.05f, 0.18f);
            var rand_dice = Random.Range(1, 6);
            SetDiceSprite(rand_dice);
            yield return new WaitForSeconds(rand_time);
        }
        SetDiceSprite(num);
    }
}