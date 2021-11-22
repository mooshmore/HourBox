using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HourBoxControl
{
    // HourBox control - created by https://github.com/mooshmore
    // If you will spot any 🐛 in the control - report it to me, and I'll fix them asap.
    // If you have any questions or suggestions about the program feel free to message me about them.

    // For usage tips and instructions read the readme file 📜.
    // Also, thorough the program there are nearly everywhere textFront and textBack variables - 
    // those just simply translate to an hour component and minute component respectively, but for 
    // because they were a bit too long I've stuck with the textFront and textBack naming.

    /// <summary>
    /// Represents a custom HourBox control, which is designed to make working with time values easier and faster.
    /// </summary>
    public class HourBox : MaskedTextBox
    {
        protected override void OnCreateControl()
        {
            this.PromptChar = ' ';
            this.Mask = "00:00";

            if (DesignMode) return;

            base.OnCreateControl();

            // Prevents from adding any text or events when in a designer mode as the events can change the
            // properties IN a designer which is not anticipated.
            if (DesignMode) return;

            defaultBackColor = Color.FromArgb(this.BackColor.A, this.BackColor.R, this.BackColor.G, this.BackColor.B);
            defaultFontColor = Color.FromArgb(this.ForeColor.A, this.ForeColor.R, this.ForeColor.G, this.ForeColor.B);

            #region Events

            #region Tooltip on focus

            this.GotFocus += ToolTipOnFocus_display;
            this.LostFocus += ToolTipOnFocus_hide;

            #endregion

            #region Show mask only on focus

            this.GotFocus += ShowMask;
            this.LostFocus += HideMask;

            // Also assigning this to a text changed event so that when the text inside is 
            // changed by a program hiding/showing the mask also takes place
            this.TextChanged += HideMask;
            this.TextChanged += ShowMask;

            #endregion

            #region Masking

            this.GotFocus += (s, evnt) => SetCaretPosition(true);
            this.LostFocus += MaskingValidation_out;
            this.Click += (s, evnt) => SetCaretPosition(false);
            this.KeyDown += Masking_keyDown;
            this.KeyUp += Masking_keyUp;
            this.TextChanged += Validate;

            #endregion

            #endregion

            // Hide the mask if it has been set to do so
            HideMask();

            // Apply the mask only on the handle created to prevent it from setting up in the designer.
        }

        private Color defaultBackColor;
        private Color defaultFontColor;

        #region Helper methods

        /// <summary>
        /// Checks if the given text is empty / only has whitespaces.
        /// </summary>
        private bool IsEmpty_space(string text) => text.Replace(" ", "") == "";
        /// <summary>
        /// Returns 0 if the text is empty / only has whitespaces; otherwise returns the parsed value.
        /// </summary>
        private int ParseInt_Zero(string text) => IsEmpty_space(text) ? 0 : int.Parse(text);
        /// <summary>
        /// Returns a Substring(0, 2) of the given text.
        /// </summary>
        private string TextFront(string text) => text.Substring(0, 2);
        /// <summary>
        /// Returns a Substring(3, 2) of the given text.
        /// </summary>
        private string TextBack(string text) => text.Substring(3, 2);

        /// <summary>
        /// Formats a time so that it is always in a "HH:mm" format (no matter what) by adding spaces.
        /// </summary>
        private string FormatTime(string text)
        {
            // If the text doesn't include any characters (besides a colon) return a default HH:mm format with whitespaces.
            if (IsEmpty_space(text.Replace(":", ""))) return "  :  ";

            // Both the minute and hour components are acquired by referring to the position of the colon - 
            // acquiring it just by the substring might return an error because the value might not always have the
            // same amounts of characters, for example: "3:52" or ":52", so it is just safer to get it by referring to the colons position.
            string textFront = text.Substring(0, text.IndexOf(":") + 1);
            string textBack = text.Substring(text.IndexOf(":"));

            // Hour component
            if (textFront == ":") textFront = "  :";
            else if (new Regex("^.:$").IsMatch(textFront)) textFront = " " + textFront;

            // Minute component
            if (textBack == ":") textBack = ":  ";
            else if (new Regex("^:.$").IsMatch(textBack)) 
                textBack += " ";

            // Automatically add hour component to "0{value}" if the HourLeadingZero is used
            if (textFront[0] == ' ' && textFront[1] != ' ' && HourLeadingZero)
                textFront = "0" + textFront.Substring(1);

            return textFront + textBack.Substring(1);
        }

        /// <summary>
        /// Formats the text to match the format of a minute component - mm.
        /// This adds a leading zero (or two zeros).
        /// If the given text is empty then the value is set to 00.
        /// </summary>
        /// <param name="returnEmpty">If this is set to true and the text is empty a "  " is returned.</param>
        private string FixMinutes(string text, bool returnEmpty = false)
        {
            if (IsEmpty_space(text) && returnEmpty) return "  ";
            int minutes = ParseInt_Zero(text);
            return minutes < 10 ? $"0{minutes}" : $"{minutes}";
        }

        /// <summary>
        /// Formats the text to match the format of an hour component - hh.
        /// This adds a leading zero (or two zeros if HourLeadingZero is set.).
        /// If the given text is empty then a value of 0 is set.
        /// </summary>
        /// <param name="returnEmpty">If this is set to true and the text is empty a "  " is returned.</param>
        private string FixHours(string text, bool returnEmpty = false)
        {
            if (IsEmpty_space(text) && returnEmpty) return "  ";
            int hours = ParseInt_Zero(text);
            return hours < 10
                ? HourLeadingZero ? $"0{hours}" : $" {hours}"
                : $"{hours}";
        }

        #endregion

        #region Text changed validation

        /// <summary>
        /// Checks if the text inside the control matches the HH:mm format - that is both syntax and values.
        /// If not, it automatically replaces the incorrect values with blank text.
        /// </summary>
        private void Validate(object sender, EventArgs e)
        {
            string text = this.Text;

            text = FormatTime(text);

            string textFront = TextFront(text);
            string textBack = TextBack(text);

            // Change the hour component to blank text if:
            // - there is only one space (correct it to two spaces)
            // - the value is bigger than 23
            // Do the same for the minute component (with max value of 59)
            if (textFront.Replace(" ", "") != "" && int.Parse(textFront) > 23)
                textFront = "  ";
            if (textBack.Replace(" ", "") != "" && int.Parse(textBack) > 59)
                textBack = "  ";

            this.Text = textFront + ":" + textBack;
        }

        #endregion

        #region Tooltip on focus

        [
        Description("The text that will be displayed when user focuses the control.")
        ]
        public string ToolTipOnFocus { get; set; }
        
        private readonly ToolTip FocusToolTip = new ToolTip();

        /// <summary>
        /// When the ToolTipOnFocus has any text inside of itself a tooltip with that text is displayed when the user 
        /// focuses the control. The tooltip hides when the control loses focus.
        /// 
        /// This is assigned to a Focus event.
        /// </summary>
        private void ToolTipOnFocus_display(object sender, EventArgs e) => FocusToolTip.Show(ToolTipOnFocus, this, this.Width + 5, 2, 999999);

        /// <summary>
        /// Hides the tooltip when the control loses focus.
        /// 
        /// This is assigned to a LostFocus event.
        /// </summary>
        private void ToolTipOnFocus_hide(object sender, EventArgs e) => FocusToolTip.Hide(this);

        #endregion

        #region Masking 

        [
        Description("Whether to automatically add a leading hour zero when mask is choosen that uses time. When set to true it automatically changes the time from ex. 7:06 => 07:06."),
        DefaultValue(typeof(bool), "False")
        ]
        public bool HourLeadingZero { get; set; }

        /// <summary>
        /// When the user focuses the control a selection is automatically made.
        /// When the control has been focused by pressing a tab the caret is always set on an hour component.
        /// </summary>
        private void SetCaretPosition(bool triggeredByATab)
        {
            int caretPosition = this.SelectionStart;

            if (triggeredByATab || caretPosition < 3)
                this.Select(0, 2);
            else if (caretPosition >= 3)
                this.Select(3, 2);
        }

        /// <summary>
        /// When the control loses focus the text is automatically formatted - there is
        /// a leading zero added to a minute and hour components if needed.
        /// </summary>
        private void MaskingValidation_out(object sender, EventArgs e)
        {
            string text = this.Text;

            if (text.Replace(" ", "") == ":") return;
            text = FormatTime(text);

            // Fix hours and fix minutes fixes the components if needed.
            this.Text = FixHours(TextFront(text)) + ":" + FixMinutes(TextBack(text));
        }

        #endregion

        #region Keys event handling

        private string textBeforeChange = "";
        private int selectionLengthBeforeChange = 0;
        private int caretPositionBeforeChange = 0;

        /// <summary>
        /// Handles the functionality for the delete and backspace keys.
        /// 
        /// Because unless like all of the other keys there is no way to suppress the default behaviour
        /// of pressing the backspace and delete key so I had to make a workaround. 
        ///
        /// Pretty much what is happening is that in the Masking_keyDown method which is fired BEFORE the 
        /// backspace and delete key behaviour the data is saved before the event.
        /// Later on, in the Masking_keyUp method, the data is "restored" to the values and the deleting event 
        /// is handled manually.
        /// </summary>
        private void Masking_keyUp(object sender, KeyEventArgs e)
        {
            // Execute the normal behaviour when the tab key is pressed
            // - and don't do anything if the control isn't active.
            if (e.KeyCode == Keys.Tab || this.ReadOnly || !this.Enabled)
                return;

            int caretPosition = caretPositionBeforeChange;
            int selectionLength = selectionLengthBeforeChange;

            textBeforeChange = FormatTime(textBeforeChange);
            string textFront = TextFront(textBeforeChange);
            string textBack = TextBack(textBeforeChange);

            switch (e.KeyCode)
            {
                case Keys.Back:
                    // Everything was selected
                    if (selectionLength == 5)
                    {
                        this.Text = $"  :  ";
                        this.Select(0, 2);
                    }
                    // Position set at the hour component
                    else if (caretPosition < 3)
                    {
                        this.Text = $"  :{textBack}";
                        this.Select(0, 2);
                    }
                    // Position set at the minute component
                    else if (caretPosition >= 3)
                    {
                        if (IsEmpty_space(textBack))
                        {
                            this.Text = $"  :  ";
                            this.Select(0, 2);
                        }
                        else
                        {
                            this.Text = $"{textFront}:  ";
                            this.Select(3, 2);
                        }
                    }

                    break;
                case Keys.Delete:
                    // Everything was selected
                    if (selectionLength == 5)
                    {
                        this.Text = $"  :  ";
                        this.Select(3, 2);
                    }
                    // Position set at the hour component
                    else if (caretPosition < 3)
                    {
                        if (IsEmpty_space(textFront))
                        {
                            this.Text = $"  :  ";
                            this.Select(3, 2);
                        }
                        else
                        {
                            this.Text = $"  :{textBack}";
                            this.Select(0, 2);
                        }
                    }
                    // Position set at the minute component
                    else if (caretPosition >= 3)
                    {
                        this.Text = $"{textFront}:  ";
                        this.Select(3, 2);
                    }

                    break;
            }
        }

        /// <summary>
        /// A method that handles all of the keyboard events and functionalities - 
        /// besides the delete and backspace - those are handled by the Masking_keyUp method.
        /// </summary>
        private void Masking_keyDown(object sender, KeyEventArgs e)
        {
            // Execute the normal behaviuor when the tab key is pressed
            // - and don't do anything if the control isn't active.
            if (e.KeyCode == Keys.Tab || this.ReadOnly || !this.Enabled)
                return;

            // Suppress all default behaviour of the keys - everything is handled by the program
            // (this does not suppress the delete & backspace keys)
            e.Handled = true;
            e.SuppressKeyPress = true;

            // Save the positions and text before any changes
            // This is needed for handling the delete and backspace keys
            textBeforeChange = this.Text;
            selectionLengthBeforeChange = this.SelectionLength;
            caretPositionBeforeChange = this.SelectionStart;

            int caretPosition = this.SelectionStart;

            string text = FormatTime(this.Text);
            string textFront = TextFront(text);
            string textBack = TextBack(text);

            switch (e.KeyCode)
            {
                // Left key - move the selection to the hour component and fix the minutes component (if necessary)
                case Keys.Left:
                    if (caretPosition >= 3)
                        this.Text = textFront + ":" + FixMinutes(textBack, true);
                    this.Select(0, 2);
                    return;
                // Right key - move the selection to the minute component and fix the hours component (if necessary)
                case Keys.Right:
                    if (caretPosition < 3)
                        this.Text = FixHours(textFront, true) + ":" + textBack;
                    this.Select(3, 2);
                    return;
                // Up key - increase the value of the selected component
                case Keys.Up:
                    // Hour component
                    if (caretPosition < 3)
                    {
                        // If the value was 23 or blank set it to 0
                        if (textFront == "  " || textFront == "23")
                            textFront = " 0";
                        else
                        {
                            int valueFront = int.Parse(textFront);
                            if (valueFront < 23)
                                textFront = (valueFront + 1).ToString();
                            if (textFront.Length == 1)
                                textFront = $" {textFront}";
                        }

                        if (HourLeadingZero && textFront[0] == ' ')
                            textFront = $"0{textFront.Substring(1)}";

                        this.Text = textFront + ":" + textBack;
                        this.Select(0, 2);
                    }
                    // Minute component
                    else if (caretPosition >= 3)
                    {
                        if (textBack == "  " || textBack == "59")
                            textBack = "00";
                        else
                        {
                            int valueBack = int.Parse(textBack);
                            if (valueBack > 8 && valueBack < 59)
                                textBack = (valueBack + 1).ToString();
                            else if (valueBack < 59)
                                textBack = $"0{valueBack + 1}";
                        }

                        this.Text = textFront + ":" + textBack;
                        this.Select(3, 2);
                    }
                    return;
                // Key down - decrease the value of the selected component
                case Keys.Down:
                    // Hour component
                    if (caretPosition < 3)
                    {
                        if (textFront == "  " || textFront == " 0" || textFront == "00")
                            textFront = "23";
                        else
                        {
                            int valueFront = int.Parse(textFront);
                            if (valueFront > 0)
                                textFront = (valueFront - 1).ToString();
                            if (textFront.Length == 1)
                                textFront = $" {textFront}";
                        }
                        if (HourLeadingZero && textFront[0] == ' ')
                            textFront = $"0{textFront.Substring(1)}";

                        this.Text = textFront + ":" + textBack;
                        this.Select(0, 2);
                    }
                    // Minute component
                    else if (caretPosition >= 3)
                    {
                        if (textBack == "  " || textBack == " 0" || textBack == "00")
                            textBack = "59";
                        else
                        {
                            int valueBack = int.Parse(textBack);
                            if (valueBack > 10 && valueBack <= 59)
                                textBack = (valueBack - 1).ToString();
                            else
                                textBack = $"0{valueBack - 1}";
                        }

                        this.Text = textFront + ":" + textBack;
                        this.Select(3, 2);
                    }
                    return;

                case Keys.D0:
                case Keys.NumPad0:
                case Keys.D1:
                case Keys.NumPad1:
                case Keys.D2:
                case Keys.NumPad2:
                case Keys.D3:
                case Keys.NumPad3:
                case Keys.D4:
                case Keys.NumPad4:
                case Keys.D5:
                case Keys.NumPad5:
                case Keys.D6:
                case Keys.NumPad6:
                case Keys.D7:
                case Keys.NumPad7:
                case Keys.D8:
                case Keys.NumPad8:
                case Keys.D9:
                case Keys.NumPad9:
                    // Passes the number of the key as an int
                    CaptureKeyPress(int.Parse(e.KeyCode.ToString().Substring(e.KeyCode.ToString().Length - 1)), textFront, textBack);
                return;
                // Copy the selected text to the clipboard
                case Keys.C:
                    if (ModifierKeys.HasFlag(Keys.Control))
                        Clipboard.SetText(this.SelectedText);
                        return;
                // Paste the clipboard text to the control if it is correct
                case Keys.V:
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        // Remove whitespace and also replace '.' with ':' so that also the values like 11.52 are accepted.
                        string copyText = Clipboard.GetText().Replace(" ", "").Replace(".", ":");
                        // If the text contains a colon then it has to have a correct syntax - if it doesn't have it, do nothing.
                        if (copyText.Contains(":"))
                        {
                            if (System.TimeSpan.TryParse(copyText, out TimeSpan parsedTime))
                                this.TimeSpan = (TimeSpan?)parsedTime;
                        }
                        // Otherwise the text has to be shorter/equal to 2 characters and can be parsable.
                        // If it is, paste it in the selected component - but before checking if it is a correct value for the component.
                        else if (copyText.Length <= 2)
                        {
                            if (uint.TryParse(copyText, out uint parsedValue))
                            {
                                // Hour component
                                if (caretPosition < 3)
                                {
                                    if (parsedValue < 10 && HourLeadingZero)
                                    {
                                        if (HourLeadingZero)
                                            this.Text = $"0{parsedValue}:{textBack}";
                                        else
                                            this.Text = $" {parsedValue}:{textBack}";
                                    }
                                    else if (parsedValue <= 23)
                                        this.Text = $"{parsedValue}:{textBack}";
                                }
                                // Minute component
                                else if (caretPosition >= 3)
                                {
                                    if (parsedValue < 10)
                                        this.Text = $"0{textFront}:{parsedValue}";
                                    else if (parsedValue <= 59)
                                        this.Text = $"{textFront}:{parsedValue}";
                                }
                            }
                        }
                    }
                    return;
            }
        }

        /// <summary>
        /// A method that is responsible for adding the given characters and changing the selection if necessary.
        /// </summary>
        /// <param name="insertValue">The value to be inserted.</param>
        /// <param name="textFront">The hour component.</param>
        /// <param name="textBack">The minute component.</param>
        private void CaptureKeyPress(int insertValue, string textFront, string textBack)
        {
            int caretPosition = this.SelectionStart;

            string secondFrontLetter = textFront.Substring(1);
            string secondBackLetter = textBack.Substring(1);

            int selectionStart = 0;

            // Hour component
            if (caretPosition < 3)
            {
                // What this does is that it automatically moves the caret to the minute component
                // if nothing else can be added to the hour component - for example when you press
                // 9, 5 or 4 the caret can be moved because you can't for example enter 91 or 52.

                // Remove the previous character and put in the second one in the second position if:
                if (ParseInt_Zero(textFront) >= 3 || // The components value is bigger/equal to 3 
                    IsEmpty_space(textFront) || // The component is empty
                    (secondFrontLetter == "2" && insertValue > 3)) // The components value is bigger than 2 and the inserted value was bigger than 3 (the hour component can't be bigger than 23)
                {
                    textFront = $" {insertValue}";
                    // If the value is bigger than 2 move the selection to the minute component - nothing else
                    // can be added because the value of an hour component can't be for example 34 or 50
                    if (insertValue > 2)
                    {
                        if (HourLeadingZero)
                            textFront = $"0{insertValue}";
                        selectionStart = 3;
                    }
                    else
                        selectionStart = 0;
                }
                // Move the second character to the first place and put the inserted character in the second place, and jump to the second time part when:
                else if (new Regex("^ [0-2]$").IsMatch(textFront) || // Second character has a value between 0-2
                        new Regex("^0[0-2]$").IsMatch(textFront) && HourLeadingZero // The first character is zero and the econd character has a value between 0-2 and a HourLeadingZero is true
                        )
                {
                    textFront = $"{secondFrontLetter}{insertValue}";
                    selectionStart = 3;
                }
            }
            // Minute component
            else if (caretPosition >= 3)
            {
                selectionStart = 3;

                // Move the second character to the first place and put the inserted character in the second place when:
                if (new Regex("^ [0-5]$").IsMatch(textBack)) // Second character has a value between 0-5
                    textBack = $"{secondBackLetter}{insertValue}";
                // Otherwise (delete) the previous character and put in the inserted value
                else
                    textBack = $" {insertValue}";

            }

            this.Text = textFront + ":" + textBack;
            this.Select(selectionStart, 2);
        }

        #endregion

        #region Getters & setters

        /// <summary>
        /// Gets or sets the value of the associated control.
        /// If the control doesn't have any values in it this will return an empty string.
        /// </summary>
        [Browsable(false)]
        public string Value
        {
            get => GetFormattedTextValue().Replace(" ", "");
            set => this.Text = FormatTime(value);
        }

        /// <summary>
        /// Checks the corectness and formats the text that is inside the control.
        /// </summary>
        /// <returns>The formatted value if everything was correct; Or a blank value if the field didn't had any values or the parsing failed.</returns>
        private string GetFormattedTextValue()
        {
            string text = this.Text;
            // Return blank value if the field doesn't have any value in it
            if (text.Replace(" ", "").Replace(":", "") == "") return "";

            text = FormatTime(text);

            string textFront = FixHours(TextFront(text));
            string textBack = FixMinutes(TextBack(text));

            // A safety validation for cases when a .TextChanged event is used

            // If any of the components fails to parse or is not in the correct
            // boundaries of the component return a blank value.
            if (int.TryParse(textFront, out int textFrontValue) && textFrontValue <= 23
                && int.TryParse(textBack, out int textBackValue) && textBackValue <= 59)
                return textFront + ":" + textBack;
            else
                return "";
        }

        /// <summary>
        /// Gets or sets the TimeSpan? value of the associated control.
        /// If the control doesn't have any values in it this will return a null value.
        /// </summary>
        [Browsable(false)]
        public TimeSpan? TimeSpan
        {
            get
            {
                if (this.Value == "") return null;
                    else return System.TimeSpan.Parse(this.Value);
            }
            set
            {
                if (value == null)
                    this.Value = "";
                else
                    this.Value = ((TimeSpan)value).ToString(@"hh\:mm");
            }
        }

        #endregion

        #region Show mask only on focus

        [Description("The mask is only shown when the user focuses the control or the control has text inside of it. This does not affect read only fields - they can't be changed - see HideMask method for more.")]
        public bool ShowMaskOnlyOnFocus { get; set; }

        /// <summary>
        /// Sets the controls fore color back to the default one.
        /// </summary>
        private void ShowMask(object sender, EventArgs e)
        {
            if (ShowMaskOnlyOnFocus)
                this.ForeColor = Color.FromArgb(defaultFontColor.A, defaultFontColor.R, defaultFontColor.G, defaultFontColor.B);
        }

        /// <summary>
        /// Hides the mask if the field doesn't have any characters in it.
        /// This does not apply when the controls read-only state is set to true.
        /// </summary>
        private void HideMask(object sender = null, EventArgs e = null)
        {
            if (ShowMaskOnlyOnFocus && this.Value == "")
            {
                // Before uncommenting this back up the files where the control is used - other.
                // this.BackColor = this.BackColor;

                // Technically simply doing this solves the problem and hides / shows the mask even when the field is read-only,
                // but this somehow messes up the read-only back colour property itself - when changing the read-only property
                // (both through the code and the designer) the back colour of the field doesn't change. Technically this would be
                // possible to work by requiring to specify the back colour for default and read-only states, but it would take some
                // time and it is not guaranteed to work. 
                // For more info:
                // https://www.codeproject.com/Questions/359467/Change-forecolor-of-text-when-textbox-is-readonly
                // https://stackoverflow.com/questions/20688408/how-do-you-change-the-text-color-of-a-readonly-textbox/20688985

                // Changes the fore colour to the background colour to "hide" it.
                // This does not work when the control ReadOnly property is set to true.
                this.ForeColor = Color.FromArgb(defaultBackColor.A, defaultBackColor.R, defaultBackColor.G, defaultBackColor.B);
            }
        }

        #endregion
    }
}
