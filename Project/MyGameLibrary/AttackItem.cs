using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    public class AttackItem : Item
    {
        public AttackItem(Vector2 initPos, Collider collider) : base(initPos, collider)
        {

        }

        public void addAttack(Player p)
        {
            bool AlreadyKnow = false;
            foreach(Attack a in p.Attacks)
            {
                if(a.atkName == "Beatdown")
                {
                    AlreadyKnow = true;
                }
            }
            if (!AlreadyKnow)
            {
            p.Attacks.Add(new Attack("Beatdown", 1, 10));
            }
        }
    }
}
