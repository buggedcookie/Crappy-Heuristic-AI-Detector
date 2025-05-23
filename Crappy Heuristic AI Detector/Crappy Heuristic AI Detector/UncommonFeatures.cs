namespace Crappy_Heuristic_AI_Detector;

[Flags]
public enum UncommonFeatures
{
    None = 0, 
    UncommonSpaces = 1 << 1, // 2
    UncommonWords = 1 << 2, // 4
    UncommonCharacters = 1 << 3, // 8
    LotsOfSentences = 1 << 4,
    VeryShortSentences = 1 << 5,
    SuspiciousQuotesCombination = 1 << 6,
}