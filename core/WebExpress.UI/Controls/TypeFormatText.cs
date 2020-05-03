namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Textformate
    /// </summary>
    public enum TypeFormatText
    {
        Default,
        /// <summary>
        /// Markiert den Inhalt als Absatz.
        /// </summary>
        Paragraph,
        /// <summary>
        /// Steht für einen Textabschnitt, der vom übrigen Inhalt abgesetzt und üblicherweise kursiv dargestellt wird, ohne für eine spezielle Betonung oder Wichtigkeit zu stehen. Dies kann beispielsweise eine taxonomische Bezeichnung, ein technischer Begriff, ein idiomatischer Ausdruck, ein Gedanke oder der Name eines Schiffes sein.
        /// </summary>
        Italic,
        /// <summary>
        /// Steht für einen Textabschnitt, der vom übrigen Inhalt abgesetzt und üblicherweise fettgedruckt dargestellt wird, ohne für eine spezielle Betonung oder Wichtigkeit zu stehen. Dies kann beispielsweise ein Schlüsselwort oder ein Produktname in einer Produktbewertung sein.
        /// </summary>
        Bold,
        /// <summary>
        /// Steht für einen Textabschnitt, der vom übrigen Inhalt abgesetzt und üblicherweise unterstrichen dargestellt wird, ohne für eine spezielle Betonung oder Wichtigkeit zu stehen. Dies könnte beispielsweise ein Eigenname auf in chinesischer Sprache sein oder ein Textabschnitt, der häufig falsch buchstabiert wird.
        /// </summary>
        Underline,
        /// <summary>
        /// Wird für Inhalte verwendet, dienicht länger relevant oder akkurat sind. Wird meist durchgestrichen dargestellt.
        /// </summary>
        StruckOut,
        /// <summary>
        /// Markiert den Titel eines Werks.
        /// </summary>
        Cite,
        /// <summary>
        /// Markiert eine Überschrift der obersten Ebene
        /// </summary>
        H1,
        /// <summary>
        /// Markiert eine Überschrift der zweiten Ebene
        /// </summary>
        H2,
        /// <summary>
        /// Markiert eine Überschrift der dritten Ebene
        /// </summary>
        H3,
        /// <summary>
        /// Markiert eine Überschrift der vierten Ebene
        /// </summary>
        H4,
        /// <summary>
        /// Markiert eine Überschrift der fünften Ebene
        /// </summary>
        H5,
        /// <summary>
        /// Markiert eine Überschrift der untersten Ebene
        /// </summary>
        H6,
        /// <summary>
        /// Markiert einen allgemeinen Textabschnitt.
        /// </summary>
        Span,
        /// <summary>
        /// Steht für das "Kleingedruckte" eines Dokuments, wie Ausschlussklauseln, Copyright-Hinweise oder andere Dinge, die für das Verständnis des Dokuments nicht unbedingt nötig sind.
        /// </summary>
        Small,
        /// <summary>
        /// Markiert einen besonders wichtigen (stark hervorgehobenen) Text.
        /// </summary>
        Strong,
        Center,
        /// <summary>
        /// Markiert ein Programmiercode.
        /// </summary>
        Code,
        /// <summary>
        /// Markiert die Ausgabe eines Programms oder eines Computers.
        /// </summary>
        Output,
        /// <summary>
        /// Markiert einen Wert, der Datum und Uhrzeit angibt
        /// </summary>
        Time,
        /// <summary>
        /// Markiert einen tiefgestellten Text.
        /// </summary>
        Sub,
        /// <summary>
        /// Markiert einen hochgestellten Text.
        /// </summary>
        Sup,
        /// <summary>
        /// Steht für Text, der aus Referenzgründen hervorgehoben wird, d.h. der in anderem Kontext von Bedeutung ist.
        /// </summary>
        Mark,
        /// <summary>
        /// Markiert einen hervorgehobenen Text. 
        /// </summary>
        Highlight,
        /// <summary>
        /// Steht für einen Begriff, dessen Definition im nächstgelegenen Nachkommen-Element enthalten ist.
        /// </summary>
        Definition,
        /// <summary>
        /// Markiert eine Abkürzung oder ein Akronym.
        /// </summary>
        Abbreviation,
        /// <summary>
        /// Markiert eine Benutzereingabe.
        /// </summary>
        Input,
        /// <summary>
        /// Markiert ein Zitat.
        /// </summary>
        Blockquote,
        /// <summary>
        /// Markiert die Beschriftung einer Abbildung.
        /// </summary>
        Figcaption,
        /// <summary>
        /// Markiert den Inhalt dieses Elements als vorformatiert und das dieses Format erhalten bleiben soll.
        /// </summary>
        Preformatted,
        /// <summary>
        /// Markiert den Text als Markdown, welcher in HTML umgewandelt wird
        /// </summary>
        Markdown

    }
}
