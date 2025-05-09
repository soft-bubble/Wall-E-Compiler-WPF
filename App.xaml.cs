using System.Configuration;
using System.Data;
using System.Windows;

namespace Wall_E_Compiler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //string pixelArtCode = @"Spawn(0, 0)
            //                     Color(Black)
            //                     n <- 5
            //                     k <- 3 + 3 * 10
            //                     n <- k * 2
            //                    actual-x <- GetActualX()
            //                     i <- 0

            //                     loop-1
            //                     DrawLine(1, 0, 1)
            //                     i <- i + 1
            //                     is-brush-color-blue <- IsBrushColor(""Blue"")
            //                     Goto [loop-ends-here] (is-brush-color-blue == 1)
            //                     GoTo [loop1] (i < 10)

            //                     Color(""Blue"")
            //                     GoTo [loop1] (1 == 1)

            //                     loop-ends-here";

            // Ejecutar las pruebas de consola
            //Wall_E_Compiler.scripts.lexer.ConsoleTester.DebugLexerOutput(pixelArtCode);

            

            //Environment.Exit(0);

           
        }
    }

}
