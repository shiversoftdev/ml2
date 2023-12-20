using ML2.UI.Core.Singletons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML2.UI.Application
{
    internal static class Themes
    {
        internal static readonly UIThemeInfo Orange, Mikey;
        static Themes()
        {
            Orange = new UIThemeInfo();
            Orange.BackColor = Color.FromArgb(28, 28, 28);
            Orange.TextColor = Color.WhiteSmoke;
            Orange.AccentColor = Color.DarkOrange;
            Orange.TitleBarColor = Color.FromArgb(36, 36, 36);
            Orange.ButtonFlatStyle = FlatStyle.Flat;
            Orange.ButtonHoverColor = Color.FromArgb(50, 50, 50);
            Orange.LightBackColor = Color.FromArgb(36, 36, 36);
            Orange.ButtonActive = Color.DarkOrange;
            Orange.TextInactive = Color.Gray;

            Mikey = new UIThemeInfo();
            Mikey.BackColor = Color.FromArgb(14, 14, 18);
            Mikey.TextColor = Color.WhiteSmoke;
            Mikey.AccentColor = Color.SteelBlue;
            Mikey.TitleBarColor = Color.FromArgb(24, 24, 28);
            Mikey.ButtonFlatStyle = FlatStyle.Flat;
            Mikey.ButtonHoverColor = Color.FromArgb(42, 42, 50);
            Mikey.LightBackColor = Color.FromArgb(22, 22, 28);
            Mikey.ButtonActive = Color.SteelBlue;
            Mikey.TextInactive = Color.Gray;
            Mikey.GripLightColor = Color.FromArgb(42, 42, 50);
            Mikey.GripDarkColor = Color.FromArgb(132, 132, 142);
            Mikey.ArrowColor = Mikey.GripDarkColor;
        }
    }
}
