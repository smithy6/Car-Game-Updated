using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using System.Diagnostics;
using System.Security.Cryptography;


namespace CarGame
{
    struct Object
    {
        public int x; //declare x coordinate
        public int y; //declare y coordinate
        public char c;
        public ConsoleColor color;
    }

    class Program
    {
        static void PrintOnPosition(int x, int y, char c,
            ConsoleColor color = ConsoleColor.Gray) //make PrintOnPosition method with parameters x, y, c and color that are used to print the object on the console
        {
            Console.SetCursorPosition(x, y); //set the position of the object
            Console.ForegroundColor = color; //set the color of the object
            Console.Write(c); //print the object
        }
        static void PrintStringOnPosition(int x, int y, string str,
            ConsoleColor color = ConsoleColor.Gray) //make PrintStringOnPosition method with parameters x, y, str and color that are used to print the string on the console
        {
            Console.SetCursorPosition(x, y); //set the position of the string
            Console.ForegroundColor = color; //set the color of the string
            Console.Write(str); //print the string
        }

        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the game \"Race Car\"!");
            Console.WriteLine("Press 1 for \"Start Game\"");
            Console.WriteLine("Press 2 for \"Instructions\"");
            Console.WriteLine("Press 3 for \"Exit\"");
            Console.Write("Your choice: ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Starting Game...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Instructions:");
                    Console.WriteLine("Use the left and right arrow keys to move the car");
                    Console.WriteLine("You can also use the A and D keys to move the car");
                    Console.WriteLine("Press \"Esc\" to exit the game");
                    Console.WriteLine("Press \"Enter\" to start the game");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine("Starting Game...");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        Console.WriteLine("Exiting Game...");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Exiting Game");
                    //wait 2 seconds before exiting the game
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice!");
                    break;
            }
            Console.WriteLine("Please enter the difficulty of the game (1-10):");
            double difficulty = int.Parse(Console.ReadLine());
            while (difficulty < 1 || difficulty > 10)
            {
                Console.WriteLine("Please enter a valid difficulty (1-10):");
                difficulty = int.Parse(Console.ReadLine());
            }
            Console.Clear();
            double speed = 100;
            double acceleration = 0.1 * difficulty; //acceleration is 0.1 times the difficulty
            int playfieldWidth = 5; //declare the width of the playfield
            int livesCount = 5;
            Console.BufferHeight = Console.WindowHeight = 20; //set the height of the console
            Console.BufferWidth = Console.WindowWidth = 30; //set the width of the console
            Object userCar = new Object(); //create new object userCar
            {
                userCar.x = 2; //set the x coordinate of the userCar
                userCar.y = Console.WindowHeight - 1; //set the y coordinate of the userCar
                userCar.c = '@'; //set the symbol of the userCar
                userCar.color = ConsoleColor.Yellow; //set the color of the userCar 
            }
            
            Random randomGenerator = new Random(); //create new randomGenerator
            List<Object> objects = new List<Object>(); //create new list of objects
            while (true)
            {
                speed += acceleration;
                if (speed > 400) //if the speed is greater than 400
                {
                    speed = 400; //set the speed to 400 so that it doesn't increase any more
                }

                bool hitted = false; //declare hitted as false 
                {
                    int chance = randomGenerator.Next(0, 100); //declare chance as random number between 0 and 100
                    if (chance < 10) //if chance is less than 10
                    {
                        Object newObject = new Object(); //create new object newObject
                        {
                            newObject.color = ConsoleColor.Cyan; //set the color of the newObject
                            newObject.c = '-'; //set the symbol of the newObject
                            newObject.x = randomGenerator.Next(0, playfieldWidth); //set the x coordinate of the newObject
                            newObject.y = 0; //set the y coordinate of the newObject
                            objects.Add(newObject); //add the newObject to the list of objects
                        }
                        
                    }
                    else if (chance < 20) //if chance is less than 20
                    {
                        Object newObject = new Object(); //create new object newObject
                        {
                            newObject.color = ConsoleColor.Cyan; //set the color of the newObject
                            newObject.c = '*'; //set the symbol of the newObject
                            newObject.x = randomGenerator.Next(0, playfieldWidth); //set the x coordinate of the newObject
                            newObject.y = 0; //set the y coordinate of the newObject
                            objects.Add(newObject); //add the newObject to the list of objects
                        }
                        
                    }
                    else
                    {
                        Object newCar = new Object(); //create new object newCar
                        {
                            newCar.color = ConsoleColor.Green; //set the color of the newCar
                            newCar.x = randomGenerator.Next(0, playfieldWidth); //set the x coordinate of the newCar
                            newCar.y = 0; //set the y coordinate of the newCar
                            newCar.c = '#'; //set the symbol of the newCar
                            objects.Add(newCar); //add the newCar to the list of objects
                        }
                        
                    }
                }

                while (Console.KeyAvailable) //while there is a key pressed
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true); //declare pressedKey as the key that is pressed
                                                                       //while (Console.KeyAvailable) Console.ReadKey(true);
                    if (pressedKey.Key == ConsoleKey.LeftArrow || pressedKey.Key == ConsoleKey.A && userCar.x - 1 >= 0) //if pressedKey == left arrow && //if the x coordinate of the userCar is greater than 0
                    {
                        userCar.x -= 1; //decrease the x coordinate of the userCar by 1
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow || pressedKey.Key == ConsoleKey.D && userCar.x + 1 < playfieldWidth) //if the pressedKey == right arrow && //if the x coordinate of the userCar is less than the width of the playField
                    {
                        userCar.x += 1; //increase the x coordinate of the userCar by 1
                    }
                }
                List<Object> newList = new List<Object>();
                for (int i = 0; i < objects.Count; i++) //loop through the list of objects
                {
                    Object oldCar = objects[i]; //declare oldCar as the object in the list of objects
                    Object newObject = new Object(); //create new object newObject
                    {
                        newObject.x = oldCar.x; //set the x coordinate of the newObject as the x coordinate of the oldCar
                        newObject.y = oldCar.y + 1; //set the y coordinate of the newObject as the y coordinate of the oldCar + 1
                        newObject.c = oldCar.c; //set the symbol of the newObject as the symbol of the oldCar
                        newObject.color = oldCar.color; //set the color of the newObject as the color of the oldCar
                    }
                    if (newObject.c == '*' && newObject.y == userCar.y && newObject.x == userCar.x) //if the symbol of the newObject is * and the y coordinate of the newObject is equal to the y coordinate of the userCar and the x coordinate of the newObject is equal to the x coordinate of the userCar
                    {
                        speed -= 20; //decrease the speed by 20
                    }
                    if (newObject.c == '-' && newObject.y == userCar.y && newObject.x == userCar.x) //if the symbol of the newObject is _ and the y coordinate of the newObject is equal to the y coordinate of the userCar and the x coordinate of the newObject is equal to the x coordinate of the userCar
                    {
                        livesCount++;
                    }
                    if (newObject.c == '#' && newObject.y == userCar.y && newObject.x == userCar.x) //if the symbol of the newObject is # and the y coordinate of the newObject is equal to the y coordinate of the userCar and the x coordinate of the newObject is equal to the x coordinate of the userCar
                    {
                        livesCount--; //decrease the livesCount by 1
                        hitted = true; //set hitted as true
                        speed += 50;
                        if (speed > 400)
                        {
                            speed = 400;
                        }
                        if (livesCount <= 0)
                        {
                            PrintStringOnPosition(8, 13, "GAME OVER!!!", ConsoleColor.Red);
                            PrintStringOnPosition(8, 14, "Press [enter] to exit", ConsoleColor.Red);
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    if (newObject.y < Console.WindowHeight) //if the y coordinate of the newObject is less than the height of the console
                    {
                        newList.Add(newObject); //add the newObject to the list of objects
                    }
                }
                objects = newList; //set the list of objects as the newList
                Console.Clear();
                if (hitted)
                {
                    objects.Clear();
                    PrintOnPosition(userCar.x, userCar.y, 'X', ConsoleColor.Red); //print the symbol X on the position of the userCar
                }
                else
                {
                    PrintOnPosition(userCar.x, userCar.y, userCar.c, userCar.color); //print the symbol of the userCar on the position of the userCar
                }
                foreach (Object car in objects) //loop through the list of objects
                {
                    PrintOnPosition(car.x, car.y, car.c, car.color); //print the symbol of the car on the position of the car
                }

                // Draw info

                PrintStringOnPosition(8, 4, "Lives: " + livesCount, ConsoleColor.White);
                PrintStringOnPosition(8, 5, "Speed: " + speed, ConsoleColor.White);
                PrintStringOnPosition(8, 6, "Acceleration: " + acceleration, ConsoleColor.White);
                PrintStringOnPosition(8, 7, "# is obstacle", ConsoleColor.Green);
                PrintStringOnPosition(8, 8, "_ is +1 lives", ConsoleColor.Cyan);
                PrintStringOnPosition(8, 9, "* is -20 speed", ConsoleColor.Cyan);
                PrintStringOnPosition(8, 10, "@ is you", ConsoleColor.Yellow);
                PrintStringOnPosition(8, 11, "Use Left and ", ConsoleColor.Blue);
                PrintStringOnPosition(8, 12, "Right arrows to move", ConsoleColor.Blue);
                //Console.Beep();
                Thread.Sleep((int)(600 - speed)); //set the speed of the game
            }
        }
    }
}
