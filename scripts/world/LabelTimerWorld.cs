using Godot;
using System;

public partial class LabelTimerWorld : Label
{
	// Camera2D camera;

	// public override void _Ready()
	// {
	// 	camera = GetNode<Camera2D>("PlayerCamera");
	// }

	// public override void _Process(double delta)
	// {
	// 	Position = camera.Position;
	// }

	private void AddSecond()
	{
		string[] timeComponents = Text.Split(':');
		int minutes = int.Parse(timeComponents[0]);
		int seconds = int.Parse(timeComponents[1]);

		seconds = (seconds + 1) % 60;

		if (seconds == 0) 
			minutes++;

		Text = $"{minutes:00}:{seconds:00}";
	}

	private void OnWorldTimerTimeout()
	{
		AddSecond();
	}
}
