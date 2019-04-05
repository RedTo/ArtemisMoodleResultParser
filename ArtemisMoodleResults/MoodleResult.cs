using FileHelpers;

namespace ArtemisMoodleResults
{
    /**
     * MoodleResult represents a entry in the moodle csv file.
     */
    [DelimitedRecord(";")]
    internal class MoodleResult
    {
        public string FirstName;
        public string LastName;
        public string ID;
        public string University;
        public string Faculty;
        public string Score;
        public string LastMoodleExport;
    }
}
