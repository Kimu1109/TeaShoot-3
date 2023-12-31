#pragma warning disable CA1416

using RoslynPad.Editor;
using RoslynPad.Roslyn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace TeaShoot_3
{
    /// <summary>
    /// Editer.xaml の相互作用ロジック
    /// </summary>
    public partial class Editer : UserControl
    {
        public Editer()
        {
            InitializeComponent();
        }

        private async void RoslynCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            var roslynPadAssemblies = new[]
                {
                    typeof(RoslynCodeEditor).Assembly,
                    typeof(GlyphExtensions).Assembly
                };

            var assemblies = new[]
                {
                     typeof(object).Assembly,
                     typeof(Obj).Assembly
                };

            var roslynHost = new CustomRoslynHost(typeof(Obj),roslynPadAssemblies,RoslynHostReferences.NamespaceDefault.With(assemblyReferences: assemblies));

            await roslynCodeEditor.InitializeAsync(roslynHost, new ClassificationHighlightColors(), Directory.GetCurrentDirectory(), string.Empty, Microsoft.CodeAnalysis.SourceCodeKind.Script);
        }
        public void setText(string text)
        {
            roslynCodeEditor.Text = text;
        }
    }
}
