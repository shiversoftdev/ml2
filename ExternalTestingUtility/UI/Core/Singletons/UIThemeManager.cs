using ML2.UI.Core.Interfaces;
using ML2.UI.Core.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ML2.UI.Core.Singletons
{
    internal struct UIThemeInfo
    {
        public Color BackColor;
        public Color AccentColor;
        public Color TextColor;
        public Color TitleBarColor;
        public FlatStyle ButtonFlatStyle;
        public Color ButtonHoverColor;
        public Color LightBackColor;
        public Color ButtonActive;
        public Color TextInactive;
        public Color GripLightColor;
        public Color GripDarkColor;
        public Color ArrowColor;

        public static UIThemeInfo Default()
        {
            UIThemeInfo theme = new UIThemeInfo();
            theme.BackColor = Color.FromArgb(28, 28, 28);
            theme.TextColor = Color.WhiteSmoke;
            theme.AccentColor = Color.DodgerBlue;
            theme.TitleBarColor = Color.FromArgb(36, 36, 36);
            theme.ButtonFlatStyle = FlatStyle.Flat;
            theme.ButtonHoverColor = Color.FromArgb(50, 50, 50);
            theme.LightBackColor = Color.FromArgb(36, 36, 36);
            theme.ButtonActive = Color.DodgerBlue;
            theme.TextInactive = Color.Gray;
            theme.GripLightColor = theme.LightBackColor;
            theme.GripDarkColor = theme.ButtonHoverColor;
            theme.ArrowColor = Color.WhiteSmoke;
            return theme;
        }
    }

    internal delegate void ThemeChangedCallback(UIThemeInfo themeData);

    internal static class UIThemeManager
    {
        public static UIThemeInfo CurrentTheme { get; private set; }
        private static HashSet<Control> ThemedControls = new HashSet<Control>();
        private static Dictionary<Type, ThemeChangedCallback> CustomTypeHandlers = new Dictionary<Type, ThemeChangedCallback>();
        private static Dictionary<Control, ThemeChangedCallback> CustomControlHandlers = new Dictionary<Control, ThemeChangedCallback>();
        private static CustomMenuStripRenderer CustomMenuRenderer;
        private static CustomToolStripRenderer ToolStripRenderer;
        static UIThemeManager()
        {
            CurrentTheme = UIThemeInfo.Default();
            CustomMenuRenderer = new CustomMenuStripRenderer();
            ToolStripRenderer = new CustomToolStripRenderer();
        }

        /// <summary>
        /// Makes this control, and all the children of this control, theme aware. Any classes which have not had a theme handler registered will throw an exception.
        /// </summary>
        /// <param name="control"></param>
        internal static void SetThemeAware(this IThemeableControl control)
        {
            if (!(control is Control ctrl)) throw new InvalidOperationException($"Cannot theme control of type '{control.GetType()}' because it is not derived from Control");
            foreach (Control c in control.GetThemedControls())
            {
                if (c == null) continue;
                if(c is IThemeableControl themed_c) SetThemeAware(themed_c);
                else RegisterAndThemeControl(c);
            }
            RegisterAndThemeControl(ctrl);
        }

        /// <summary>
        /// When a themed control is disposed, we are going remove it from the controls registry so it no longer receives theming data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ThemedControlDisposed(object sender, EventArgs e)
        {
            ThemedControls.Remove(sender as Control);
        }

        private static Brush HighlightBrush = new SolidBrush(CurrentTheme.AccentColor);
        private static Brush BackgroundBrush = new SolidBrush(CurrentTheme.BackColor);
        private static Brush TitlebarBrush = new SolidBrush(CurrentTheme.TitleBarColor);
        private static Brush ButtonHoverBrush = new SolidBrush(CurrentTheme.ButtonHoverColor);
        internal static void ApplyTheme(UIThemeInfo theme)
        {
            CurrentTheme = theme;
            HighlightBrush = new SolidBrush(CurrentTheme.AccentColor);
            BackgroundBrush = new SolidBrush(CurrentTheme.BackColor);
            ButtonHoverBrush = new SolidBrush(CurrentTheme.ButtonHoverColor);
            foreach (var control in ThemedControls)
            {
                ThemeSpecificControl(control);
            }
        }

        /// <summary>
        /// Register a handler for a non-default type when theming is requested.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        public static void RegisterCustomThemeHandler(Type type, ThemeChangedCallback callback)
        {
            if (callback == null)
            {
                CustomTypeHandlers.Remove(type);
                return;
            }

            if(CustomTypeHandlers.ContainsKey(type))
            {
                CustomTypeHandlers[type] += callback;
            }
            else
            {
                CustomTypeHandlers[type] = callback;
            }
        }

        public static void OnThemeChanged(Control control, ThemeChangedCallback callback)
        {
            if (control == null) return;
            if(callback == null)
            {
                CustomControlHandlers.Remove(control);
                return;
            }
            CustomControlHandlers[control] = callback;
            control.Disposed += CustomThemeCallback_ControlDisposed;
        }

        private static void CustomThemeCallback_ControlDisposed(object sender, EventArgs e)
        {
            CustomControlHandlers.Remove(sender as Control);
        }

        private static void ThemeSpecificControl(Control control)
        {
            if (CustomTypeHandlers.ContainsKey(control.GetType()))
            {
                CustomTypeHandlers[control.GetType()]?.Invoke(CurrentTheme);
            }
            else
            {
                switch (control)
                {
                    case Form form:
                        form.BackColor = CurrentTheme.BackColor;
                        form.ForeColor = CurrentTheme.TextColor;
                        break;
                    case GroupBox gBox:
                        gBox.Paint -= ThemedGroupBoxPaint;
                        gBox.Paint += ThemedGroupBoxPaint;
                        break;
                    case CComboBox cBox:
                        cBox.ForeColor = CurrentTheme.TextColor;
                        cBox.BackColor = CurrentTheme.BackColor;
                        cBox.BorderColor = CurrentTheme.AccentColor;
                        cBox.Cursor = Cursors.Hand;
                        cBox.FlatStyle = FlatStyle.Flat;
                        break;
                    case Button button:
                        button.BackColor = CurrentTheme.BackColor;
                        button.FlatAppearance.BorderColor = CurrentTheme.AccentColor;
                        button.FlatStyle = CurrentTheme.ButtonFlatStyle;
                        button.ForeColor = CurrentTheme.TextColor;
                        button.FlatAppearance.MouseOverBackColor = CurrentTheme.ButtonHoverColor;
                        break;
                    case Label label:
                        label.ForeColor = CurrentTheme.TextColor;
                        break;
                    case CThemedTextbox cTextBox:
                        cTextBox.BackColor = CurrentTheme.BackColor;
                        cTextBox.ForeColor = CurrentTheme.TextColor;
                        cTextBox.BorderStyle = BorderStyle.Fixed3D;
                        cTextBox.BorderColor = CurrentTheme.AccentColor;
                        break;
                    case RichTextBox rtb:
                        rtb.BorderStyle = BorderStyle.None;
                        rtb.BackColor = CurrentTheme.BackColor;
                        rtb.ForeColor = CurrentTheme.TextColor;
                        break;
                    case TextBox textBox:
                        textBox.BackColor = CurrentTheme.BackColor;
                        textBox.ForeColor = CurrentTheme.TextColor;
                        break;
                    case Panel panel:
                        panel.BackColor = CurrentTheme.BackColor;
                        break;
                    case UserControl uControl:
                        uControl.BackColor = CurrentTheme.BackColor;
                        uControl.ForeColor = CurrentTheme.TextColor;
                        break;
                    case CheckBox cb:
                        cb.ForeColor = CurrentTheme.TextColor;
                        cb.FlatStyle = FlatStyle.Flat;
                        cb.FlatAppearance.BorderColor = CurrentTheme.AccentColor;
                        break;
                    case TreeView tv:
                        tv.BackColor = CurrentTheme.BackColor;
                        tv.ForeColor = CurrentTheme.TextColor;
                        tv.BorderStyle = BorderStyle.None;
                        tv.DrawNode -= ThemeableNode_Draw;
                        tv.DrawMode = TreeViewDrawMode.OwnerDrawText;
                        tv.DrawNode += ThemeableNode_Draw;
                        break;
                    case DataGridView dgv:
                        dgv.BackgroundColor = CurrentTheme.BackColor;
                        dgv.ForeColor = CurrentTheme.TextColor;
                        dgv.BorderStyle = BorderStyle.None;
                        dgv.DefaultCellStyle.ForeColor = CurrentTheme.TextColor;
                        dgv.DefaultCellStyle.BackColor = CurrentTheme.BackColor;
                        dgv.GridColor = CurrentTheme.AccentColor;
                        dgv.DefaultCellStyle.SelectionForeColor = CurrentTheme.TextColor;
                        dgv.DefaultCellStyle.SelectionBackColor = CurrentTheme.AccentColor;
                        dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = CurrentTheme.TitleBarColor;
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = CurrentTheme.AccentColor;
                        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                        dgv.RowHeadersDefaultCellStyle.BackColor = CurrentTheme.BackColor;
                        dgv.RowHeadersDefaultCellStyle.ForeColor = CurrentTheme.AccentColor;
                        dgv.RowHeadersVisible = false;
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor = CurrentTheme.TitleBarColor;
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            dgv.Rows[i].DefaultCellStyle.ForeColor = dgv.ForeColor;
                            dgv.Rows[i].DefaultCellStyle.BackColor = CurrentTheme.BackColor;
                            dgv.Rows[i].DefaultCellStyle.SelectionForeColor = CurrentTheme.TextColor;
                            dgv.Rows[i].DefaultCellStyle.SelectionBackColor = CurrentTheme.AccentColor;
                        }
                        break;
                    case MenuStrip ms:
                        ms.BackColor = CurrentTheme.TitleBarColor;
                        ms.ForeColor = CurrentTheme.TextColor;
                        ms.RenderMode = ToolStripRenderMode.Professional;
                        ms.Renderer = CustomMenuRenderer;
                        foreach (ToolStripItem item in ms.Items)
                        {
                            ThemeTSI(item);
                        }

                        break;
                    case ToolStrip ts:
                        ts.BackColor = CurrentTheme.TitleBarColor;
                        ts.ForeColor = CurrentTheme.TextColor;
                        ts.RenderMode = ToolStripRenderMode.Professional;
                        ts.Renderer = ToolStripRenderer;
                        foreach (ToolStripItem item in ts.Items)
                        {
                            ThemeTSI(item);
                        }
                        break;
                    case ElementHost eh:
                        eh.BackColor = CurrentTheme.BackColor;
                        eh.ForeColor = CurrentTheme.TextColor;
                        break;
                    default: throw new NotImplementedException($"Theming procedure for control type: '{control.GetType()}' has not been implemented.");
                }
            }

            // invoke registered callbacks for theme changed
            if (CustomControlHandlers.ContainsKey(control))
                CustomControlHandlers[control].Invoke(CurrentTheme);
        }

        private static void ThemeTSI(ToolStripItem item, bool topmost = true)
        {
            switch(item)
            {
                case ToolStripSeparator sep:
                    sep.BackColor = CurrentTheme.BackColor;
                    break;
                case ToolStripButton tsb:
                    tsb.ForeColor = CurrentTheme.TextColor;
                    tsb.BackColor = topmost ? CurrentTheme.TitleBarColor : CurrentTheme.BackColor;
                    break;
                case ToolStripComboBox tscb:
                    tscb.BackColor = topmost ? CurrentTheme.TitleBarColor : CurrentTheme.BackColor;
                    tscb.ForeColor = CurrentTheme.TextColor;
                    tscb.FlatStyle = FlatStyle.Flat;
                    //tscb.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    //tscb.ComboBox.DrawItem += ComboBox_DrawItem;
                    break;
                case ToolStripSplitButton tssb:
                    tssb.BackColor = topmost ? CurrentTheme.TitleBarColor : CurrentTheme.BackColor;
                    tssb.ForeColor = CurrentTheme.TextColor;

                    foreach (ToolStripItem i in tssb.DropDownItems)
                    {
                        ThemeTSI(i, false);
                    }
                    break;
                case ToolStripDropDownButton ddb:
                    ddb.BackColor = topmost ? CurrentTheme.TitleBarColor : CurrentTheme.BackColor;
                    ddb.ForeColor = CurrentTheme.TextColor;
                    
                    foreach (ToolStripItem i in ddb.DropDownItems)
                    {
                        ThemeTSI(i, false);
                    }

                    break;
                case ToolStripMenuItem mi:
                    mi.ForeColor = CurrentTheme.TextColor;
                    mi.BackColor = topmost ? CurrentTheme.TitleBarColor : CurrentTheme.BackColor;
                   
                    foreach (ToolStripItem i in mi.DropDownItems)
                    {
                        ThemeTSI(i, false);
                    }
                    break;
                default:

                    throw new NotImplementedException($"Theming procedure for control type: '{item.GetType()}' has not been implemented.");
            }
        }

        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private static void Tscb_Paint(object sender, PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.Red))
                e.Graphics.FillRectangle(brush, (sender as ToolStripComboBox).ContentRectangle);

            ControlPaint.DrawBorder(e.Graphics, (sender as ToolStripComboBox).ContentRectangle,
                              Color.Red, 2, ButtonBorderStyle.Solid,
                              Color.Red, 2, ButtonBorderStyle.Solid,
                              Color.Red, 2, ButtonBorderStyle.Solid,
                              Color.Red, 2, ButtonBorderStyle.Solid);
        }

        internal static void RegisterAndThemeControl(Control control)
        {
            if(ThemedControls.Contains(control))
            {
                ThemeSpecificControl(control);
                return;
            }
            control.Disposed += ThemedControlDisposed;
            ThemedControls.Add(control);
            ThemeSpecificControl(control);
        }

        private static void ThemedGroupBoxPaint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, CurrentTheme.TextColor, CurrentTheme.AccentColor, CurrentTheme.BackColor);
        }

        private static void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor, Color backColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(backColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

        private static void ThemeableNode_Draw(object sender, DrawTreeNodeEventArgs e)
        {
            if(e.Node.IsEditing)
            {
                e.Graphics.FillRectangle(BackgroundBrush, e.Bounds);
                return;
            }

            if (e.Node.IsSelected && (sender as TreeView).Focused)
            {
                e.Graphics.FillRectangle(HighlightBrush, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(BackgroundBrush, e.Bounds);
            }

            if(!e.Node.TreeView.CheckBoxes)
            {
                TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Node.Bounds, CurrentTheme.TextColor);
            }
            else
            {
                Rectangle r = new Rectangle(new Point(e.Node.Bounds.Location.X + 2, e.Node.Bounds.Location.Y - 1), e.Node.Bounds.Size);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, r, CurrentTheme.TextColor);
            }
        }

        private class OverrideColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground
            {
                get
                {
                    return CurrentTheme.BackColor;
                }
            }

            public override Color ToolStripBorder
            {
                get
                {
                    return CurrentTheme.TitleBarColor;
                }
            }

            public override Color GripDark
            {
                get
                {
                    return CurrentTheme.GripDarkColor;
                }
            }

            public override Color GripLight
            {
                get
                {
                    return CurrentTheme.GripLightColor;
                }
            }

            public override Color SeparatorLight
            {
                get
                {
                    return CurrentTheme.GripLightColor;
                }
            }
            
            public override Color SeparatorDark
            {
                get
                {
                    return CurrentTheme.GripDarkColor;
                }
            }

            public override Color MenuItemSelected
            {
                get
                {
                    return CurrentTheme.ButtonHoverColor;
                }
            }

            public override Color ButtonSelectedHighlight
            {
                get
                {
                    return CurrentTheme.ButtonHoverColor;
                }
            }

            public override Color MenuBorder
            {
                get
                {
                    return CurrentTheme.ButtonHoverColor;
                }
            }

            public override Color MenuItemBorder
            {
                get
                {
                    return CurrentTheme.ButtonHoverColor;
                }
            }
            public override Color ToolStripGradientMiddle
            {
                get
                {
                    return CurrentTheme.TitleBarColor;
                }
            }

            public override Color ToolStripGradientEnd
            {
                get
                {
                    return CurrentTheme.TitleBarColor;
                }
            }
        }

        private class CustomMenuStripRenderer : ToolStripProfessionalRenderer
        {
            public CustomMenuStripRenderer() : base(new OverrideColorTable())
            {
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color c = (e.Item.Selected || e.Item.Pressed) ? CurrentTheme.ButtonHoverColor : e.Item.BackColor;
                using (SolidBrush brush = new SolidBrush(c))
                    e.Graphics.FillRectangle(brush, rc);
            }

            protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color c = e.Item.Selected ? CurrentTheme.AccentColor : e.Item.BackColor;
                using (SolidBrush brush = new SolidBrush(c))
                    e.Graphics.FillRectangle(brush, rc);
            }

            protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color c = (e.Item.Selected || e.Item.Pressed) ? CurrentTheme.AccentColor : e.Item.BackColor;
                using (SolidBrush brush = new SolidBrush(c))
                    e.Graphics.FillRectangle(brush, rc);
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                if(e is ToolStripArrowRenderEventArgsSBB x)
                {
                    e.ArrowColor = x.Color;
                    base.OnRenderArrow(e);
                    return;
                }
                e.ArrowColor = CurrentTheme.ArrowColor;
                base.OnRenderArrow(e);
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                base.OnRenderToolStripBorder(e);
                Rectangle rc = new Rectangle(e.ConnectedArea.Location, new Size(e.ConnectedArea.Width, e.ConnectedArea.Height > 0 ? (e.ConnectedArea.Height - 1) : 0));
                using (SolidBrush brush = new SolidBrush(CurrentTheme.ButtonHoverColor))
                    e.Graphics.FillRectangle(brush, rc);
            }

            private class ToolStripArrowRenderEventArgsSBB : ToolStripArrowRenderEventArgs
            {
                public Color Color;
                public ToolStripArrowRenderEventArgsSBB(Graphics g, ToolStripItem t, Rectangle r, Color c, ArrowDirection a) : base(g, t, r, c, a)
                {
                    this.Color = c;
                }
            }

            protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
            {
                if((e.Item as ToolStripSplitButton).DropDownButtonPressed)
                {
                    e.Graphics.FillRectangle(HighlightBrush, e.Item.ContentRectangle);
                    OnRenderArrow(new ToolStripArrowRenderEventArgsSBB(
                        e.Graphics, e.Item, (e.Item as ToolStripSplitButton).DropDownButtonBounds, CurrentTheme.TextColor,
                        ArrowDirection.Down));
                    return;
                }
                if (!e.Item.Selected || e.Item.Pressed)
                    base.OnRenderSplitButtonBackground(e);
                else
                {
                    var sb = e.Item as ToolStripSplitButton;
                    var button = sb.ButtonBounds;
                    var dropdown = sb.DropDownButtonBounds;

                    using(Pen pen = new Pen(CurrentTheme.GripDarkColor))
                    {
                        e.Graphics.FillRectangle(sb.ButtonPressed ? HighlightBrush : ButtonHoverBrush, button);
                        e.Graphics.FillRectangle(ButtonHoverBrush, dropdown);
                    }

                    OnRenderArrow(new ToolStripArrowRenderEventArgs(
                        e.Graphics, e.Item, sb.DropDownButtonBounds, e.Item.ForeColor,
                        ArrowDirection.Down));
                }
            }
        }

        private class CustomToolStripRenderer : CustomMenuStripRenderer
        {
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                base.OnRenderToolStripBorder(e);

                if(e.ToolStrip.TopLevelControl is ToolStripDropDownMenu)
                {
                    return;
                }

                ControlPaint.DrawBorder(e.Graphics, e.ToolStrip.ClientRectangle,
                              CurrentTheme.TitleBarColor, 2, ButtonBorderStyle.Solid,
                              CurrentTheme.TitleBarColor, 2, ButtonBorderStyle.Solid,
                              CurrentTheme.TitleBarColor, 2, ButtonBorderStyle.Solid,
                              CurrentTheme.TitleBarColor, 2, ButtonBorderStyle.Solid);
            }
        }
    }
}
