using Godot;
using System;

public partial class EnemySpawner : Node
{
	Timer timer;
	PackedScene enemy;

	int enemies = 5;
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		enemy = (PackedScene)ResourceLoader.Load("res://scenes/enemies/Enemy.tscn");
	}

	private void GenerateEnemy()
	{
		NormalEnemy instantiableEnemy = (NormalEnemy)enemy.Instantiate();
		instantiableEnemy.Position = new Vector2(0,0);
		AddChild(instantiableEnemy);
	}

	private void OnTimerTimeout()
	{
		if (enemies > 0)
		{
			GenerateEnemy();
			enemies--;
		}
	}
}
