using Godot;
using System;
using System.Reflection;

public partial class ButtonWindowMode : Control
{
	private static readonly string[] WINDOW_MODE_ARRAY = {
		"Full-Screen",
		"Windowed",
		"Borderless Window",
		"Borderless Full-Screen"
	};

	OptionButton dropdown;

	public override void _Ready()
	{
		dropdown = GetNode<OptionButton>("%ButtonOption");

		AddDropdownItems();
	}

	private void AddDropdownItems()
	{
		foreach (string windowMode in WINDOW_MODE_ARRAY)
		{
			dropdown.AddItem(windowMode);
		}
	}

	private void OnWindowModeSelected(long index)
	{
		switch (index)
		{
			case 0: // Full-Screen
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false); // #TODO maybe set a checkbox to make borderless instead of copy-pasting this statement?
				break;
			case 1: // Windowed
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
				break;
			case 2: // Borderless Window
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
				break;
			case 3: // Borderless Full-Screen
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
				break;
		}
	}
}
