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
    public partial class AttackMage : Window
    {
        
        public AttackMage()
        {
            InitializeComponent();
        }

        private void B_attack_fireball_Click(object sender, RoutedEventArgs e)
        {
           /*ai.HowShouldEveryoneAttack(0);
            if (!player.IsAlive() || !enemy.IsAlive())
            {
                B_attack_fireball.IsEnabled = false;
                B_attack_iceball.IsEnabled = false;
                Close();
            }*/
                
        }

        private void B_attack_iceball_Click(object sender, RoutedEventArgs e)
        {/*
            ai.HowShouldEveryoneAttack(1);
            if (!player.IsAlive() || !enemy.IsAlive())
            {
                B_attack_fireball.IsEnabled = false;
                B_attack_iceball.IsEnabled = false;
                Close();
            }*/
        }
    }
}
