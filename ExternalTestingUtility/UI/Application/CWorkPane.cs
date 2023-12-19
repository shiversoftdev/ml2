using ML2.Core;
using ML2.UI.Core.Interfaces;
using ML2.UI.Core.Singletons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML2.UI.Application
{
    public partial class CWorkPane : UserControl, IThemeableControl
    {
        public CWorkPane()
        {
            InitializeComponent();
            ProjectManager.OnActiveProjectChanged += OnActiveProjectChanged;
            Shared.Console.OnLogMessage += OnConsoleMessage;
            UIThemeManager.OnThemeChanged(this, ThemeUpdated);
        }

        private void ThemeUpdated(UIThemeInfo themeData)
        {
            ConsoleBox.BorderStyle = BorderStyle.FixedSingle;
            ConsoleBox.ForeColor = Color.WhiteSmoke;
            ConsoleBox.BackColor = Color.Black;
        }

        private void OnActiveProjectChanged(ML2Project project)
        {

        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield break;
        }

        Color previous = Color.White;
        private void HighlightText(ReadOnlySpan<char> text)
        {
            int index;
            Color c = previous;

            while ((index = text.IndexOf('^')) > -1)
            {
                var selection = text.Slice(0, index);
                int prevLen = ConsoleBox.Text.Length;
                ConsoleBox.AppendText(selection.ToString());
                ConsoleBox.SelectionStart = prevLen;
                ConsoleBox.SelectionLength = selection.Length;
                ConsoleBox.SelectionColor = c;

                if (text.Length > index + 1)
                {
                    switch (text[index + 1])
                    {
                        case '0':
                            c = Color.White;
                            break;
                        case '1':
                            c = Color.FromArgb(247, 10, 10);
                            break;
                        case '2':
                            c = Color.FromArgb(79, 235, 52);
                            break;
                        case '3':
                            c = Color.FromArgb(237, 222, 5);
                            break;
                        case '4':
                            c = Color.Blue;
                            break;
                        case '5':
                            c = Color.FromArgb(15, 171, 214);
                            break;
                        case '6':
                            c = Color.FromArgb(189, 52, 235);
                            break;
                        case '7':
                            c = Color.White;
                            break;
                        case '8':
                            c = Color.Gray;
                            break;
                        case '9':
                            c = Color.DarkOrange;
                            break;
                    }
                }

                if (index + 2 < text.Length)
                {
                    text = text.Slice(index + 2);
                }
                else
                {
                    text = "".AsSpan();
                }
            }
            {
                var selection = text;
                int prevLen = ConsoleBox.Text.Length;
                ConsoleBox.AppendText(text.ToString());
                ConsoleBox.SelectionStart = prevLen;
                ConsoleBox.SelectionLength = selection.Length;
                ConsoleBox.SelectionColor = c;
            }
            previous = c;
        }

        private void OnConsoleMessage(string message)
        {
            HighlightText(message.AsSpan());
            ConsoleBox.SelectionStart = ConsoleBox.Text.Length;
            ConsoleBox.ScrollToCaret();
        }
    }
}
