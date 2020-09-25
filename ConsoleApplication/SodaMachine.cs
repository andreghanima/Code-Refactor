using System;
using System.Linq;

namespace ConsoleApplication
{
    public class SodaMachine
    {
        private static int _money;
        private Soda[] _inventory;

        public SodaMachine(Soda[] inventory)
        {
            _inventory = inventory;
        }

        public void Start()
        {
            while (true)
            {
                WriteUserOptions();

                var input = Console.ReadLine().ToLower();
                var splitInput = input.Split(' ');
                var command = splitInput[0];
                var request = "";

                if (splitInput.Length > 1)
                    request = splitInput[1];
                 
                switch (command)
                {
                    case "insert":
                        try
                        {
                            InsertMoney(int.Parse(request));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        
                        break;
                    case "order":
                        Order(request);
                        break;
                    case "sms":
                        Order(request, true);
                        break;
                    case "recall":
                        RecallMoney();
                        break;
                    case "inventory":
                        WriteInventory();
                        break;
                    default:
                        Console.WriteLine("You typed an invalid request, please read documentation");
                        break;
                }
            }
        }

        public void WriteInventory()
        {
            var stringOut = "Available sodas: ";
            foreach (var soda in _inventory)
            {
                stringOut += $"{soda.Name}({soda.Nr}) ";
            }
            Console.WriteLine(stringOut);
        }

        public int GetMoney()
        {
            return _money;
        }

        public void RecallMoney()
        {
            Console.WriteLine("Returning " + _money + " to customer");
            _money = 0;
        }

        public void InsertMoney(int amount)
        {
            if(amount < 0)
                {
                    Console.WriteLine("You have to write a valid amount of money");
                    return;
                } 

            _money += amount;
            Console.WriteLine($"Adding {_money} to credit");

        }

        public void Order(string sodaName, bool isSms = false)
        {
            var csoda = _inventory.Select(s => s).Where(n => n.Name == sodaName.ToLower()).FirstOrDefault();

            if (csoda == null)
            {
                Console.WriteLine($"You have entered invalid soda!");
                return;
            }
                

            if(isSms && csoda.Nr > 0)
                {
                    Console.WriteLine($"Giving {csoda.Name} out by SMS");
                    csoda.Nr--;
                    return;
                }
            else if (_money > 19 && csoda.Nr > 0)
                {
                    Console.WriteLine("Giving coke out");
                    _money -= 20;
                    Console.WriteLine($"Giving {_money} out in change");
                    _money = 0;
                    csoda.Nr--;
                }
            else if (csoda.Nr == 0)
                {
                    Console.WriteLine($"No {csoda.Name} left");
                }
            else 
                {
                    Console.WriteLine($"Need {(csoda.Cost - _money)} more");
                }   
        }

        public void WriteUserOptions()
        {
            Console.WriteLine("\n\nAvailable commands:");
            Console.WriteLine("insert (money) - Money put into money slot");
            foreach(var soda in _inventory)
            {
                Console.WriteLine($"order {soda.Name} - Order from machines buttons");
                
            }
            Console.WriteLine("inventory - writes avaiable sodas and their stock");
            Console.WriteLine("sms soda - Order sent by sms");
            Console.WriteLine($"recall - {_money} gives money back");
            Console.WriteLine("-------");
            Console.WriteLine($"Inserted money: {_money}");
            Console.WriteLine("-------\n\n");
        }

    }
}
