using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmBattle : Form {
    public static FrmBattle instance = null;
    public FrmAttackMenu atkMenu;
    private Enemy enemy;
    private Player player;

    private FrmBattle() {
      InitializeComponent();
      player = Game.player;
    }

    public void Setup() {
      // update for this enemy
      picEnemy.BackgroundImage = enemy.Img;
      picEnemy.Refresh();
      BackColor = enemy.Color;
      picBossBattle.Visible = false;

      // Observer pattern
      enemy.AttackEvent += PlayerDamage;
      player.AttackEvent += EnemyDamage;

      // show health
      UpdateHealthBars();
    }

    public void SetupForBossBattle() {
      picBossBattle.Location = Point.Empty;
      picBossBattle.Size = ClientSize;
      picBossBattle.Visible = true;

      SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
      simpleSound.Play();

      tmrFinalBattle.Enabled = true;
    }

    public static FrmBattle GetInstance(Enemy enemy) {
      if (instance == null) {
        instance = new FrmBattle();
        instance.enemy = enemy;
        instance.Setup();
      }
      return instance;
    }

    private void UpdateHealthBars() {
      float playerHealthPer = player.Health / (float)player.MaxHealth;
      float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

      const int MAX_HEALTHBAR_WIDTH = 226;
      lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
      lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

      lblPlayerHealthFull.Text = player.Health.ToString();
      lblEnemyHealthFull.Text = enemy.Health.ToString();
    }

    private void btnAttack_Click(object sender, EventArgs e) {

        if (!(player.Health <= 0 || enemy.Health <= 0))
        {
            atkMenu = FrmAttackMenu.GetInstance(enemy);
            atkMenu.ShowDialog();
        }

        // random move selection for enemy
        Random random = new Random();
        if (enemy.Health > 0)
        {
            float probability = random.Next(0, 1);
            if(probability > 0.95)
            {
                enemy.OnAttack(enemy.Attacks[0].atkVal * -1);
            }
            else
            {
                enemy.OnAttack(enemy.Attacks[1].atkVal * -1);
            }
        }
        
      UpdateHealthBars();
      if (player.Health <= 0 || enemy.Health <= 0) {
        instance = null;
        Close();
      }

      // adding random dialogue to the Battle using the textboxes
      Random random1 = new Random();
      
      int playerResponse = random1.Next(0, 5);
      int enemyReponse = random1.Next(0, 5);

      textboxBattleEnemy.Text = enemy.responses[enemyReponse]; 
      textboxBattlePlayer.Text = player.responses[playerResponse];
    }

    // placeholder EventHandler function for the items button
    private void btnItems_Click(object sender, EventArgs e)
    {
        player.AlterHealth(4);
        UpdateHealthBars();
    }
    private void EnemyDamage(int amount) {
      enemy.AlterHealth(amount);
    }

    private void PlayerDamage(int amount) {
      player.AlterHealth(amount);
    }

    private void tmrFinalBattle_Tick(object sender, EventArgs e) {
      picBossBattle.Visible = false;
      tmrFinalBattle.Enabled = false;
    }

        private void FrmBattle_Load(object sender, EventArgs e)
        {

        }
    }
}
