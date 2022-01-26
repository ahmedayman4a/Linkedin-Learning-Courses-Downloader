using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLCD.DownloaderGUI
{
    static class FormHelpers
    {
        private static PrivateFontCollection _fontCollection = new PrivateFontCollection();
        internal static FontFamily QuicksandFontFamilyRegular { get; set; }
        internal static FontFamily QuicksandFontFamilyMedium { get; set; }
        internal static FontFamily QuicksandFontFamilySemiBold { get; set; }

        static FormHelpers()
        {
            _fontCollection.AddFontFile("./fonts/Quicksand-Regular.ttf");
            _fontCollection.AddFontFile("./fonts/Quicksand-Medium.ttf");
            _fontCollection.AddFontFile("./fonts/Quicksand-SemiBold.ttf");
            _fontCollection.AddFontFile("./fonts/Quicksand-Bold.ttf");
            QuicksandFontFamilyRegular = _fontCollection.Families[0];
            QuicksandFontFamilyMedium = _fontCollection.Families[1];
            QuicksandFontFamilySemiBold = _fontCollection.Families[2];
        }
        internal static void SetFonts(Control control)
        {
            var fontRegular = new Font(QuicksandFontFamilyRegular, 14);
            if (control.HasChildren)
            {
                foreach (Control childControl in control.Controls)
                {
                    SetFonts(childControl);
                }
            }
            else
            {
                switch (control)
                {
                    case Button btn:
                        btn.Font = new Font(QuicksandFontFamilyMedium, 14);
                        break;
                    default:
                        control.Font = fontRegular;
                        break;
                }
                
                
            }
        }
        internal static void SetLoadingStatus(bool isLoading, Control control, Form form, Control[] exceptions = null,bool changeCursor = true)
        {
            if (changeCursor)
            {
                form.Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            }
            if ((!exceptions?.Contains(control) ?? true) && !(control is Label) && !control.HasChildren)
            {
                control.Enabled = !isLoading;
            }

            if (control.HasChildren)
            {
                foreach (Control child in control.Controls)
                {
                    SetLoadingStatus(isLoading, child, form,exceptions, changeCursor);
                }
            }
            return;
        }

    }
}
