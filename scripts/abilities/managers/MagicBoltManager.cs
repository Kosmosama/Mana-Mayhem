using Godot;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;

public partial class MagicBoltManager : Node2D
{
	// private void _on_ice_spear_timer_attack_timer_timeout()
	// {
	// 	if (iceSpearAmmo > 0)
	// 	{
	// 		ice_spear iceSpearAttack = (ice_spear)iceSpear.Instantiate();
	// 		iceSpearAttack.Position = Position;
	// 		iceSpearAttack.targetLocation = GetClosestTarget();
	// 		iceSpearAttack.level = iceSpearLevel;
	// 		AddChild(iceSpearAttack);
	// 		iceSpearAmmo --;
	// 		if (iceSpearAmmo > 0)
	// 		{
	// 			iceSpearTimerAttackTimer.Start();
	// 		}
	// 		else
	// 		{
	// 			iceSpearTimerAttackTimer.Stop();
	// 		}
	// 	}
	// }

	int penetration; // number of enemies that a magic bolt can hit, more with level | to::child
	float magicCooldown; // seconds to await for instancing another magic bolt batch
	float magicDuration; // seconds to await to de-instance a magic bolt if it hasnt hit anything | to::child
	float magicSize; // multiplier of how big the hitbox and sprite are in comparison to base size | to::child
	int magicNumber; // number of magic bolt instances per batch
	float damage; // damage each magic bolt does | to::child

	PackedScene magicBolt;
	Timer magicBoltCooldown;
	Player player;

	public override void _Ready()
	{
		magicBolt = (PackedScene)ResourceLoader.Load("res://scenes/abilities/instantiable/MagicBolt.tscn");
		magicBoltCooldown = GetNode<Timer>("MagicBoltCooldown");
		player = GetTree().GetFirstNodeInGroup("Player") as Player;

		Initialize();
	}

	public override void _Process(double delta)
	{
	}

	private void Initialize()
	{
		penetration = 1;
		magicCooldown = 1f;
		magicDuration = 0.5f;
		magicSize = 1f;
		magicNumber = 1;
		damage = 5f;
	}

	public void UpdateCooldown(float waitTime)
	{
		magicBoltCooldown.WaitTime = waitTime;
	}

	// func: updateDuration
	// ...

	private List<Vector2> GetEnemyPositions(int numberOfCloseEnemies)
	{
		List<Vector2> positions = new List<Vector2>();

		List<IEnemy> closestEnemies = player.GetClosestEnemy(numberOfCloseEnemies);

		if (closestEnemies.Count != 0)
		{
			if (numberOfCloseEnemies == closestEnemies.Count)
			{
				foreach (IEnemy enemy in closestEnemies)
				{
					positions.Add(enemy.GetPosition());
				}
			}
			else if (closestEnemies.Count > numberOfCloseEnemies)
			{
				for (int i = 0; i < numberOfCloseEnemies; i++)
				{
					positions.Add(closestEnemies[i].GetPosition());
				}
			}
			else
			{
				for (int i = 0; i < (closestEnemies.Count / numberOfCloseEnemies); i++)
				{
					foreach (IEnemy enemy in closestEnemies)
					{
						positions.Add(enemy.GetPosition());
					}
				}

				for (int j = 0; j < (numberOfCloseEnemies - positions.Count); j++)
				{
					positions.Add(closestEnemies[j].GetPosition());
				}
			}
		}
		

		return positions;
	}

	private void OnMagicBoltCooldownTimeout()
	{
		// if number of enemies < 1, fire random
		for (int p = 0; p < penetration; p++)
		{
			MagicBolt magicBoltAttack = (MagicBolt) magicBolt.Instantiate();
			magicBoltAttack.Position = player.Position;
	// 		iceSpearAttack.targetLocation = GetClosestTarget();
	// 		iceSpearAttack.level = iceSpearLevel;
	// 		AddChild(iceSpearAttack);
	// 		iceSpearAmmo --;
	// 		if (iceSpearAmmo > 0)
	// 		{
	// 			iceSpearTimerAttackTimer.Start();
	// 		}
	// 		else
	// 		{
	// 			iceSpearTimerAttackTimer.Stop();
	// 		}
			AddChild(magicBoltAttack);
		}
	}
}
