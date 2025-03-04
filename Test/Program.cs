using Test;

    Console.WriteLine("Internet Usage Fee Calculator");
    Console.WriteLine("=============================");

    try
    {
        Console.Write("Enter start hour (0-23): ");
        int startHour = int.Parse(Console.ReadLine());

        Console.Write("Enter start minute (0-59): ");
        int startMinute = int.Parse(Console.ReadLine());

        Console.Write("Enter end hour (0-23): ");
        int endHour = int.Parse(Console.ReadLine());

        Console.Write("Enter end minute (0-59): ");
        int endMinute = int.Parse(Console.ReadLine());

        if (startHour < 0 || startHour > 23 || endHour < 0 || endHour > 23 ||
            startMinute < 0 || startMinute > 59 || endMinute < 0 || endMinute > 59)
        {
            Console.WriteLine("Invalid time input. Hours must be 0-23 and minutes must be 0-59.");
            return;
        }

        double fee = FeeCalculator.CalculateFee(startHour, startMinute, endHour, endMinute);

        Console.WriteLine($"Total fee: {fee:N0} đồng");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();