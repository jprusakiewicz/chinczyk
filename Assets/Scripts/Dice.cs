using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] public GameObject DiceButton;
    private List<Sprite> diceSides;
    private int num;
    private string turn_id = " ";

    private void Start()
    {
        diceSides = new List<Sprite>();
        for (int i = 1; i <= 6; i++)
        {
            diceSides.Add(Resources.Load<Sprite>(Path.Combine("Dice", "dice_" + i)));
        }
    }

    public void SetDice(int _num, string _turn_id)
    {
        if (_turn_id != turn_id)
        {
            DiceButton.SetActive(true);
            num = _num;
            SetOpacity();
            turn_id = _turn_id;
        }
        else
        {
            SetDiceSprite(_num);
        }
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
            var rand_time = Random.Range(0.05f, 0.11f);
            var rand_dice = Random.Range(1, 6);
            SetDiceSprite(rand_dice);
            yield return new WaitForSeconds(rand_time);
        }

        SetDiceSprite(num);
    }

    void SetOpacity()
    {
        var currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        currentColor.a = 0.35f;
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }

    void ResetOpacity()
    {
        var currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        currentColor.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }

    public void RollTheDice()
    {
        DiceButton.SetActive(false);
        ResetOpacity();
        StartCoroutine(AnimateDice(num));
        SetDiceSprite(num);
    }

    public void RollTheDice(int _num, string _turn_id)
    {
        if (_turn_id != turn_id)
        {
            DiceButton.SetActive(false);
            ResetOpacity();
            StartCoroutine(AnimateDice(_num));
            SetDiceSprite(_num);
            turn_id = _turn_id;
        }
        else
        {
            SetDiceSprite(_num);

        }
    }
}