using __ProjectCodeNeon.ImplantsRenderSystem;
using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectCodeNeon.Entities
{

    public class CharacterGameController : MonoBehaviour
    {
        private IInputController _inputController;
        private ImplantController _implantController;
        private ImplantsRenderer _implantsRenderer;

        #region SerializeField
        [SerializeField]
        private CharacterData data;
        [SerializeField]
        private Animator anim;
        #endregion

        #region Some
        private int horizonalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");
        private float collisionOverlapRadius = 0.5f;
        #endregion

        #region Properties
        public float NormalColliderHeight => data.normalColliderHeight;
        public float CrouchColliderHeight => data.crouchColliderHeight;
        public float DiveForce => data.diveForce;
        public float JumpForce => data.jumpForce;
        public float MovementSpeed => data.movementSpeed;
        public float CrouchSpeed => data.crouchSpeed;
        public float RotationSpeed => data.rotationSpeed;
        public float CrouchRotationSpeed => data.crouchRotationSpeed;
        public GameObject MeleeWeapon => data.meleeWeapon;
        public GameObject ShootableWeapon => data.staticShootable;
        public float DiveCooldownTimer => data.diveCooldownTimer;
        public int isMelee => Animator.StringToHash("IsMelee");
        public int crouchParam => Animator.StringToHash("Crouch");
        public float CollisionOverlapRadius => collisionOverlapRadius;
        #endregion

        #region Variables
        public StateMachine _movementSM;
        public StandingState standing;
        public DuckingState ducking;
        public JumpingState jumping;
        #endregion

        public List<Implant> ImplantsList { get; set; }

        private string _loadedImplantList = "";

        public void Awake()
        {
            _movementSM = new StateMachine();
            standing = new StandingState(this, _movementSM);
            ducking = new DuckingState(this, _movementSM);
            jumping = new JumpingState(this, _movementSM);
            _movementSM.Initialize(standing);
            //ImplantsList = _implantController.GetAllImplantsBasedOnList(_loadedImplantList);
            //_implantsRenderer = new ImplantsRenderer(ImplantsList.ImplantToIRenderableImplant());
        }

        private void Update()
        {

            _movementSM.CurrentState.HandleInput();

            _movementSM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _movementSM.CurrentState.PhysicsUpdate();
        }

        #region Methods
        public void Move(float speed, float rotationSpeed)
        {
            Vector3 targetVelocity = speed * transform.forward * Time.deltaTime;
            targetVelocity.y = GetComponent<Rigidbody>().velocity.y;
            GetComponent<Rigidbody>().velocity = targetVelocity;

            GetComponent<Rigidbody>().angularVelocity = rotationSpeed * Vector3.up * Time.deltaTime;

            if (targetVelocity.magnitude > 0.01f || GetComponent<Rigidbody>().angularVelocity.magnitude > 0.01f)
            {
                //SoundManager.Instance.PlayFootSteps(Mathf.Abs(speed));
            }

            //anim.SetFloat(horizonalMoveParam, GetComponent<Rigidbody>().angularVelocity.y);
            //anim.SetFloat(verticalMoveParam, speed * Time.deltaTime);
        }

        public void ResetMoveParams()
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //anim.SetFloat(horizonalMoveParam, 0f);
            //anim.SetFloat(verticalMoveParam, 0f);
        }

        //public bool CheckCollisionOverlap(Vector3 point)
        //{
        //    Debug.Log(Physics.OverlapSphere(point, CollisionOverlapRadius, whatIsGround).Length > 0);
        //    return Physics.OverlapSphere(point, CollisionOverlapRadius, whatIsGround).Length > 0;
        //}

        public void ApplyImpulse(Vector3 force)
        {
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
        #endregion
    }
}
