using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nicks : MonoBehaviour
{
    [SerializeField] private GameObject RedNick;
    [SerializeField] private GameObject BlueNick;
    [SerializeField] private GameObject GreenNick;
    [SerializeField] private GameObject YellowNick;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateNicks();
    }

    public void DeactivateNicks()
    {
        RedNick.SetActive(false);
        BlueNick.SetActive(false);
        GreenNick.SetActive(false);
        YellowNick.SetActive(false);
    }

    public void ActivateNicks(Dictionary<string, string> nicks)
    {
        DeactivateNicks();
        if (nicks.TryGetValue("Red", out var player))
        {
            Debug.Log("n  "+ player);
            RedNick.GetComponent<TMPro.TextMeshProUGUI>().text = player;
            RedNick.SetActive(true);
        }

        if (nicks.TryGetValue("Blue", out var top))
        {
            Debug.Log("n  "+ top);

            BlueNick.GetComponent<TMPro.TextMeshProUGUI>().text = top;
            BlueNick.SetActive(true);

        }

        if (nicks.TryGetValue("Green", out var left))
        {
            Debug.Log("n  "+ left);

            GreenNick.GetComponent<TMPro.TextMeshProUGUI>().text = left;
            GreenNick.SetActive(true);

        }

        if (nicks.TryGetValue("Yellow", out var right))
        {
            Debug.Log("n  "+ right);

            YellowNick.GetComponent<TMPro.TextMeshProUGUI>().text = right;
            YellowNick.SetActive(true);

        }
    }
}