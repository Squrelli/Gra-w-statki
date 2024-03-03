using System.Data.Common;

class Board
{
    //Action: 1 - display board (with player ships), 2 - shooting board (to shoot enemies ships)
    Player owner;
    byte action;
    public char[,] board ={
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
    };
    public Board(Player owner, byte action)
    {
        this.owner = owner;
        this.action = action;
    }
    public void DisplayBoard()
    {
        if (action == 1)
        {
            Console.Write("Player " + owner.id + " display board \n");
        }
        else
        {
            Console.Write("Player " + owner.id + " shooting board \n");
        }

        Console.Write("     A   B   C   D   E   F   G   H   I   J  \n   +---+---+---+---+---+---+---+---+---+---+\n");
        for (int i = 0; i < 10; i++)
                {
            if (i == 9)
            {
                Console.Write((i + 1) + " | ");
            }
            else
            {
                Console.Write(" " + (i+1) + " | ");
            }

            for (int j = 0; j < 10; j++)
                {
                    Console.Write(board[i,j] + " | ");
                }
                Console.Write("\n   +---+---+---+---+---+---+---+---+---+---+\n");
            }
        }
}
class Player
{
    public Board displayBoard;
    public Board shootingBoard;
    public int amountOfShips = 0;
    public byte id;
    public int win;
    public Player(byte id)
    {
        this.id = id;
        displayBoard = new Board(this, 1);
        shootingBoard = new Board(this, 2);
    }

}
class Ship
{
    byte length, owner;
    public Ship(Player owner, int row, int column)
    {
        owner.displayBoard.board[column, row] = 's';
        owner.amountOfShips++;
    }
    public Ship(Player owner, int row, int column, int row2, int column2)
    {
        owner.displayBoard.board[column, row] = 's';
        owner.displayBoard.board[column2, row2] = 's';
        owner.amountOfShips++;
    }
    public Ship(Player owner, int row, int column, int row2, int column2, int row3, int column3)
    {
        owner.displayBoard.board[column, row] = 's';
        owner.displayBoard.board[column2, row2] = 's';
        owner.displayBoard.board[column3, row3] = 's';
        owner.amountOfShips++;
    }
    public Ship(Player owner, int row, int column, int row2, int column2, int row3, int column3, int row4, int column4)
    {
        owner.displayBoard.board[column, row] = 's';
        owner.displayBoard.board[column2, row2] = 's';
        owner.displayBoard.board[column3, row3] = 's';
        owner.displayBoard.board[column4, row4] = 's';
        owner.amountOfShips++;
    }
}
class Game
{
    string selectedField;
    bool correctField = false;
    Player currentPlayer, underAttackPlayer;
    public void SetCurrentPlayer(Player p, Player e)
    {
        this.currentPlayer = p;
        this.underAttackPlayer = e;
    }
    public void PlaceShips()
    {
        int counter = 3;
        while(counter < 4)
        {
            currentPlayer.displayBoard.DisplayBoard();
            Console.WriteLine("Player: " + currentPlayer.id + " Select where to place 1x1 ship (for example A1, B2):");
            selectedField = GetPlayerField();
            if (CheckIfFieldOcupated((byte)selectedField[1] - 48, (byte)selectedField[0] - 48, currentPlayer,'s') > 0)
            {
                Console.WriteLine("You can't place your ship here.");
            }
            else {
            Ship ship = new Ship(currentPlayer, (byte)selectedField[0] -48, (byte)selectedField[1] -48);
                counter++;
                Console.Clear();
            }
        }
        string field1, field2;
        counter = 2;
        while (counter < 3)
        {
            currentPlayer.displayBoard.DisplayBoard();
            Console.WriteLine("Player: " + currentPlayer.id + " Select where 2x1 ship should start (for example A1, B2):");
            field1 = GetPlayerField();
            Console.WriteLine("Player: " + currentPlayer.id + " Select where 2x1 ship should end (for example A1, B2):");
            field2 = GetPlayerField();
            if (CheckIfFieldOcupated((byte)field1[1] - 48, (byte)field1[0] - 48, currentPlayer, 's') == 0 && CheckIfFieldOcupated((byte)field2[1] - 48, (byte)field2[0] - 48, currentPlayer, 's') == 0)
            {
                if ((field1[0] - 0) == (field2[0] - 0) && ((field1[1] - 0) == (field2[1] - 1) || (field1[1] - 0) == (field2[1] + 1)))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48);
                    counter++;
                    Console.Clear();
                }
                else if ((field1[1] - 0) == (field2[1] - 0) && ((field1[0] - 0) == (field2[0] - 1) || (field1[0] - 0) == (field2[0] + 1)))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48);
                    counter++;
                    Console.Clear();
                }
                else{
                    Console.Clear(); Console.WriteLine("You can't place your ship here.");}
            }else{ Console.Clear(); Console.WriteLine("You can't place your ship here.");}
        }
        counter = 1;
        while (counter < 2)
        {
            currentPlayer.displayBoard.DisplayBoard();
            Console.WriteLine("Player: " + currentPlayer.id + " Select where 3x1 ship should start (for example A1, B2):");
            field1 = GetPlayerField();
            Console.WriteLine("Player: " + currentPlayer.id + " Select where 3x1 ship should end (for example A1, B2):");
            field2 = GetPlayerField();
            if (CheckIfFieldOcupated((byte)field1[1] - 48, (byte)field1[0] - 48, currentPlayer, 's') ==0&& CheckIfFieldOcupated((byte)field2[1] - 48, (byte)field2[0] - 48, currentPlayer, 's')==0)
            {
                if ((field1[0] - 0) == (field2[0] - 0) && (field1[1] - 0) == (field2[1] + 2))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 48, field1[1] - 49);
                    counter++;
                    Console.Clear();
                }
                else if ((field1[0] - 0) == (field2[0] - 0) && (field1[1] - 0) == (field2[1] - 2))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 48, field1[1] - 47);
                    counter++;
                    Console.Clear();
                }
                else if ((field1[1] - 0) == (field2[1] - 0) && ((field1[0] - 0) == (field2[0] + 2)))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 49, field1[1] - 48);
                    counter++;
                    Console.Clear();
                }
                else if ((field1[1] - 0) == (field2[1] - 0) && ((field1[0] - 0) == (field2[0] - 2)))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 47, field1[1] - 48);
                    counter++;
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You can't place your ship here.");
                }
            }else { Console.Clear(); Console.WriteLine("You can't place your ship here."); }
        }
        counter = 0;
        while (counter < 1)
        {
            currentPlayer.displayBoard.DisplayBoard();
            Console.WriteLine("Player: " + currentPlayer.id + " Select where 4x1 ship should start (for example A1, B2):");
            field1 = GetPlayerField();
            Console.WriteLine("Player: " + currentPlayer.id + " Select where 4x1 ship should end (for example A1, B2):");
            field2 = GetPlayerField();
            if (CheckIfFieldOcupated((byte)field1[1] - 48, (byte)field1[0] - 48, currentPlayer, 's') == 0&& CheckIfFieldOcupated((byte)field2[1] - 48, (byte)field2[0] - 48, currentPlayer, 's') == 0)
            {
                if ((field1[0] - 0) == (field2[0] - 0) && (field1[1] - 0) == (field2[1] + 3))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 48, field1[1] - 49, field1[0] - 48, field1[1] - 50);
                    Console.Clear();
                    counter++;
                }
                else if ((field1[0] - 0) == (field2[0] - 0) && (field1[1] - 0) == (field2[1] - 3))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 48, field1[1] - 47, field1[0] - 48, field1[1] - 46);
                    Console.Clear();
                    counter++;
                }
                else if ((field1[1] - 0) == (field2[1] - 0) && ((field1[0] - 0) == (field2[0] + 3)))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 49, field1[1] - 48, field1[0] - 50, field1[1] - 48);
                    Console.Clear();
                    counter++;
                }
                else if ((field1[1] - 0) == (field2[1] - 0) && ((field1[0] - 0) == (field2[0] - 3)))
                {
                    Ship ship = new Ship(currentPlayer, field1[0] - 48, field1[1] - 48, field2[0] - 48, field2[1] - 48, field1[0] - 47, field1[1] - 48, field1[0] - 46, field1[1] - 48);
                    Console.Clear();
                    counter++;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You can't place your ship here.");
                }
            }else{ Console.Clear(); Console.WriteLine("You can't place your ship here."); }
        }

        currentPlayer.displayBoard.DisplayBoard();
    }
    string GetPlayerField()
    {
        string field = Console.ReadLine();
        if (field.Length == 3)
        {
            if (field[1] == '1' && field[2] == '0')
            {
                field = field[0] + "9";
                correctField = CheckCorrectField(field);
            }
            else
            {
                correctField = false;
            }
        }
        else if (field.Length == 2)
        {
            field = "" + field[0] + (char)(field[1] - 1);
            correctField = CheckCorrectField(field);
        }
        else
        {
            correctField = false;
        }
        if (correctField)
        {
            char temp = field[0];
            int a = (int)temp - 65;
                        
            return "" +a + field[1];
        }
        else
        {
            Console.WriteLine("Incorrect field!\n");
            return GetPlayerField();    
        }
    }
    bool CheckCorrectField(string text)
    {
        if (text[0] <= 74 && text[0] >=65 && text[1] >= 48 && text[1] <= 57)
        {
            return true;
        }
        return false;

    }
    int CheckIfFieldOcupated(int column, int row, Player p, char target)
    {
        int temp = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try
                {
                    if (p.displayBoard.board[column + i, row + j] == target)
                    {
                        temp++;
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
        return temp;
    }
    void MarkAreaAsEmpty(Player p,Player p2)
    {
        for (int row = 0; row < 10; row++)
        {
            for (int column = 0; column < 10; column++)
            {
                if (p.displayBoard.board[column,row] == '*')
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            try
                            {
                                if (p.displayBoard.board[column + i, row + j] == ' ')
                                {
                                    p2.shootingBoard.board[column + i, row + j] = 'o';
                                    p.displayBoard.board[column + i, row + j] = 'o';
                                }
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                }
            }
        }
        
    }
    bool IsShipFullyHitted(Player p, Player p2, int column, int row)
    {
        int[] arr = {0,0,0,0};
        int temp = 0;
        p2.displayBoard.board[column, row] = 't';
        if (CheckIfFieldOcupated(column, row, p2, 's') > 0)
        {
            return false;
        }
        else if (CheckIfFieldOcupated(column, row, p2, 'x') == 1)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    try
                    {
                        if (p2.displayBoard.board[column + i, row + j] == 'x')
                        {
                            return IsShipFullyHitted(p, p2, column + i, row + j);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }

        }
        else if (CheckIfFieldOcupated(column, row, p2, 'x') == 2)
        {

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    try
                    {
                        if (p2.displayBoard.board[column + i, row + j] == 'x')
                        {
                            arr[temp] = column + i;
                            arr[temp + 1] = row + j;
                            temp = 2;
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            if (IsShipFullyHitted(p, p2, arr[0], arr[1]) && IsShipFullyHitted(p, p2, arr[2], arr[3]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
        
        return false;
    }
    void ChangeTempToChar(Player p, Player p2, char toChange)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (p.displayBoard.board[j,i] == 't')
                {
                    p.displayBoard.board[j, i] = toChange;
                    p2.shootingBoard.board[j, i] = toChange;
                }
            }
        }
        
    }
    public void Shooting()
    {
        if (underAttackPlayer.amountOfShips > 0)
        {
            Console.Clear();
            currentPlayer.shootingBoard.DisplayBoard();
            Console.WriteLine("Select field to shoot: ");
            selectedField = GetPlayerField();
            if (underAttackPlayer.displayBoard.board[selectedField[1] - 48, selectedField[0] - 48] != 's' && underAttackPlayer.displayBoard.board[selectedField[1] - 48, selectedField[0] - 48] != ' ')
            {
                Console.WriteLine("You can't shoot in the same place twice");
                ContinueOnEnter();
                Shooting();
            }
            else
            {

                if (underAttackPlayer.displayBoard.board[selectedField[1] - 48, selectedField[0] - 48] == 's')
                {
                    if (IsShipFullyHitted(currentPlayer, underAttackPlayer, selectedField[1] - 48, selectedField[0] - 48))
                    {
                        Console.WriteLine("Hit and sink");
                        ChangeTempToChar(underAttackPlayer, currentPlayer, '*');
                        MarkAreaAsEmpty(underAttackPlayer, currentPlayer);
                        underAttackPlayer.amountOfShips--;
                    }
                    else
                    {
                        Console.WriteLine("Hit");
                        ChangeTempToChar(underAttackPlayer, currentPlayer, 'x');
                    }
                    Console.WriteLine("You earned one more shot");
                    ContinueOnEnter();
                    Shooting();
                }
                else
                {
                    Console.WriteLine("Empty");
                    currentPlayer.shootingBoard.board[selectedField[1] - 48, selectedField[0] - 48] = 'o';
                    underAttackPlayer.displayBoard.board[selectedField[1] - 48, selectedField[0] - 48] = 'o';
                }
            }
        }
        
    }
    public void ContinueOnEnter()
    {
        Console.WriteLine("Press ENTER key to continue: ");
        Console.ReadLine();
    }
    public bool ChechForWin(Player p1, Player p2)
    {
        if (p1.amountOfShips == 0||p2.amountOfShips == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void PlayersSwitch()
    {
        Console.Clear();
        Console.WriteLine("Change the player and click ENTER: ");
        Console.ReadLine();
    }
    public void TogglePlayers()
    {
        Player temp = currentPlayer;
        currentPlayer = underAttackPlayer;
        underAttackPlayer = temp;
    }
}
class Program
{
    static void Main(String[] args)
    {
        Player player1 = new Player(1);
        Player player2 = new Player(2);
        Game game = new Game();

        while (true)
        {
            game.SetCurrentPlayer(player1, player2);
            game.PlaceShips();
            game.PlayersSwitch();
            game.TogglePlayers();
            game.PlaceShips();
            Console.Clear();

            while (game.ChechForWin(player1, player2) == false)
            {
                game.ContinueOnEnter();
                game.PlayersSwitch();
                game.TogglePlayers();
                game.Shooting();
            }
            if (player1.amountOfShips == 0)
            {
                Console.WriteLine("Player 2 Won!!!");
                player2.win++;
            }
            else
            {
                Console.WriteLine("Player 1 Won!!!");
                player1.win++;
            }
            Console.Write("Current ratio: \nPlayer 1: " + player1.win + "\nPlayer 2: " + player2.win + "\nIf you want to play again click ENTER: \n");
            Console.ReadLine();

        }
    }
}

/*
 s - ship;
 x - hit;
 o - empty;
 * - sinked;
*/