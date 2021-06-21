using UnityEngine;
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

        private bool _isInitial = true;

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
            if (_isInitial == false)
            {
                var previousTeam = DeterminePreviousTeam(team);
                _teamCameraPositions[previousTeam] = _transposer.m_XAxis.Value;
                _isInitial = false;
            }

            _transposer.m_XAxis.Value = _teamCameraPositions[team];
        }

        private Team DeterminePreviousTeam(Team currentTeam)
        {
            return currentTeam == Team.First ? Team.Second : Team.First;
        }
    }
}