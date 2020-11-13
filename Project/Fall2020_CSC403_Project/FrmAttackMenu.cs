using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmAttackMenu : Form
    {
        public static FrmAttackMenu instance = null;
        private Enemy enemy;
        private Player player;
        public FrmAttackMenu()
        {
            InitializeComponent();
            player = Game.player;

            // initializes AttackMenu with the attack options for the player
            for(int i = 0; i < player.Attacks.Count; i++)
            {
                Button btnAttack = new Button();
                btnAttack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnAttack.Location = new System.Drawing.Point(0, 0+i*60);
                btnAttack.Margin = new System.Windows.Forms.Padding(6);
                btnAttack.Name = player.Attacks[i].atkName;
                btnAttack.Size = new System.Drawing.Size(160, 40);
                btnAttack.TabIndex = 2;
                btnAttack.Text = player.Attacks[i].atkName;
                btnAttack.UseVisualStyleBackColor = true;
                btnAttack.Click += new System.EventHandler(this.btnAttack_Click);
                this.Controls.Add(btnAttack);
            }
        }
        private void btnAttack_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int atkVal = 0;
            foreach(Attack a in player.Attacks)
            {
                if(a.atkName == btn.Text)
                {
                    atkVal = a.atkVal;
                    break;
                }
            }
            player.OnAttack(atkVal*-1);
            Close();
        }
        private void FrmAttackMenu_Load(object sender, EventArgs e)
        {

        }

        // Allows for instance generation with enemy context
        public static FrmAttackMenu GetInstance(Enemy enemy) 
        {
            instance = new FrmAttackMenu();
            instance.enemy = enemy;
            return instance;
        }
    }
}
