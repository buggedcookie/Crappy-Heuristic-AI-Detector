namespace Crappy_Heuristic_AI_Detector;

public class DetectionResult
{
    public double FinalScore = 0;
    public UncommonFeatures UncommonFeatures = UncommonFeatures.None;
    public int UncommonCharsCount = 0;
    public int UncommonWordsCount = 0;
    public int UncommonUncommonSpacesCount = 0;
    public int WordsCount = 0;
    public int SentencesCount = 0;
}