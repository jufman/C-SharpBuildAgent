using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpBuildAgent.Lib.Build
{
    public class Logic
    {

        private Settings.Objects.Details SettingsDetails;

        public Logic(Settings.Objects.Details settingsDetails)
        {
            SettingsDetails = settingsDetails;
        }

        public void BuildItem(Objects.BuildItem buildItem)
        {
            if (PullGitRepo(buildItem) == false)
            {
                return;
            }

            if (BuildProject(buildItem) == false)
            {
                return;
            }

            if (PublishToS3(buildItem) == false)
            {
                return;
            }

        }

        private bool PublishToS3(Objects.BuildItem buildItem)
        {
            return true;
        }

        private bool BuildProject(Objects.BuildItem buildItem)
        {
            BuildLogic.BuildProject(buildItem, SettingsDetails);

            return true;
        }

        private bool PullGitRepo(Objects.BuildItem buildItem)
        {
            var cloneFoler = SettingsDetails.SourcesLocation + buildItem.Name;

            if (Directory.Exists(cloneFoler + "\\.git"))
            { 
                return GitLogic.PullRepo(SettingsDetails, cloneFoler);
            }
            else
            {
                return GitLogic.CloneRepo(SettingsDetails, buildItem.GitRepoAddress, cloneFoler);
            }
        }

    }
}
