using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpBuildAgent.Lib.Build.Objects
{
    public class BuildItem
    {
   
        public string GitRepoAddress { get; set; }

        public string S3BuketLocation { get; set; }

        public string S3FileLocation { get; set; }

        public string Version { get; set; }

        public string Name { get; set; }

        public string Platform { get; set; }

        public string PathToSln { get; set; }
        

    }
}
