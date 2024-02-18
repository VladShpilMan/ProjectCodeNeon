using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace __ProjectCodeNeon.Entities
{
    public class GroundedState : State
    {
        protected float speed;
        protected float rotationSpeed;

        private float horizontalInput;
        private float verticalInput;
        private bool isShoot;

        public GroundedState(CharacterGameController character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            horizontalInput = verticalInput = 0.0f;
        }

        public override void Exit()
        {
            base.Exit();
            character.ResetMoveParams();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            verticalInput = character.InputController.GetVerticalMovement();
            horizontalInput = character.InputController.GetHorizontalMovement();
            isShoot = character.InputController.IsShooting();
            if (character.InputController.NextCard()) character.ShowNextCard();
            if(character.InputController.PreviousCard()) character.ShowPreviousCard();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //base.character.anim.SetInteger("MovementState", 2);
            character.Move(verticalInput * speed, horizontalInput * rotationSpeed);

            if (isShoot) character.currentImplant.Action();
        }
    }
}