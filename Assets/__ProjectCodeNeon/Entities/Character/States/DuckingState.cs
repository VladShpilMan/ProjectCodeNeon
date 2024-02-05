using __ProjectCodeNeon.Entities;
using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace __ProjectCodeNeon.Entities
{
    public class DuckingState : GroundedState
    {
        private bool belowCeiling;
        private bool crouchHeld;

        public DuckingState(CharacterGameController character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //character.SetAnimationBool(character.crouchParam, true);
            speed = character.CrouchSpeed;
            rotationSpeed = character.CrouchRotationSpeed;
            //character.ColliderSize = character.CrouchColliderHeight;
            belowCeiling = false;
        }

        public override void Exit()
        {
            base.Exit();
            //character.SetAnimationBool(character.crouchParam, false);
            //.ColliderSize = character.NormalColliderHeight;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            crouchHeld = Input.GetButton("Fire3");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!(crouchHeld || belowCeiling))
            {
                stateMachine.ChangeState(character.standing);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //belowCeiling = character.CheckCollisionOverlap(character.transform.position +
            //    Vector3.up * character.NormalColliderHeight);
        }
    }
}