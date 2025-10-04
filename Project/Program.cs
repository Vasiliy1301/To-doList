namespace To-doList;

public class Program
{
    List<TaskItem> tasks = new();

    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n===== TASK LIST MENU =====\n");
        Console.ResetColor();
        Console.Write("Enter 0 - to exit the program\n" +
                      "Enter 1 - to add a new task to the list\n" +
                      "Enter 2 - display the entire to-do list\n" +
                      "Enter 3 - mark task as completed\n" +
                      "Enter 4 - delete a task\n" +
                      "Number: ");

        string inputSelection = Console.ReadLine();

        if (inputSelection == "0")
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nThe program is completed.");
            Console.ResetColor();
            break;
        }

        if (inputSelection == "1")
            NewListItem();
        else if (inputSelection == "2")
            TodoListDisplay();
        else if (inputSelection == "3")
            CompletedTasks();
        else if (inputSelection == "4")
            DeleteTask();

        Console.Clear();
    }

    void NewListItem()
    {
        Console.Clear();
        Console.Write($"Task #{tasks.Count + 1}. Enter the task text: ");
        
        Console.ForegroundColor = ConsoleColor.White;
        string inputSelection = Console.ReadLine();
        Console.ResetColor();

        if (string.IsNullOrWhiteSpace(inputSelection) == false)
            tasks.Add(new TaskItem(tasks.Count + 1, inputSelection));
        else
            Console.WriteLine("\nEmpty input. Task not added.");

        Console.Write("\nPress any key to continue...");
        Console.ReadKey();
    }

    void TodoListDisplay()
    {
        ConsoleOutput();
        
        Console.Write("\nPress any key to continue...");
        Console.ReadKey();
    }

    void CompletedTasks()
    {
        if (tasks.Count == 0)
        {
            OutputTaskHeader();

            Console.WriteLine("\nNo tasks to mark. Add task.");
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        
        ConsoleOutput();

        Console.Write("\nEnter task number to mark completed: ");
        if (int.TryParse(Console.ReadLine(), out int id) == false)
            Console.WriteLine("\nInvalid number.");
        else
        {
            var task = tasks.Find(task => task.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\nTask #{id} marked completed.");
                Console.ResetColor();
            }
            else
                Console.WriteLine("\nTask not found.");
        }

        Console.Write("\nPress any key to continue...");
        Console.ReadKey();
    }

    void DeleteTask()
    {
        if (tasks.Count == 0)
        {
            OutputTaskHeader();

            Console.WriteLine("\nNo tasks to delete. Add task.");
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        ConsoleOutput();

        Console.Write("\nEnter task number to delete: ");
        
        if (int.TryParse(Console.ReadLine(), out int id) == false)
            Console.WriteLine("\nInvalid number.");
        else
        {
            int removed = tasks.RemoveAll(task => task.Id == id);
            
            if (removed > 0)
            {
                for (int i = 0; i < tasks.Count; i++) 
                    tasks[i].Id = i + 1;
                
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nTask #{id} deleted.");
                Console.ResetColor();
            }
            else
                Console.WriteLine($"\nTask not found.");
        }

        Console.Write("\nPress any key to continue...");
        Console.ReadKey();
    }

    void ConsoleOutput()
    {
        OutputTaskHeader();

        if (tasks.Count == 0)
        {
            Console.WriteLine("\nNo tasks available.");
            return;
        }
        
        foreach (var task in tasks)
        {
            Console.ForegroundColor = task.IsCompleted ? ConsoleColor.DarkCyan : ConsoleColor.White;

            Console.WriteLine($"#{task.Id} - {task.Text}");
        }

        Console.ResetColor();
    }

    void OutputTaskHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("===== LIST OF TASKS =====");
        Console.ResetColor();
    }

    class TaskItem
    {
        public int Id { get; set; }
        public string Text { get; }
        public bool IsCompleted { get; set; }

        public TaskItem(int id, string text)
        {
            Id = id;
            Text = text;
            IsCompleted = false;
        }
    }
}