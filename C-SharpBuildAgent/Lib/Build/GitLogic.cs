using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace C_SharpBuildAgent.Lib.Build
{
    public static class GitLogic
    {

        public static bool PullRepo(Settings.Objects.Details details, string repoFolder)
        {
            using (var repo = new Repository(repoFolder))
            {
                // Credential information to fetch
                LibGit2Sharp.PullOptions options = new LibGit2Sharp.PullOptions();
                options.FetchOptions = new FetchOptions();
                options.FetchOptions.CredentialsProvider = getCredentialsHandler(details);

                // User information to create a merge commit
                var signature = new LibGit2Sharp.Signature(
                    new Identity("MERGE_USER_NAME", "MERGE_USER_EMAIL"), DateTimeOffset.Now);

                // Pull
                MergeResult mergeResult = Commands.Pull(repo, signature, options);

                if (mergeResult.Status == MergeStatus.Conflicts || mergeResult.Status == MergeStatus.NonFastForward)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CloneRepo(Settings.Objects.Details details, string repoAddress, string destination)
        {
            var output = string.Empty;

            Directory.CreateDirectory(destination);

            try 
            {
                CloneOptions options = new CloneOptions
                {
                    Checkout = true,
                    CredentialsProvider = getCredentialsHandler(details)
                };

                output = Repository.Clone(repoAddress, destination, options);
            }
            catch (Exception e)
            { 
                MessageBox.Show("Error: " + e.Message);
                return false;
            }

            return true;
        }

        private static CredentialsHandler getCredentialsHandler(Settings.Objects.Details details)
        {
            return new CredentialsHandler((url, user, cred) => new SshUserKeyCredentials()
            {
                PrivateKey = details.PrivriteKeyLocation,
                PublicKey = details.PublicKeyLocation,
                Passphrase = string.Empty,
                Username = "git"
            });
        }

    }
}
