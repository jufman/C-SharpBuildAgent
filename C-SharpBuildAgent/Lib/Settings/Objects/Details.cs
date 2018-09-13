using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpBuildAgent.Lib.Settings.Objects
{
    public class Details
    {

        public string PublicKeyLocation { get; set; }

        public string PrivriteKeyLocation { get; set; }

        public string TempFolderLocation { get; set; }

        public string MsBuildEXELocaion { get; set; }

        public string BuildLocation => TempFolderLocation + "Build\\";

        public string SourcesLocation => TempFolderLocation + "Sources\\";
    }
}
