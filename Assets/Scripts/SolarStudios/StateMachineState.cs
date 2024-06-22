using UnityEngine;

namespace SolarStudios
{
    public class StateMachineState : MonoBehaviour, IState
    {
        private StateMachine stateMachine;
         
        public void Enter(StateMachine stateMachine) //Runs when we enter the state
        {
            this.stateMachine = stateMachine;
            //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
        public void Run() //Runs every frame
        {

        }
        public void Exit() //Runs when we exit
        {

        }
    }

}
