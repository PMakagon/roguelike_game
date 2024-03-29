﻿using LiftGame.FPSController.ScriptableObjects;
using LiftGame.GameCore.Input;
using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.FPSController.CameraController
{
    public class CameraController : MonoBehaviour, IPauseable
    {
        #region Variables

        #region Data

        [Space, Header("Data")] [SerializeField]
        private CameraInputData camInputData = null;

        [Space, Header("Custom Classes")] [SerializeField]
        private CameraZoom cameraZoom = null;

        [SerializeField] private CameraSwaying cameraSway = null;

        #endregion

        #region Settings

        [Space, Header("Look Settings")] [SerializeField]
        private Vector2 sensitivity = Vector2.zero;

        [SerializeField] private Vector2 smoothAmount = Vector2.zero;

        [SerializeField] [MinMaxSlider(-90f, 90f)]
        private Vector2 lookAngleMinMax = Vector2.zero;

        #endregion

        #region Private

        private float m_yaw;
        private float m_pitch;

        private float m_desiredYaw;
        private float m_desiredPitch;

        private bool _isCameraLocked;

        #region Components

        private Transform m_pitchTranform;
        private Camera m_cam;

        #endregion

        #endregion

        #endregion

        #region BuiltIn Methods

        void Awake()
        {
            GetComponents();
            InitValues();
            InitComponents();
            ChangeCursorState();
        }

        void LateUpdate()
        {
            if (_isCameraLocked)
            {
                ApplyRotation();
                return;
            }
            CalculateRotation();
            SmoothRotation();
            ApplyRotation();
            HandleZoom();
        }

        #endregion

        #region Custom Methods
        void GetComponents()
        {
            m_pitchTranform = transform.GetChild(0).transform;
            m_cam = GetComponentInChildren<Camera>();
        }

        void InitValues()
        {
            m_yaw = transform.eulerAngles.y;
            m_desiredYaw = m_yaw;
        }

        void InitComponents()
        {
            cameraZoom.Init(m_cam, camInputData);
            cameraSway.Init(m_cam.transform);
        }

        void CalculateRotation()
        {
            m_desiredYaw += camInputData.InputVector.x * sensitivity.x * Time.deltaTime;
            m_desiredPitch -= camInputData.InputVector.y * sensitivity.y * Time.deltaTime;

            m_desiredPitch = Mathf.Clamp(m_desiredPitch, lookAngleMinMax.x, lookAngleMinMax.y);
        }

        void SmoothRotation()
        {
            m_yaw = Mathf.Lerp(m_yaw, m_desiredYaw, smoothAmount.x * Time.deltaTime);
            m_pitch = Mathf.Lerp(m_pitch, m_desiredPitch, smoothAmount.y * Time.deltaTime);
        }

        void ApplyRotation()
        {
            transform.eulerAngles = new Vector3(0f, m_yaw, 0f);
            m_pitchTranform.localEulerAngles = new Vector3(m_pitch, 0f, 0f);
        }

        public void HandleSway(Vector3 _inputVector, float _rawXInput)
        {
            cameraSway.SwayPlayer(_inputVector, _rawXInput);
        }

        void HandleZoom()
        {
            if (camInputData.ZoomClicked || camInputData.ZoomReleased)
                cameraZoom.ChangeFOV(this);
        }

        public void ChangeRunFOV(bool _returning)
        {
            cameraZoom.ChangeRunFOV(_returning, this);
        }

        void ChangeCursorState()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void LockCursor(bool isLocked)
        {
            if (isLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void LockCameraPosition(bool state)
        {
            _isCameraLocked = state;
        }
        
        private void LockCameraPosition()
        {
            _isCameraLocked = true;
        }

        private void UnlockCameraPosition()
        {
            _isCameraLocked = false;
        }

        #endregion

        public void SetPaused(bool isPaused)
        {
            LockCursor(!isPaused);
        }

        public Camera PlayerCamera => m_cam;

        public Vector2 Sensitivity
        {
            get => sensitivity;
            set => sensitivity = value;
        }
    }
}