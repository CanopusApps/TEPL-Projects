﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Bootstrap.ControlInterfaces
{
    public interface IBootstrapRadioButton : IHtmlString
    {
        IBootstrapRadioButton Id(string id);
        IBootstrapRadioButton IsChecked(bool isChecked);
        IBootstrapRadioButton HtmlAttributes(IDictionary<string, object> htmlAttributes);
        IBootstrapRadioButton HtmlAttributes(object htmlAttributes);
        IBootstrapRadioButton Tooltip(TooltipConfiguration configuration);
        IBootstrapRadioButton Tooltip(Tooltip tooltip);
        IBootstrapRadioButton Tooltip(string text);
        IBootstrapRadioButton ShowValidationMessage(bool displayValidationMessage);
        IBootstrapRadioButton ShowValidationMessage(bool displayValidationMessage, HelpTextStyle validationMessageStyle);
        IBootstrapLabel Label();

        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        [EditorBrowsable(EditorBrowsableState.Never)]
        new string ToHtmlString();

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}