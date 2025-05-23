namespace Crappy_Heuristic_AI_Detector;

public class Configuration
{
    public string UncommonCharsRegex { get; set; }
    public string UncommonSpacesRegex { get; set; }
    public string UncommonQuotesCharsRegex { get; set; }   // fancy quotes like “ ” ‘ ’
    public string CommonQuotesRegex { get; set; }          // common quotes " '
    public string[] UncommonWords { get; set; }
    
    public string[] WordSplitChars { get; set; }
    public char[] SentenceSplitChars { get; set; }
    
    public SentenceMetricsConfig SentenceMetrics { get; set; }
    public ScoringWeightsConfig ScoringWeights { get; set; }
}


public class SentenceMetricsConfig
{
    public int CommonSentenceLengthForHuman { get; set; }
    public int VeryLongSentenceThreshold { get; set; }
    public int HighSentenceCountThreshold { get; set; }
}

public class ScoringWeightsConfig
{
    public float UncommonCharWeight { get; set; }
    public float UncommonSpaceWeight { get; set; }
    public float UncommonWordWeight { get; set; }
    public float LongSentencePenalty { get; set; }
    public float HighSentenceCountBonus { get; set; }
}
