using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LetsBattle
{
    public partial class CreatePlayer : Window
    {
        #region variables
        public string name = "";
        public int classP = 0;
        public int health = 0;
        public int damage = 0;
        public int defense = 0;
        #endregion

        public CreatePlayer(){ InitializeComponent(); }

        public void B_readInput_Click(object sender, RoutedEventArgs e)
        {
            name = tb_inputName.Text;
            classP = cb_Class.SelectedIndex;
            
            health = Convert.ToInt32(tb_Health.Text);
            damage = Convert.ToInt32(tb_Damage.Text);
            defense = Convert.ToInt32(tb_Defense.Text);

            Close();
        }
        
        //generating random stats
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            classP = cb_Class.SelectedIndex;
            Dice dice = new Dice();

            int h = dice.DiceRoll(20);
            int da = dice.DiceRoll(20);
            int de = dice.DiceRoll(20);

            h += dice.DiceRoll(5);
            da += dice.DiceRoll(5);
            de += dice.DiceRoll(5);

            #region IDontKnow

            tb_Health.Text = Convert.ToString(h);
            tb_Damage.Text = Convert.ToString(da);
            tb_Defense.Text = Convert.ToString(de);

            int tbH = Convert.ToInt16(tb_Health.Text);
            int tbDa = Convert.ToInt16(tb_Damage.Text);
            int tbDe = Convert.ToInt16(tb_Defense.Text);

            #endregion

            switch (classP)
            {
                case 0:
                    if(tbH < 7 || tbDa < 10 || tbDe < 5)
                    {
                        tb_Health.Text = 7.ToString();
                        tb_Damage.Text = 10.ToString();
                        tb_Defense.Text = 5.ToString();
                    }
                    break;
                case 1:
                    if (tbH < 10 || tbDa < 8 || tbDe < 9)
                    {
                        tb_Health.Text = 10.ToString();
                        tb_Damage.Text = 8.ToString();
                        tb_Defense.Text = 9.ToString();
                    }
                    break;
            }
        }
    }
}
