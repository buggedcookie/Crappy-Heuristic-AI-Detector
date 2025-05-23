namespace Crappy_Heuristic_AI_Detector;

class Program
{
    static void Main(string[] args)
    {
        ShowBanner();
        ShowWarning();

        Detector detector = new("./Config.json");
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine(">>> Write \"--warning\" to print warning.");
            Console.WriteLine(">>> Write \"--exit\" to exit.");
            Console.WriteLine();
            Console.WriteLine("<<< Paste the text below >>> ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            var text = Console.ReadLine() ?? string.Empty;
            Console.Clear();
            if (text == "--exit") break;
            if (text == "--warning")
            {
                ShowWarning();
                continue;
            }

            var results = detector.GetSuspicionResult(text);
            PrintDetectionResult(results);
        }
    }

    static void ShowBanner()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("             ┏┓ ┓┏ ┏┓ ┳┓        ");
        Console.WriteLine("             ┃  ┣┫ ┣┫ ┃┃        ");
        Console.WriteLine("             ┗┛•┛┗•┛┗•┻┛        ");
        Console.WriteLine("    Crappy Heuristic AI Detector");
        Console.WriteLine("             (C.H.A.D.)         ");
        Console.ResetColor();
    }

    static void ShowWarning()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════ WARNING ══════════════╗");
        Console.WriteLine("║ This tool analyzes input based on   ║");
        Console.WriteLine("║ uncommon characters and words.      ║");
        Console.WriteLine("║ It may produce false positives or   ║");
        Console.WriteLine("║ miss subtle patterns.               ║");
        Console.WriteLine("║                                     ║");
        Console.WriteLine("║ Do not rely solely on its output.   ║");
        Console.WriteLine("╚═════════════════════════════════════╝");
        Console.ResetColor();
    }

    public static void PrintDetectionResult(DetectionResult result)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║      C.H.A.D. Detection Summary      ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.WriteLine("· Lower is better");
        Console.WriteLine($"· Final Score            : {result.FinalScore:0.00}");
        Console.WriteLine($"· Flags Detected         : {result.UncommonFeatures,-20}");
        Console.WriteLine("╠═══════════ Counts ══════════════════╣");
        Console.WriteLine("· Lower is better");
        Console.WriteLine($"· Uncommon Characters    : {result.UncommonCharsCount,5}");
        Console.WriteLine($"· Uncommon Words         : {result.UncommonWordsCount,5}");
        Console.WriteLine($"· Uncommon Spaces        : {result.UncommonUncommonSpacesCount,5}");
        Console.WriteLine("╠═══════════ Text Metrics ════════════╣");
        Console.WriteLine($"· Word Count             : {result.WordsCount,5}");
        Console.WriteLine($"· Sentence Count         : {result.SentencesCount,5}");
        PrintColoredVerdict(result.FinalScore);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╚═════════════════════════════════════╝");
        Console.WriteLine();
    }

    public static void PrintColoredVerdict(double score)
    {
        string verdict;
        ConsoleColor color;

        if (score < 1)
        {
            verdict = "Unlikely AI-generated";
            color = ConsoleColor.Green;
        }
        else if (score < 3)
        {
            verdict = "Possibly AI-generated";
            color = ConsoleColor.Yellow;
        }
        else if (score < 5)
        {
            verdict = "Likely AI-generated";
            color = ConsoleColor.DarkYellow;
        }
        else
        {
            verdict = "Very Likely AI-generated";
            color = ConsoleColor.Red;
        }

        Console.ForegroundColor = color;
        Console.WriteLine($"· Verdict : {verdict}");
        Console.ResetColor();
    }
}