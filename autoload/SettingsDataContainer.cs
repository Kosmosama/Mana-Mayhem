using Godot;
using Godot.Collections;
using System;

public partial class SettingsDataContainer : Node
{

    public static SettingsDataContainer Instance { get; private set; }
    public override void _Ready() 
    { 
        Instance = this;
        SignalHandler();
    }

    int windowModeIndex = 0;
    float masterVolume = 0f;
    float musicVolume = 0f;
    float sfxVolume = 0f;

    public Dictionary CreateStorageDictionary()
    {
        Dictionary settingsDictionary = new Dictionary{
            {"windowModeIndex", windowModeIndex},
            {"masterVolume", masterVolume},
            {"musicVolume", musicVolume},
            {"sfxVolume", sfxVolume},
            {"upKeybind", InputMap.ActionGetEvents("up")[0]},
            {"downKeybind", InputMap.ActionGetEvents("down")[0]}, 
            {"leftKeybind", InputMap.ActionGetEvents("left")[0]},
            {"rightKeybind", InputMap.ActionGetEvents("right")[0]}, 
            {"escKeybind", InputMap.ActionGetEvents("esc")[0]}
        };

        GD.Print(settingsDictionary);
        return settingsDictionary;
    }

    public void SignalHandler()
    {
        SettingsSignalBus.Instance.OnWindowModeSelected += OnWindowModeSelected;
        SettingsSignalBus.Instance.OnMasterSoundVolumeSet += OnMasterSoundVolumeSet;
        SettingsSignalBus.Instance.OnMusicSoundVolumeSet += OnMusicSoundVolumeSet;
        SettingsSignalBus.Instance.OnSfxSoundVolumeSet += OnSfxSoundVolumeSet;
    }

    public void OnWindowModeSelected(int index)
    {
        windowModeIndex = index;
    }

    public void OnMasterSoundVolumeSet(float volume)
    {
        masterVolume = volume;
    }

    public void OnMusicSoundVolumeSet(float volume)
    {
        musicVolume = volume;
    }

    public void OnSfxSoundVolumeSet(float volume)
    {
        sfxVolume = volume;
    }
}
