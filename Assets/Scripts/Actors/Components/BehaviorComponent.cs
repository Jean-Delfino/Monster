using System.Collections;
using UnityEngine;

namespace Actors.Components
{
    [CreateAssetMenu(menuName = "Monster/Game-Design/Components/Behaviour Component")]

    public class BehaviorComponent : Component
    {
        
        public virtual IEnumerator ExecuteComponent(Actor actor)
        {
            while (actor.enabled)
            {
                //Place brain logic
            }

            yield return null;
        }
    }
}