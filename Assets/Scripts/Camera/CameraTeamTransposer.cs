using System.Collections.Generic;
using CarSumo.Teams;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CarSumo.Cameras
{
    public class CameraTeamTransposer : SerializedMonoBehaviour
    {
        [SerializeField] private IReactiveTeamChangeHandler _changeHandler;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private IDictionary<Team, float> _teamCameraPositions;
        
        private IPreviousTeamDefiner _previousTeamDefiner;
        private CinemachineOrbitalTransposer _transposer;

        private int _times = 0;

        [Inject]
        private void Construct(IPreviousTeamDefiner previousTeamDefiner)
        {
            _previousTeamDefiner = previousTeamDefiner;
        }

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
            if (_times > 0)
            {
                var previousTeam = DeterminePreviousTeam(team);
                _teamCameraPositions[previousTeam] = _transposer.m_XAxis.Value;
            }

            _times++;
            _transposer.m_XAxis.Value = _teamCameraPositions[team];
        }

        private Team DeterminePreviousTeam(Team currentTeam)
        {
            return _previousTeamDefiner.DefineTeam(currentTeam);
        }
    }
}