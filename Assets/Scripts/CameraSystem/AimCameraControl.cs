﻿using System;
using Cinemachine;
using Golf;
using TMPro;
using UnityEngine;

namespace CameraSystem
{
	public class AimCameraControl : MonoBehaviour, AxisState.IInputAxisProvider
	{
		//[SerializeField] InputReader _inputReader;
		[SerializeField] private ActiveGolfConfiguration _caddy;
		private CinemachinePOV _pov;
		private CinemachineCameraOffset _cameraOffset;
		
		private void Awake()
		{ 
			var virtualCamera = GetComponent<CinemachineVirtualCamera>();
			_pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
			_cameraOffset = virtualCamera.GetComponent<CinemachineCameraOffset>();
		}

		private void Start()
		{
			_pov.VirtualCamera.Follow = _caddy.CurrentPlayer.transform;
			_pov.VirtualCamera.LookAt = _caddy.CurrentPlayer.transform;
		}

		protected void LateUpdate()
		{
			// if (_caddy.CurrentPlayer.transform.position != null)
			// {
			// 	transform.position = _caddy.CurrentPlayer.transform.position;
			// }
			// transform.Rotate(Vector3.up,_inputReader.Look.x); 

			var aim = _caddy.CurrentStroke.AimDir;
			_pov.m_HorizontalAxis.Value = Quaternion.LookRotation(aim).eulerAngles.y;

			var height = _cameraOffset.m_Offset.z;
			//_caddy.CurrentStroke.inputPower;
			height = 3;//club.minHeight, club.maxHeight, inputPower.

			//idk we'll figure out this later.
			float pullback = Mathf.Clamp(_caddy.CurrentStroke.inputPower * _caddy.SelectedClub.power / 4, 1, 10);
			
			_cameraOffset.m_Offset = new Vector3(_cameraOffset.m_Offset.x, height, -pullback);
		}

		
		
		//stops cinemachine from complaining about not having an input provider.
		//Axis index ranges from 0...2 for X, Y, and Z.
		public float GetAxisValue(int axis)
		{
			return 0;
		}
	}
}