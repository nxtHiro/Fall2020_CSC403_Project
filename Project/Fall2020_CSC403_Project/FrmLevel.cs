using Fall2020_CSC403_Project.code;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace Fall2020_CSC403_Project {
  public partial class FrmLevel : Form {
    private Player player;

    private Enemy enemyPoisonPacket;
    private Enemy bossKoolaid;
    private Enemy enemyCheeto;
    private AttackItem attackItem;
    private Character[] walls;

    private DateTime timeBegin;
    private FrmBattle frmBattle;
    private PictureBox AtkItem;
    public FrmLevel() {
      InitializeComponent();
     AtkItem = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(AtkItem)).BeginInit();
      this.SuspendLayout();
      // 
      // AttackSpawn
      // 
      Random r = new Random();
      int spawnX = r.Next(100, 1686);
      int spawnY = r.Next(100, 1073);
      AtkItem.BackColor = System.Drawing.Color.Transparent;
      AtkItem.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.attack_orb;
      AtkItem.Image = global::Fall2020_CSC403_Project.Properties.Resources.attack_orb;
      AtkItem.Location = new System.Drawing.Point(spawnX, spawnY);
      AtkItem.Name = "AtkItem";
      AtkItem.TabStop = false;
      AtkItem.Size = new System.Drawing.Size(80, 100);
      AtkItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      AtkItem.TabIndex = 18;
      ((System.ComponentModel.ISupportInitialize)(AtkItem)).EndInit();
      this.Controls.Add(AtkItem);
      this.ResumeLayout(false);
      this.PerformLayout();
        }

    private void FrmLevel_Load(object sender, EventArgs e) {
      const int PADDING = 7;
      const int NUM_WALLS = 13;
      
      player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
      bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
      enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING));
      enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING));
      attackItem = new AttackItem(CreatePosition(AtkItem), CreateCollider(AtkItem, PADDING));

      bossKoolaid.Img = picBossKoolAid.BackgroundImage;
      enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
      enemyCheeto.Img = picEnemyCheeto.BackgroundImage;

      bossKoolaid.Color = Color.Red;
      enemyPoisonPacket.Color = Color.Green;
      enemyCheeto.Color = Color.FromArgb(255, 245, 161);

      walls = new Character[NUM_WALLS];
      for (int w = 0; w < NUM_WALLS; w++) {
        PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
        walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
      }

      Game.player = player;
      timeBegin = DateTime.Now;
    }

    private Vector2 CreatePosition(PictureBox pic) {
      return new Vector2(pic.Location.X, pic.Location.Y);
    }

    private Collider CreateCollider(PictureBox pic, int padding) {
      Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
      return new Collider(rect);
    }

    private void FrmLevel_KeyUp(object sender, KeyEventArgs e) {
      player.ResetMoveSpeed();
    }

    private void tmrUpdateInGameTime_Tick(object sender, EventArgs e) {
      TimeSpan span = DateTime.Now - timeBegin;
      string time = span.ToString(@"hh\:mm\:ss");
      lblInGameTime.Text = "Time: " + time.ToString();
    }

    private void tmrPlayerMove_Tick(object sender, EventArgs e) {
      // move player
      player.Move();

      // check collision with walls
      if (HitAWall(player)) {
        player.MoveBack();
      }

      // check collision with enemies
      if (HitAChar(player, enemyPoisonPacket)) {
        Fight(enemyPoisonPacket);
      }
      else if (HitAChar(player, enemyCheeto)) {
        Fight(enemyCheeto);
      }
      if (HitAChar(player, bossKoolaid)) {
        Fight(bossKoolaid);
      }

      if (HitAnItem(player, attackItem))
      {
        attackItem.addAttack(player);
      }

      // update player's picture box
      picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
    }

    private bool HitAWall(Character c) {
      bool hitAWall = false;
      for (int w = 0; w < walls.Length; w++) {
        if (c.Collider.Intersects(walls[w].Collider)) {
          hitAWall = true;
          break;
        }
      }
      return hitAWall;
    }

    private bool HitAChar(Character you, Character other) {
      return you.Collider.Intersects(other.Collider);
    }

    private bool HitAnItem(Character you, Item i)
    {
           return you.Collider.Intersects(i.Collider);
    }

    private void Fight(Enemy enemy) {
      player.ResetMoveSpeed();
      player.MoveBack();
      frmBattle = FrmBattle.GetInstance(enemy);
      frmBattle.Show();

      if (enemy == bossKoolaid) {
        frmBattle.SetupForBossBattle();
      }
    }

    //This function handles the win forms Keyboard evente. Effectively any time a key is pressed this function gets called
    //the way win forms handles events only the state of the key that is pressed gets sent tothe function. So we call another
    //newer, way cooler library that definately didnt take 3 hours to get working: System.Windows.Input from WPF. This library
    //gives the current state of the keyboard and lets us check the state of indiviual keys.
    private void FrmLevel_KeyDown(object sender, KeyEventArgs e) {
      if (Keyboard.IsKeyDown(Key.Up)) {
        player.GoUp();
      }

      if (Keyboard.IsKeyDown(Key.Down)) {
        player.GoDown();
      }

      if (Keyboard.IsKeyDown(Key.Left)) {
         player.GoLeft();
      }

      if (Keyboard.IsKeyDown(Key.Right)) {
        player.GoRight();
        CheckKonamiStatus();
      }
    }

    private void lblInGameTime_Click(object sender, EventArgs e) {

    }
    private void CheckKonamiStatus() {
        if (string.Join(" ", player.MoveHistory.ToArray()).Equals(string.Join(" ", new String[] { "up", "up", "down", "down", "left", "right", "left", "right" }))){
            player.Attacks.Add(new Attack("Hadouken", 2, 20));
       }
    }
  }
}
