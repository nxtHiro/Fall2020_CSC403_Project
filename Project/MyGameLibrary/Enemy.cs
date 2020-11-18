using System.Drawing;

namespace Fall2020_CSC403_Project.code {
    /// <summary>
    /// This is the class for an enemy
    /// </summary>
    public class Enemy : BattleCharacter {
    /// <summary>
    /// THis is the image for an enemy
    /// </summary>
    public Image Img { get; set; }

    /// <summary>
    /// this is the background color for the fight form for this enemy
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="initPos">this is the initial position of the enemy</param>
    /// 
    /// <param name="collider">this is the collider for the enemy</param>
        //states are still (0), moving (1) and following (2)  
        public int state = 0;
        public Vector2 pos = new Vector2();
        public int xVel = 0;
        public int yVel = 0;
    public Enemy(Vector2 initPos, Collider collider) : base(initPos, collider) {
        Attacks.Add(new Attack("Burst", 2, 5));
        Attacks.Add(new Attack("Bite", 1, 2));
        responses = new string[5] { "Please, have mercy", "No. Stop.", "Ah", "Oh no", "How could you do this?" };
        pos = initPos;
        }
        public void updateState(Vector2 playerPos) {
            //Euler Distance
            if (this.Health <= 0) {
                state = 0;
                return;
            }
            double distance = System.Math.Pow((playerPos.x - pos.x) * (playerPos.x - pos.x) + (playerPos.y - pos.y) * (playerPos.y - pos.y), 0.5);
            if (distance < 300) {
                state = 2;
                if (System.Math.Abs(playerPos.x - pos.x) > System.Math.Abs(playerPos.y - pos.y)) {
                    if ((playerPos.x - pos.x) < 0){
                        xVel = -1;
                    }else{
                        xVel = 1;
                    }

                    yVel = 0;
                } else {
                    if ((playerPos.y - pos.y) < 0){
                        yVel = 1;
                    }else{
                        yVel = -1;
                    }

                    xVel = 0;
                }
                return;
            }

            System.Random rand = new System.Random();


            if (rand.NextDouble() > 0.5) {
                state = (state + 1) % 2;
            }
            if (state == 1) {
                xVel = rand.Next(-1, 2);
                yVel = rand.Next(-1, 2);
            }
            if (state == 0) {
                xVel = 0;
                yVel = 0;
            }
        }
  }
}
