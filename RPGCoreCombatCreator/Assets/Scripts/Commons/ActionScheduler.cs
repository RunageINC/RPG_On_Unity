using UnityEngine;

namespace RPG.Commons
{
    public class ActionScheduler : MonoBehaviour
    {
        Action currentAction;

        public void StartAction(Action action)
        {
            if (currentAction == action) return;

            if (currentAction != null)
            {
                currentAction.CancelAction();
            }

            currentAction = action;
        }
    }
}