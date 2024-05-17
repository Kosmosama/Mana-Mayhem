using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class SettingsSignalBus : Node
{
    public static SettingsSignalBus Instance { get; private set; }
    public override void _Ready() { Instance = this; }

    // Graphics

	[Signal]
	public delegate void OnWindowModeSelectedEventHandler(int index);

    [Signal]
	public delegate void OnResolutionSelectedEventHandler(int index);

    // Sound

    [Signal]
	public delegate void OnMasterSoundVolumeSetEventHandler(float volume);

    [Signal]
	public delegate void OnMusicSoundVolumeSetEventHandler(float volume);

    [Signal]
	public delegate void OnSfxSoundVolumeSetEventHandler(float volume);

    // Save/Load

    [Signal]
	public delegate void SetSettingsDictionaryEventHandler(Dictionary settingsDictionary);

    [Signal]
	public delegate void LoadSettingsDataEventHandler(Dictionary settingsDictionary);

    // Signal-Emision

    public void EmitOnLoadSettingsDictionary(Dictionary settingsDictionary)
    {
        EmitSignal(SignalName.LoadSettingsData, settingsDictionary);
    }

    public void EmitOnSetSettingsDictionary(Dictionary settingsDictionary)
    {
        EmitSignal(SignalName.SetSettingsDictionary, settingsDictionary);
    }

    public void EmitOnWindowModeSelected(int index)
    {
        EmitSignal(SignalName.OnWindowModeSelected, index);
    }

    public void EmitResolutionSelected(int index)
    {
        EmitSignal(SignalName.OnResolutionSelected, index);
    }

    public void EmitOnMasterSoundVolumeSet(float volume)
    {
        EmitSignal(SignalName.OnMasterSoundVolumeSet, volume);
    }

    public void EmitOnMusicSoundVolumeSet(float volume)
    {
        EmitSignal(SignalName.OnMusicSoundVolumeSet, volume);
    }

    public void EmitOnSfxSoundVolumeSet(float volume)
    {
        EmitSignal(SignalName.OnSfxSoundVolumeSet, volume);
    }
}
