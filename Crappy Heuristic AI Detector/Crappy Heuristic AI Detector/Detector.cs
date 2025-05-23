using System.Text.RegularExpressions;
using System.Text.Json;
namespace Crappy_Heuristic_AI_Detector;

public class Detector
{
    private int weirdScore;

    private string[] _uncommonWords;
    private string _uncommonCharsRegex;
    private string _uncommonSpacesRegex;

    Configuration _config = new Configuration();
    
    public Detector(string jsonConfigFile)
    {
        var text = File.ReadAllText(jsonConfigFile);
        if (string.IsNullOrEmpty(text))
        {
            ShowError("No Config.json file found");
            return;
        }

        var deserializedJson = JsonSerializer.Deserialize<Configuration>(text);
        if (deserializedJson == null)
        {
            ShowError("Couldn't deserialize config.json");
            return;
        }
        
        _config = deserializedJson;
        
        _uncommonCharsRegex = _config.UncommonCharsRegex;
        _uncommonSpacesRegex = _config.UncommonSpacesRegex;
        _uncommonWords = _config.UncommonWords;
    }

    
    public DetectionResult GetSuspicionResult(string input)
    {
        var result = new DetectionResult();
        if (string.IsNullOrWhiteSpace(input))
            return new DetectionResult();
        double score = 0;

        // Count uncommon chars
        var uncommonCharMatches = Regex.Matches(input, _config.UncommonCharsRegex);
        var uncommonQuotesMatches = Regex.Matches(input, _config.UncommonQuotesCharsRegex);
        double susCharScore = 0;
        
        susCharScore += uncommonQuotesMatches.Count * _config.ScoringWeights.UncommonCharWeight;
        susCharScore += uncommonCharMatches.Count * _config.ScoringWeights.UncommonCharWeight;
        score += susCharScore;
        if(susCharScore > 0) result.UncommonFeatures |= UncommonFeatures.UncommonCharacters;
        result.UncommonCharsCount = uncommonCharMatches.Count;
        
        // Count uncommon spaces
        var uncommonSpaceMatches = Regex.Matches(input, _config.UncommonSpacesRegex);
        score += uncommonSpaceMatches.Count * _config.ScoringWeights.UncommonSpaceWeight;
        if(uncommonSpaceMatches.Count >= 1) result.UncommonFeatures |= UncommonFeatures.UncommonSpaces;
        result.UncommonUncommonSpacesCount = uncommonSpaceMatches.Count;
        
        // Count uncommon words (case-insensitive)
        var words = input.Split(_config.WordSplitChars, StringSplitOptions.RemoveEmptyEntries);
        int uncommonWordCount = words.Count(w => _config.UncommonWords.Contains(w, StringComparer.OrdinalIgnoreCase));
        
        score += uncommonWordCount * _config.ScoringWeights.UncommonWordWeight;
        if(uncommonWordCount >= 1) result.UncommonFeatures |= UncommonFeatures.UncommonWords;
        result.WordsCount = words.Length;
        result.UncommonWordsCount = uncommonWordCount;
        
        // Sentence splitting (by . ! ?)
        var sentences = input.Split(_config.SentenceSplitChars, StringSplitOptions.RemoveEmptyEntries);
        result.SentencesCount = sentences.Count();
        
        // Count very long sentences (words > threshold)
        int veryLongSentences = sentences.Count(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length >= _config.SentenceMetrics.VeryLongSentenceThreshold);
        score += veryLongSentences * _config.ScoringWeights.LongSentencePenalty; // negative weight (penalty, which is good for human-like sentence)
        
        // Add score if total sentences > threshold
        if (sentences.Length >= _config.SentenceMetrics.HighSentenceCountThreshold)
        {
            score += _config.ScoringWeights.HighSentenceCountBonus;
            result.UncommonFeatures |= UncommonFeatures.LotsOfSentences;
        } // Unless this is for a story etc... in common forums people tend to forget common ! ? . etc...
        
// Assume config is an instance of the class containing the regex patterns
        var uncommonQuotesRegex = new Regex(_config.UncommonQuotesCharsRegex);
        var commonQuotesRegex = new Regex(_config.CommonQuotesRegex);

        bool hasCommonQuotes = commonQuotesRegex.IsMatch(input);
        bool hasUncommonQuotes = uncommonQuotesRegex.IsMatch(input);

        if (hasCommonQuotes && hasUncommonQuotes)
        {
            result.UncommonFeatures |= UncommonFeatures.SuspiciousQuotesCombination;
            score += 5; // adjust boost score as needed
        }

        result.FinalScore = score;
        return result;
    }
    void ShowError(string errorText)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(">>> Error: " + errorText);
        Console.ResetColor();
    }
}