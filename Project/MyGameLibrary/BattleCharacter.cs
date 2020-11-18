using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591 // use this to disable comment warnings

namespace Fall2020_CSC403_Project.code {
  public class BattleCharacter : Character {
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    private float strength;

    public event Action<int> AttackEvent;

    public static string[] responses = new string[10] { "Take that", "I've got you now", "Ha. Weakling.", "You're going down", "What's the matter? Can't take a hit?", "Please, have mercy", "No. Stop.", "Ah", "Oh no", "How could you do this?"};

    public BattleCharacter(Vector2 initPos, Collider collider) : base(initPos, collider) {
      MaxHealth = 20;
      strength = 2;
      Health = MaxHealth;
    }

    public void OnAttack(int amount) {
      AttackEvent((int)(amount * strength));
    }

    public void AlterHealth(int amount) {
      Health += amount;
    }
  }
}
