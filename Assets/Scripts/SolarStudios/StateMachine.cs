using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SolarStudios
{

    public interface IState //Make the states inherit from this. Basically will force that script to have these functions. If it dont it dont work.
    {
        void Enter(StateMachine stateMachine);
        void Run();
        void Exit();
        
    }

    public class StateMachine : MonoBehaviour //Dont touch this script.
    {
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public IState currentState; //DONT TOUCH 

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            SetState(gameObject.GetComponent<StateMachineState>()); //This is how you change state.
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Run();
            }
            else
            {
                SetState(gameObject.GetComponent<StateMachineState>());
            }

        }

        public void SetState(IState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter(this);
        }
    }
}
