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
    /// <summary>
    /// Interakční logika pro AttackWarrior.xaml
    /// </summary>
    public partial class AttackWarrior : Window
    {
        Ai ai;
        Game game;
        MainWindow mw = ((MainWindow)Application.Current.MainWindow);
        public AttackWarrior()
        {
            InitializeComponent();
        }

        private void B_attack_heavy_Click(object sender, RoutedEventArgs e)
        {
            ai.HowShouldEveryoneAttack(0);
            if (!game.player.IsAlive() || !game.enemy.IsAlive())
            {
                B_attack_heavy.IsEnabled = false;
                B_attack_light.IsEnabled = false;
                Close();
            }
        }

        private void B_attack_light_Click(object sender, RoutedEventArgs e)
        {
            ai.HowShouldEveryoneAttack(1);
            if (!game.player.IsAlive() || !game.enemy.IsAlive())
            {
                B_attack_heavy.IsEnabled = false;
                B_attack_light.IsEnabled = false;
                Close();
            }
        }
    }
}
