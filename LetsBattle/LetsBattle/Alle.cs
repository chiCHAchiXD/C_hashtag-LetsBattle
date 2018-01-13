using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LetsBattle
{
    #region characters
    //character class with one not completed method at the end
    public class Character
    {
        #region variablesWhichYouCanReadOnceAndYoullKnowWhatAreThey
        protected string name;
        protected int health;
        protected int damage;
        protected int defense;
        protected int who;
        protected int level;
        protected int actionpts;
        protected Dice dice = new Dice();
        #endregion
        
        public Character() { }
        
        public Character(int h, int da, int de, string na, int wh)
        {
            health = h;
            damage = da;
            defense = de;
            name = na;
            who = wh;
            level = 1;
            actionpts = 5;
        }

        #region getAndSet
        public int Health { get { return health; } set { health = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public int Defense { get { return defense; } set { defense = value; } }
        public int Who { get { return who; } set { who = value; } }
        public int Level { get { return level; } set { defense = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int ActionPoint { get { return actionpts; } set { actionpts = value; } }
        #endregion

        public int Fight(Character attackedOn, int dmg) { return attackedOn.DamageDealt(attackedOn, dmg); }
        
        private int DamageDealt(Character attackedOn, int dmg)
        {
            int damageDealt = dice.DiceRoll(dmg) - dice.DiceRoll(attackedOn.Defense);
            if (damageDealt < 0) damageDealt = 0; //nobody wants to add lives, do you ?
            attackedOn.MinusHealth(damageDealt); 
            return damageDealt; 
        }
        
        public bool IsAlive() { return (health > 0); }

        protected void MinusHealth(int dmg) { health -= dmg; }
        
        public void LevelPlusPlus()
        {
            level++;
            health += 5;
            damage += 5;
            defense += 5;
            actionpts += 2;
        }
        
        //lets get some stats, 1todo get usefull!!!!!!!
        public void LevelStat(Character attacker) { int livelo = attacker.Level; }
    }
    
    public abstract class Player : Character
    {
        public Player() { }

        public Player(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }

        //i think youll get it
        #region someVirtualShit
        //Some player skills
        public virtual int FireBall() { return 0; }
        public virtual int IceBall() { return 0; }

        public virtual int LightAttack(int damageOfItem) { return 0; } //for some time just for player but enemy will come
        public virtual int HeavyAttack(int damageOfItem) { return 0; } //for some time just for player but enemy will come

        public virtual int LightAttack() { return 0; } //for some time just for enemy but it get deleted
        public virtual int HeavyAttack() { return 0; } //for some time just for enemy but it get deleted

        //Inventory / Items methods
        public virtual int GetItemDamage(Item item) { return 0; }
        public virtual string GetItemName(Item item) { return ""; } //get item name 
        public virtual int GetSwordSharpness() { return 0; } //get item sharpness
        #endregion
    }
    #endregion

    #region classes
    class Mage : Player
    {
        //create mage
        public Mage(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }
        
        public override int FireBall()
        {
            int damage = 0;
            damage = dice.DiceRoll(Damage) + 1;
            return damage;
        }

        public override int IceBall()
        {
            int damage = 0;
            damage = dice.DiceRoll(Damage) + 2;
            return damage;
        }
    }

    class Warrior : Player
    {
        //create warrior
        public Warrior(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }
        
        public override int HeavyAttack(int damageOfItem)
        {
            int damage = 0;
            damage = dice.DiceRoll(damageOfItem) + 3;
            return damage;
        }

        public override int LightAttack(int damageOfItem)
        {

            int damage = 0;
            damage = dice.DiceRoll(damageOfItem) + 1;
            return damage;
        }
    }
    #endregion

    //((MainWindow) Application.Current.MainWindow)
    
    #region inventory

    public class Inventory : Player { public Inventory() { } }

    public class Item : Inventory
    {
        protected string nameOfItem; //name?
        protected int damageOfItem; //damage?

        public Item() { }

        public Item(string nme, int dmg)
        {
            nameOfItem = nme;
            damageOfItem = dmg;
        }

        #region getAtTheEndEverythingThingsOfItem
        
        public override int GetItemDamage(Item item) { return item.damageOfItem; }
        
        public override string GetItemName(Item item) { return item.nameOfItem; }
        
        public string ToString(Item item) { return Convert.ToString(GetItemName(item) + " " + GetItemDamage(item)); }
        #endregion
    }

    #region items

    class Sword : Item
    {
        int sharpness = 0;

        public Sword(string nme, int dmg, int sness) : base(nme, dmg) { sharpness = sness; }

        public override int GetSwordSharpness() { return sharpness; }
    }

    #endregion

    #endregion
    
    #region gameMechanics

    public abstract class Game : Player
    {
        public Game() { }

        public Game(int howBig, int whereIsPlayer, int whereIsEnemy)
        {
            gameArena = new char[howBig]; //setup arena
           
            //fill whole field with #
            for (int i = 0; i < gameArena.Length; i++)
            {
                gameArena[i] = '#';
            }

            //where if enemy write char E
            gameArena[whereIsEnemy] = 'E';

            //where if player write char P
            gameArena[whereIsPlayer] = 'P';

            //and into variables which are for identyfing where is who write these indexes, because i want to make !user-friendly
            wIP = whereIsPlayer;
            wIE = whereIsEnemy;
        }
        
        public int GetWhereIsWho(Character a)
        {
            int where = 0;

            switch (a.Who)
            {
                //Player
                case 0:
                    where = wIP;
                    break;
                //Enemy
                case 1:
                    where = wIE;
                    break;
            }

            return where;
        }

        #region anotherVirtualShit
        public virtual Player InitialyPlayer() { return null; }

        public virtual Player InitialyEnemy() { return null; }

        public virtual Game CreateGameArena() { return null; }

        public virtual char[] GetArena() { return gameArena; } 

        public virtual string GetGameArenaLook() { return ""; }

        public virtual void Move(Character a, int where) { }
        #endregion
    }

    class Creation : Game
    {
        public override Player InitialyPlayer()
        {
            #region fillVariables
            var createPlayer = new CreatePlayer();
            createPlayer.ShowDialog(); //createPlayer Window will show when you run this program
            string name = createPlayer.name;
            classP = createPlayer.classP;
            int h = createPlayer.health;
            int dam = createPlayer.damage;
            int def = createPlayer.defense;
            whoP = 0;
            whoE = 1;
            int roll = dice.DiceRoll();
            #endregion

            Player returnPlayer = null;

            switch (classP)
            {
                case 0: // warrior

                    if (roll % 2 == 0) //if sude
                        inventoryPlayer = new Sword("Broken Sword", 5, 5);
                    else //if liche
                        inventoryPlayer = new Sword("Straight Sword", 10, 25);

                    inventoryListPlayer.Add(inventoryPlayer); //lets add that generated shit into player inventory

                    returnPlayer = new Warrior(h, dam, def, name, whoP); //create player as warrior
                    break;
                case 1: //mage
                    returnPlayer = new Mage(h, dam, def, name, whoP);
                    break;
            }

            return returnPlayer;
            /*
            
            if (roll % 2 == 0) //if sude 
                inventoryEnemy = new Sword("Broken Sword", 5, 5);
            else //if liche
                inventoryEnemy = new Sword("Straight Sword", 10, 25);

            inventoryListEnemy.Add(inventoryEnemy);
            */
        }

        public override Game CreateGameArena()
        {
            Game returnGameArena = new GameArena(10, 1, 9);

            return returnGameArena;
        }

        public override Player InitialyEnemy()
        {
            Player returnEnemy = null;

          //  returnEnemy = ChooseOfEnemy(whoE); // creation of enemy

            return returnEnemy;
        }

        public Player ChooseOfEnemy(int whoE, Character player)
        {
            Player returnEnemy = null;
            int roll = dice.DiceRoll();
            if (roll % 2 == 0) //if sude
                returnEnemy = new Warrior(dice.DiceRoll(player.Health) + 5, dice.DiceRoll(player.Damage) + 6, dice.DiceRoll(player.Defense) + 6, "Ferda", whoE);
            else //if liche 
                returnEnemy = new Mage(dice.DiceRoll(player.Health) + 3, dice.DiceRoll(player.Damage) + 7, dice.DiceRoll(player.Defense) + 2, "Janko", whoE);
            return returnEnemy;
        }

    }

    public class GameArena : Game
    {
        public GameArena(int howBig, int whereIsPlayer, int whereIsEnemy) : base(howBig, whereIsPlayer, whereIsEnemy) { }

        //it returns whole 2D arena
        public override string GetGameArenaLook()
        {
            string a = "";
            for (int i = 0; i < gameArena.Length; i++)
            {
                a += Convert.ToString(gameArena[i]);
            }
            return a;
        }
        
        public override void Move(Character a, int where)
        {
            switch (a.Who)
            {
                case 0:
                    int p = wIP + where;
                    try
                    {
                        if (wIP > 1 || wIP < gameArena.Length - 1)
                        {
                            gameArena[GetWhereIsWho(a)] = '#';
                            gameArena[p] = 'P';
                            wIP = p;
                        }
                    }
                    catch
                    {
                        if (wIP == 0)
                        {
                            gameArena[0] = 'P';
                            wIP = 0;
                        }
                        else if(wIP ==gameArena.Length - 1)
                        {
                            gameArena[gameArena.Length - 1] = 'P';
                            wIP = gameArena.Length - 1;
                        }
                    }
                    break;
                    //enemy
                case 1:
                    int e = wIE + where;
                    try
                    {
                        if (wIE > 1 || wIE < gameArena.Length - 1)
                        {
                            gameArena[GetWhereIsWho(a)] = '#';
                            gameArena[e] = 'E';
                            wIP = e;
                        }
                    }
                    catch
                    {
                        if (wIE == 0)
                        {
                            gameArena[0] = 'E';
                            wIP = 0;
                        }
                        else if (wIE == gameArena.Length - 1)
                        {
                            gameArena[gameArena.Length - 1] = 'E';
                            wIP = gameArena.Length - 1;
                        }
                    }
                    break;
            }
        }
    }
    
    class WritingMethods
    {
        Game game;
        Ai ai = new Ai();

        /*
        public void WhoDied()
        {
            bool a = !player.IsAlive();
            if (a)
            {
                if (player.Health < 0) player.Health = 0;
                GetInformedContinuoslyTb("you died" + Environment.NewLine);
                ((MainWindow)Application.Current.MainWindow).B_fight.IsEnabled = false;
            }

            else
            {
                GetInformedContinuoslyTb(game.enemy.Name + " died" + Environment.NewLine);
                game.player.LevelPlusPlus(); // players livelo + 1
                ai.NextEnemy(game.whoE); // add another enemy
            }

        }
        */

        public void WhoDied(Character player, Character enemy)
        {
            bool a = !player.IsAlive();
            if (a)
            {
                if (player.Health < 0) player.Health = 0;
                GetInformedContinuoslyTb("you died" + Environment.NewLine);
                ((MainWindow)Application.Current.MainWindow).B_fight.IsEnabled = false;
            }

            else
            {
                GetInformedContinuoslyTb(enemy.Name + " died" + Environment.NewLine);
                player.LevelPlusPlus(); // players livelo + 1
                ai.NextEnemy(game.whoE); // add another enemy
            }

        }




        public string whoChoose(string whoCalled)
        {
            string retValue = "";
            switch (whoCalled)
            {
                case "player":
                    retValue = "player";
                    break;

                case "enemy":
                    retValue = "enemy";
                    break;
            }

            return retValue;
        }


        /// <summary> 
        /// <para>1 - player </para>
        /// <para>2 - enemy </para>
        /// <para>3 - arena </para>
        /// <para>4 - player + enemy </para>
        /// <para>7 - player + enemy + arena </para>
        /// </summary>
        /// <returns>void</returns>
        public void GetInformedIntoLabels(int choose, Character player, Character enemy)
        {
            switch (choose)
            {
                case 1:
                    ((MainWindow)Application.Current.MainWindow).l_playerDamage.Content = player.Damage;
                    ((MainWindow)Application.Current.MainWindow).l_playerDefense.Content = player.Defense;
                    ((MainWindow)Application.Current.MainWindow).l_playerHealth.Content = player.Health;
                    ((MainWindow)Application.Current.MainWindow).l_playerName.Content = player.Name;
                    ((MainWindow)Application.Current.MainWindow).l_playerLevel.Content = player.Level;
                    break;
                case 2:
                    ((MainWindow)Application.Current.MainWindow).l_enemyDamage.Content = enemy.Damage;
                    ((MainWindow)Application.Current.MainWindow).l_enemyDefense.Content = enemy.Defense;
                    ((MainWindow)Application.Current.MainWindow).l_enemyHealth.Content = enemy.Health;
                    ((MainWindow)Application.Current.MainWindow).l_enemyName.Content = enemy.Name;
                    break;
                case 3:
                    ((MainWindow)Application.Current.MainWindow).L_arena.Content = game.GetGameArenaLook();
                    break;
                case 4:
                    ((MainWindow)Application.Current.MainWindow).l_playerDamage.Content = player.Damage;
                    ((MainWindow)Application.Current.MainWindow).l_playerDefense.Content = player.Defense;
                    ((MainWindow)Application.Current.MainWindow).l_playerHealth.Content = player.Health;
                    ((MainWindow)Application.Current.MainWindow).l_playerName.Content = player.Name;
                    ((MainWindow)Application.Current.MainWindow).l_playerLevel.Content = player.Level;

                    ((MainWindow)Application.Current.MainWindow).l_enemyDamage.Content = enemy.Damage;
                    ((MainWindow)Application.Current.MainWindow).l_enemyDefense.Content = enemy.Defense;
                    ((MainWindow)Application.Current.MainWindow).l_enemyHealth.Content = enemy.Health;
                    ((MainWindow)Application.Current.MainWindow).l_enemyName.Content = enemy.Name;
                    break;
                case 7:
                    ((MainWindow)Application.Current.MainWindow).l_playerDamage.Content = player.Damage;
                    ((MainWindow)Application.Current.MainWindow).l_playerDefense.Content = player.Defense;
                    ((MainWindow)Application.Current.MainWindow).l_playerHealth.Content = player.Health;
                    ((MainWindow)Application.Current.MainWindow).l_playerName.Content = player.Name;
                    ((MainWindow)Application.Current.MainWindow).l_playerLevel.Content = player.Level;

                    ((MainWindow)Application.Current.MainWindow).l_enemyDamage.Content = enemy.Damage;
                    ((MainWindow)Application.Current.MainWindow).l_enemyDefense.Content = enemy.Defense;
                    ((MainWindow)Application.Current.MainWindow).l_enemyHealth.Content = enemy.Health;
                    ((MainWindow)Application.Current.MainWindow).l_enemyName.Content = enemy.Name;

                    ((MainWindow)Application.Current.MainWindow).L_arena.Content = game.GetGameArenaLook();
                    break;
            }
        }

        #region infoInTb/Mb
        public void GetInformedContinuoslyTb(Character player, Character enemy, string who, string withWhat, int howMuch)
        {
            //if player attacked
            if (who == player.Name)
            {
                ((MainWindow)Application.Current.MainWindow).Tb_get_informed.Text += ">> you attacked on " + game.enemy.Name + " with " + withWhat + " for " + howMuch + " damage!" + Environment.NewLine;
                ((MainWindow)Application.Current.MainWindow).Tb_get_informed.Text += "-------------------------------------" + Environment.NewLine;
            }

            //if enemy attacked
            else if (who == enemy.Name)
            {
                ((MainWindow)Application.Current.MainWindow).Tb_get_informed.Text += "<< " + who + " attacked on " + game.player.Name + " with " + withWhat + " for " + howMuch + " damage!" + Environment.NewLine;
                ((MainWindow)Application.Current.MainWindow).Tb_get_informed.Text += "-------------------------------------" + Environment.NewLine;
            }

            ((MainWindow)Application.Current.MainWindow).Tb_get_informed.ScrollToEnd();
        }

        public void GetInformedContinuoslyTb(string whateverYouWant)
        {
            ((MainWindow)Application.Current.MainWindow).Tb_get_informed.Text += whateverYouWant + Environment.NewLine;
            ((MainWindow)Application.Current.MainWindow).Tb_get_informed.Text += "-------------------------------------" + Environment.NewLine;
            ((MainWindow)Application.Current.MainWindow).Tb_get_informed.ScrollToEnd();
        }

        public void GetInformedContinuoslyMb(string who, string toWho, string withWhat, int howMuch)
        {
            MessageBox.Show(who + " attacked on " + toWho + " with " + withWhat + " for " + howMuch + " damage!");
        }

        public void GetInformedContinuoslyMb(string whateverYouWant)
        {
            MessageBox.Show(whateverYouWant);
        }
        #endregion

        /// <summary> 
        /// <para>1 - item name </para>
        /// <para>2 - item damage </para>
        /// <para>3 - item name + item damage </para>
        /// <para>4 - sword sharpness </para>
        /// <para>7 - item name + item damage + sword sharpness </para>
        /// </summary>
        /// <returns>void</returns>
        public string GetInventoryList(int whatDoYouWantToKnow)
        {
            string a = "";

            foreach (var item in game.inventoryListPlayer)
            {
                switch (whatDoYouWantToKnow)
                {
                    case 1:
                        a += Convert.ToString(item.GetItemName((Item)item));
                        break;
                    case 2:
                        a += Convert.ToString(item.GetItemDamage((Item)item));
                        break;
                    case 3:
                        a += Convert.ToString(item.GetItemName((Item)item) + " " + item.GetItemDamage((Item)item));
                        break;
                    case 4:
                        a += Convert.ToString(item.GetSwordSharpness());
                        break;
                    case 7:
                        a += Convert.ToString(item.GetItemName((Item)item) + " " + item.GetItemDamage((Item)item) + " " + item.GetSwordSharpness());
                        break;
                }

                a += Environment.NewLine;
            }

            return a;
        }
    }
    
    class Ai : EveryVariable
    {
        EveryVariable ev = new EveryVariable();
        WritingMethods wm = new WritingMethods();
        Creation creation = new Creation();
        
        public void HowToEnemy()
        {
            
            string enemyM = Convert.ToString(ev.Game.enemy.Name);
            string playerM = Convert.ToString(game.player.Name);

            if (game.enemy is Mage)
            {
                int attack0 = game.enemy.FireBall(); //make damage value attack of FireBall
                int attack1 = game.enemy.IceBall(); //make damage value attack of IceBall
                if (attack1 > attack0)
                {
                    int a = game.enemy.Fight(game.player, attack1); //get real damage and attack on player
                    wm.GetInformedContinuoslyTb(enemyM, "FireBall", a); //get player informed
                } //if one is more damagefull

                else
                {
                    int a = game.enemy.Fight(game.player, attack0); //get real damage and attack on player
                    wm.GetInformedContinuoslyTb(enemyM, "IceBall", a); //get player informed
                } //if another is more damagefull 
            }

            else if (game.enemy is Warrior)
            {
                int attack0 = game.enemy.LightAttack(game.inventoryEnemy.GetItemDamage((Item)game.inventoryListEnemy[0]));
                int attack1 = game.enemy.HeavyAttack(game.inventoryEnemy.GetItemDamage((Item)game.inventoryListEnemy[0]));

                if (attack1 > attack0)
                {
                    int a = game.enemy.Fight(game.player, attack1);
                    wm.GetInformedContinuoslyTb(enemyM, "Heavy Attack", a);
                }

                else
                {
                    int a = game.enemy.Fight(game.player, attack0);
                    wm.GetInformedContinuoslyTb(enemyM, "Light Attack", a);
                }
            }
        }

        public void HowToPlayer(int B_number)
        {
            if (game.player is Mage)
            {
                switch (B_number)
                {
                    case 0:
                        int attack = game.player.Fight(game.enemy, game.player.FireBall());
                        wm.GetInformedContinuoslyTb(game.player.Name, "FireBall", attack);
                        break;
                    case 1:
                        int attack1 = game.player.Fight(game.enemy, game.player.IceBall());
                        wm.GetInformedContinuoslyTb(game.player.Name, "IceBall", attack1);
                        break;
                }

            }

            else if (game.player is Warrior)
            {
                switch (B_number)
                {
                    //if 0 then attack with Heavy Attack
                    case 0:
                        int attack = game.player.Fight(game.enemy, game.player.HeavyAttack(game.inventoryPlayer.GetItemDamage((Item)game.inventoryListPlayer[0])));
                        wm.GetInformedContinuoslyTb(game.player.Name, "Heavy Attack", attack);
                        break;
                    //if 1 then attack with Light Attack
                    case 1:
                        int attack1 = game.player.Fight(game.enemy, game.player.LightAttack(game.inventoryPlayer.GetItemDamage((Item)game.inventoryListPlayer[0])));
                        wm.GetInformedContinuoslyTb(game.player.Name, "Light Attack", attack1);
                        break;
                }
            }
        }

        public void HowShouldEveryoneAttack(int B_number)
        {
            int whoWillAttackFirst = dice.DiceRoll();
            int whoAttacked;

            //if sude player will have the first attack
            if (whoWillAttackFirst % 2 == 0)
            {
                HowToPlayer(B_number);
                whoAttacked = 0;
            }

            //if liche player will have the first attack
            else
            {
                HowToEnemy();
                whoAttacked = 1;
            }

            wm.GetInformedIntoLabels(4);

            //based on whoattacked the other one will be next
            switch (whoAttacked)
            {
                //enemy
                case 0:
                    HowToEnemy();
                    break;
                //player
                case 1:
                    HowToPlayer(B_number);
                    break;
            }

            wm.GetInformedIntoLabels(4);

            if (!game.player.IsAlive() || !game.enemy.IsAlive())
                wm.WhoDied();
        }

        public void NextEnemy(int whoE)
        {
            enemy = creation.ChooseOfEnemy(whoE);
            wm.GetInformedIntoLabels(4);
        }
    }


    class EveryVariable
    {
        int whoE;
        Game game = null;
        WritingMethods wm = new WritingMethods();
        Dice dice = new Dice();

        public int WhoE { get { return whoE; } set { whoE = value; } }

        public Game Game { get { return game; } set { game = value; } }

    }
    #endregion
    
    public class Dice
    {
        static Random rnd = new Random();
        public Dice() { }
        
        public int DiceRoll() { return rnd.Next() + 1; }
        
        public int DiceRoll(int max) { return rnd.Next(max) + 1; }
    }
}
