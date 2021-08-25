using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetCounters : MonoBehaviour
{
    [SerializeField] private GameObject counterPrefab;
    private List<GameObject> fields;

    void Start()
    {
        fields = new List<GameObject>(GameObject.FindGameObjectsWithTag("field"));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetCounters()
    {
        Debug.Log("ResetCounters!");
    }

    public void SeTCounters(Item.GameData gameData)
    {
        var gameDataRegular = gameData.regular;

        if (gameDataRegular.Blue != null && gameDataRegular.Blue.Count != 0)
        {
            foreach (var counter in gameDataRegular.Blue)
            {
                var field = fields.Find(f => f.GetComponent<Field>().number == counter && f.GetComponent<Field>().fieldColor == Field.FieldColor.Regular);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorBlue();
            }
        }

        if (gameDataRegular.Red != null && gameDataRegular.Red.Count != 0)
        {
            int RedOffset = 30;
            foreach (var counter in gameDataRegular.Red)
            {
                int fieldNumber = CountFieldNumber(counter, RedOffset);
                var field = fields.Find(f => f.GetComponent<Field>().number == fieldNumber && f.GetComponent<Field>().fieldColor == Field.FieldColor.Regular);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorRed();
            }
        }

        if (gameDataRegular.Green != null && gameDataRegular.Green.Count != 0)
        {
            int GreenOffset = 20;

            foreach (var counter in gameDataRegular.Green)
            {
                int fieldNumber = CountFieldNumber(counter, GreenOffset);

//                Debug.Log(fieldNumber);
                var field = fields.Find(f => f.GetComponent<Field>().number == fieldNumber && f.GetComponent<Field>().fieldColor == Field.FieldColor.Regular);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorGreen();
            }
        }

        if (gameDataRegular.Yellow != null && gameDataRegular.Yellow.Count != 0)
        {
            int YellowOffset = 10;

            foreach (var counter in gameDataRegular.Yellow)
            {
//                Debug.Log(counter + YellowOffset);
                int fieldNumber = CountFieldNumber(counter, YellowOffset);

                var field = fields.Find(f => f.GetComponent<Field>().number == fieldNumber && f.GetComponent<Field>().fieldColor == Field.FieldColor.Regular);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorYellow();
            }
        }
        
        
        var gameDataFinnish = gameData.finnish;
        if (gameDataFinnish.Blue != null && gameDataFinnish.Blue.Count != 0)
        {
            foreach (var counter in gameDataFinnish.Blue)
            {
                var field = fields.Find(f => f.GetComponent<Field>().number == counter && f.GetComponent<Field>().fieldColor == Field.FieldColor.Blue && f.GetComponent<Field>().isFinnish);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorBlue();
            }
        }
        
        if (gameDataFinnish.Red != null && gameDataFinnish.Red.Count != 0)
        {
            foreach (var counter in gameDataFinnish.Red)
            {
                var field = fields.Find(f => f.GetComponent<Field>().number == counter && f.GetComponent<Field>().fieldColor == Field.FieldColor.Red && f.GetComponent<Field>().isFinnish);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorRed();
            }
        }
        
        if (gameDataFinnish.Green != null && gameDataFinnish.Green.Count != 0)
        {
            foreach (var counter in gameDataFinnish.Green)
            {
                var field = fields.Find(f => f.GetComponent<Field>().number == counter && f.GetComponent<Field>().fieldColor == Field.FieldColor.Green && f.GetComponent<Field>().isFinnish);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorGreen();
            }
        }
        
        if (gameDataFinnish.Yellow != null && gameDataFinnish.Yellow.Count != 0)
        {
            foreach (var counter in gameDataFinnish.Red)
            {
                var field = fields.Find(f => f.GetComponent<Field>().number == counter && f.GetComponent<Field>().fieldColor == Field.FieldColor.Yellow && f.GetComponent<Field>().isFinnish);
                var newCounter = Instantiate(counterPrefab, field.transform);
                newCounter.transform.SetParent(gameObject.transform);
                newCounter.GetComponent<Counter>().SetColorYellow();
            }
        }

        var gameDataIdle = gameData.idle;
        
        
        for (int i = 1; i <= gameDataIdle.Blue; i++)
        {
            var field = fields.Find(f => f.GetComponent<Field>().number == i && f.GetComponent<Field>().fieldColor == Field.FieldColor.Blue && f.GetComponent<Field>().isIdle);
            var newCounter = Instantiate(counterPrefab, field.transform);
            newCounter.transform.SetParent(gameObject.transform);
            newCounter.GetComponent<Counter>().SetColorBlue();
        }
        
        for (int i = 1; i <= gameDataIdle.Red; i++)
        {
            var field = fields.Find(f => f.GetComponent<Field>().number == i && f.GetComponent<Field>().fieldColor == Field.FieldColor.Red && f.GetComponent<Field>().isIdle);
            var newCounter = Instantiate(counterPrefab, field.transform);
            newCounter.transform.SetParent(gameObject.transform);
            newCounter.GetComponent<Counter>().SetColorRed();
        }
        
        for (int i = 1; i <= gameDataIdle.Green; i++)
        {
            var field = fields.Find(f => f.GetComponent<Field>().number == i && f.GetComponent<Field>().fieldColor == Field.FieldColor.Green && f.GetComponent<Field>().isIdle);
            var newCounter = Instantiate(counterPrefab, field.transform);
            newCounter.transform.SetParent(gameObject.transform);
            newCounter.GetComponent<Counter>().SetColorGreen();
        }
        for (int i = 1; i <= gameDataIdle.Yellow; i++)
        {
            var field = fields.Find(f => f.GetComponent<Field>().number == i && f.GetComponent<Field>().fieldColor == Field.FieldColor.Yellow && f.GetComponent<Field>().isIdle);
            var newCounter = Instantiate(counterPrefab, field.transform);
            newCounter.transform.SetParent(gameObject.transform);
            newCounter.GetComponent<Counter>().SetColorYellow();
        }

    }

    private int CountFieldNumber(int counter, int offset)
    {
        const int maxfieldnumber = 40;
        int sum = counter + offset;
        if (sum > maxfieldnumber)
        {
            sum = sum % maxfieldnumber;
        }

        return sum;
    }
}