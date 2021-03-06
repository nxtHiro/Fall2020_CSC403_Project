﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code {
  public class Player : BattleCharacter {
        public Player(Vector2 initPos, Collider collider) : base(initPos, collider) {
            Attacks.Add(new Attack("Whack", 1, 4));
            Attacks.Add(new Attack("Punch", 1, 3));
            responses = new string[5] { "Take that", "I've got you now", "Ha. Weakling.", "You're going down", "What's the matter? Can't take a hit?" };
        }
  }
}
