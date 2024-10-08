﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Bootstrap.ControlInterfaces;
using Bootstrap.ControlModels;
using Bootstrap.Infrastructure;
using Bootstrap.Infrastructure.Enums;
using Bootstrap.Renderers;
using Bootstrap.TypeExtensions;

namespace Bootstrap.Controls
{
    public class BootstrapListBox : IBootstrapListBox
    {
        protected HtmlHelper html;
        protected BootstrapSelectElementModel _model = new BootstrapSelectElementModel();

        public BootstrapListBox(HtmlHelper html, string htmlFieldName, IEnumerable<SelectListItem> selectList, ModelMetadata metadata)
        {
            this.html = html;
            this._model.htmlFieldName = htmlFieldName;
            this._model.selectList = selectList;
            this._model.metadata = metadata;
        }

        public IBootstrapListBox Id(string id)
        {
            this._model.id = id;
            return this;
        }

        public IBootstrapListBox HtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            this._model.htmlAttributes = htmlAttributes;
            return this;
        }

        public IBootstrapListBox HtmlAttributes(object htmlAttributes)
        {
            this._model.htmlAttributes = htmlAttributes.ToDictionary();
            return this;
        }

        public IBootstrapListBox HelpText()
        {
            this._model.helpText = new BootstrapHelpText(BootstrapHelper.GetHelpTextFromMetadata(_model.metadata), HelpTextStyle.Inline);
            return this;
        }

        public IBootstrapListBox HelpText(string text)
        {
            this._model.helpText = new BootstrapHelpText(text, HelpTextStyle.Inline);
            return this;
        }

        public IBootstrapListBox HelpText(string text, HelpTextStyle style)
        {
            this._model.helpText = new BootstrapHelpText(text, style);
            return this;
        }

        public IBootstrapListBox Size(InputSize size)
        {
            this._model.size = size;
            return this;
        }

        [Obsolete("This overload is deprecated and will be removed in the future versions. Use .Tooltip(Tooltip tooltip) instead.")]
        public IBootstrapListBox Tooltip(TooltipConfiguration configuration)
        {
            this._model.tooltipConfiguration = configuration;
            return this;
        }

        public IBootstrapListBox Tooltip(Tooltip tooltip)
        {
            this._model.tooltip = tooltip;
            return this;
        }

        public IBootstrapListBox Tooltip(string title)
        {
            this._model.tooltip = new Tooltip(title);
            return this;
        }

        public IBootstrapListBox ShowValidationMessage(bool displayValidationMessage)
        {
            this._model.displayValidationMessage = displayValidationMessage;
            if (displayValidationMessage) this._model.validationMessageStyle = HelpTextStyle.Inline;
            return this;
        }

        public IBootstrapListBox ShowValidationMessage(bool displayValidationMessage, HelpTextStyle validationMessageStyle)
        {
            this._model.displayValidationMessage = displayValidationMessage;
            this._model.validationMessageStyle = validationMessageStyle;
            return this;
        }

        public virtual IBootstrapLabel Label()
        {
            IBootstrapLabel l = new BootstrapInputLabeled(html, _model, BootstrapInputType.ListBox);
            return l;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual string ToHtmlString()
        {
            return Renderer.RenderSelectElement(html, _model, BootstrapInputType.ListBox);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return ToHtmlString(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }
    }
}
