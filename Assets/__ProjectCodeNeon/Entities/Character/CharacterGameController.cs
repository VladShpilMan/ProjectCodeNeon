using __ProjectCodeNeon.ImplantsRenderSystem;
using __ProjectCodeNeon.ImplantsRenderSystem.DataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using __ProjectCodeNeon;
using UnityEditor;
using System.Linq;
using System;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace __ProjectCodeNeon.Entities
{

    public class CharacterGameController : MonoBehaviour
    {
        public static CharacterGameController Instance;

        public int maxHealth = 10;
        private int currentHealth;
        private IInputController _inputController;
        private ImplantController _implantController;
        [SerializeField]
        private ImplantsRenderer _implantsRenderer;

        private int _currentImplantIndex; 
        public List<GameObject> implant0, implant1, implant2;

        public GameObject FirePoint;

        #region SerializeField
        [SerializeField]
        private CharacterData data;
        [SerializeField]
        public Animator anim;
        [SerializeField]
        private CardsPull _cardsPull;
        #endregion

        #region Some
        private int horizonalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");
        private float collisionOverlapRadius = 0.5f;
        public float rotationSpeed = 10f;
        public CursorController cursor;
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

        public List<(Implant, int)> ActiveImplantsList { get; set; }
        public List<Implant> PassiveImplantsList { get; set; }

        public Implant currentImplant()
        {
            return ActiveImplantsList[_currentImplantIndex].Item1;
        }

        private string _loadedActiveImplantsList = "";
        private string _loadedPassiveImplantsListt = "";

        public Bullet Bullet { get; set; }

        public IInputController InputController 
        { 
            get
            {
                return _inputController;
            }
            set
            {
                _inputController = value;
            }
        }

        public void Awake()
        {
            Instance = this;
            GameManager.Instance.SetPlayer(this);
            _movementSM = new StateMachine();
            standing = new StandingState(this, _movementSM);
            ducking = new DuckingState(this, _movementSM);
            jumping = new JumpingState(this, _movementSM);
            _movementSM.Initialize(standing);
            cursor = FindObjectOfType<CursorController>();
            //ActiveImplantsList = _implantController.GetAllImplantsBasedOnList(_loadedActiveImplantsList);
            //PassiveImplantsList = _implantController.GetAllImplantsBasedOnList(_loadedPassiveImplantsListt);
            //_implantsRenderer = new ImplantsRenderer(ActiveImplantsList.ImplantToIRenderableImplant());

            ActiveImplantsList = new List<(Implant, int)>
        {
            (new PlasmaPistolImplant { Id = 0, Name = "Implant1", Description = "Description1", Damage = 5, Placement = ImplantPlacement.RightHand }, -1),
            (new ElectromagneticImplant { Id = 1, Name = "Implant2", Description = "Description2", Damage = 10, Placement = ImplantPlacement.LeftHand }, 15),
            (new FireRingImplant { Id = 2, Name = "Implant3", Description = "Description3", Damage = 15, Placement = ImplantPlacement.RightHand }, 5),
        };

            PassiveImplantsList = new List<Implant>
        {
            new Implant { Id = 1, Name = "Implant1", Description = "Description1", Damage = 10, Placement = ImplantPlacement.Head },
            new Implant { Id = 2, Name = "Implant2", Description = "Description2", Damage = 15, Placement = ImplantPlacement.Body },
            new Implant { Id = 3, Name = "Implant3", Description = "Description3", Damage = 20, Placement = ImplantPlacement.RightLeg },
            new Implant { Id = 4, Name = "Implant4", Description = "Description4", Damage = 25, Placement = ImplantPlacement.RightHand }
        };

            _currentImplantIndex = 0;
            _cardsPull.InitializePull(ActiveImplantsList, _currentImplantIndex);
            
            DrawImplants();
        }

        private void DrawImplants()
        {
            for(int i = 0; i < implant0.Count; i ++)
                implant0[i].SetActive(false);
            
            for(int i = 0; i < implant1.Count; i ++)
                implant1[i].SetActive(false);
            
            for(int i = 0; i < implant2.Count; i ++)
                implant2[i].SetActive(false);

            for (int i = 0; i < ActiveImplantsList.Count; i++)
            {
                if(ActiveImplantsList[i].Item1.Id == 0)
                    for(int a = 0; a < implant0.Count; a ++)
                        implant0[a].SetActive(true);
                
                if(ActiveImplantsList[i].Item1.Id == 1)
                    for(int a = 0; a < implant1.Count; a ++)
                        implant1[a].SetActive(true);
                
                if(ActiveImplantsList[i].Item1.Id == 2)
                    for(int a = 0; a < implant2.Count; a ++)
                        implant2[a].SetActive(true);
            }
        }

        private void Update()
        {

            _movementSM.CurrentState.HandleInput();

            _movementSM.CurrentState.LogicUpdate();

            transform.rotation = InputController.GetLook(transform, cursor.transform);
        }

        private void FixedUpdate()
        {
            _movementSM.CurrentState.PhysicsUpdate();
        }

        #region Methods

        public void Move(float speed, float rotationSpeed)
        {
            Vector3 targetVelocity = speed * transform.TransformDirection(Vector3.forward) * Time.deltaTime;
            targetVelocity.y = GetComponent<Rigidbody>().velocity.y;
            GetComponent<Rigidbody>().velocity = targetVelocity;

            GetComponent<Rigidbody>().angularVelocity = rotationSpeed * Vector3.up * Time.deltaTime;

            if (targetVelocity.magnitude > 0.01f || GetComponent<Rigidbody>().angularVelocity.magnitude > 0.01f)
            {
                //SoundManager.Instance.PlayFootSteps(Mathf.Abs(speed));
            }

            anim.SetFloat(horizonalMoveParam, GetComponent<Rigidbody>().angularVelocity.y);
            anim.SetFloat(verticalMoveParam, speed * Time.deltaTime);
        }

        public void Shoot()
        {
            if (ActiveImplantsList[_currentImplantIndex].Item2 == -1) return;
            
            var amount = ActiveImplantsList[_currentImplantIndex].Item2 - 1;
            ActiveImplantsList[_currentImplantIndex] = (ActiveImplantsList[_currentImplantIndex].Item1,amount);

            if (amount <= 0)
            {
                ActiveImplantsList.RemoveAt(_currentImplantIndex);
                if (_currentImplantIndex >= ActiveImplantsList.Count)
                    _currentImplantIndex = ActiveImplantsList.Count - 1;
            }
            
            _cardsPull.InitializePull(ActiveImplantsList, _currentImplantIndex);
            DrawImplants();
        }

        public void ShowNextCard()
        {
            if (_currentImplantIndex + 1 >= ActiveImplantsList.Count)
                _currentImplantIndex = 0;
            else 
                _currentImplantIndex++;
            CalculateDamage();
            _cardsPull.InitializePull(ActiveImplantsList, _currentImplantIndex);
            DrawImplants();
        }

        public void ShowPreviousCard()
        {
            if (_currentImplantIndex - 1 < 0)
                _currentImplantIndex = ActiveImplantsList.Count - 1;
            else
                _currentImplantIndex--;
            
            CalculateDamage();
            _cardsPull.InitializePull(ActiveImplantsList, _currentImplantIndex);
            DrawImplants();
        }

        private void CalculateDamage()
        {
            data.damage = CombatController.CalculateDamage(currentImplant(), PassiveImplantsList);
        }

        public void ResetMoveParams()
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            anim.SetFloat(horizonalMoveParam, 0f);
            anim.SetFloat(verticalMoveParam, 0f);
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

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("die");
            GameManager.Instance.RemoveEnemy();
            Destroy(gameObject);
        }
        #endregion
    }
}
