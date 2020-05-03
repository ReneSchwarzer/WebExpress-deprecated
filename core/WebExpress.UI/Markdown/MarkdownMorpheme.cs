﻿using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebExpress.Test")]
namespace WebExpress.UI.Markdown
{
    /// <summary>
    /// Sprcheinheiten des Markup-Sprache (siehe Dokumentation https://github.com/ReneSchwarzer/WebExpress/blob/master/doc/Markdown.vsd)
    /// </summary>
    internal enum MarkdownMorpheme
    {
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
        ///  '***' | '---' | '___' | '**...*' | '* * ... *' | ...
        /// </summary>
        HorizontaleLinie,
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
        /// Normaler Text
        /// </summary>
        Text,
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
