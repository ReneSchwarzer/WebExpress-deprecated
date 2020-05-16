using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebExpress.Test")]
namespace WebExpress.UI.Markdown
{
    internal class MarkdownMorphemes
    {
        /// <summary>
        /// Stack mit den Morphemen
        /// </summary>
        private Stack<MarkdownMorpheme> Stack { get; set; } = new Stack<MarkdownMorpheme>();

        /// <summary>
        /// Liefert oder setzt die Fragmente der Zeile
        /// </summary>
        public List<MarkdownMorpheme> Morphemes => Stack.Reverse().ToList();

        /// <summary>
        /// Fügt ein neues Morphem hinzu
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="text">Der Text</param>
        public void Add(MarkdownFragmentState type, string text = null)
        {
            if (Stack.Count == 0)
            {
                switch (type)
                {
                    case MarkdownFragmentState.Headheadline1:
                        Push(MarkdownMorphemeState.Headheadline1);
                        break;
                    case MarkdownFragmentState.Headheadline2:
                        Push(MarkdownMorphemeState.Headheadline2);
                        break;
                    case MarkdownFragmentState.Headheadline3:
                        Push(MarkdownMorphemeState.Headheadline3);
                        break;
                    case MarkdownFragmentState.Headheadline4:
                        Push(MarkdownMorphemeState.Headheadline4);
                        break;
                    case MarkdownFragmentState.Headheadline5:
                        Push(MarkdownMorphemeState.Headheadline5);
                        break;
                    case MarkdownFragmentState.Headheadline6:
                        Push(MarkdownMorphemeState.Headheadline6);
                        break;
                    default:
                        Push(MarkdownMorphemeState.Text, text);
                        break;
                }
                return;
            }

            var peek = Stack.Peek();

            switch (peek.Type)
            {
                case MarkdownMorphemeState.Headheadline1:
                case MarkdownMorphemeState.Headheadline2:
                case MarkdownMorphemeState.Headheadline3:
                case MarkdownMorphemeState.Headheadline4:
                case MarkdownMorphemeState.Headheadline5:
                case MarkdownMorphemeState.Headheadline6:
                    switch (type)
                    {
                        case MarkdownFragmentState.Asterisk1:
                            Push(MarkdownMorphemeState.ItalicOpen);
                            break;
                        case MarkdownFragmentState.Asterisk2:
                            Push(MarkdownMorphemeState.BoldOpen);
                            break;
                        case MarkdownFragmentState.Asterisk3:
                            Push(MarkdownMorphemeState.BoldItalicOpen);
                            break;
                        case MarkdownFragmentState.Underline1:
                            Push(MarkdownMorphemeState.UnderlineOpen);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.Bold:
                    switch (type)
                    {
                        case MarkdownFragmentState.Asterisk2:
                            Push(MarkdownMorphemeState.BoldOpen);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.BoldOpen:
                    switch (type)
                    {
                        case MarkdownFragmentState.Asterisk2:
                            Change(MarkdownMorphemeState.Bold);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.BoldItalic:
                    switch (type)
                    {
                        case MarkdownFragmentState.Asterisk3:
                            Push(MarkdownMorphemeState.BoldItalicOpen);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.BoldItalicOpen:
                    switch (type)
                    {
                        case MarkdownFragmentState.Asterisk3:
                            Change(MarkdownMorphemeState.BoldItalic);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.Italic:
                    switch (type)
                    {
                        case MarkdownFragmentState.Asterisk1:
                            Push(MarkdownMorphemeState.ItalicOpen);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.ItalicOpen:
                    switch (type)
                    {
                        case MarkdownFragmentState.Asterisk1:
                            Change(MarkdownMorphemeState.Italic);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.Underline:
                    switch (type)
                    {
                        case MarkdownFragmentState.Underline1:
                            Push(MarkdownMorphemeState.UnderlineOpen);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                case MarkdownMorphemeState.UnderlineOpen:
                    switch (type)
                    {
                        case MarkdownFragmentState.Underline1:
                            Change(MarkdownMorphemeState.Underline);
                            break;
                        default:
                            Append(text);
                            break;
                    }
                    break;
                default:
                    Append(text);
                    break;
            }
        }

        /// <summary>
        /// Schließt die Verarbeitung ab
        /// </summary>
        public void Completed()
        {
            var peek = Stack.Peek();

            switch (peek.Type)
            {
                case MarkdownMorphemeState.BoldOpen:
                    Change(MarkdownMorphemeState.Text);
                    Insert("**");
                    break;
                case MarkdownMorphemeState.BoldItalicOpen:
                    Change(MarkdownMorphemeState.Text);
                    Insert("***");
                    break;
                case MarkdownMorphemeState.ItalicOpen:
                    Change(MarkdownMorphemeState.Text);
                    Insert("*");
                    break;
                case MarkdownMorphemeState.UnderlineOpen:
                    Change(MarkdownMorphemeState.Text);
                    Insert("_");
                    break;
            }
        }

        /// <summary>
        /// Fügt ein neues Morphem hinzu
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="text">Der Text</param>
        private void Push(MarkdownMorphemeState type, string text = null)
        {
            var morpheme = new MarkdownMorpheme() { Type = type };


            if (!string.IsNullOrWhiteSpace(text?.Trim()))
            {
                morpheme.Text.Append(text?.Trim());
            }

            Stack.Push(morpheme);
        }

        /// <summary>
        /// Erweitert das letzte Morphem
        /// </summary>
        /// <param name="text">Der Text</param>
        private void Append(string text = null)
        {
            var morpheme = new MarkdownMorpheme() { };

            if (Stack.Count == 0)
            {
                morpheme.Type = MarkdownMorphemeState.Text;
                if (!string.IsNullOrWhiteSpace(text?.Trim()))
                {
                    morpheme.Text.Append(text?.Trim());
                }

                Stack.Push(morpheme);

                return;
            }

            var peek = Stack.Peek();

            if (peek.Text.Length > 0)
            {
                peek.Text.Append(" ");

            }

            peek.Text.Append(text?.Trim());
        }

        /// <summary>
        /// Fügt am Beginn des letzte Morphems ein Wort ein
        /// </summary>
        /// <param name="text">Der Text</param>
        private void Insert(string text = null)
        {
            var peek = Stack.Peek();

            peek.Text.Insert(0, text?.Trim());
        }

        /// <summary>
        /// Ändert den Typ des aktuellen Morphems
        /// </summary>
        /// <param name="type">Der neue Type</param>
        private void Change(MarkdownMorphemeState type)
        {
            var peek = Stack.Peek();

            peek.Type = type;
        }

        ///// <summary>
        ///// Liefert oder setzt die Fragmente der Zeile
        ///// </summary>
        //public List<MarkdownMorpheme> Morphemes { get; private set; } = new List<MarkdownMorpheme>();

        ///// <summary>
        ///// Fügt ein neues Morphem hinzu
        ///// </summary>
        ///// <param name="type">Der Typ</param>
        ///// <param name="text">Der Text</param>
        //public void Add(MarkdownMorphemeState type, string text = null)
        //{
        //    var morpheme = new MarkdownMorpheme() { Type = type };

        //    if (!string.IsNullOrWhiteSpace(text?.Trim()))
        //    {
        //        morpheme.Text.Append(text);
        //    }

        //    Morphemes.Add(morpheme);
        //}

        ///// <summary>
        ///// Erweitert das letzte Morphem
        ///// </summary>
        ///// <param name="type">Der Typ</param>
        ///// <param name="text">Der Text</param>
        //public void Append(string text, MarkdownMorphemeState type = MarkdownMorphemeState.Text)
        //{
        //    var last = Morphemes.LastOrDefault();
        //    if (last == null)
        //    {
        //        Add(type, text);
        //    }
        //    else
        //    {
        //        if (last.Text.Length > 0)
        //        {
        //            last.Text.Append(" ");
        //        }
        //        last.Text.Append(text?.Trim());
        //    }
        //}
    }
}
