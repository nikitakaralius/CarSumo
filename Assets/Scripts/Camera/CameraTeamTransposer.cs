﻿using UnityEngine;
using Cinemachine;
using CarSumo.Teams;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace CarSumo.Cameras
{
    public class CameraTeamTransposer : SerializedMonoBehaviour
    {
        [SerializeField] private IReactiveTeamChangeHandler _changeHandler;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private IDictionary<Team, float> _teamCameraPositions;

        private CinemachineOrbitalTransposer _transposer;

        private void Awake()
        {
            _transposer = _camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        private void OnEnable()
        {
            _changeHandler.TeamChanged += ChangeCameraPosition;
        }

        private void OnDisable()
        {
            _changeHandler.TeamChanged -= ChangeCameraPosition;
        }

        private void ChangeCameraPosition(Team team)
        {
            var previousTeam = team == Team.First ? Team.Second : Team.First;
            _teamCameraPositions[previousTeam] = _transposer.m_XAxis.Value;
            _transposer.m_XAxis.Value = _teamCameraPositions[team];
        }
    }
}