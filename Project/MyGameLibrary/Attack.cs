using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project {
    public class Attack
    {
        public String atkName;
        public int atkType;
        public int atkVal;
        public float probability;

        public Attack(String attackName, int attackType, int attackVal)
        {
            atkName = attackName;
            atkType = attackType;
            atkVal = attackVal;
        }

    }
}
