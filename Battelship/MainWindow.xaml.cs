using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battelship
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Map playermap = new Map();
        public Map cpumap = new Map();

        public List<string> cpulocationslist = new List<string>();
        public List<string> cpu2strat = new List<string>();
        public List<string> cpu3strat = new List<string>();
        public List<string> cpu4strat = new List<string>();
        public List<string> cpu5strat = new List<string>();
        public List<string> playerguesseslist = new List<string>();

        public string userPlayerName = "player";
        public string aiPlayStyle = "Random";

        public bool verticalship = true;
        public bool carrierlinetest;
        public bool battleshiplinetest;
        public bool cruiserlinetest;
        public bool submarinelinetest;
        public bool destroyerlinetest;
        public bool carrierlineordertest;
        public bool battleshiplineordertest;
        public bool cruiserlineordertest;
        public bool submarinelineordertest;
        public bool destroyerlineordertest;
        


        public MainWindow()
        {
            InitializeComponent();
            gamewindowLaunch();       
        }

        private void gamewindowLaunch()
        {
            MainMenu.Visibility = Visibility.Visible;
            NewGame.Visibility = Visibility.Hidden;
            Settings.Visibility = Visibility.Hidden;
            QuitGame.Visibility = Visibility.Hidden;
            HowTo.Visibility = Visibility.Hidden;
            Setup.Visibility = Visibility.Hidden;
            // cpu initialises a list of all locations.
            foreach (var location in playermap.locations)
            {
                cpulocationslist.Add(location.ID);
            }

        }
        //MAIN MENU BUTTON EVENTS
        private void newGameClick(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            Setup.Visibility = Visibility.Visible;
            pleaseSelect.Visibility = Visibility.Hidden;
        }

        private void howToClick(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            HowTo.Visibility = Visibility.Visible;
        }

        private void settingsClick(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            Settings.Visibility = Visibility.Visible;
        }

        //QUIT BUTTON FUNCTION
        private void quitClick(object sender, RoutedEventArgs e)
        {
            QuitGame.Visibility = Visibility.Visible;
        }
        private void closeQuitBox(object sender, RoutedEventArgs e)
        {
            QuitGame.Visibility = Visibility.Hidden;
        }
        private void programQuitFunction(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // SETTINGS BUTTONS
        private void toMainMenu(object sender, RoutedEventArgs e)
        {
            
            HowTo.Visibility = Visibility.Hidden;
            MainMenu.Visibility = Visibility.Visible;
                       
        }
     
        private void toMainMenuUpdateSettings(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;            
            MainMenu.Visibility = Visibility.Visible;
            userPlayerName = playerName.Text;
           
        }        
        // PLAYER SETTING UP SHIPS
        private void selectedShip(object sender, RoutedEventArgs e) //know what ship is selected
        {
            Button shipname = sender as Button;
            List<string> greensquares = new List<string>();

            shipSelect.Content = shipname.Content.ToString();
            allPlaced.Visibility = Visibility.Hidden;
            pleaseSelect.Visibility = Visibility.Hidden; 
            foreach (var item in playermap.locations)
            {
                if (item.shipYN == "ship")
                {
                    greensquares.Add(item.ID);
                }
            }
           
        }       
        private void placeShipOnClick(object sender, RoutedEventArgs e) // click to place ship method
        {
            
            Button btn = sender as Button; //declares current spae as a button object
            allPlaced.Visibility = Visibility.Hidden;

            if (shipSelect.Content.ToString() == "Select ship") 
            { // error message
                pleaseSelect.Visibility = Visibility.Visible;
            }
            else // run code
            {                
                if (shipSelect.Content.ToString().Contains("Carrier")) // placing the Carrier 5 squares
                {
                    int c = 0;
                    foreach (var item in playermap.locations) // test how many carrier squares have been placed.
                    {
                        if (item.shipID == "carrier")
                        {
                            c++;
                        }
                    }
                    if (c == 5 && btn.Background == Brushes.Transparent) // if all are placed and user clicks again, tell them to select new ship.
                    {
                        allPlaced.Visibility = Visibility.Visible;
                    }
                    else // otherwise they are free to place another square.
                    {
                        
                        foreach (var place in playermap.locations)
                        {
                            if (btn.Content.ToString() == place.ID)
                            {
                                if (place.shipID == "battleship" || place.shipID == "cruiser" || place.shipID == "submarine" || place.shipID == "destroyer")
                                {

                                }
                                else
                                {
                                    if (btn.Background == Brushes.Transparent)
                                    {
                                        btn.Background = Brushes.Green;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "ship";
                                                item.shipID = "carrier";
                                                item.status = "afloat";
                                                item.shipSize = "5";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        btn.Background = Brushes.Transparent;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "";
                                                item.shipID = "";
                                                item.status = "miss";
                                                item.shipSize = "0";
                                            }
                                        }
                                    }
                                }
                            }
                        }                                               
                    }                                      
                }
                else if (shipSelect.Content.ToString().Contains("Battleship")) //Placing the Battleship 4 squares
                {
                    int c = 0;
                    foreach (var item in playermap.locations) // test how many carrier squares have been placed.
                    {
                        if (item.shipID == "battleship")
                        {
                            c++;
                        }
                    }
                    if (c == 4 && btn.Background == Brushes.Transparent)
                    {
                        allPlaced.Visibility = Visibility.Visible;
                    }
                    else // otherwise they are free to place another square.
                    {

                        foreach (var place in playermap.locations)
                        {
                            if (btn.Content.ToString() == place.ID)
                            {
                                if (place.shipID == "carrier" || place.shipID == "cruiser" || place.shipID == "submarine" || place.shipID == "destroyer")
                                {

                                }
                                else
                                {
                                    if (btn.Background == Brushes.Transparent)
                                    {
                                       
                                        btn.Background = Brushes.Black;
                                        btn.Foreground = Brushes.White;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "ship";
                                                item.shipID = "battleship";
                                                item.status = "afloat";
                                                item.shipSize = "4";
                                            }
                                        }

                                    }
                                    else
                                    {
                                        btn.Background = Brushes.Transparent;
                                        btn.Foreground = Brushes.Black;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "";
                                                item.shipID = "";
                                                item.status = "miss";
                                                item.shipSize = "0";
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                else if (shipSelect.Content.ToString().Contains("Cruiser")) //Placing the Cruiser 3 squares
                {
                    int c = 0;
                    foreach (var item in playermap.locations) // test how many carrier squares have been placed.
                    {
                        if (item.shipID == "cruiser")
                        {
                            c++;
                        }
                    }
                    if (c == 3 && btn.Background == Brushes.Transparent) // if all are placed and user clicks again, tell them to select new ship.
                    {
                        allPlaced.Visibility = Visibility.Visible;
                    }
                    else // otherwise they are free to place another square.
                    {

                        foreach (var place in playermap.locations)
                        {
                            if (btn.Content.ToString() == place.ID)
                            {
                                if (place.shipID == "carrier" || place.shipID == "battleship" || place.shipID == "submarine" || place.shipID == "destroyer")
                                {

                                }
                                else
                                {
                                    if (btn.Background == Brushes.Transparent)
                                    {
                                        btn.Background = Brushes.Yellow;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "ship";
                                                item.shipID = "cruiser";
                                                item.status = "afloat";
                                                item.shipSize = "3";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        btn.Background = Brushes.Transparent;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "";
                                                item.shipID = "";
                                                item.status = "miss";
                                                item.shipSize = "0";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (shipSelect.Content.ToString().Contains("Submarine")) //Placing the Submarine 3 squares
                {
                    int c = 0;
                    foreach (var item in playermap.locations) // test how many carrier squares have been placed.
                    {
                        if (item.shipID == "submarine")
                        {
                            c++;
                        }
                    }
                    if (c == 3 && btn.Background == Brushes.Transparent) // if all are placed and user clicks again, tell them to select new ship.
                    {
                        allPlaced.Visibility = Visibility.Visible;
                    }
                    else // otherwise they are free to place another square.
                    {

                        foreach (var place in playermap.locations)
                        {
                            if (btn.Content.ToString() == place.ID)
                            {
                                if (place.shipID == "carrier" || place.shipID == "battleship" || place.shipID == "cruiser" || place.shipID == "destroyer")
                                {

                                }
                                else
                                {
                                    if (btn.Background == Brushes.Transparent)
                                    {
                                        btn.Background = Brushes.Gray;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "ship";
                                                item.shipID = "submarine";
                                                item.status = "afloat";
                                                item.shipSize = "3";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        btn.Background = Brushes.Transparent;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "";
                                                item.shipID = "";
                                                item.status = "miss";
                                                item.shipSize = "0";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (shipSelect.Content.ToString().Contains("Destroyer")) //Placing the Destroyer 2 squares
                {
                    int c = 0;
                    foreach (var item in playermap.locations) // test how many carrier squares have been placed.
                    {
                        if (item.shipID == "destroyer")
                        {
                            c++;
                        }
                    }
                    if (c == 2 && btn.Background == Brushes.Transparent) // if all are placed and user clicks again, tell them to select new ship.
                    {
                        allPlaced.Visibility = Visibility.Visible;
                    }
                    else // otherwise they are free to place another square.
                    {

                        foreach (var place in playermap.locations)
                        {
                            if (btn.Content.ToString() == place.ID)
                            {
                                if (place.shipID == "carrier" || place.shipID == "battleship" || place.shipID == "cruiser" || place.shipID == "submarine")
                                {

                                }
                                else
                                {
                                    if (btn.Background == Brushes.Transparent)
                                    {
                                        btn.Background = Brushes.Pink;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "ship";
                                                item.shipID = "destroyer";
                                                item.status = "afloat";
                                                item.shipSize = "2";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        btn.Background = Brushes.Transparent;
                                        foreach (var item in playermap.locations)
                                        {
                                            if (btn.Content.ToString() == item.ID)
                                            {
                                                item.shipYN = "";
                                                item.shipID = "";
                                                item.status = "miss";
                                                item.shipSize = "0";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }
           
        }
        private void calculateUserShips(object sender, RoutedEventArgs e)
        {
            List<string> listOfShipIDs = new List<string>();
            foreach (var item in playermap.locations)
            {
                if (item.shipYN == "ship")
                {
                    listOfShipIDs.Add(item.ID);
                }
            }
            int p = listOfShipIDs.Count;          
            if (p != 17)
            {              
                invalidmessage.Content = "INVALID SHIP LAYOUT!!";
            }
            else
            {
                invalidmessage.Content = "";            
            }
            carriertest();
            battleshiptest();
            cruisertest();
            submarinetest();
            destroyertest();
            
            if (carrierlineordertest && battleshiplineordertest && cruiserlineordertest && submarinelineordertest && destroyerlineordertest)
            {
                Setup.Visibility = Visibility.Hidden;
                NewGame.Visibility = Visibility.Visible;
                cpuSetupShips();
                consoleBox.Content = "Welcome: " + userPlayerName;
                
            }
            else
            {
                invalidmessage.Content = "INVALID SHIP LAYOUT!!";
            }
            
        } // this button goes into the gameplay function and ai setupships
        private void carriertest()
        { // parameters list
            List<int> carrierx = new List<int>();
            List<int> carriery = new List<int>();
            int x1; int x2; int x3; int x4; int x5;
            int y1; int y2; int y3; int y4; int y5;
            //find every carrier item in list of locations
            foreach (var item in playermap.locations)
            {
               if (item.shipID == "carrier") // put every x coord and y coord into a list
               {
                    carrierx.Add(item.xcoord);
                    carriery.Add(item.ycoord);
               }
            }
            // test if xcoords are in a line: return true if in line
            int linex = 0;
            int liney = 0;
            if (carrierx[0] == carrierx[1] && carrierx[0] == carrierx[2] && carrierx[0] == carrierx[3] && carrierx[0] == carrierx[4])
            {
                carrierlinetest = true;
                //now test for sequential order if it is infact in a line.
                carriery.Sort();
                if (carriery[0] + 1 == carriery[1] && carriery[1] + 1 == carriery[2] && carriery[2] + 1 == carriery[3] && carriery[3] + 1 == carriery[4])
                {
                    carrierlineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
               //will return default false
            }
            //test if ycoords are in a line: return true if in line
            if (carriery[0] == carriery[1] && carriery[0] == carriery[2] && carriery[0] == carriery[3] && carriery[0] == carriery[4])
            {
                carrierlinetest = true;
                //now test for sequential order if it is infact in a line.
                carrierx.Sort();
                if (carrierx[0] + 1 == carrierx[1] && carrierx[1] + 1 == carrierx[2] && carrierx[2] + 1 == carrierx[3] && carrierx[3] + 1 == carrierx[4])
                {
                    carrierlineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
                   

        }
        private void battleshiptest()
        {
            // parameters list
            List<int> battleshipx = new List<int>();
            List<int> battleshipy = new List<int>();            
            int x1; int x2; int x3; int x4;
            int y1; int y2; int y3; int y4;
            //find every battleship item in list of locations
            foreach (var item in playermap.locations)
            {
                if (item.shipID == "battleship") // put every x coord and y coord into a list
                {
                    battleshipx.Add(item.xcoord);
                    battleshipy.Add(item.ycoord);
                }
            }
            // test if xcoords are in a line: return true if in line
            int linex = 0;
            int liney = 0;
            if (battleshipx[0] == battleshipx[1] && battleshipx[0] == battleshipx[2] && battleshipx[0] == battleshipx[3])
            {
                battleshiplinetest = true;
                //now test for sequential order if it is infact in a line.
                battleshipy.Sort();
                if (battleshipy[0] + 1 == battleshipy[1] && battleshipy[1] + 1 == battleshipy[2] && battleshipy[2] + 1 == battleshipy[3])
                {
                    battleshiplineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
            //test if ycoords are in a line: return true if in line
            if (battleshipy[0] == battleshipy[1] && battleshipy[0] == battleshipy[2] && battleshipy[0] == battleshipy[3])
            {
                battleshiplinetest = true;
                //now test for sequential order if it is infact in a line.
                battleshipx.Sort();
                if (battleshipx[0] + 1 == battleshipx[1] && battleshipx[1] + 1 == battleshipx[2] && battleshipx[2] + 1 == battleshipx[3])
                {
                    battleshiplineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
        }
        private void cruisertest()
        {
            // parameters list
            List<int> cruiserx = new List<int>();
            List<int> cruisery = new List<int>();
            int x1; int x2; int x3;
            int y1; int y2; int y3;
            //find every cruiser item in list of locations
            foreach (var item in playermap.locations)
            {
                if (item.shipID == "cruiser") // put every x coord and y coord into a list
                {
                    cruiserx.Add(item.xcoord);
                    cruisery.Add(item.ycoord);
                }
            }
            // test if xcoords are in a line: return true if in line
            int linex = 0;
            int liney = 0;
            if (cruiserx[0] == cruiserx[1] && cruiserx[0] == cruiserx[2])
            {
                cruiserlinetest = true;
                //now test for sequential order if it is infact in a line.
                cruisery.Sort();
                if (cruisery[0] + 1 == cruisery[1] && cruisery[1] + 1 == cruisery[2])
                {
                    cruiserlineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
            //test if ycoords are in a line: return true if in line
            if (cruisery[0] == cruisery[1] && cruisery[0] == cruisery[2])
            {
                cruiserlinetest = true;
                //now test for sequential order if it is infact in a line.
                cruiserx.Sort();
                if (cruiserx[0] + 1 == cruiserx[1] && cruiserx[1] + 1 == cruiserx[2])
                {
                    cruiserlineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
        }
        private void submarinetest()
        {
            // parameters list
            List<int> submarinex = new List<int>();
            List<int> submariney = new List<int>();
            int x1; int x2; int x3;
            int y1; int y2; int y3;
            //find every submarine item in list of locations
            foreach (var item in playermap.locations)
            {
                if (item.shipID == "submarine") // put every x coord and y coord into a list
                {
                    submarinex.Add(item.xcoord);
                    submariney.Add(item.ycoord);
                }
            }
            // test if xcoords are in a line: return true if in line
            int linex = 0;
            int liney = 0;
            if (submarinex[0] == submarinex[1] && submarinex[0] == submarinex[2])
            {
                submarinelinetest = true;
                //now test for sequential order if it is infact in a line.
                submariney.Sort();
                if (submariney[0] + 1 == submariney[1] && submariney[1] + 1 == submariney[2])
                {
                    submarinelineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
            //test if ycoords are in a line: return true if in line
            if (submariney[0] == submariney[1] && submariney[0] == submariney[2])
            {
                submarinelinetest = true;
                //now test for sequential order if it is infact in a line.
                submarinex.Sort();
                if (submarinex[0] + 1 == submarinex[1] && submarinex[1] + 1 == submarinex[2])
                {
                    submarinelineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
        }
        private void destroyertest()
        {
            // parameters list
            List<int> destroyerx = new List<int>();
            List<int> destroyery = new List<int>();
            int x1; int x2;
            int y1; int y2;
            //find every destroyer item in list of locations
            foreach (var item in playermap.locations)
            {
                if (item.shipID == "destroyer") // put every x coord and y coord into a list
                {
                    destroyerx.Add(item.xcoord);
                    destroyery.Add(item.ycoord);
                }
            }
            // test if xcoords are in a line: return true if in line
            int linex = 0;
            int liney = 0;
            if (destroyerx[0] == destroyerx[1])
            {
                destroyerlinetest = true;
                //now test for sequential order if it is infact in a line.
                destroyery.Sort();
                if (destroyery[0] + 1 == destroyery[1])
                {
                    destroyerlineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
            //test if ycoords are in a line: return true if in line
            if (destroyery[0] == destroyery[1])
            {
                destroyerlinetest = true;
                //now test for sequential order if it is infact in a line.
                destroyerx.Sort();
                if (destroyerx[0] + 1 == destroyerx[1])
                {
                    destroyerlineordertest = true;
                }
                else
                {
                    //will return default false
                }
            }
            else
            {
                //will return default false
            }
        }
        //CPU SHIP SETTING UP
        private void cpuSetupShips()
        {
            cpucarriersetup();
            cpubattleshipsetup();
            cpucruisersetup();
            cpusubmarinesetup();
            cpudestroyersetup();
        }
        private void cpucarriersetup()
        {
            Random rnd = new Random();
            // initilaise values
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int x3 = 0;
            int y3 = 0;
            int x4 = 0;
            int y4 = 0;
            int x5 = 0;
            int y5 = 0;
            int p = 0;

            while (p <= 4)  // will keep choosing spot for ship until it does not collide with another ship
            {
                int i = rnd.Next(2);        // make AI choose 1: horizontal, or 0: vertical
                x1 = rnd.Next(10);    // randomises x1 and y1
                y1 = rnd.Next(10);
                if (i == 0) // Vertical positioning of object (change y values only)
                {   //keep x axis the same                 
                    x2 = x1;
                    x3 = x1;
                    x4 = x1;
                    x5 = x1;
                    if (y1 >= 7) // if object lies on top edge
                    {
                        y2 = y1 - 1;
                        y3 = y1 - 2;
                        y4 = y1 - 3;
                        y5 = y1 - 4;
                    }
                    else if (y1 <= 2) // if object lies on bottom edge
                    {
                        y2 = y1 + 1;
                        y3 = y1 + 2;
                        y4 = y1 + 3;
                        y5 = y1 + 4;
                    }
                    else // all other cases between 
                    {
                        y2 = y1 + 1;
                        y3 = y1 - 1;
                        y4 = y1 + 2;
                        y5 = y1 - 2;
                    }

                }
                else // ie. i == 1
                {
                    y2 = y1;
                    y3 = y1;
                    y4 = y1;
                    y5 = y1;
                    if (x1 >= 7) // if object lies on right edge
                    {
                        x2 = x1 - 1;
                        x3 = x1 - 2;
                        x4 = x1 - 3;
                        x5 = x1 - 4;
                    }
                    else if (x1 <= 2) // if object lies on left edge
                    {
                        x2 = x1 + 1;
                        x3 = x1 + 2;
                        x4 = x1 + 3;
                        x5 = x1 + 4;
                    }
                    else // all other cases between 
                    {
                        x2 = x1 + 1;
                        x3 = x1 - 1;
                        x4 = x1 + 2;
                        x5 = x1 - 2;
                    }
                }

                p = 0;
                foreach (var location in cpumap.locations) // check for ship already at all locations.
                {
                    if (location.xcoord == x1 && location.ycoord == y1)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x2 && location.ycoord == y2)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x3 && location.ycoord == y3)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x4 && location.ycoord == y4)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x5 && location.ycoord == y5)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }

                }

            }

            foreach (var location in cpumap.locations)
            {
                if (location.xcoord == x1 && location.ycoord == y1)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "carrier";
                    location.shipSize = "5";
                }
                if (location.xcoord == x2 && location.ycoord == y2)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "carrier";
                    location.shipSize = "5";
                }
                if (location.xcoord == x3 && location.ycoord == y3)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "carrier";
                    location.shipSize = "5";
                }
                if (location.xcoord == x4 && location.ycoord == y4)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "carrier";
                    location.shipSize = "5";
                }
                if (location.xcoord == x5 && location.ycoord == y5)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "carrier";
                    location.shipSize = "5";
                }
            }
        }
        private void cpubattleshipsetup()
        {
            Random rnd = new Random();
            // initilaise values
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int x3 = 0;
            int y3 = 0;
            int x4 = 0;
            int y4 = 0;
            int p = 0;

            while (p <= 3)  // will keep choosing spot for ship until it does not collide with another ship
            {
                int i = rnd.Next(2);        // make AI choose 1: horizontal, or 0: vertical
                x1 = rnd.Next(10);    // randomises x1 and y1
                y1 = rnd.Next(10);
                if (i == 0) // Vertical positioning of object (change y values only)
                {   //keep x axis the same                 
                    x2 = x1;
                    x3 = x1;
                    x4 = x1;
                    if (y1 >= 8) // if object lies on top edge
                    {
                        y2 = y1 - 1;
                        y3 = y1 - 2;
                        y4 = y1 - 3;
                    }
                    else if (y1 <= 1) // if object lies on bottom edge
                    {
                        y2 = y1 + 1;
                        y3 = y1 + 2;
                        y4 = y1 + 3;
                    }
                    else // all other cases between 
                    {
                        y2 = y1 + 1;
                        y3 = y1 - 1;
                        y4 = y1 + 2;
                    }

                }
                else // ie. i == 1
                {
                    y2 = y1;
                    y3 = y1;
                    y4 = y1;
                    if (x1 >= 8) // if object lies on right edge
                    {
                        x2 = x1 - 1;
                        x3 = x1 - 2;
                        x4 = x1 - 3;
                    }
                    else if (x1 <= 1) // if object lies on left edge
                    {
                        x2 = x1 + 1;
                        x3 = x1 + 2;
                        x4 = x1 + 3;
                    }
                    else // all other cases between 
                    {
                        x2 = x1 + 1;
                        x3 = x1 - 1;
                        x4 = x1 + 2;
                    }
                }

                p = 0;
                foreach (var location in cpumap.locations) // check for ship already at all locations.
                {
                    if (location.xcoord == x1 && location.ycoord == y1)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x2 && location.ycoord == y2)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x3 && location.ycoord == y3)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x4 && location.ycoord == y4)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }

                }

            }

            foreach (var location in cpumap.locations)
            {
                if (location.xcoord == x1 && location.ycoord == y1)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "battleship";
                    location.shipSize = "4";
                }
                if (location.xcoord == x2 && location.ycoord == y2)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "battleship";
                    location.shipSize = "4";
                }
                if (location.xcoord == x3 && location.ycoord == y3)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "battleship";
                    location.shipSize = "4";
                }
                if (location.xcoord == x4 && location.ycoord == y4)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "battleship";
                    location.shipSize = "4";
                }
            }
        }
        private void cpucruisersetup()
        {
            Random rnd = new Random();
            // initilaise values
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int x3 = 0;
            int y3 = 0;
            int p = 0;

            while (p <= 2)  // will keep choosing spot for ship until it does not collide with another ship
            {
                int i = rnd.Next(2);        // make AI choose 1: horizontal, or 0: vertical
                x1 = rnd.Next(10);    // randomises x1 and y1
                y1 = rnd.Next(10);
                if (i == 0) // Vertical positioning of object (change y values only)
                {   //keep x axis the same                 
                    x2 = x1;
                    x3 = x1;
                    if (y1 == 9) // if object lies on top edge
                    {
                        y2 = y1 - 1;
                        y3 = y1 - 2;
                    }
                    else if (y1 == 0) // if object lies on bottom edge
                    {
                        y2 = y1 + 1;
                        y3 = y1 + 2;
                    }
                    else // all other cases between 
                    {
                        y2 = y1 + 1;
                        y3 = y1 - 1;
                    }

                }
                else // ie. i == 1
                {
                    y2 = y1;
                    y3 = y1;
                    if (x1 == 9) // if object lies on top edge
                    {
                        x2 = x1 - 1;
                        x3 = x1 - 2;
                    }
                    else if (x1 == 0) // if object lies on bottom edge
                    {
                        x2 = x1 + 1;
                        x3 = x1 + 2;
                    }
                    else // all other cases between 
                    {
                        x2 = x1 + 1;
                        x3 = x1 - 1;
                    }
                }

                p = 0;
                foreach (var location in cpumap.locations) // check for ship already at all locations.
                {
                    if (location.xcoord == x1 && location.ycoord == y1)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x2 && location.ycoord == y2)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x3 && location.ycoord == y3)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }

                }

            }

            foreach (var location in cpumap.locations)
            {
                if (location.xcoord == x1 && location.ycoord == y1)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "cruiser";
                    location.shipSize = "3";
                }
                if (location.xcoord == x2 && location.ycoord == y2)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "cruiser";
                    location.shipSize = "3";
                }
                if (location.xcoord == x3 && location.ycoord == y3)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "cruiser";
                    location.shipSize = "3";
                }
            }
        }
        private void cpusubmarinesetup()
        {
            Random rnd = new Random();
            // initilaise values
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int x3 = 0;
            int y3 = 0;
            int p = 0;

            while (p <= 2)  // will keep choosing spot for ship until it does not collide with another ship
            {
                int i = rnd.Next(2);        // make AI choose 1: horizontal, or 0: vertical
                x1 = rnd.Next(10);    // randomises x1 and y1
                y1 = rnd.Next(10);
                if (i == 0) // Vertical positioning of object (change y values only)
                {   //keep x axis the same                 
                    x2 = x1;
                    x3 = x1;
                    if (y1 == 9) // if object lies on top edge
                    {
                        y2 = y1 - 1;
                        y3 = y1 - 2;
                    }
                    else if (y1 == 0) // if object lies on bottom edge
                    {
                        y2 = y1 + 1;
                        y3 = y1 + 2;
                    }
                    else // all other cases between 
                    {
                        y2 = y1 + 1;
                        y3 = y1 - 1;
                    }

                }
                else // ie. i == 1
                {
                    y2 = y1;
                    y3 = y1;
                    if (x1 == 9) // if object lies on top edge
                    {
                        x2 = x1 - 1;
                        x3 = x1 - 2;
                    }
                    else if (x1 == 0) // if object lies on bottom edge
                    {
                        x2 = x1 + 1;
                        x3 = x1 + 2;
                    }
                    else // all other cases between 
                    {
                        x2 = x1 + 1;
                        x3 = x1 - 1;
                    }
                }

                p = 0;
                foreach (var location in cpumap.locations) // check for ship already at all locations.
                {
                    if (location.xcoord == x1 && location.ycoord == y1)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x2 && location.ycoord == y2)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x3 && location.ycoord == y3)
                    {
                        if (location.shipYN == "ship")
                        {

                        }
                        else
                        {
                            p++;
                        }
                    }

                }

            }

            foreach (var location in cpumap.locations)
            {
                if (location.xcoord == x1 && location.ycoord == y1)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "submarine";
                    location.shipSize = "3";
                }
                if (location.xcoord == x2 && location.ycoord == y2)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "submarine";
                    location.shipSize = "3";
                }
                if (location.xcoord == x3 && location.ycoord == y3)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "submarine";
                    location.shipSize = "3";
                }
            }
        }
        private void cpudestroyersetup()
        {
            Random rnd = new Random();
            // initialise values
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int p = 0;

            while (p <= 1)  // will keep choosing spot for ship until it does not collide with another ship
            {
                int i = rnd.Next(2);        // make AI choose 1: horizontal, or 0: vertical
                x1 = rnd.Next(10);    // chooses random x axis value
                y1 = rnd.Next(10);
                if (i == 0) // Vertical positioning of object (change y values only)
                {   //keep x axis the same                 
                    x2 = x1;

                    if (y1 == 9) // if object lies on top edge
                    {
                        y2 = y1 - 1;

                    }
                    else if (y1 == 0) // if object lies on bottom edge
                    {
                        y2 = y1 + 1;

                    }
                    else // all other cases between 
                    {
                        y2 = y1 - 1;
                    }

                }
                else // ie. i == 1
                {
                    y2 = y1;
                    if (x1 == 9) // if object lies on top edge
                    {
                        x2 = x1 - 1;

                    }
                    else if (x1 == 0) // if object lies on bottom edge
                    {
                        x2 = x1 + 1;

                    }
                    else // all other cases between 
                    {
                        x2 = x1 + 1;

                    }
                }

                p = 0;
                foreach (var location in cpumap.locations) // check for ship already at all locations.
                {
                    if (location.xcoord == x1 && location.ycoord == y1)
                    {
                        if (location.shipYN == "ship")
                        {
                            break;
                        }
                        else
                        {
                            p++;
                        }
                    }
                    if (location.xcoord == x2 && location.ycoord == y2)
                    {
                        if (location.shipYN == "ship")
                        {
                            break;
                        }
                        else
                        {
                            p++;
                        }
                    }


                }

            }

            foreach (var location in cpumap.locations)
            {
                if (location.xcoord == x1 && location.ycoord == y1)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "destroyer";
                    location.shipSize = "2";
                }
                if (location.xcoord == x2 && location.ycoord == y2)
                {
                    location.shipYN = "ship";
                    location.status = "afloat";
                    location.shipID = "destroyer";
                    location.shipSize = "2";
                }
            }
        }
        // GAMEPLAY
        private void makeGuessOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            
            int t = 0;
            if (btn.Background == Brushes.Transparent) // if square is unguessed
            {
                foreach (var item in cpumap.locations) //lookup list of locations
                {
                    if (item.ID == btn.Content.ToString()) // find square name in list
                    {
                        if (item.shipYN == "ship") // if there is a ship at location
                        {
                            btn.Background = Brushes.Red;
                            item.status = "hit";
                            int i = 0;
                            foreach (var loc in cpumap.locations) // check if it is a hit or sunk
                            {
                                if (loc.shipID == item.shipID)
                                {
                                    if (loc.status == "hit")
                                    {
                                        i++;
                                    }                                   
                                }
                            }
                            if (i.ToString() == item.shipSize) // sunk = all parts being hit
                            {
                                foreach (var part in cpumap.locations)
                                {
                                    if (part.shipID == item.shipID)
                                    {
                                        part.status = "sunk";
                                        consoleBox.Content = btn.Content.ToString() + ": you SUNK! a " + item.shipID;
                                    }
                                }
                            }
                            else
                            {
                                consoleBox.Content = btn.Content.ToString() + ": Hit!";
                            }
                        }
                        else
                        {
                            consoleBox.Content = btn.Content.ToString() + ": Miss... ";
                            btn.Background = Brushes.White;
                            btn.Foreground = Brushes.Black;
                        }
                        t++;
                    }                    
                }                
            }
            else
            {
                consoleBox.Content = btn.Content.ToString() + ": You already guessed that one";
            }                   
            if (t == 1)
            {
                int g = 0;
                foreach (var guesses in cpumap.locations)
                {
                    if (guesses.status == "sunk")
                    {
                        g++;
                    }
                }
                if (g == 17)
                {
                    consoleBox.Content = userPlayerName + " Wins! well done" + Environment.NewLine;
                }
                else
                {
                    cpuMakesGuess();
                }
            }                                 
        }
        private void cpuMakesGuess()
        {
            Random rnd = new Random();

            int x = cpulocationslist.Count;
            int index = rnd.Next(x);

            string guess = cpulocationslist[index];
            cpulocationslist.RemoveAt(index);

            foreach (var item in playermap.locations)
            {
                if (item.ID == guess)
                {
                    if (item.shipYN == "ship") // cpu hits
                    {
                        item.status = "hit";
                        int i = 0;
                        foreach (var loc in cpumap.locations)
                        {
                            if (loc.shipID == item.shipID)
                            {
                                if (loc.status == "hit")
                                {
                                    i++;
                                }
                            }
                        }
                        if (i.ToString() == item.shipSize)
                        {
                            foreach (var part in cpumap.locations)
                            {
                                if (part.shipID == item.shipID)
                                {
                                    part.status = "sunk";
                                    consoleBox.Content = consoleBox.Content + Environment.NewLine + Environment.NewLine + "cpu @ " + guess + ": SUNK! your " + item.shipID;
                                }
                            }
                        }
                        else
                        {
                            consoleBox.Content = consoleBox.Content + Environment.NewLine + Environment.NewLine + "cpu @ " + guess + ": Hit!";
                        }
                    }
                    else // cpu misses
                    {
                        consoleBox.Content = consoleBox.Content + Environment.NewLine + Environment.NewLine + "cpu missed @ " + guess;
                    }
                }
            }

            int g = 0;
            foreach (var guesses in cpumap.locations)
            {
                if (guesses.status == "sunk")
                {
                    g++;
                }
            }
            if (g == 17)
            {
                consoleBox.Content = "CPU wins! better luck next time" + Environment.NewLine;
            }
        }       
    }


    public class Map
    {
        public List<Location> locations = new List<Location>();


        public Map()
        {

            locations.Add(new Location("A10", "miss", 0, 0, "", "", "0"));
            locations.Add(new Location("B10", "miss", 0, 1, "", "", "0"));
            locations.Add(new Location("C10", "miss", 0, 2, "", "", "0"));
            locations.Add(new Location("D10", "miss", 0, 3, "", "", "0"));
            locations.Add(new Location("E10", "miss", 0, 4, "", "", "0"));
            locations.Add(new Location("F10", "miss", 0, 5, "", "", "0"));
            locations.Add(new Location("G10", "miss", 0, 6, "", "", "0"));
            locations.Add(new Location("H10", "miss", 0, 7, "", "", "0"));
            locations.Add(new Location("I10", "miss", 0, 8, "", "", "0"));
            locations.Add(new Location("J10", "miss", 0, 9, "", "", "0"));

            locations.Add(new Location("A9", "miss", 1, 0, "", "", "0"));
            locations.Add(new Location("B9", "miss", 1, 1, "", "", "0"));
            locations.Add(new Location("C9", "miss", 1, 2, "", "", "0"));
            locations.Add(new Location("D9", "miss", 1, 3, "", "", "0"));
            locations.Add(new Location("E9", "miss", 1, 4, "", "", "0"));
            locations.Add(new Location("F9", "miss", 1, 5, "", "", "0"));
            locations.Add(new Location("G9", "miss", 1, 6, "", "", "0"));
            locations.Add(new Location("H9", "miss", 1, 7, "", "", "0"));
            locations.Add(new Location("I9", "miss", 1, 8, "", "", "0"));
            locations.Add(new Location("J9", "miss", 1, 9, "", "", "0"));

            locations.Add(new Location("A8", "miss", 2, 0, "", "", "0"));
            locations.Add(new Location("B8", "miss", 2, 1, "", "", "0"));
            locations.Add(new Location("C8", "miss", 2, 2, "", "", "0"));
            locations.Add(new Location("D8", "miss", 2, 3, "", "", "0"));
            locations.Add(new Location("E8", "miss", 2, 4, "", "", "0"));
            locations.Add(new Location("F8", "miss", 2, 5, "", "", "0"));
            locations.Add(new Location("G8", "miss", 2, 6, "", "", "0"));
            locations.Add(new Location("H8", "miss", 2, 7, "", "", "0"));
            locations.Add(new Location("I8", "miss", 2, 8, "", "", "0"));
            locations.Add(new Location("J8", "miss", 2, 9, "", "", "0"));

            locations.Add(new Location("A7", "miss", 3, 0, "", "", "0"));
            locations.Add(new Location("B7", "miss", 3, 1, "", "", "0"));
            locations.Add(new Location("C7", "miss", 3, 2, "", "", "0"));
            locations.Add(new Location("D7", "miss", 3, 3, "", "", "0"));
            locations.Add(new Location("E7", "miss", 3, 4, "", "", "0"));
            locations.Add(new Location("F7", "miss", 3, 5, "", "", "0"));
            locations.Add(new Location("G7", "miss", 3, 6, "", "", "0"));
            locations.Add(new Location("H7", "miss", 3, 7, "", "", "0"));
            locations.Add(new Location("I7", "miss", 3, 8, "", "", "0"));
            locations.Add(new Location("J7", "miss", 3, 9, "", "", "0"));

            locations.Add(new Location("A6", "miss", 4, 0, "", "", "0"));
            locations.Add(new Location("B6", "miss", 4, 1, "", "", "0"));
            locations.Add(new Location("C6", "miss", 4, 2, "", "", "0"));
            locations.Add(new Location("D6", "miss", 4, 3, "", "", "0"));
            locations.Add(new Location("E6", "miss", 4, 4, "", "", "0"));
            locations.Add(new Location("F6", "miss", 4, 5, "", "", "0"));
            locations.Add(new Location("G6", "miss", 4, 6, "", "", "0"));
            locations.Add(new Location("H6", "miss", 4, 7, "", "", "0"));
            locations.Add(new Location("I6", "miss", 4, 8, "", "", "0"));
            locations.Add(new Location("J6", "miss", 4, 9, "", "", "0"));

            locations.Add(new Location("A5", "miss", 5, 0, "", "", "0"));
            locations.Add(new Location("B5", "miss", 5, 1, "", "", "0"));
            locations.Add(new Location("C5", "miss", 5, 2, "", "", "0"));
            locations.Add(new Location("D5", "miss", 5, 3, "", "", "0"));
            locations.Add(new Location("E5", "miss", 5, 4, "", "", "0"));
            locations.Add(new Location("F5", "miss", 5, 5, "", "", "0"));
            locations.Add(new Location("G5", "miss", 5, 6, "", "", "0"));
            locations.Add(new Location("H5", "miss", 5, 7, "", "", "0"));
            locations.Add(new Location("I5", "miss", 5, 8, "", "", "0"));
            locations.Add(new Location("J5", "miss", 5, 9, "", "", "0"));

            locations.Add(new Location("A4", "miss", 6, 0, "", "", "0"));
            locations.Add(new Location("B4", "miss", 6, 1, "", "", "0"));
            locations.Add(new Location("C4", "miss", 6, 2, "", "", "0"));
            locations.Add(new Location("D4", "miss", 6, 3, "", "", "0"));
            locations.Add(new Location("E4", "miss", 6, 4, "", "", "0"));
            locations.Add(new Location("F4", "miss", 6, 5, "", "", "0"));
            locations.Add(new Location("G4", "miss", 6, 6, "", "", "0"));
            locations.Add(new Location("H4", "miss", 6, 7, "", "", "0"));
            locations.Add(new Location("I4", "miss", 6, 8, "", "", "0"));
            locations.Add(new Location("J4", "miss", 6, 9, "", "", "0"));

            locations.Add(new Location("A3", "miss", 7, 0, "", "", "0"));
            locations.Add(new Location("B3", "miss", 7, 1, "", "", "0"));
            locations.Add(new Location("C3", "miss", 7, 2, "", "", "0"));
            locations.Add(new Location("D3", "miss", 7, 3, "", "", "0"));
            locations.Add(new Location("E3", "miss", 7, 4, "", "", "0"));
            locations.Add(new Location("F3", "miss", 7, 5, "", "", "0"));
            locations.Add(new Location("G3", "miss", 7, 6, "", "", "0"));
            locations.Add(new Location("H3", "miss", 7, 7, "", "", "0"));
            locations.Add(new Location("I3", "miss", 7, 8, "", "", "0"));
            locations.Add(new Location("J3", "miss", 7, 9, "", "", "0"));

            locations.Add(new Location("A2", "miss", 8, 0, "", "", "0"));
            locations.Add(new Location("B2", "miss", 8, 1, "", "", "0"));
            locations.Add(new Location("C2", "miss", 8, 2, "", "", "0"));
            locations.Add(new Location("D2", "miss", 8, 3, "", "", "0"));
            locations.Add(new Location("E2", "miss", 8, 4, "", "", "0"));
            locations.Add(new Location("F2", "miss", 8, 5, "", "", "0"));
            locations.Add(new Location("G2", "miss", 8, 6, "", "", "0"));
            locations.Add(new Location("H2", "miss", 8, 7, "", "", "0"));
            locations.Add(new Location("I2", "miss", 8, 8, "", "", "0"));
            locations.Add(new Location("J2", "miss", 8, 9, "", "", "0"));
                        
            locations.Add(new Location("A1", "miss", 9, 0, "", "", "0"));
            locations.Add(new Location("B1", "miss", 9, 1, "", "", "0"));
            locations.Add(new Location("C1", "miss", 9, 2, "", "", "0"));
            locations.Add(new Location("D1", "miss", 9, 3, "", "", "0"));
            locations.Add(new Location("E1", "miss", 9, 4, "", "", "0"));
            locations.Add(new Location("F1", "miss", 9, 5, "", "", "0"));
            locations.Add(new Location("G1", "miss", 9, 6, "", "", "0"));
            locations.Add(new Location("H1", "miss", 9, 7, "", "", "0"));
            locations.Add(new Location("I1", "miss", 9, 8, "", "", "0"));
            locations.Add(new Location("J1", "miss", 9, 9, "", "", "0"));

        }
    }
    public class Location
    {
        public string ID;
        public int xcoord;
        public int ycoord;
        public string status;
        public string shipYN;
        public string shipID;
        public string shipSize;

        public Location(string ID, string status, int xcoord, int ycoord, string shipYN, string shipID, string shipSize)
        {
            this.ID = ID;
            this.status = status;
            this.xcoord = xcoord;
            this.ycoord = ycoord;
            this.shipYN = shipYN;
            this.shipID = shipID;
            this.shipSize = shipSize;
            
        }
    }
}
