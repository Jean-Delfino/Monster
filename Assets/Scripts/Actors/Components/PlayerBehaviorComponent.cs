using System.Collections;
using System.Collections.Generic;
using Actors.Actions;
using Reuse.InputManagement.InputManagerIndependent;
using UnityEngine;


namespace Actors.Components
{
    [CreateAssetMenu(menuName = "Monster/Game-Design/Components/Player Behaviour Component")]
    public class PlayerBehaviorComponent : BehaviorComponent
    {
        //TEST FOR NOW
        [SerializeField] private PlayerMovementAction action;
        public override IEnumerator ExecuteComponent(Actor actor)
        {
            yield return new WaitUntil(()=>InputManager.Instance != null);
            while (actor.enabled)
            {
                //Place brain logic
                yield return actor.StartCoroutine(action.ProcessAction(actor));
            }

            yield return null;
        }
    }
}