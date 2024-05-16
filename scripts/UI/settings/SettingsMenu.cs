using Godot;
using System;

public partial class SettingsMenu : Control
{
	[Signal]
	public delegate void BackPressedEventHandler();

	public override void _Ready()
	{
		
	}

	private void OnButtonBackPressed()
	{
		EmitSignal(nameof(BackPressed));
		SettingsSignalBus.Instance.EmitOnSetSettingsDictionary(SettingsDataContainer.Instance.CreateStorageDictionary());
	}
}
