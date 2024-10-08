﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Bootstrap.ControlInterfaces
{
    public interface IBootstrapCheckBox : IHtmlString
    {
        IBootstrapCheckBox Id(string id);
        IBootstrapCheckBox IsChecked(bool isChecked);
        IBootstrapCheckBox HtmlAttributes(IDictionary<string, object> htmlAttributes);
        IBootstrapCheckBox HtmlAttributes(object htmlAttributes);
        IBootstrapCheckBox Tooltip(TooltipConfiguration configuration);
        IBootstrapCheckBox Tooltip(Tooltip tooltip);
        IBootstrapCheckBox Tooltip(string text);
        IBootstrapCheckBox ShowValidationMessage(bool displayValidationMessage);
        IBootstrapCheckBox ShowValidationMessage(bool displayValidationMessage, HelpTextStyle validationMessageStyle);
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
