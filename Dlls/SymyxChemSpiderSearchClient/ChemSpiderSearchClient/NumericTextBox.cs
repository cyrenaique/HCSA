using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChemSpiderClient
{

    public class NumericTextBox : TextBox
    {
        private bool _allowSpace;
        private bool _integerOnly;

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            var numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK?
                if (_integerOnly)
                {
                    e.Handled = true;
                    MessageBeep(MB_OK);
                }
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            //    {
            //     // Let the edit control handle control and alt key combinations
            //    }
            else if (this._allowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                MessageBeep(MB_OK);
            }
        }

        #region Win32
        [DllImport("user32.dll")]
        static extern void MessageBeep(uint uType);

        const uint MB_OK = 0x00000000;

        const uint MB_ICONHAND = 0x00000010;
        const uint MB_ICONQUESTION = 0x00000020;
        const uint MB_ICONEXCLAMATION = 0x00000030;
        const uint MB_ICONASTERISK = 0x00000040;

        #endregion

        public int IntValue
        {
            get
            {
                try
                {
                    return Int32.Parse(Text);
                }
                catch (Exception)
                {
                    return 0;
                }                
            }
        }

        public decimal DecimalValue
        {
            get
            {
                try
                {
                    return Decimal.Parse(Text);
                }
                catch (Exception)
                {
                    return 0;
                }             
            }
        }

        public bool AllowSpace
        {
            set
            {
                _allowSpace = value;
            }

            get
            {
                return _allowSpace;
            }
        }

        public bool IntegerOnly
        {
            set
            {
                _integerOnly = value;
            }

            get
            {
                return _integerOnly;
            }
        }
    }
}

