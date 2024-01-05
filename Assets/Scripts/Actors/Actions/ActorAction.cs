using System.Collections;
using UnityEngine;

namespace Actors.Actions
{
    public abstract class ActorAction : ScriptableObject
    {
        public abstract IEnumerator ProcessAction(Actor actor);
    }
}