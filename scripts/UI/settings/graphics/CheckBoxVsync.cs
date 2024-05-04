using Godot;
using System;

public partial class CheckBoxVsync : Control
{
	CheckBox checkboxVsync;
	
	public override void _Ready()
	{
		checkboxVsync = GetNode<CheckBox>("%CheckBoxVsyncActive");
		checkboxVsync.ButtonPressed = true;
		DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
	}

	private void OnVsyncCheckBoxToggled(bool toggled_on)
	{
		if (toggled_on)
		{
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
		}
		else
		{
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);
		}
	}
}
