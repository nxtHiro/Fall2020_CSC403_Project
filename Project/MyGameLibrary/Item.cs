using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    public class Item
    {
        public Vector2 Position { get; private set; }
        public Collider Collider { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initPos">this is the initial position of the enemy</param>
        /// <param name="collider">this is the collider for the enemy</param>
        public Item(Vector2 initPos, Collider collider)
        {
            Position = initPos;
            Collider = collider;
        }
    }
}
