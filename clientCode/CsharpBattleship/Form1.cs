using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CsharpBattleship
{
    public partial class Form1 : Form
    {
        // BUGS //
        // resizing window causes panel to erase. Force window size or redraw.
        // FIXED - changed line "if (size>=30)" to "if (size == 31 || size == 32)" - mashing r while placing submarine or destroyer can lead to multiple being placed?

        // NEXT UP... //
        // DONE - ship rotation. very important. rotate using r key.
        // DONE - move all the draw stuff to it's own function
        // DONE - count how many of each type are placed
        // DONE - stop after all pieces are placed
        // DONE - keep track of which dots belong to a group (which dots form a ship)
        // right click to remove ship (only possible after above step is completed)
        // prevent placement on rough waters (which also means implementing rough waters)



        private Point panelSize;
        private bool mouseDown;
        private bool rotateNext;
        private bool rotatePrevious;
        private int currentShipCode;

        private Point mousePosition;

        private GridSquare[,] playerGridSquares;
        private GridSquare currentPlayerGridsquare;
        private GridSquare[,] enemyGridSquares;
        //private GridSquare currentEnemyGridsquare;

        private int xGridSquareSize;
        private int yGridSquareSize;

        private Drawer playerDraw;
        private Drawer enemyDraw;

        private bool gameRunning;// false means game is in ship placing state. true means game is started (or seeking opponent)

        private bool aircraftCarrierPlaced; // ship code = 5
        private bool battleshipPlaced;      // ship code = 4
        private bool submarinePlaced;       // ship code = 31
        private bool destroyerPlaced;       // ship code = 32
        private bool patrolBoatPlaced;      // ship code = 2

        private bool wentFirst; // For LOG!
        private bool isFirstMessage = true; // For LOG!
        private int roundCount = 1; // LOG! 

        int gameEndCounter = 0; // when counter = 17, end game

        int aircraftCarrierHitsRemaining = 5;
        int battleshipsHitsRemaining = 4;
        int destroyerHitsRemainging = 3;
        int submarineHitsRemaining = 3;
        int patrolBoatHitsRemaining = 2;


       public void GameFound(string initialCommand)
       {
            Console.WriteLine("Game Found");
            Console.WriteLine("Initial Command is: " + initialCommand + " for " + this.Text);
       }

        public Form1(string idNo)
        {
            InitializeComponent();
            this.Text = "Game Window "+idNo;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            triggerLabel.Visible = false;

            gameRunning = false;

            aircraftCarrierPlaced = false;
            battleshipPlaced = false;
            submarinePlaced = false;
            destroyerPlaced = false;
            patrolBoatPlaced = false;


            mouseDown = false;// keep writing not used, should honestly just get rid of it (but don't)
            rotateNext = false;
            rotatePrevious = false;
            currentShipCode = 0;

            playerGridSquares = new GridSquare[10, 10];
            enemyGridSquares = new GridSquare[10, 10];

            panelSize.X = playerPanel.Width;// gets width of the player panel
            panelSize.Y = playerPanel.Height;// gets height of the player panel

            xGridSquareSize = (panelSize.X / 10);//individual panel x size
            yGridSquareSize = (panelSize.Y / 10);//individual panel y size

            playerDraw = new Drawer(playerPanel, xGridSquareSize, yGridSquareSize); //this handles all graphics. repeating it in here got ugly.
            playerPanel.Enabled = false;//set to false to prevent input before setup. Set to false again on game end pls.

            enemyDraw = new Drawer(enemyPanel, xGridSquareSize, yGridSquareSize);
            enemyPanel.Enabled = false;


            
        }


        


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = new Point(e.X, e.Y);


            // this math allows you to avoid any looping / searching for the current grid square.
            // Just 1 line to get the current grid square. Ain't that neat.
            // the mouse position / the size of the grid square will give you a single digit # from 0 to 9
            // which will correspond to it's position in the grid squares 2D array
            // casting as an int just cuts off the decimal
            // example:
            // mousePosition = (452, 387)
            // grid size = (50 x 50)
            // 452 / 50 = 9.04 -> cast as int -> 9
            // 387 / 50 = 7.74 -> cast as int -> 7
            // this gives position [9,7] in the array or (visually) square 10,8 in the grid


            if (!mouseDown)
            {
                // On some machines the player grid squares are drawn a bit too big so cut the indexs to avoid out of range errors
                // This can't break the game because if the mouse was that far away for the origin they wanted the last square anyways
                int xGridSquareIndex = (int)(mousePosition.X / xGridSquareSize);
                if (xGridSquareIndex > 9)
                    xGridSquareIndex = 9;
                int yGridSquareIndex = (int)(mousePosition.Y / yGridSquareSize);
                if (yGridSquareIndex > 9)
                    yGridSquareIndex = 9;
                
                if (currentPlayerGridsquare != playerGridSquares[xGridSquareIndex, yGridSquareIndex] || currentPlayerGridsquare == null || (rotateNext != rotatePrevious))
                {
                    //code for determining if there's any ships to be placed goes here
                    switch (listBox.GetItemText(listBox.SelectedItem))
                    {
                        case "AircraftCarrier":
                            if (!aircraftCarrierPlaced)
                                DrawShip(5);
                            break;
                        case "Battleship":
                            if (!battleshipPlaced)
                                DrawShip(4);
                            break;
                        case "Submarine":
                            if (!submarinePlaced)
                                DrawShip(31);
                            break;
                        case "Destroyer":
                            if (!destroyerPlaced)
                                DrawShip(32);
                            break;
                        case "PatrolBoat":
                            if (!patrolBoatPlaced)
                                DrawShip(2);
                            break;
                    }
                }
            }
            label2.Text = mousePosition.ToString();
        }


        private void DrawShip(int size)
        {
            currentShipCode = size;
            // clear
            if (currentPlayerGridsquare != null)
            {
                for (int i = 0; i < currentPlayerGridsquare.size; i++)
                {

                    if(rotatePrevious==false)
                    {
                        //got an out of bounds exception on the next if. This should prevent that (and it did)
                        if (((int)((currentPlayerGridsquare.min.Y / yGridSquareSize) + i)) <= 9)
                        {
                            //currently only accounting for y going down starting from head
                            if (playerGridSquares[(int)(currentPlayerGridsquare.min.X / xGridSquareSize), (int)((currentPlayerGridsquare.min.Y / yGridSquareSize) + i)].set != true)
                            {
                                playerDraw.FilledRectangle(playerDraw.with.whiteBrush, currentPlayerGridsquare, i,0);
                            }
                        }
                    }
                    if (rotatePrevious == true)
                    {
                        //got an out of bounds exception on the next if. This should prevent that (and it did)
                        if (((int)((currentPlayerGridsquare.min.X / xGridSquareSize) + i)) <= 9)
                        {
                            //currently only accounting for y going down starting from head
                            if (playerGridSquares[(int)((currentPlayerGridsquare.min.X / xGridSquareSize) + i), (int)(currentPlayerGridsquare.min.Y / yGridSquareSize)].set != true)
                            {
                                playerDraw.FilledRectangle(playerDraw.with.whiteBrush, currentPlayerGridsquare, 0,i);
                            }
                        }
                    }
                }
            }

            // On some machines the player grid squares are drawn a bit too big so cut the indexs to avoid out of range errors
            // This can't break the game because if the mouse was that far away for the origin they wanted the last square anyways
            int xGridSquareIndex = (int)(mousePosition.X / xGridSquareSize);
            if (xGridSquareIndex > 9)
                xGridSquareIndex = 9;
            int yGridSquareIndex = (int)(mousePosition.Y / yGridSquareSize);
            if (yGridSquareIndex > 9)
                yGridSquareIndex = 9;

            // set new current
            currentPlayerGridsquare = playerGridSquares[xGridSquareIndex, yGridSquareIndex];
            //the amount after the head
            if (size == 31 || size == 32)
                size = 3;
            currentPlayerGridsquare.size = size;

            if(rotateNext==false)//draw vertical
            {
                // draw new
                if (yGridSquareIndex + (size - 1) <= 9 && checkForSetSquares(rotateNext))
                {
                    for (int i = 0; i < size; i++)
                    {
                        playerDraw.Ellipse(playerDraw.with.greenPen, currentPlayerGridsquare, i,0);
                    }
                    currentPlayerGridsquare.ok = true; // ready to be set / placed
                }
                else
                {
                    //this if prevents drawing a no go marker over an existing (currently set) grid square
                    if (currentPlayerGridsquare.set != true)
                    {
                        playerDraw.Ellipse(playerDraw.with.redPen, currentPlayerGridsquare, 0,0);
                        currentPlayerGridsquare.ok = false; // not ready to be set / placed
                    }
                }
            }
            if (rotateNext == true)//draw horizontal
            {
                // draw new
                if (xGridSquareIndex + (size - 1) <= 9 && checkForSetSquares(rotateNext))
                {
                    for (int i = 0; i < size; i++)
                    {
                        playerDraw.Ellipse(playerDraw.with.greenPen, currentPlayerGridsquare,0, i);
                    }
                    currentPlayerGridsquare.ok = true; // ready to be set / placed
                }
                else
                {
                    //this if prevents drawing a no go marker over an existing (currently set) grid square
                    if (currentPlayerGridsquare.set != true)
                    {
                        playerDraw.Ellipse(playerDraw.with.redPen, currentPlayerGridsquare, 0,0);
                        currentPlayerGridsquare.ok = false; // not ready to be set / placed
                    }
                }
            }
            rotatePrevious = rotateNext;
        }


        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            label1.Text = mouseDown.ToString();
        }
        private void playerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            label1.Text = mouseDown.ToString();
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            if(!gameRunning)
            {
                //Enable the player panel so the game can begin (also needs to be enabled to draw)
                playerPanel.Enabled = true;
                enemyPanel.Enabled = true;
                DrawGrid(playerDraw, playerGridSquares); // draw a grid on the player's panel
                DrawGrid(enemyDraw, enemyGridSquares); // draw a grid on the enemy's panel
                currentPlayerGridsquare = null; 
                startButton.Text = "Connect to Opponent";
                startButton.Enabled = false;
                startButton.Visible = false;
            }
            if(gameRunning)
            {
                startButton.Enabled = false;
                startButton.Visible = false;
                listBox.Enabled = false;
                listBox.Visible = false;
                attackOpponentLabel.Visible = true;
                placeShipsLabel.Visible = false;
                try
                {
                    log.Text += "Waiting for opponent ... ";
                    log.ScrollToCaret(); // LOG!
                    // (these 3 lines start the game)
                    socket = new TcpClient("52.23.160.4", 2000);
                    Thread sockets = new Thread(() => TCPRead(socket));
                    sockets.Start();
                }
                catch
                {
                    Console.WriteLine("server at " + "52.23.160.4" + " down");
                    log.Text += "\n\nThe server at 52.23.160.4 is down."; // LOG!
                    log.ScrollToCaret();
                }
            }
        }

        TcpClient socket;
        bool go = false;
        Point enemyPanelMousePosition;
        Point lastGridCoordinateShotAt;

        // Does damage to the ship and sees if it sunk (indicated by the return value)
        private bool hitShipWithCode(int code)
        {
            if (code == 2)
            {
                if (--patrolBoatHitsRemaining == 0)
                    return true;
                else
                    return false;
            }
            else if (code == 31)
            {
                if (--submarineHitsRemaining == 0)
                    return true;
                else
                    return false;
            }
            else if (code == 32)
            {
                if(--destroyerHitsRemainging == 0)
                    return true;
                else
                    return false;
            }
            else if (code == 4)
            {
                if(--battleshipsHitsRemaining == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if (--aircraftCarrierHitsRemaining == 0)
                    return true;
                else
                    return false;
            }
        }

        private void triggerLabel_TextChanged(object sender, EventArgs e)
        {
            
            logWrite(triggerLabel.Text); // update LOG!
            Console.WriteLine(this.Text +": "+ triggerLabel.Text);
            if (triggerLabel.Text == "go"+"\n")
            {
                //unblock ui
                enemyPanel.Enabled = true;
                go = true;
            }
            else if (triggerLabel.Text == "wait" + "\n")
            {
                //block ui
                // go back to listening for the next message
                Thread sockets = new Thread(() => TCPRead(socket));
                sockets.Start();                
            }
            else if (triggerLabel.Text == "hit" + "\n")
            {
                // update view
                // disable click on that square
                //go = true;
                enemyDraw.FilledEllipse(enemyDraw.with.greenBrush, enemyGridSquares[lastGridCoordinateShotAt.X, lastGridCoordinateShotAt.Y], 0, 0);
                
                Thread sockets = new Thread(() => TCPRead(socket));
                sockets.Start();
            }
            else if (triggerLabel.Text == "miss" + "\n")
            {
                // update view
                // disable click on that square
                //go = true;
                enemyDraw.Ellipse(enemyDraw.with.greenPen, enemyGridSquares[lastGridCoordinateShotAt.X, lastGridCoordinateShotAt.Y], 0, 0);

                Thread sockets = new Thread(() => TCPRead(socket));
                sockets.Start();
            }
            else if(triggerLabel.Text == "close" + "\n")
            {
                if (socket != null)
                    socket.Close();
                Console.WriteLine(this.Text + ": opponent left ");
            }
            else if (triggerLabel.Text == "win" + "\n")
            {
                enemyDraw.FilledEllipse(enemyDraw.with.greenBrush, enemyGridSquares[lastGridCoordinateShotAt.X, lastGridCoordinateShotAt.Y], 0, 0);
                log.Text += "\nYou win.";

                log.SelectionStart = log.TextLength;
                log.ScrollToCaret();
            }
            else if (triggerLabel.Text == "win\nclose\n")
            {
                enemyDraw.FilledEllipse(enemyDraw.with.greenBrush, enemyGridSquares[lastGridCoordinateShotAt.X, lastGridCoordinateShotAt.Y], 0, 0);
                log.Text += "\nYou win.";

                log.SelectionStart = log.TextLength;
                log.ScrollToCaret();

                if (socket != null)
                    socket.Close();
            }
            else if (triggerLabel.Text == "patrolboat"
                     || triggerLabel.Text == "submarine"
                     || triggerLabel.Text == "destroyer"
                     || triggerLabel.Text == "battleship"
                     || triggerLabel.Text == "aircraftcarrier")
            {
                // You sank an opponent's ship, (this was already logged at the top of this function). Draw the hit and wait for a new message.
                enemyDraw.FilledEllipse(enemyDraw.with.greenBrush, enemyGridSquares[lastGridCoordinateShotAt.X, lastGridCoordinateShotAt.Y], 0, 0);

                Thread sockets = new Thread(() => TCPRead(socket));
                sockets.Start();
            }
            else if (triggerLabel.Text == "")
            {
                // The server is down
                if (socket != null)
                    socket.Close();
                Console.WriteLine(this.Text + ": server retreated ");
                log.Text += ("\nServer retreated. ");
                log.SelectionStart = log.TextLength;
                log.ScrollToCaret();
            }
            else //coordinates, handle shot and end your turn
            {
                char[] delimiterChars = { ' ', ',', '.', ':', '\n' };
                string[] splitCoords = triggerLabel.Text.Split(delimiterChars);
                Point check = new Point(Int32.Parse(splitCoords[0]), Int32.Parse(splitCoords[1]));
                go = false;
                if(playerGridSquares[check.X, check.Y].set==true)
                {
                    // First decrement the appropriate hits remaining counter and see if any ships were sunk
                    string message = "hit" + "\n"; // If any ships were sunk this message will be changed, else it remains as is
                    switch (playerGridSquares[check.X, check.Y].getShipCode())
                    {
                        case 2:
                            if (hitShipWithCode(2))
                                message = "patrolboat";
                            break;

                        case 31:
                            if (hitShipWithCode(31))
                                message = "submarine";
                            break;
   
                        case 32:
                            if (hitShipWithCode(32))
                                message = "destroyer";
                            break;
                        
                        case 4:
                            if (hitShipWithCode(4))
                                message = "battleship";
                            break;

                        case 5:
                            if (hitShipWithCode(5))
                                message = "aircraftcarrier";
                            break;
                    }                 
                    
                    logWrite(message);// adds hit or miss to "your opponent shot at [x,y]"
                    playerDraw.FilledEllipse(playerDraw.with.redBrush, playerGridSquares[check.X, check.Y], 0, 0);


                    gameEndCounter++;
                    // you got hit 17 times, you lose
                    if (gameEndCounter >= 17)
                    {
                        
                        log.Text += "\nYou Lose.";
                        log.SelectionStart = log.TextLength;
                        log.ScrollToCaret();

                        //go = false;

                        message = "win\n";
                        Thread sockets = new Thread(() => TCPWrite(socket, message));
                        sockets.Start();
                    }
                    else
                    {
                        // Just send the hit or ship name message since the game is not over yet
                        Thread sockets = new Thread(() => TCPWrite(socket, message));
                        sockets.Start();
                    }

                }
                else
                {
                    string message = "miss" + "\n";
                    logWrite(message);// adds hit or miss to "your opponent shot at [x,y]"
                    playerDraw.Ellipse(playerDraw.with.redPen, playerGridSquares[check.X, check.Y], 0, 0);
                    Thread sockets = new Thread(() => TCPWrite(socket, message));
                    sockets.Start();
                }
            }
            
        }

        public delegate void UpdateTextCallback(string text);
        private void UpdateText(string text)
        {
            triggerLabel.Text = text;
        }

        public void TCPRead(TcpClient socket)
        {
            byte[] data = new byte[1024];

            NetworkStream ns = socket.GetStream();

            // closing app causes crash on this line
            // A first chance exception of type 'System.IO.IOException' occurred in System.dll
            // An unhandled exception of type 'System.IO.IOException' occurred in System.dll
            // Additional information: Unable to read data from the transport connection: A blocking operation was interrupted by a call to WSACancelBlockingCall.
            // surround with try catch
            try
            {
                int recv = ns.Read(data, 0, data.Length);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                triggerLabel.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { stringData });
            }
            catch (Exception e)
            {
                Console.WriteLine("Client Closed");
                Console.WriteLine(e);
            }
        }

        public void TCPWrite(TcpClient socket, string message)
        {
            byte[] data = new byte[1024];
            NetworkStream ns = socket.GetStream();
            ns.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
            ns.Flush();

            data = new byte[1024];
            int recv = ns.Read(data, 0, data.Length);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            triggerLabel.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { stringData });
        }

        private void enemyPanel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("go is "+go);
            Console.WriteLine("grid square is "+enemyGridSquares[(int)(enemyPanelMousePosition.X / xGridSquareSize), (int)(enemyPanelMousePosition.Y / yGridSquareSize)].shotAt);
            Console.WriteLine("x "+(int)(enemyPanelMousePosition.X / xGridSquareSize));
            Console.WriteLine("y " + (int)(enemyPanelMousePosition.Y / yGridSquareSize));

            //if it's your turn to go and the position has not been shot at
            if (go && enemyGridSquares[(int)(enemyPanelMousePosition.X / xGridSquareSize), (int)(enemyPanelMousePosition.Y / yGridSquareSize)].shotAt==false)// your turn to shoot
            {
                Console.WriteLine(this.Text + " clicked on enemy panel");
                Point point = new Point((int)(enemyPanelMousePosition.X / xGridSquareSize), (int)(enemyPanelMousePosition.Y / yGridSquareSize));
                enemyGridSquares[point.X, point.Y].shotAt = true;
                lastGridCoordinateShotAt = point;
                string message = point.X+","+point.Y + "\n";
                logWrite(message); // update LOG!
                go = false;

                Thread sockets = new Thread(() => TCPWrite(socket, message));
                sockets.Start();
            }
        }
        private void enemyPanel_MouseMove(object sender, MouseEventArgs e)
        {
            enemyPanelMousePosition = new Point(e.X, e.Y);
            label4.Text = enemyPanelMousePosition.ToString(); 
        }

        private void DrawGrid(Drawer draw, GridSquare[,] GridSquares)
        {         
            for (int i = 1; i < 10; i++)
            {
                //vertical lines
                draw.VerticalLine(draw.with.blackPen, panelSize, i);
                //horizontal lines
                draw.HorizontalLine(draw.with.blackPen, panelSize, i);
            }

            // filling a 2D array with blank gridSquares
            // these will store information about that square
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    GridSquares[x, y] = new GridSquare(new Point(x * xGridSquareSize, y * yGridSquareSize), new Point((x * xGridSquareSize) + xGridSquareSize, (y * yGridSquareSize) + yGridSquareSize));
                }
            }
            
        }


        //returns true if can draw, false if can't
        private bool checkForSetSquares(bool xORy)//draw along x or y. false = vertical, true = horizontal
        {
            if (xORy==false)//vertical
            {
                //this loop check if and grid squares under the head are set. If not, dont continue onto setting a new ship
                for (int i = 0; i < currentPlayerGridsquare.size; i++)
                {
                    if (playerGridSquares[(int)(currentPlayerGridsquare.min.X / xGridSquareSize), (int)((currentPlayerGridsquare.min.Y / yGridSquareSize) + i)].set == true)
                    {
                        //_continue = false;
                        return false;
                    }
                }
            }
            else if (xORy == true)//horizontal
            {
                //this loop check if and grid squares under the head are set. If not, dont continue onto setting a new ship
                for (int i = 0; i < currentPlayerGridsquare.size; i++)
                {
                    if (playerGridSquares[(int)((currentPlayerGridsquare.min.X / xGridSquareSize) + i), (int)(currentPlayerGridsquare.min.Y / yGridSquareSize)].set == true)
                    {
                        //_continue = false;
                        return false;
                    }
                }
            }
            return true;
        }

        private void playerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentPlayerGridsquare != null && !gameRunning)
            {
                if (currentPlayerGridsquare.ok == true)
                {
                    bool _continue = checkForSetSquares(rotateNext);//returns true if there's no set squares in the way
                    if (_continue)
                    {
                        Console.WriteLine(currentShipCode);
                        switch (currentShipCode)
                        {
                            case 5:
                                if (aircraftCarrierPlaced) { return; }
                                else { 
                                    aircraftCarrierPlaced = true; 
                                }
                                break;
                            case 4:
                                if (battleshipPlaced) { return; }
                                else { battleshipPlaced = true; }
                                break;
                            case 31:
                                if (submarinePlaced) { return; }
                                else { submarinePlaced = true; }
                                break;
                            case 32:
                                if (destroyerPlaced) { return; }
                                else { destroyerPlaced = true; }
                                Console.WriteLine("destroy");

                                break;
                            case 2:
                                if (patrolBoatPlaced) { return; }
                                else { patrolBoatPlaced = true; }
                                break;
                        }

                        if(rotateNext==false)
                        {
                            // clear
                            for (int i = 0; i < currentPlayerGridsquare.size; i++)
                            {
                                playerDraw.FilledRectangle(playerDraw.with.whiteBrush, currentPlayerGridsquare, i, 0);
                            }
                            //draw and set
                            for (int i = 0; i < currentPlayerGridsquare.size; i++)
                            {
                                playerDraw.FilledEllipse(playerDraw.with.greenBrush, currentPlayerGridsquare, i, 0);
                                playerGridSquares[(int)(currentPlayerGridsquare.min.X / xGridSquareSize), (int)((currentPlayerGridsquare.min.Y / yGridSquareSize) + i)].set = true;
                                playerGridSquares[(int)(currentPlayerGridsquare.min.X / xGridSquareSize), (int)((currentPlayerGridsquare.min.Y / yGridSquareSize) + i)].setShipCode(currentShipCode);
                            }
                            Console.WriteLine();
                        }
                        else if (rotateNext == true)
                        {
                            // clear
                            for (int i = 0; i < currentPlayerGridsquare.size; i++)
                            {
                                playerDraw.FilledRectangle(playerDraw.with.whiteBrush, currentPlayerGridsquare,0, i);
                            }
                            //draw and set
                            for (int i = 0; i < currentPlayerGridsquare.size; i++)
                            {
                                playerDraw.FilledEllipse(playerDraw.with.greenBrush, currentPlayerGridsquare, 0, i);
                                playerGridSquares[(int)((currentPlayerGridsquare.min.X / xGridSquareSize) + i), (int)(currentPlayerGridsquare.min.Y / yGridSquareSize)].set = true;
                                playerGridSquares[(int)((currentPlayerGridsquare.min.X / xGridSquareSize) + i), (int)(currentPlayerGridsquare.min.Y / yGridSquareSize)].setShipCode(currentShipCode);
                            }
                        }

                    }
                }
            }

            // if all ships are placed
            if (aircraftCarrierPlaced && battleshipPlaced && submarinePlaced && destroyerPlaced && patrolBoatPlaced && !gameRunning)
            {
                gameRunning = true;
                startButton.Enabled = true;
                startButton.Visible = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            label3.Text = e.KeyCode.ToString();

            switch (currentShipCode)
            {
                case 5:
                    if (aircraftCarrierPlaced) { return; }
                    break;
                case 4:
                    if (battleshipPlaced) { return; }
                    break;
                case 31:
                    if (submarinePlaced) { return; }
                    break;
                case 32:
                    if (destroyerPlaced) { return; }
                    break;
                case 2:
                    if (patrolBoatPlaced) { return; }
                    break;
            }

            // Rotate if R is presseed
            if(e.KeyCode.ToString()=="R")
            {
                if (currentPlayerGridsquare != null && !gameRunning)
                {
                    Console.WriteLine(currentShipCode);
                    rotateNext = !rotateNext;
                    DrawShip(currentShipCode);
                }
            }
        }

        private void logWrite(string msg)
        {
            string coordmsg = msg;
            char[] delimiterChars = { ' ', ',', '.', ':', '\n' };
            string[] splitMsg = msg.Split(delimiterChars);
            msg = splitMsg[0];            
            Console.WriteLine(msg);

            if (msg == "")
                return; // Server died. This function will be called later with a message indicating this.

            if (gameEndCounter >= 17)
                return; // If the game is over dont write anything to the log
            
            if (isFirstMessage)
            {
                if (msg=="wait")
                {
                    wentFirst = false;
                    //isFirstMessage = false;
                    //return;
                }
                else if (msg=="go")
                {
                    wentFirst = true;
                    //isFirstMessage = false;
                    //return;
                }
                else
                    Console.WriteLine("SOMETHING OTHER THAN WAIT OR GO IS READ/WRITTEN FIRST");
            }
            if (msg=="miss" || msg=="hit")
            {
                // these two types of messages can be added to the log pretty much as is.
                log.Text += msg + '!';
            }
            else if (msg=="wait" || msg=="go")
            {
                if (isFirstMessage)
                {
                    log.Text += "\nWelcome to Battleship!";
                    isFirstMessage = false;
                }
                if (msg=="go")
                    log.Text += "\nYour move. ";
                if(msg=="wait")
                    log.Text += "\nYour opponent's move. ";

                log.SelectionStart = log.TextLength;
                log.ScrollToCaret();

                return;
            }
            else if (msg=="win")
            {
                return;
            }
            else if (msg=="close")
            {
                log.Text += "\nYour opponent has retreated.\nYou win.";
            }
            else if (msg == "patrolboat")
            {
                log.Text += "hit!\nPatrol Boat Sunk!";
            }
            else if (msg == "submarine")
            {
                log.Text += "hit!\nSubmarine Sunk!";
            }
            else if (msg == "destroyer")
            {
                log.Text += "hit!\nDestroyer Sunk!";
            }
            else if (msg == "battleship")
            {
                log.Text += "hit!\nBattleShip Sunk!";
            }
            else if (msg == "aircraftcarrier")
            {
                log.Text += "hit!\nAircraft Carrier Sunk!";
            }
            else
            {
                // if it gets here the only other option is that the msg is a coordinate pair
                // parse and get values
                
                string[] splitCoords = coordmsg.Split(delimiterChars);
                string x = splitCoords[0];
                string y = splitCoords[1];

                if (go == true) // i.e. it's your turn
                {
                    if (wentFirst == true) // you start the round
                        log.Text += "\n\nROUND " + (roundCount++) + ":\n";
                    log.Text += "You fired at " + x + ',' + y + "  --  ";
                }
                else // it's your opponent's turn
                {
                    if (wentFirst == false) // your opponent starts the round
                        log.Text += "\n\nROUND " + (roundCount++) + ":\n";
                    log.Text += "Your opponent fired at " + x + ',' + y + "  --  ";
                }
            }
            log.SelectionStart = log.TextLength; 
            log.ScrollToCaret();
            return;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //send a Close message

            //socket.Shutdown(SocketShutdown.Both);
            if(socket!=null)
                socket.Close();
        }









    }
}
