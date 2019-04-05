using FileHelpers;

namespace ArtemisMoodleResults
{
    /**
     * ArtemisResult represents a entry in the artemis csv file.
     */
    [DelimitedRecord(",")]
    internal class ArtemisResult
    {
        public string Name;
        public string Username;
        public string Score;
        public string RepoLink;
    }
}
