using System.Collections;
using Actors.Components.Attributes;
using Reuse.InputManagement.InputManagerIndependent;
using UnityEngine;

namespace Actors.Actions
{
        [CreateAssetMenu(menuName = "Monster/Game-Design/Actions/Player Movement Action")]
    public class PlayerMovementAction : ActorAction
    {
        private const string HorizontalMovement = "Horizontal";
        private const string VerticalMovement = "Vertical";

        [SerializeField] private float speed = 5;
        
        public override IEnumerator ProcessAction(Actor actor)
        {
            var horizontal = InputManager.GetAxis(HorizontalMovement);
            var vertical = InputManager.GetAxis(VerticalMovement);
            var dir = new Vector3(horizontal, 0, vertical);

            if (horizontal == 0 && vertical == 0)
            {
                yield break;
            }

            actor.Agent.Move(dir.normalized * (GetSpeed(actor) * Time.deltaTime));

            yield return null;
        }

        private float GetSpeed(Actor actor)
        {
            return speed * actor.GetAttributeValue(Attribute.Speed);
        }
    }
}
