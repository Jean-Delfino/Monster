//This player is the "mountable monster" or it's controller, so, yes, he'll probably be a weird player, but several thing will be made thrown 

using System;
using Actors;
using MountableMonster;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Actor playerActor;

    private MonsterBody _playerMonsterBody;

#if UNITY_EDITOR
    //Just for any given test
    public void SetMonsterBody(MonsterBody monsterBody)
    {
        this._playerMonsterBody = monsterBody;
    }
#endif
    
    public void PrepareNavMesh()
    {
        var bounds = _playerMonsterBody.FindBodyExtends();
        
        var agent = playerActor.Agent;
        var agentTransform = agent.transform;
        var agentPos = agentTransform.position;
        
        agent.height = bounds.MaxY - bounds.MinY;
        agent.radius = (bounds.MaxX - bounds.MinX) / 2;
        
        var newX = (float)Math.Round((bounds.MinX + bounds.MaxX) / 2, 4); //Precision of "only" 4 
        
        agent.baseOffset = Mathf.Abs(bounds.MinY); 
        _playerMonsterBody.Connection.transform.localPosition = new Vector3(-newX, 0, 0);
    }
    
}