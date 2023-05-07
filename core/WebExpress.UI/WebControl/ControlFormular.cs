using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormular : Control, IControlFormular
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public virtual TypeLayoutFormular Layout { get; set; } = TypeLayoutFormular.Vertical;

        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Event nach Abschluss der Validation
        /// </summary>
        public event EventHandler<ValidationResultEventArgs> Validated;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular initialisiert wurde
        /// </summary>
        public event EventHandler<FormularEventArgs> InitializeFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn die Daten des Formulars ermittelt werden müssen
        /// </summary>
        public event EventHandler<FormularEventArgs> FillFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet werden soll
        /// </summary>
        public event EventHandler<FormularEventArgs> ProcessFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet und die nächsten Daten geladen werden sollen
        /// </summary>
        public event EventHandler<FormularEventArgs> ProcessAndNextFormular;

        /// <summary>
        /// Liefert oder setzt den Formularnamen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weiterleitungs-Uri
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Liefert oder setzt das Hiddenfeld, welches die Submit-Methode enthält
        /// </summary>
        public ControlFormularItemInputHidden FormularId { get; } = new ControlFormularItemInputHidden(Guid.NewGuid().ToString())
        {
            Name = "formular-id"
        };

        /// <summary>
        /// Liefert oder setzt das Hiddenfeld, welches die Submit-Methode enthält
        /// </summary>
        public ControlFormularItemInputHidden SubmitType { get; } = new ControlFormularItemInputHidden(Guid.NewGuid().ToString())
        {
            Name = "formular-submit-type",
            Value = "update"
        };

        /// <summary>
        /// Liefert oder setzt die Submit-Schaltfläche
        /// </summary>
        public ControlFormularItemButton SubmitButton { get; } = new ControlFormularItemButton()
        {
            Icon = new PropertyIcon(TypeIcon.Save),
            Color = new PropertyColorButton(TypeColorButton.Success),
            Type = TypeButton.Submit,
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None),
        };

        /// <summary>
        /// Liefert oder setzt die Formulareinträge
        /// </summary>
        public IList<ControlFormularItem> Items { get; } = new List<ControlFormularItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormular(string id = null)
            : base(id)
        {
            SubmitButton.Name = SubmitButton.Id;
            SubmitButton.OnClick = new PropertyOnClick($"$('#{SubmitType.Id}').val('submit');");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormular(string id, params ControlFormularItem[] items)
            : this(id)
        {
            if (items != null)
            {
                (Items as List<ControlFormularItem>).AddRange(items);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormular(params ControlFormularItem[] items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public virtual void Initialize(RenderContextFormular context)
        {
            // ID überprüfen
            if (string.IsNullOrWhiteSpace(Id))
            {
                context.Host.Log.Warning(I18N("webexpress.ui:form.empty.id"));
            }

            FormularId.Value = Id;

            if (string.IsNullOrWhiteSpace(SubmitButton.Text))
            {
                SubmitButton.Text = context.I18N("webexpress.ui", "form.submit.label");
            }
            else
            {
                SubmitButton.Text = context.I18N(SubmitButton.Text);
            }
        }

        /// <summary>
        /// Füllung der Formulardate 
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public virtual void Fill(RenderContextFormular context)
        {
            OnFill(context);
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        /// <param name="context">Der Kontext, indem die Eingaben validiert werden</param>
        /// <returns>True wenn alle Formulareinträhe gültig sind, false sonst</returns>
        public virtual bool Validate(RenderContextFormular context)
        {
            var valid = true;
            var validationResults = context.ValidationResults as List<ValidationResult>;

            foreach (var v in Items.Where(x => x is IFormularValidation).Select(x => x as IFormularValidation))
            {
                v.Validate(context);

                if (v.ValidationResult == TypesInputValidity.Error)
                {
                    valid = false;
                }

                validationResults.AddRange(v.ValidationResults);
            }

            var args = new ValidationEventArgs() { Value = null, Context = context };
            OnValidation(args);

            validationResults.AddRange(args.Results);

            if (args.Results.Where(x => x.Type == TypesInputValidity.Error).Any())
            {
                valid = false;
            }

            var validatedArgs = new ValidationResultEventArgs(valid);
            validatedArgs.Results.AddRange(validationResults);

            OnValidated(validatedArgs);

            return valid;
        }

        /// <summary>
        /// Weist an, die initialen Formaulardaten erneut zu laden
        /// </summary>
        public void Reset()
        {
            Fill(null);
        }

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public virtual void PreProcess(RenderContextFormular context)
        {

        }

        /// <summary>
        /// Processing of the resource. des Formulars
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public virtual void Process(RenderContextFormular context)
        {
            OnProcess(context);

            if (!string.IsNullOrWhiteSpace(RedirectUri?.ToString()))
            {
                context.Page.Redirecting(RedirectUri);
            }
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var renderContext = new RenderContextFormular(context, this);
            var fill = false;
            var process = false;

            // Prüfe ob und wie das Formular abgeschickt wurde 
            if (context.Request.GetParameter("formular-id")?.Value == Id && context.Request.HasParameter("formular-submit-type"))
            {
                var value = context.Request.GetParameter("formular-submit-type")?.Value;
                switch (value)
                {
                    case "submit":
                        process = true;
                        fill = false;
                        break;
                    case "reset":
                        process = false;
                        fill = true;
                        break;
                    case "update":
                    default:
                        break;
                }
            }
            else
            {
                // erster Aufruf
                fill = true;
                process = false;
            }

            // Initialization
            Initialize(renderContext);
            (Items as List<ControlFormularItem>).ForEach(x => x?.Initialize(renderContext));
            OnInitialize(renderContext);
            SubmitButton.Initialize(renderContext);

            // Formular mit Daten füllen
            if (fill)
            {
                Fill(renderContext);
            }

            // Vorverarbeitung
            PreProcess(renderContext);

            // Formular verarbeiten (z.B. Formulardaten sichern)
            if (process && Validate(renderContext))
            {
                Process(renderContext);
            }

            // HTML erzeugen
            var button = SubmitButton.Render(renderContext);

            var form = new HtmlElementFormForm()
            {
                ID = Id,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Action = Uri?.ToString() ?? renderContext.Uri?.ToString(),
                Method = RequestMethod.POST.ToString(),
                Enctype = TypeEnctype.None
            };

            form.Elements.Add(FormularId.Render(renderContext));
            form.Elements.Add(SubmitType.Render(renderContext));
            var header = new HtmlElementSectionHeader();

            foreach (var v in renderContext.ValidationResults)
            {
                var bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Default);

                switch (v.Type)
                {
                    case TypesInputValidity.Error:
                        bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Danger);
                        break;
                    case TypesInputValidity.Warning:
                        bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Warning);
                        break;
                }

                header.Elements.Add(new ControlAlert()
                {
                    BackgroundColor = bgColor,
                    Text = I18N(context.Culture, v.Text),
                    Dismissible = TypeDismissibleAlert.Dismissible,
                    Fade = TypeFade.FadeShow
                }.Render(renderContext));
            }

            foreach (var item in Items.Where(x => x is ControlFormularItemInputHidden))
            {
                form.Elements.Add(item.Render(renderContext));
            }

            var main = new HtmlElementSectionMain();

            var group = null as ControlFormularItemGroup;

            group = Layout switch
            {
                TypeLayoutFormular.Horizontal => new ControlFormularItemGroupHorizontal(),
                TypeLayoutFormular.Mix => new ControlFormularItemGroupMix(),
                _ => new ControlFormularItemGroupVertical(),
            };

            foreach (var item in Items.Where(x => x is not ControlFormularItemInputHidden))
            {
                group.Items.Add(item);
            }

            main.Elements.Add(group.Render(renderContext));

            var footer = new HtmlElementSectionFooter();

            footer.Elements.Add(button);
            form.Elements.Add(header);
            form.Elements.Add(main);
            form.Elements.Add(footer);

            form.Elements.AddRange(renderContext.Scripts.Select(x => new HtmlElementScriptingScript(x.Value)));

            return form;
        }

        /// <summary>
        /// Fügt eine Formularsteuerelement hinzu
        /// </summary>
        /// <param name="item">Das Formularelement</param>
        public void Add(params ControlFormularItem[] item)
        {
            (Items as List<ControlFormularItem>).AddRange(item);
        }

        /// <summary>
        /// Löst das Verarbeiten-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnProcess(RenderContextFormular context)
        {
            ProcessFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Verarbeiten-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnProcessAndNextFormular(RenderContextFormular context)
        {
            ProcessAndNextFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Initializations-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnInitialize(RenderContextFormular context)
        {
            InitializeFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Datenbereitstellungs-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnFill(RenderContextFormular context)
        {
            FillFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Validation-Event aus
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnValidation(ValidationEventArgs e)
        {
            Validation?.Invoke(this, e);
        }

        /// <summary>
        /// Löst das Validated-Event aus
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnValidated(ValidationResultEventArgs e)
        {
            Validated?.Invoke(this, e);
        }
    }
}
