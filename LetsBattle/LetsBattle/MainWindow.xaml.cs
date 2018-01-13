using /*no_brain, no_logic, rather dont look at that cause your end should be near*/ System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LetsBattle
{
    public partial class MainWindow : Window
    {
        #region variables
        Ai ai = new Ai();
        WritingMethods wm = new WritingMethods();
        Game game = new Creation();
        
        protected char[] gameArena; //field for arena 
        
        public Inventory inventoryPlayer = new Inventory();
        public List<Inventory> inventoryListPlayer = new List<Inventory>(); // something where player can store anyhting in its inventory

        public Inventory inventoryEnemy = new Inventory();
        public List<Inventory> inventoryListEnemy = new List<Inventory>(); // something where enemy can store anyhting in its inventory

        public int classP;

        public int whoP; //reference to classP
        public int whoE; //reference to classP

        public int wIP;
        public int wIE;
        

        #endregion
        
        public MainWindow()
        {
            player = game.InitialyPlayer();
            enemy = game.InitialyEnemy();
            classP = game.GetClassP();

            wm.GetInformedIntoLabels(7, player, enemy);

            InitializeComponent(); 
            wm.GetInformedIntoLabels(7, player, enemy);
        }

        #region JustAllButtonsThatAreOnForm
        private void B_fight_Click(object sender, RoutedEventArgs e)
        {
            switch (classP)
            {
                case 0:
                    var attackWarior = new AttackWarrior();
                    attackWarior.ShowDialog();
                    break;
                case 1:
                    var attackMage = new AttackMage();
                    attackMage.ShowDialog();
                    break;
            }
        }

        private void B_inventory_Click(object sender, RoutedEventArgs e)
        {
            var inventory = new PlayerInventory();
            inventory.ShowDialog();
        }

        private void B_transfer_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button) == B_go_left)
            {
                wm.GetInformedIntoLabels(3);
                game.Move(game.player, -1);
                wm.GetInformedIntoLabels(3);
            }

            else if ((sender as Button) == B_go_right)
            {
                wm.GetInformedIntoLabels(3);
                game.Move(game.player, +1);
                wm.GetInformedIntoLabels(3);
            }
        }

        private void Key_pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                wm.GetInformedIntoLabels(3);
                game.Move(game.player, -1);
                wm.GetInformedIntoLabels(3);
            }
            else if (e.Key == Key.Right)
            {
                wm.GetInformedIntoLabels(3);
                game.Move(game.player, +1);
                wm.GetInformedIntoLabels(3);
            }
        }

        #endregion
        
    }
}
