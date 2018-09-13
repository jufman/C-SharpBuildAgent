using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            Lib.Build.Logic buildLogic = new Logic(new Details()
            {
                TempFolderLocation = @"C:\temp\AutoBuild\",
                PublicKeyLocation = @"C:\Users\userc\.ssh\pub.key",
                PrivriteKeyLocation = @"C:\Users\userc\.ssh\Pub-2.ppk",
                MsBuildEXELocaion = ""
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
