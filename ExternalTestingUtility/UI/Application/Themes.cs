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
        internal static readonly UIThemeInfo Orange, Mikey, Light, Watermelon, PurpleDrank;
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
            Orange.GripLightColor = Orange.ButtonHoverColor;
            Orange.GripDarkColor = Color.FromArgb(132, 132, 132);
            Orange.ArrowColor = Orange.GripDarkColor;

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

            Light = new UIThemeInfo();
            Light.BackColor = SystemColors.ControlLight;
            Light.TextColor = SystemColors.ControlText;
            Light.AccentColor = SystemColors.Highlight;
            Light.TitleBarColor = SystemColors.Control;
            Light.ButtonFlatStyle = FlatStyle.Flat;
            Light.ButtonHoverColor = SystemColors.Highlight;
            Light.LightBackColor = SystemColors.Control;
            Light.ButtonActive = SystemColors.HotTrack;
            Light.TextInactive = SystemColors.InactiveCaptionText;
            Light.GripLightColor = Color.LightGray;
            Light.GripDarkColor = Color.Black;
            Light.ArrowColor = Color.Black;

            Watermelon = new UIThemeInfo();
            Watermelon.BackColor = Color.MediumAquamarine;
            Watermelon.TextColor = Color.LavenderBlush;
            Watermelon.AccentColor = Color.Crimson;
            Watermelon.TitleBarColor = Color.LightCoral;
            Watermelon.ButtonFlatStyle = FlatStyle.Flat;
            Watermelon.ButtonHoverColor = Color.SeaGreen;
            Watermelon.LightBackColor = Color.LightCoral;
            Watermelon.ButtonActive = Color.Crimson;
            Watermelon.TextInactive = Color.Black;
            Watermelon.GripLightColor = Color.Firebrick;
            Watermelon.GripDarkColor = Color.DarkOliveGreen;
            Watermelon.ArrowColor = Color.Red;

            PurpleDrank = new UIThemeInfo();
            PurpleDrank.BackColor = Color.MediumPurple;
            PurpleDrank.TextColor = Color.WhiteSmoke;
            PurpleDrank.AccentColor = Color.MediumOrchid;
            PurpleDrank.TitleBarColor = Color.SlateBlue;
            PurpleDrank.ButtonFlatStyle = FlatStyle.Flat;
            PurpleDrank.ButtonHoverColor = Color.Plum;
            PurpleDrank.LightBackColor = Color.Thistle;
            PurpleDrank.ButtonActive = Color.MediumOrchid;
            PurpleDrank.TextInactive = Color.Black;
            PurpleDrank.GripLightColor = Color.Purple;
            PurpleDrank.GripDarkColor = Color.Lavender;
            PurpleDrank.ArrowColor = Color.WhiteSmoke;
        }
    }
}
