using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAgent : MonoBehaviour
{
    public TeamEnum m_team;
    public RoleEnum m_rol;
    public Player m_player;
    
    //Variables que depronto sirvan
    protected string playerId; //Está serializado para ver el jugador asignado al agente, no tocar en editor

    public void IdentifyMyPlayer(Player _player)
    {
        m_player = _player;
        playerId = m_player.Id;
    }
    
    public void ReceiveInput(string _playerId, Commands _command)
    {
        if(_playerId == m_player.Id)
        {
            ChangeSide(_command);
        }        
    }

    public abstract void ChangeSide(Commands _command);
    
}
