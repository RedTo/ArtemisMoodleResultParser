﻿using FileHelpers;

namespace ArtemisMoodleResults
{
    [DelimitedRecord(";")]
    class MoodleResult
    {
        public string FirstName;
        public string LastName;
        public string ID;
        public string University;
        public string Faculty;
        //public string Unused;
        public string Score;
        public string Unused2;
    }
}