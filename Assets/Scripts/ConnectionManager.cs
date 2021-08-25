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


    private const float connectTimeout = 3;
    private float timeFromLastConnectionRequest = connectTimeout;

    void Start()
    {
        config = new Config();
        setCounters = GameObject.Find("Counters").GetComponent<SetCounters>();
        nicks = GameObject.Find("Nicks").GetComponent<Nicks>();

//        disableUIs = GameObject.FindGameObjectsWithTag("DisableUI");
//        foreach (GameObject go in disableUIs)
//        {
//            go.SetActive(false);
//        }
        config = new Config {player_id = "1", room_id = "1", server_address = "ws://localhost:5000/test/"}; // todo
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
//        nicks.ActivateNicks(item.nicks); //todo add to pyhon test endpoint
        if (item.is_game_on)
        {
            setCounters.SeTCounters(item.game_data);
//            arrows.ActivateArrow(item.whos_turn);
        }
    }

    public static void SendUpdateToServer(List<string> pickedField)
    {
        var dictToSend = new Dictionary<string, List<string>>
        {
            ["picked_field"] = pickedField
        };

        string dictAsStr = JsonConvert.SerializeObject(dictToSend);
        Debug.Log("sending update to server ");

        webSocket.Send(dictAsStr);
    }

    public void ConfigFromJson(string json)
    {
        config = JsonUtility.FromJson<Config>(json);
    }
}