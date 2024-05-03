using Godot;
using System;
using System.Collections.Generic;

public partial class ButtonResolutionMode : Control
{
	private static readonly Dictionary<string, Vector2I> WINDOW_RESOLUTION_DICTIONARY = new Dictionary<string, Vector2I>
	{
		{ "1920 x 1080", new Vector2I(1920, 1080) },
		{ "1280 x 720", new Vector2I(1280, 720) },
		{ "1024 x 768", new Vector2I(1024, 768) },
		{ "800 x 600", new Vector2I(800, 600) },
		{ "640 x 360", new Vector2I(640, 360) }
	};

	OptionButton dropdown;

	public override void _Ready()
	{
		dropdown = GetNode<OptionButton>("%ButtonOption");
		
		AddDropdownItems();
	}

	private void AddDropdownItems()
	{
		foreach (KeyValuePair<string, Vector2I> windowResolutionKVP in WINDOW_RESOLUTION_DICTIONARY)
		{
			dropdown.AddItem(windowResolutionKVP.Key);
		}
	}

	private void OnWindowResolutionSelected(long index)
	{
		// DisplayServer.(WINDOW_RESOLUTION_DICTIONARY[dropdown.GetItemText((int) index)]);
		GetWindow().Size = WINDOW_RESOLUTION_DICTIONARY[dropdown.GetItemText((int) index)];
	}
}
