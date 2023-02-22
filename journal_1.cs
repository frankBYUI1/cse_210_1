using System;
using System.Collections.Generic;
using System.IO;

class Entry {
    public string prompt;
    public string response;
    public DateTime date;

    public Entry(string p, string r) {
        prompt = p;
        response = r;
        date = DateTime.Now;
    }

    public override string ToString() {
        return $"{date.ToString("MM/dd/yyyy")} - {prompt}: {response}";
    }
}

class Journal {
    private List<Entry> entries = new List<Entry>();

    public void AddEntry(string prompt, string response) {
        entries.Add(new Entry(prompt, response));
    }

    public void DisplayEntries() {
        foreach (Entry entry in entries) {
            Console.WriteLine(entry.ToString());
        }
    }

    public void SaveToFile(string filename) {
        using (StreamWriter writer = new StreamWriter(filename)) {
            foreach (Entry entry in entries) {
                writer.WriteLine($"{entry.prompt},{entry.response},{entry.date.ToString()}");
            }
        }
    }

    public void LoadFromFile(string filename) {
        entries.Clear();
        using (StreamReader reader = new StreamReader(filename)) {
            while (!reader.EndOfStream) {
                string[] fields = reader.ReadLine().Split(',');
                entries.Add(new Entry(fields[0], fields[1]) { date = DateTime.Parse(fields[2]) });
            }
        }
    }
}

class Program {
    static void Main(string[] args) {
        Journal journal = new Journal();
        List<string> prompts = new List<string> {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        while (true) {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to a file");
            Console.WriteLine("4. Load journal from a file");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice) {
                case "1":
                    string prompt = prompts[new Random().Next(prompts.Count)];
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("Response: ");
                    string response = Console.ReadLine();
                    journal.AddEntry(prompt, response);
                    Console.WriteLine("Entry added.");
                    break;

                case "2":
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("Enter a filename: ");
                    string filename1 = Console.ReadLine();
                    journal.SaveToFile(filename1);
                    Console.WriteLine("Journal saved to file.");
                    break;

                case "4":
                    Console.Write("Enter a filename: ");
                    string filename2 = Console.ReadLine();
                    journal.LoadFromFile(filename2);
                    Console.WriteLine("Journal loaded from file.");
                    break;

                case "5":
                    Console.WriteLine("Exiting program.");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}