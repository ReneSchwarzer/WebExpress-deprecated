using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebExpress.Test")]
namespace WebExpress.UI.Markdown
{
    /// <summary>
    /// Zustände des Zustandsautomaten (siehe Dokumentation https://github.com/ReneSchwarzer/WebExpress/blob/master/doc/Markdown.vsd)
    /// </summary>
    internal enum MarkdownTokenState
    {
        /// <summary>
        /// Keine Zuordnung
        /// </summary>
        None, 
        /// <summary>
        /// '*'
        /// </summary>
        A1,
        /// <summary>
        /// '* '
        /// </summary>
        A1E,
        /// <summary>
        /// '* *'
        /// </summary>
        ALT,
        /// <summary>
        /// '* * '
        /// </summary>
        ALTE,
        /// <summary>
        /// '**'
        /// </summary>
        A2,
        /// <summary>
        /// '** '
        /// </summary>
        A2E,
        /// <summary>
        /// '***'
        /// </summary>
        A3,
        /// <summary>
        /// '*** '
        /// </summary>
        A3E,
        /// <summary>
        /// '**...*' | '* * ... *'
        /// </summary>
        AL,
        /// <summary>
        /// '**...* ' | '* * ... * '
        /// </summary>
        ALE,
        /// <summary>
        /// '`'
        /// </summary>
        B,
        /// <summary>
        /// '~'
        /// </summary>
        D1,
        /// <summary>
        /// '~ '
        /// </summary>
        D1E,
        /// <summary>
        /// '~ ~'
        /// </summary>
        DLT,
        /// <summary>
        /// '~ ~ '
        /// </summary>
        DLTE,
        /// <summary>
        /// '~~'
        /// </summary>
        D2,
        /// <summary>
        /// '~~ '
        /// </summary>
        D2E,
        /// <summary>
        /// '~~~'
        /// </summary>
        D3,
        /// <summary>
        /// '~~~ '
        /// </summary>
        D3E,
        /// <summary>
        /// '~~...~' | '~ ~ ... ~'
        /// </summary>
        DL,
        /// <summary>
        /// '~~...~ ' | '~ ~ ... ~ '
        /// </summary>
        DLE,
        /// <summary>
        /// '='
        /// </summary>
        E,
        /// <summary>
        /// '==' | '==...='
        /// </summary>
        EH,
        /// <summary>
        /// '>'
        /// </summary>
        G,
        /// <summary>
        /// '#'
        /// </summary>
        H1,
        /// <summary>
        /// '##'
        /// </summary>
        H2,
        /// <summary>
        /// '###'
        /// </summary>
        H3,
        /// <summary>
        /// '####'
        /// </summary>
        H4,
        /// <summary>
        /// '#####'
        /// </summary>
        H5,
        /// <summary>
        /// '######'
        /// </summary>
        H6,
        /// <summary>
        /// '['
        /// </summary>
        I1T,
        /// <summary>
        /// '[Bild 1'
        /// </summary>
        IN,
        /// <summary>
        /// '[Bild 1]'
        /// </summary>
        I2T,
        /// <summary>
        /// '[Bild 1]:'
        /// </summary>
        I3T,
        /// <summary>
        /// '[Bild 1]: /Path/zum/Image.png'
        /// </summary>
        IP,
        /// <summary>
        /// '[Bild 1]: /Path/zum/Image.png '
        /// </summary>
        IPE,
        /// <summary>
        /// '[Bild 1]: /Path/zum/Image.png "Optionaler Titel
        /// </summary>
        IO,
        /// <summary>
        /// '[Bild 1]: /Path/zum/Image.png "Optionaler Titel"'
        /// </summary>
        IOE,
        /// <summary>
        /// 'h'
        /// </summary>
        LH1,
        /// <summary>
        /// 'ht'
        /// </summary>
        LH2,
        /// <summary>
        /// 'htt'
        /// </summary>
        LH3,
        /// <summary>
        /// 'http'
        /// </summary>
        LH4,
        /// <summary>
        /// 'http:'
        /// </summary>
        LH5,
        /// <summary>
        /// 'http:/'
        /// </summary>
        LH6,
        /// <summary>
        /// 'http://'
        /// </summary>
        LH7,
        /// <summary>
        /// 'm'
        /// </summary>
        LM1,
        /// <summary>
        /// 'ma'
        /// </summary>
        LM2,
        /// <summary>
        /// 'mai'
        /// </summary>
        LM3,
        /// <summary>
        /// 'mail'
        /// </summary>
        LM4,
        /// <summary>
        /// 'mailt'
        /// </summary>
        LM5,
        /// <summary>
        /// 'mailto'
        /// </summary>
        LM6,
        /// <summary>
        /// 'mailto:'
        /// </summary>
        LM7,
        /// <summary>
        /// 'http://example.com' | 'mailto:info@example.com'
        /// </summary>
        LT,
        /// <summary>
        /// 'http://example.com ' | 'mailto:info@example.com '
        /// </summary>
        L,
        /// <summary>
        /// '+'
        /// </summary>
        P,
        /// <summary>
        /// '!'
        /// </summary>
        R1T,
        /// <summary>
        /// '![Alt-Text'
        /// </summary>
        RT,
        /// <summary>
        /// '![Alt-Text]'
        /// </summary>
        R2T,
        /// <summary>
        /// '![Alt-Text][Bild 1'
        /// </summary>
        RN,
        /// <summary>
        /// '![Alt-Text][Bild 1] | ![Alt-Text](/Path/zum/Image.png "Optionaler Titel")'
        /// </summary>
        R,
        /// <summary>
        /// '![Alt-Text]('
        /// </summary>
        R3T,
        /// <summary>
        /// '![Alt-Text]( /Path/zum/Image.png '
        /// </summary>
        RP,
        /// <summary>
        /// '![Alt-Text]( /Path/zum/Image.png "Optionaler Titel'
        /// </summary>
        RO,
        /// <summary>
        /// '![Alt-Text]( /Path/zum/Image.png "Optionaler Titel"'
        /// </summary>
        ROT,
        /// <summary>
        /// '\'
        /// </summary>
        S,
        /// <summary>
        /// '|'
        /// </summary>
        T,
        /// <summary>
        /// '_'
        /// </summary>
        U1,
        /// <summary>
        /// '_ '
        /// </summary>
        U1E,
        /// <summary>
        /// '_ _'
        /// </summary>
        ULT,
        /// <summary>
        /// '_ _ '
        /// </summary>
        ULTE,
        /// <summary>
        /// '__'
        /// </summary>
        U2,
        /// <summary>
        /// '__ '
        /// </summary>
        U2E,
        /// <summary>
        /// '___'
        /// </summary>
        U3,
        /// <summary>
        /// '___ '
        /// </summary>
        U3E,
        /// <summary>
        /// '__..._' | '_ _ ... _'
        /// </summary>
        UL,
        /// <summary>
        /// '__...-_ ' | '_ _ ... _ '
        /// </summary>
        ULE,
        /// <summary>
        /// ' '
        /// </summary>
        W1,
        /// <summary>
        /// '  '
        /// </summary>
        W2,
        /// <summary>
        /// '   '
        /// </summary>
        W3,
        /// <summary>
        /// '    '
        /// </summary>
        W4,
        /// <summary>
        /// '-'
        /// </summary>
        Y1,
        /// <summary>
        /// '- '
        /// </summary>
        Y1E,
        /// <summary>
        /// '- -'
        /// </summary>
        YLT,
        /// <summary>
        /// '- - '
        /// </summary>
        YLTE,
        /// <summary>
        /// '--'
        /// </summary>
        Y2,
        /// <summary>
        /// '-- '
        /// </summary>
        Y2E,
        /// <summary>
        /// '---'
        /// </summary>
        Y3,
        /// <summary>
        /// '--- '
        /// </summary>
        Y3E,
        /// <summary>
        /// '--...-' | '- - ... -'
        /// </summary>
        YL,
        /// <summary>
        /// '--...- ' | '- - ... - '
        /// </summary>
        YLE,
        /// <summary>
        /// Text
        /// </summary>
        X
    }
}
