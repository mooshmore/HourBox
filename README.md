# HourBox
A custom C#/WinForms control created to make working with time values easier and more efficient.

![HourBox demo](https://i.imgur.com/vHr1ppE.gif)

## Accessing the value

You can access the value in one of the three ways:
- `.Value` - this first formats the value and then returns it in a form of a `string` in a `HH:mm` format. If there were no characters in the control an empty string will be returned. **Although this returns the value in a HH:mm format the whitespaces are removed, so values with the hour's value of less than 10 will be in a "`H:mm`" format. This can be disabled by removing the `.Replace()` method in the `.Value` getter.**
- `.TimeSpan` - returns the value in a form of a `TimeSpan?`. If there were no characters in the control, a null value will be returned.
- `.Text` - this returns the text in a raw, `string` form.

## Setting the value
**This control does not support setting to negative values.**

You can set the value in the same three ways:
- `.Value` - this first parses the value to a `TimeSpan`, then back to a `string` in `"HH:mm"` format, and finally formats it and assigns it to a `.Text` property. The accepted values are: **time strings** in either a `HH:mm` or `H:mm`, or an **empty string** (it can contain whitespaces or a colon).
- `.TimeSpan` - this will set the Value by parsing it to string using `.ToString(@"hh\:mm")`. If a null value is given, the value will be set to `""`.
- `.Text` - this will put the text directly into the control, but the validating methods will still be run. Inserting invalid text might throw an error.

Additionally, the control supports both copying and pasting.

## Arrows
Besides switching with left and right arrow keys you can **increase/decrease** the value by **up/down arrow keys**.

## Additional settings
- `HourLeadingZero` - if set to true, transforms the value given by getting the `.Value.` by adding zero to the hour component. Example (when set to true): `7:14` => `07:14`. **Set to `false` by default**.
- `ShowMaskOnlyOnFocus` - if set to true, it automatically "hides" the mask when the control is empty by changing the text colour to the same as the controls background colour. **Set to `false` by default**.
- `ToolTipOnFocus` - displays the text in a tooltip next to the control when it is focused and hides it when the focus is lost. The tooltip won't be displayed if the text isn't set. 

