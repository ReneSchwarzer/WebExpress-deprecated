using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.UI.Markdown
{
    public enum MarkdownMorphemeState
    {
        None,
        /// <summary>
        /// Fett
        /// </summary>
        BoldOpen,
        Bold,

        /// <summary>
        /// Fett und Kursiv
        /// </summary>
        BoldItalicOpen,
        BoldItalic,

        /// <summary>
        /// Kursiv
        /// </summary>
        ItalicOpen,
        Italic,

        /// <summary>
        /// Unterstrichen
        /// </summary>
        UnderlineOpen,
        Underline,

        /// <summary>
        /// '*'
        /// </summary>
        Asterisk1,
        /// <summary>
        /// '**'
        /// </summary>
        Asterisk2,
        /// <summary>
        /// '***'
        /// </summary>
        Asterisk3,
        /// <summary>
        /// '`'
        /// </summary>
        Code,
        /// <summary>
        ///  '***' | '---' | '___' | '**...*' | '* * ... *' | '~~...~' | ...
        /// </summary>
        HorizontalLine,
        /// <summary>
        /// '==' | '==...='
        /// </summary>
        Headheadline1Marker,
        /// <summary>
        /// '#'
        /// </summary>
        Headheadline1,
        /// <summary>
        /// '--' | '--...-'
        /// </summary>
        Headheadline2Marker,
        /// <summary>
        /// '##'
        /// </summary>
        Headheadline2,
        /// <summary>
        /// '###'
        /// </summary>
        Headheadline3,
        /// <summary>
        /// '####'
        /// </summary>
        Headheadline4,
        /// <summary>
        /// '#####'
        /// </summary>
        Headheadline5,
        /// <summary>
        /// '######'
        /// </summary>
        Headheadline6,
        /// <summary>
        /// '-'
        /// </summary>
        Hyphen1,
        /// <summary>
        /// '--'
        /// </summary>
        Hyphen2,
        /// <summary>
        /// '---'
        /// </summary>
        Hyphen3,
        /// <summary>
        /// '[Bild 1]: /Path/zum/Image.png' | '[Bild 1]: /Path/zum/Image.png "Optionaler Titel"'
        /// </summary>
        Image,
        /// <summary>
        /// '![Alt-Text][Bild 1] | ![Alt-Text](/Path/zum/Image.png "Optionaler Titel")'
        /// </summary>
        ImageReference,
        /// <summary>
        /// 'http://example.com ' | 'mailto:info@example.com '
        /// </summary>
        Link,
        /// <summary>
        /// '\'
        /// </summary>
        Mask,
        /// <summary>
        /// Leerzeile
        /// </summary>
        Newline,
        /// <summary>
        /// '|'
        /// </summary>
        Pipe,
        /// <summary>
        /// '+'
        /// </summary>
        Plus,
        /// <summary>
        /// '>'
        /// </summary>
        Quote,
        /// <summary>
        /// '    '
        /// </summary>
        Space4,
        /// <summary>
        /// Normaler Text
        /// </summary>
        Text,
        /// <summary>
        /// '~'
        /// </summary>
        Tilde1,
        /// <summary>
        /// '~~'
        /// </summary>
        Tilde2,
        /// <summary>
        /// '~~~'
        /// </summary>
        Tilde3,
        /// <summary>
        /// '_'
        /// </summary>
        Underline1,
        /// <summary>
        /// '__'
        /// </summary>
        Underline2,
        /// <summary>
        /// '___'
        /// </summary>
        Underline3
    }
}
