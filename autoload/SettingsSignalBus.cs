using Godot;
using System;
using System.Collections.Generic;

public partial class SettingsSignalBus : Node
{
    // #TODO
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
	public delegate void CreateSettingsDictionaryEventHandler(float settingsDictionary); // Yet to know

    // Signal-Emitters

    public void EmitOnWindowModeSelected(int index)
    {
        EmitSignal("OnWindowModeSelected", index);
    }
}
