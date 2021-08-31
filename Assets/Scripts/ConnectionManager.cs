using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using UnityEngine;
using BestHTTP;
using BestHTTP.Extensions;
using BestHTTP.WebSocket;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Item
{
    public class GameData
    {
        public class CounterConfig
        {
            public List<int> Red;
            public List<int> Blue;
            public List<int> Green;
            public List<int> Yellow;
        }

        public class IdleConfig
        {
            public int Red;
            public int Blue;
            public int Green;
            public int Yellow;
        }

        public CounterConfig regular;
        public CounterConfig finnish;
        public IdleConfig idle;
    }


    [JsonProperty("is_game_on")] public bool is_game_on { get; set; }
    public string whos_turn { get; set; }
    public int dice { get; set; }
    
    
    [JsonProperty("my_color")]public string myColor { get; set; }
    public GameData game_data { get; set; }
    public Dictionary<string, string> nicks { get; set; }
}


public struct Config
{
    // do not change variables names names
    public string player_id;
    public string room_id;
    public string server_address;
}

public class ConnectionManager : MonoBehaviour
{
    private static WebSocket webSocket;
    [SerializeField] private bool isMyTurn;
    [SerializeField] private Config config;
    private GameObject[] disableUIs;
    private Nicks nicks;
    private SetCounters setCounters;
//    private int? drawnNumber = null;
    private Arrows arrows;
    private Dice dice;


    private const float connectTimeout = 3;
    private float timeFromLastConnectionRequest = connectTimeout;

    void Start()
    {
        config = new Config();
        setCounters = GameObject.Find("Counters").GetComponent<SetCounters>();
        nicks = GameObject.Find("Nicks").GetComponent<Nicks>();
        arrows = GameObject.Find("Arrows").GetComponent<Arrows>();
        dice = GameObject.Find("Dice").GetComponent<Dice>();

//        disableUIs = GameObject.FindGameObjectsWithTag("DisableUI");
//        foreach (GameObject go in disableUIs)
//        {
//            go.SetActive(false);
//        }
        config = new Config {player_id = "2", room_id = "1", server_address = "ws://localhost:5000/ws/"}; // todo
    }

    private void Update()
    {
        if ((string.IsNullOrEmpty(config.player_id) || webSocket != null) &&
            (string.IsNullOrEmpty(config.player_id) || webSocket.IsOpen)) return;
        if (timeFromLastConnectionRequest >= connectTimeout)
        {
            Debug.Log("Opening connection!");
            webSocket = ConnectToServer(config);

            timeFromLastConnectionRequest = 0;
        }
        else
        {
            Debug.Log("No Connection!!!");
            ClearDesk();
            timeFromLastConnectionRequest += Time.deltaTime;
        }
    }

    private void ClearDesk()
    {
        nicks.DeactivateNicks();
        setCounters.ResetCounters();
    }

    private WebSocket ConnectToServer(Config config)
    {
        string fullAddress = Path.Combine(config.server_address + config.room_id + "/" + config.player_id);
        Debug.Log("full_path: " + fullAddress);

        webSocket = new WebSocket(new Uri(fullAddress));
        webSocket.OnMessage += OnMessageRecieved;
        webSocket.Open();

        return webSocket;
    }

    private void OnMessageRecieved(WebSocket webSocket, string message)
    {
        Debug.Log(message);
        Item item = JsonConvert.DeserializeObject<Item>(message);
        ClearDesk();
        nicks.ActivateNicks(item.nicks);
        
        if (!item.is_game_on) return;
        isMyTurn = item.whos_turn == item.myColor;
        setCounters.SeTCounters(item.game_data);
        arrows.ActivateArrow(item.whos_turn);
        dice.SetDice(item.dice);
    }

    static void SendUpdateToServer(Dictionary<string, dynamic> dictToSend)
    {
        string dictAsStr = JsonConvert.SerializeObject(dictToSend);
        Debug.Log("sending update to server ");

        webSocket.Send(dictAsStr);
    }

    public void ConfigFromJson(string json)
    {
        config = JsonUtility.FromJson<Config>(json);
    }

    public void SkipMove()
    {
        string stringToSend = "{\"other\": \"skip\"}";
        Debug.Log("sending update to server: skip");

        webSocket.Send(stringToSend);
    }
//    public void DrawNumber()
//    {
//        if (drawnNumber == null)
//        {
//            drawnNumber = Random.Range(1, 7);
//            Debug.Log(drawnNumber);
//        }
//    }

    public void CounterClick(bool isFinnish, bool isIdle, Field.FieldColor fieldColor, int number = 0)
    {
        if (isMyTurn)
        {
            var dictToSend = new Dictionary<string, dynamic>
            {
                ["isFinnish"] = isFinnish,
                ["isIdle"] = isIdle,
                ["number"] = number,
                ["fieldColor"] = fieldColor.ToString()
            };
            SendUpdateToServer(dictToSend);
        }
        else
        {
            Debug.Log("It's not your turn!");
        }
    }
}