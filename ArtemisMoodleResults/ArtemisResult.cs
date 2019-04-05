using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace ArtemisMoodleResults
{
    [DelimitedRecord(",")]
    class ArtemisResult
    {
        public string Name;
        public string Username;
        public string Score;
        public string RepoLink;
    }
}
