using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using CarSumo;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Selector;
using UnityEngine;
using UnityEngine.UI;

public static class Create
{
    private const string AssetPath = "Explosion Cue Test";

    public static VehicleCollection VehicleCollection()
    {
        return new VehicleCollection();
    }

    public static IVehicle.FakeVehicleMono FakeVehicleMono(Team team = Team.First)
    {
        return new GameObject().AddComponent<IVehicle.FakeVehicleMono>().Init(team);
    }
    
    public static VerticalLayoutGroup VerticalLayoutGroup()
    {
        return new GameObject("VerticalLayoutSpacingTweenTests").AddComponent<VerticalLayoutGroup>();
    }
    
    public static AudioCue AudioCue()
    {
        return Resources.Load<AudioCue>(AssetPath);
    }
    
    public static MonoSoundEmitter MonoSoundEmitter()
    {
        return new GameObject("Sound Emitter").AddComponent<MonoSoundEmitter>();
    }
    
    public static RectTransform RectTransform()
    {
        return new GameObject("Rect transform").AddComponent<RectTransform>();
    }

    public static Range Range()
    {
        return new Range(-50, 50);
    }

    public static Range<Vector2> Vector2Range()
    {
        return new Range<Vector2>(Vector2.one * -50, Vector2.one * 50);
    }
    
    public static Image Image()
    {
        return new GameObject("Image").AddComponent<Image>();
    }

}