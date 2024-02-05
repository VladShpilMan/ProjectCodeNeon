using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace __ProjectCodeNeon.Entities
{
    public class JumpingState : State
    {
        private bool grounded;
        private int jumpParam = Animator.StringToHash("Jump");
        private int landParam = Animator.StringToHash("Land");

        public JumpingState(CharacterGameController character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //SoundManager.Instance.PlaySound(SoundManager.Instance.jumpSounds);
            grounded = false;
            Jump();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (grounded)
            {
                //character.TriggerAnimation(landParam);
                //SoundManager.Instance.PlaySound(SoundManager.Instance.landing);
            }
                stateMachine.ChangeState(character.standing);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //grounded = character.CheckCollisionOverlap(character.transform.position);
        }

        private void Jump()
        {
            character.transform.Translate(Vector3.up * (character.CollisionOverlapRadius + 0.1f));
            character.ApplyImpulse(Vector3.up * character.JumpForce);
            //character.TriggerAnimation(jumpParam);
        }
    }
}