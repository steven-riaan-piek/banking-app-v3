using System;
using System.Collections.Generic;
using System.IO;

class BankAccount
{
    public decimal Balance { get; private set; }

    public List<string> History { get; private set; }

    private string filePath;

    public BankAccount(string userName)
    {
        filePath = userName.ToLower() + "_data.txt";
        History = new List<string>();
        LoadData();

    }

    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            return false;
        }
        Balance += amount;
        string record = $"Deposited: {amount:C2} on {DateTime.Now}";
        History.Add(record);
        SaveData();
        return true;

    }
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > Balance)
        {
            return false;
        }
        Balance -= amount;
        string record = $"Withdraw: {amount:C2} on {DateTime.Now}";
        History.Add(record);
        SaveData();
        return true;
    }
    public void ShowHistory()
    {
        foreach (string item in History)
        {
            Console.WriteLine(item);
        }
    }
    public bool TransferTo(BankAccount recipient, decimal amount)
    {
        if(amount <= 0 || amount > Balance || recipient == null)
        {
            return false;
        }
        Balance -= amount;
        recipient.Balance += amount;

        string senderRecord = $"Trassffered {amount:C2} to {recipient.GetName()} on {DateTime.Now}";
        string recieveRecord = $"Recieved {amount:C2} from transfer on {DateTime.Now}";

        History.Add(senderRecord);
        recipient.History.Add(recieveRecord);

        SaveData();
        recipient.SaveData();

        return true;

    }

    public string GetName()
    {
        return filePath.Replace("_data.txt", "");
    }

    public void SaveData()
    {
        List<string> lines = new List<string>();
        lines.Add(Balance.ToString());
        lines.AddRange(History);

        File.WriteAllLines(filePath, lines);

    }
    public void LoadData()
    {
        if (!File.Exists(filePath))
        {
            return;
        }
        var lines = File.ReadAllLines(filePath);
        if (lines.Length > 0)
        {
            Balance = Convert.ToDecimal(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                History.Add(lines[i]);
            }
        }
    }


}


