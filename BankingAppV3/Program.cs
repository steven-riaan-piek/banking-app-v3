using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    //I extended my banking app into a multi-user system using a dictionary
    //to manage accounts, added user login, and implemented secure fund transfers between accounts with persistent storage.


// So instead of single user, it is now a multi-user system
//Added account management and transfers so users can transfer money between each other


    static Dictionary<string, BankAccount> accounts = new Dictionary<string, BankAccount>();
    static void Main()
    {

        //BankAccount account = new BankAccount("pete");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Please enter your username (or `exit` to quit)");
            string name = Console.ReadLine().Trim();

            if(name.ToLower() == "exit")
            {
                return;
            }
            if (!accounts.ContainsKey(name))
            {
                accounts[name] = new BankAccount(name);
            }
            RunMenu(name, accounts[name]);
        }

        


    static void RunMenu(string name, BankAccount account)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to banking app v2");
                Console.WriteLine("1.Balance \n2. Deposit\n3. Withdraw\n4. History\n5. Transfer\n6. Logout");
                Console.WriteLine("Choose an option");
                string option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            Console.WriteLine($"Balance: {account.Balance:C2}");
                            break;
                        case "2":
                            Console.WriteLine("Amount?");
                            decimal deposit = Convert.ToDecimal(Console.ReadLine());
                            if (account.Deposit(deposit))
                            {
                                Console.WriteLine("Deposit successful");
                            }
                            else
                            {
                                Console.WriteLine("Invalid deposit amount");
                            }
                            break;
                        case "3":
                            Console.WriteLine("Amount?");
                            decimal withdraw = Convert.ToDecimal(Console.ReadLine());
                            if (account.Withdraw(withdraw))
                            {
                                Console.WriteLine("Withdrawel successful");
                            }
                            else
                            {
                                Console.WriteLine("Invalid deposit amount");
                            }

                            break;
                        case "4":
                            Console.WriteLine("Transaction History:");
                            account.ShowHistory();
                            break;
                        case "5":
                            Console.WriteLine("Enter recipient name");
                            string recipientName = Console.ReadLine();
                            if (!accounts.ContainsKey(recipientName))
                            {
                                Console.WriteLine("Does not exist");
                                break;
                            }

                            Console.WriteLine("Amount");
                            decimal amount = Convert.ToDecimal(Console.ReadLine());

                            if (account.TransferTo(accounts[recipientName], amount))
                            {
                                Console.WriteLine("Transfer succsefull");
                            }
                            else
                            {
                                Console.WriteLine("Transfer Failed");
                            }
                            break;

                        case "6":
                                    Console.WriteLine("Goodbye");
                                    return;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;

                                }
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                }

            }
        }




    }
}

