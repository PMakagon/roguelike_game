using System;
using System.Collections.Generic;
using PlayerPowerSystem;
using UnityEngine;

namespace PlayerEquipment
{
    public class Scanner : MonoBehaviour,IPowerEquipment
    {
        [SerializeField] private float power = 3;
        [SerializeField] private Transform target;
        [SerializeField] private Light[] leds = new Light[36];
        [SerializeField] private int scanFrequency;
        [SerializeField] private Transform arrowTransform;
        // [SerializeField] private Quaternion targetDirectionQuaternion;

        private Animator _animator;
        
        
        private Vector3 origin;
        private Vector3 dest;
        private Vector3 directionToTarget;

        private float distance;
        private float maxLedIntensity;
        private float sideLedIntensity;

        private float scanInterval;
        private float scanTimer;

        public Transform EquipmentTransform 
        {
            get => gameObject.transform;
        }

        public Animator EquipmentAnimator
        {
            get => _animator;
            set => _animator = value;
        }

        public bool IsEquipped { get; set; }
        
        public PlayerPowerData PlayerPowerData { get; set; }
        public bool IsTurnedOn { get; set; }

        private void Awake()
        {
            origin = transform.position;
            dest = target.position;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            scanInterval = 1f / scanFrequency;
        }

        private void Update()
        {
            // scanTimer -= Time.deltaTime;
            // if (scanTimer<0)
            // {
            //     scanTimer += scanInterval;
            //     Scan();
            // }
            
        }


        private void Scan()
        {
            origin = transform.position;
            dest = target.position;
            directionToTarget = dest - origin;
            // arrowTransform.rotation = Quaternion.LookRotation(directionToTarget,Vector3.up);
            // arrowTransform.rotation = Quaternion.LookRotation(directionToTarget,origin);
            // arrowTransform.rotation = Quaternion.Euler(0,0,0);
            // arrowTransform.rotation = Quaternion.Euler(directionToTarget);
            // arrowTransform.rotation = Quaternion.AngleAxis(45f,directionToTarget);
            
            
            Debug.Log(directionToTarget.x);
            Debug.DrawRay(origin,directionToTarget,Color.yellow);
        }

        public void Use()
        {
            _animator.SetBool("Use",true);
            Debug.Log("SCANER");
        }

        public void Equip()
        {
            IsEquipped = true;
            _animator.SetBool("Equip",true);
        }

        public void UnEquip()
        {
            IsEquipped = false;
            _animator.SetBool("UnEquip",true);
        }

        public void TurnOn()
        {
            IsTurnedOn = true;
            _animator.SetBool("TurnOn",true);
            PlayerPowerData.PowerLoad += power;
            Debug.Log("ON");
        }

        public void TurnOff()
        {
            IsTurnedOn = false;
            _animator.SetBool("TurnOff",true);
            PlayerPowerData.PowerLoad -= power;
            Debug.Log("OFF");
        }
    }
}