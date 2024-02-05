using UnityEngine;

    public class Character : MonoBehaviour
    {
        #region Variables

        public StateMachine movementSM;
        public StandingState standing;
        public DuckingState ducking;
        public JumpingState jumping;

#pragma warning disable 0649
        [SerializeField]
        private Transform handTransform;
        [SerializeField]
        private Transform sheathTransform;
        [SerializeField]
        private Transform shootTransform;
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private Collider hitBox;
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private ParticleSystem shockWave;
#pragma warning restore 0649
        [SerializeField]
        private float meleeRestThreshold = 10f;
        [SerializeField]
        private float diveThreshold = 1f;
        [SerializeField]
        private float collisionOverlapRadius = 0.5f;

        private GameObject currentWeapon;
        private Quaternion currentRotation;
        private int horizonalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");
        private int shootParam = Animator.StringToHash("Shoot");
        private int hardLanding = Animator.StringToHash("HardLand");
        #endregion

        #region Properties

        public float NormalColliderHeight => 3;
        public float CrouchColliderHeight => 2.5f;
        public float DiveForce => 30;
        public float JumpForce => 10;
        public float MovementSpeed => 200;
        public float CrouchSpeed => 100;
        public float RotationSpeed => 180;
        public float CrouchRotationSpeed => 70;
        public float DiveCooldownTimer => 2.1f;
        public float CollisionOverlapRadius => collisionOverlapRadius;
        public float DiveThreshold => diveThreshold;
        public float MeleeRestThreshold => meleeRestThreshold;
        public int isMelee => Animator.StringToHash("IsMelee");
        public int crouchParam => Animator.StringToHash("Crouch");

        public float ColliderSize
        {
            get => GetComponent<CapsuleCollider>().height;

            set
            {
                GetComponent<CapsuleCollider>().height = value;
                Vector3 center = GetComponent<CapsuleCollider>().center;
                center.y = value / 2f;
                GetComponent<CapsuleCollider>().center = center;
            }
        }

        #endregion

        #region Methods

        public void Move(float speed, float rotationSpeed)
        {
            Vector3 targetVelocity = speed * transform.forward * Time.deltaTime;
            targetVelocity.y = GetComponent<Rigidbody>().velocity.y;
            GetComponent<Rigidbody>().velocity = targetVelocity;

            GetComponent<Rigidbody>().angularVelocity = rotationSpeed * Vector3.up * Time.deltaTime;


            //anim.SetFloat(horizonalMoveParam, GetComponent<Rigidbody>().angularVelocity.y);
            //anim.SetFloat(verticalMoveParam, speed * Time.deltaTime);
        }

        public void ResetMoveParams()
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //anim.SetFloat(horizonalMoveParam, 0f);
            //anim.SetFloat(verticalMoveParam, 0f);
        }

        public void ApplyImpulse(Vector3 force)
        {
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        public void SetAnimationBool(int param, bool value)
        {
            //anim.SetBool(param, value);
        }

        public void TriggerAnimation(int param)
        {
            //anim.SetTrigger(param);
        }

        public void Shoot()
        {
            //TriggerAnimation(shootParam);
            //GameObject shootable = Instantiate(data.shootableObject, shootTransform.position, shootTransform.rotation);
            //shootable.GetComponent<Rigidbody>().velocity = shootable.transform.forward * data.bulletInitialSpeed;
        }

    //public bool CheckCollisionOverlap(Vector3 point)
    //{
    //    Debug.Log(Physics.OverlapSphere(point, CollisionOverlapRadius, whatIsGround).Length > 0);
    //    return Physics.OverlapSphere(point, CollisionOverlapRadius, whatIsGround).Length > 0;
    //}

    public void Equip(GameObject weapon = null)
        {
            if (weapon != null)
            {
                currentWeapon = Instantiate(weapon, handTransform.position, handTransform.rotation, handTransform);
            }
            else
            {
                ParentCurrentWeapon(handTransform);
            }
        }

        public void DiveBomb()
        {
            TriggerAnimation(hardLanding);
            shockWave.Play();
        }

        public void SheathWeapon()
        {
            ParentCurrentWeapon(sheathTransform);
        }

        public void Unequip()
        {
            Destroy(currentWeapon);
        }

        public void ActivateHitBox()
        {
            hitBox.enabled = true;
        }

        public void DeactivateHitBox()
        {
            hitBox.enabled = false;
        }

        private void ParentCurrentWeapon(Transform parent)
        {
            if (currentWeapon.transform.parent == parent)
            {
                return;
            }

            currentWeapon.transform.SetParent(parent);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            movementSM = new StateMachine();

            standing = new StandingState(this, movementSM);
            ducking = new DuckingState(this, movementSM);
            jumping = new JumpingState(this, movementSM);

            movementSM.Initialize(standing);
        }

        private void Update()
        {
            movementSM.CurrentState.HandleInput();

            movementSM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            movementSM.CurrentState.PhysicsUpdate();
        }

        #endregion
    }
