using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Commands {
    NONE,
    LEFT,
    RIGHT
}

public class GameController : MonoBehaviour
{

    Network networkController;        
    
    List<Player> playersInGame = new List<Player>();
    GameAgent[] gameAgents; 
    
    private void Awake()
    { 
        for (int i = 0; i < LobbyController._instance.Players.Count; i++)
        {
            playersInGame.Add(LobbyController._instance.Players[i]);
        }       
    }
    void Start()
    {        
        networkController = GameObject.Find("SocketIO").GetComponent<Network>();
        networkController.onPlayerInput+= OnPlayerInput;

        gameAgents = FindObjectsOfType<GameAgent>();         


        foreach (var agent in gameAgents)
        {            
            for (int i = 0; i < playersInGame.Count; i++)
            {
                if(agent.m_player == null && agent.m_rol == playersInGame[i].Role && agent.m_team == playersInGame[i].Team)
                {
                    agent.IdentifyMyPlayer(playersInGame[i]);
                }                
            }
        }        
    }

    void OnPlayerInput(string playerId, string command)
    {
        proccessCommand(playerId,command);
    }

    void proccessCommand(string playerId,string command)
    {

        Commands playerCommand = Commands.NONE;


        if (command == ("LEFT"))
            playerCommand = Commands.LEFT;
        else if (command == "RIGHT")
            playerCommand = Commands.RIGHT;

        foreach (var agent in gameAgents)
        {
            if(agent.m_player.Id == playerId)
            {
                agent.ReceiveInput(playerId, playerCommand);
            }
        }       

    }
}
