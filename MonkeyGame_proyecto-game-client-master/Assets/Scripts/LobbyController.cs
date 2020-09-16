using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public static LobbyController _instance = null;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


    public int MAX_PLAYERS = 4;

    Network networkController;
    List<Player> players = new List<Player>();
    public List<Player> Players { get => players; set => players = value; }

    Player[] playersDemo = { 

        new Player(TeamEnum.Blue, RoleEnum.Monkey),
        new Player(TeamEnum.Red, RoleEnum.Monkey),
        new Player(TeamEnum.Blue, RoleEnum.CocoShooter),
        new Player(TeamEnum.Red, RoleEnum.CocoShooter)
        
        
    };

    [SerializeField] Button startBtn;
    


    // Start is called before the first frame update
    void Start()
    {
        networkController = GameObject.Find("SocketIO").GetComponent<Network>();
        networkController.CreateRoom();

        networkController.onRoomCreated += SetRoomCode;

        networkController.onPlayerEnter += OnPlayerEnter;
        networkController.onPlayerReady += OnPlayerReady;
        startBtn.interactable = false;

    }

   
    void SetRoomCode(string roomCode)
    {
        InputField txtRoomCode = GameObject.Find("txtRoomCode").GetComponent<InputField>();
        txtRoomCode.text = roomCode;
    }


    void OnPlayerEnter(string playerId)
    {        
        if (players.Count <= this.MAX_PLAYERS)
        {
            Player player = new Player(playerId, players.Count);
            players.Add(player);
            GameObject.Find("SlotPlayer" + players.Count).GetComponentInChildren<Text>().text = player.Nickname+" Conectado";            
            AssignTeamAndRole();           
            
        }

    }

    void OnPlayerReady(string playerId)
    {
        var player = players.Find(p => p.Id == playerId);
        player.Ready = true;
        Debug.Log("Player Ready: " + player.Id + "Name : " + player.Nickname);
        int index = players.IndexOf(player)+1;
        Debug.Log(player.Nickname + "Ready..... Slot" + index);
        GameObject.Find("SlotPlayer" + index).GetComponentInChildren<Text>().text = player.Nickname+ " listo";

        if (!players.Exists(p => !p.Ready) && players.Count == MAX_PLAYERS) //Si no existe alguien que no esté listo y....
        {
            startBtn.interactable = true;
            startBtn.onClick.AddListener(GameReady);
        }
            //this.gameObject.SendMessage("LoadScene", 2, SendMessageOptions.RequireReceiver);  
    }
    void GameReady()
    {
        networkController.GameReady();
        this.gameObject.SendMessage("LoadScene", 2, SendMessageOptions.RequireReceiver);
    }

    void AssignTeamAndRole()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].Team == TeamEnum.None && players[i].Role == RoleEnum.None)
            {
                players[i].Team = playersDemo[i].Team;
                players[i].Role = playersDemo[i].Role;
                networkController.SendCharacter(players[i].Team, players[i].Role, players[i].Id, players[i].Nickname);
            }
        }
        
    }

}

public class Player
{
    public string Id { get; set; }
    public bool Ready { get; set; }
    public string Nickname { get; set; }
    public TeamEnum Team { get; set; }
    public RoleEnum Role { get; set; }    
   

    public Player(string id, int number)
    {
        this.Id = id;
        this.Nickname = "Jugador " + (number + 1);
        Team = TeamEnum.None;
        Role = RoleEnum.None;
    }    

    public Player (TeamEnum _team, RoleEnum _Role)
    {
        Team = _team;
        Role = _Role;
    }
}
