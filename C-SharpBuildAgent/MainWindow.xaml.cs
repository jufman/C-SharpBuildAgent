using System.Windows;
using C_SharpBuildAgent.Lib.Build;
using C_SharpBuildAgent.Lib.Build.Objects;
using C_SharpBuildAgent.Lib.Settings.Objects;

namespace C_SharpBuildAgent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Logic buildLogic = new Logic(new Details()
            {
                TempFolderLocation = @"C:\temp\AutoBuild\",
                PublicKeyLocation = @"‪C:\Temp\idrsa.key",
                PrivriteKeyLocation = @"‪‪C:\Temp\github.ppk",
            });

            buildLogic.BuildItem(new BuildItem()
            {
                GitRepoAddress = "git@github.com:jufman/AutoFormGenorator.git",
                Name = "AutoFormGenorator",
                PathToSln = @"AutoFormGenorator\AutoFormGenorator.csproj",
                Platform = "Any CPU"
            });
        }
    }
}
