using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	Timer timer;
	PackedScene enemy;
	private RandomNumberGenerator random;
	Player player;
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		enemy = (PackedScene)ResourceLoader.Load("res://scenes/enemies/Enemy.tscn");
		random = new RandomNumberGenerator();
		random.Randomize();
		player = GetTree().GetFirstNodeInGroup("Player") as Player;
		// if (player == null)
		// {
		// 	GD.PrintErr("Player node not found in the scene tree.");
		// 	return;
		// }
	}

	private void GenerateEnemy(Vector2 position)
	{
		NormalEnemy instantiableEnemy = (NormalEnemy)enemy.Instantiate();
		instantiableEnemy.Position = position;
		AddChild(instantiableEnemy);
	}

	private void OnTimerTimeout()
	{
		GenerateEnemy(GetRandomPosition());
	}

	private Vector2 GetRandomPosition()
	{
		Vector2 viewportSize = GetViewportRect().Size * random.RandfRange(1.1f, 1.4f);
		Vector2 playerPosition = player.GlobalPosition;

		Vector2 spawnPos1 = Vector2.Zero;
		Vector2 spawnPos2 = Vector2.Zero;

		switch (random.RandiRange(0, 3)) // Randomly select a side
		{
			case 0: // Up
				spawnPos1 = playerPosition + new Vector2(-viewportSize.X / 2, -viewportSize.Y / 2);
				spawnPos2 = playerPosition + new Vector2(viewportSize.X / 2, -viewportSize.Y / 2);
				break;
			case 1: // Down
				spawnPos1 = playerPosition + new Vector2(-viewportSize.X / 2, viewportSize.Y / 2);
				spawnPos2 = playerPosition + new Vector2(viewportSize.X / 2, viewportSize.Y / 2);
				break;
			case 2: // Right
				spawnPos1 = playerPosition + new Vector2(viewportSize.X / 2, -viewportSize.Y / 2);
				spawnPos2 = playerPosition + new Vector2(viewportSize.X / 2, viewportSize.Y / 2);
				break;
			case 3: // Left
				spawnPos1 = playerPosition + new Vector2(-viewportSize.X / 2, -viewportSize.Y / 2);
				spawnPos2 = playerPosition + new Vector2(-viewportSize.X / 2, viewportSize.Y / 2);
				break;
		}

		float x_spawn = random.RandfRange(spawnPos1.X, spawnPos2.X);
		float y_spawn = random.RandfRange(spawnPos1.Y, spawnPos2.Y);

		return new Vector2(x_spawn, y_spawn);
	}
}
