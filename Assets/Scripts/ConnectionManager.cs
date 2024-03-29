﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BestHTTP.WebSocket;
using Newtonsoft.Json;

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
    public string turn_id { get; set; }
    public int dice { get; set; }
    
    
    [JsonProperty("my_color")]public string myColor { get; set; }
    public GameData game_data { get; set; }
    public Dictionary<string, string> nicks { get; set; }
    
    public DateTime timestamp { get; set; }

}


public class Config
{
    // do not change variables names names
    public string player_id;
    public string room_id;
    public string server_address;
    public string player_nick;
}

public class ConnectionManager : MonoBehaviour
{
    private static WebSocket webSocket;
    private bool isMyTurn;
    private Config config;

    [SerializeField] GameObject waitingText;
    private GameObject[] disableUIs;
    private Nicks nicks;
    private SetCounters setCounters;
    private Arrows arrows;
    private Dice dice;
    private Timer timer;


    private const float connectTimeout = 3;
    private float timeFromLastConnectionRequest = connectTimeout;

    void Start()
    {
        setCounters = GameObject.Find("Counters").GetComponent<SetCounters>();
        nicks = GameObject.Find("Nicks").GetComponent<Nicks>();
        arrows = GameObject.Find("Arrows").GetComponent<Arrows>();
        dice = GameObject.Find("Dice").GetComponent<Dice>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();

//        config = new Config {player_id = "1", room_id = "1", server_address = "ws://localhost:5000/test/", player_nick="player"}; // todo
    }

    private void Update()
    {
        if ((string.IsNullOrEmpty(config.player_id) || webSocket != null) &&
            (string.IsNullOrEmpty(config.player_id) || webSocket.IsOpen)) return;
        if (timeFromLastConnectionRequest >= connectTimeout)
        {
            webSocket = ConnectToServer(config);
            timeFromLastConnectionRequest = 0;
        }
        else
        {
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
        string fullAddress = Path.Combine(config.server_address + config.room_id + "/" 
                                          + config.player_id + "/" + config.player_nick);

        webSocket = new WebSocket(new Uri(fullAddress));
        webSocket.OnMessage += OnMessageRecieved;
        webSocket.Open();

        return webSocket;
    }

    private void OnMessageRecieved(WebSocket webSocket, string message)
    {
        Item item = JsonConvert.DeserializeObject<Item>(message);
        ClearDesk();
        nicks.ActivateNicks(item.nicks);

        if (!item.is_game_on)
        {
            waitingText.SetActive(true);
            return;
        }
        waitingText.SetActive(false);

        isMyTurn = item.whos_turn == item.myColor;
        setCounters.SeTCounters(item.game_data);
        arrows.ActivateArrow(item.whos_turn);
        timer.SetTimer(item.timestamp);

        if (isMyTurn)
        {
            dice.SetDice(item.dice, item.turn_id);
        }
        else
        {
            dice.RollTheDice(item.dice, item.turn_id);

        }
//        Debug.Log(message);
    }

    static void SendUpdateToServer(Dictionary<string, dynamic> dictToSend)
    {
        string dictAsStr = JsonConvert.SerializeObject(dictToSend);
        webSocket.Send(dictAsStr);
    }

    public void ConfigFromJson(string json)
    {
        if (config == null)
            config = JsonUtility.FromJson<Config>(json);
    }

    public void SkipMove()
    {
        webSocket.Send("{\"other\": \"skip\"}");
    }

    public void CounterClick(bool isFinnish, bool isIdle, Field.FieldColor fieldColor, int number = 0)
    {
        if (!isMyTurn) return;
        var dictToSend = new Dictionary<string, dynamic>
        {
            ["isFinnish"] = isFinnish,
            ["isIdle"] = isIdle,
            ["number"] = number,
            ["fieldColor"] = fieldColor.ToString()
        };
        SendUpdateToServer(dictToSend);

    }
}