using ML2.Cheats;
using ML2.UI.Application;
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
using static ML2.Cheats.BlackOps3.ClassSetType;
using static ML2.Cheats.BlackOps3.loadoutClass_t;

namespace ML2
{
    public partial class MainForm : Form, IThemeableControl
    {
        public MainForm()
        {
            // Sets up stealth calls for native funcs to try to avoid api hooking
            NativeStealth.SetStealthMode(NativeStealthType.ManualInvoke);

            InitializeComponent();
            UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
            this.SetThemeAware();
            UIThemeManager.ApplyTheme(Themes.Orange);
            MaximizeBox = true;
            MinimizeBox = true;

            FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckExitAllowed())
            {
                return;
            }
            e.Cancel = true;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return InnerForm;
            yield return TopMenuBar;
            yield return toolStrip1;
        }

        private void OnThemeChanged_Implementation(UIThemeInfo currentTheme)
        {
        }

        private void RPCTest1_Click(object sender, EventArgs e)
        {
            BlackOps3.Cbuf_AddText("disconnect\n");
        }

        private void RPCExample2_Click(object sender, EventArgs e)
        {
            BlackOps3.ApplyHostDvars();
        }

        private void RPCExample3_Click(object sender, EventArgs e)
        {
            BlackOps3.Cbuf_AddText("lobbyLaunchGame");
        }

        private void ExampleRPC4_Click(object sender, EventArgs e)
        {
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primary", 14);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primarycamo", 124);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment1", 6);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment2", 15);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment3", 8);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment4", 9);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment5", 10);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment6", 14);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment7", 12);
            BlackOps3.BG_UnlockablesSetClassSetItem(CLASS_SET_TYPE_MP_PUBLIC, 0, CUSTOM_CLASS_1, "primaryattachment8", 13);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BlackOps3.SetZMWeaponLoadout(1, new BlackOps3.ZMLoadoutData());
        }

        private void openInRadiantToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool CheckExitAllowed()
        {
            if (AppEnv.ExitStatus == ExitProhibitedReason.ALLOWED)
            {
                return true;
            }
            // TODO (remember to edit notepad too when done)
            return false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitAllowed())
            {
                return;
            }
            Application.Exit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
