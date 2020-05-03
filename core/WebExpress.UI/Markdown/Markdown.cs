using System.Collections.Generic;
using System.IO;
using WebExpress.Html;

namespace WebExpress.UI.Markdown
{
    /// <summary>
    /// Markdown ist eine vereinfachte Auszeichnungssprache.
    /// </summary>
    public sealed class Markdown
    {
        /// <summary>
        /// Transformiert den Quelltext in ein HTML-Knoten
        /// </summary>
        /// <param name="text">Der zu pardende Text</param>
        /// <returns>Die Html-Umwandlung</returns>
        public IHtmlNode Transform(string text)
        {
            var lines = SplitLines(text);
            var fragments = new List<MarkdownFragment>();

            foreach (var line in lines)
            {
                var md = ConvertLine(line);
                fragments.AddRange(md.Fragments);
            }

            var html = new List<IHtmlNode>();


            return new HtmlList(html);
        }

        /// <summary>
        /// Ermittelt den Typ
        /// </summary>
        /// <param name="text">Die zu prüfende Zeile</param>
        /// <returns>Die geprüfte und bestimmte Zeile</returns>
        private MarkdownLine ConvertLine(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new MarkdownLine()
                {
                    Text = text,
                    Fragments = new List<MarkdownFragment>() 
                    {
                        new MarkdownFragment()
                        {
                            Text = string.Empty,
                            Type = MarkdownMorpheme.Newline
                        }
                     }
                };
            }

            var fragments = new List<MarkdownFragment>();
            var fragment = null as MarkdownFragment;
            var token = new MarkdownToken()
            {
                Text = text,
                Position = 0
            };

            while ((fragment = GetFragment(token)) != null)
            {
                fragments.Add(fragment);
            }

            return new MarkdownLine()
            {
                Text = text,
                Fragments = fragments
            };
        }

        /// <summary>
        /// Ermittelt ein Fragment und gibt dieses zurück. Die Position
        /// des Tokens wird dabei weitergeschoben, bis das Ende des Fragments
        /// erreicht ist.
        /// </summary>
        /// <param name="token">Das Token</param>
        /// <returns>Das Fragment</returns>
        private MarkdownFragment GetFragment(MarkdownToken token)
        {
            var state = MarkdownState.None;
            var orign = token.Position;
            var position = orign;

            if (token.Empty)
            {
                return new MarkdownFragment() 
                { 
                    Text= string.Empty, 
                    Type = MarkdownMorpheme.Newline 
                };
            }
            else if (token.EoL)
            {
                return null;
            }

            do
            {
                var t = token.Token;

                switch (t)
                {
                    case ' ':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.None;
                                break;
                            case MarkdownState.A1:
                                state = MarkdownState.A1E;
                                break;
                            case MarkdownState.A1E:
                                goto exitLoop;
                            case MarkdownState.ALT:
                                state = MarkdownState.ALTE;
                                break;
                            case MarkdownState.ALTE:
                                goto exitLoop;
                            case MarkdownState.A2:
                                state = MarkdownState.A2E;
                                break;
                            case MarkdownState.A2E:
                                state = MarkdownState.A1;
                                goto exitLoop;
                            case MarkdownState.A3:
                                state = MarkdownState.A3E;
                                break;
                            case MarkdownState.A3E:
                                goto exitLoop;
                            case MarkdownState.AL:
                                state = MarkdownState.ALE;
                                break;
                            case MarkdownState.ALE:
                                state = MarkdownState.ALE;
                                break;
                            case MarkdownState.B:
                                goto exitLoop;
                            case MarkdownState.E:
                                goto exitLoop;
                            case MarkdownState.EH:
                                goto exitLoop;
                            case MarkdownState.G:
                                goto exitLoop;
                            case MarkdownState.H1:
                                goto exitLoop;
                            case MarkdownState.H2:
                                goto exitLoop;
                            case MarkdownState.H3:
                                goto exitLoop;
                            case MarkdownState.H4:
                                goto exitLoop;
                            case MarkdownState.H5:
                                goto exitLoop;
                            case MarkdownState.H6:
                                goto exitLoop;
                            case MarkdownState.I1T:
                                state = MarkdownState.IN;
                                break;
                            case MarkdownState.IN:
                                state = MarkdownState.IN;
                                break;
                            case MarkdownState.I2T:
                                state = MarkdownState.I2T;
                                break;
                            case MarkdownState.I3T:
                                state = MarkdownState.I3T;
                                break;
                            case MarkdownState.IP:
                                state = MarkdownState.IPE;
                                break;
                            case MarkdownState.IO:
                                state = MarkdownState.IO;
                                break;
                            case MarkdownState.LT:
                                state = MarkdownState.L;
                                break;
                            case MarkdownState.P:
                                goto exitLoop;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            case MarkdownState.R2T:
                                state = MarkdownState.R2T;
                                break;
                            case MarkdownState.RN:
                                state = MarkdownState.RN;
                                break;
                            case MarkdownState.R3T:
                                state = MarkdownState.RP;
                                break;
                            case MarkdownState.RP:
                                state = MarkdownState.RP;
                                break;
                            case MarkdownState.RO:
                                state = MarkdownState.RO;
                                break;
                            case MarkdownState.ROT:
                                state = MarkdownState.ROT;
                                break;
                            case MarkdownState.S:
                                goto exitLoop;
                            case MarkdownState.T:
                                goto exitLoop;
                            case MarkdownState.U1:
                                state = MarkdownState.U1E;
                                break;
                            case MarkdownState.U1E:
                                goto exitLoop;
                            case MarkdownState.ULT:
                                state = MarkdownState.ULTE;
                                break;
                            case MarkdownState.ULTE:
                                goto exitLoop;
                            case MarkdownState.U2:
                                state = MarkdownState.U2E;
                                break;
                            case MarkdownState.U2E:
                                state = MarkdownState.U1;
                                goto exitLoop;
                            case MarkdownState.U3:
                                state = MarkdownState.U3E;
                                break;
                            case MarkdownState.U3E:
                                goto exitLoop;
                            case MarkdownState.UL:
                                state = MarkdownState.ULE;
                                break;
                            case MarkdownState.ULE:
                                state = MarkdownState.ULE;
                                break;
                            case MarkdownState.Y1:
                                state = MarkdownState.Y1E;
                                break;
                            case MarkdownState.Y1E:
                                goto exitLoop;
                            case MarkdownState.YLT:
                                state = MarkdownState.YLTE;
                                break;
                            case MarkdownState.YLTE:
                                goto exitLoop;
                            case MarkdownState.Y2:
                                state = MarkdownState.Y2E;
                                break;
                            case MarkdownState.Y2E:
                                state = MarkdownState.Y1;
                                goto exitLoop;
                            case MarkdownState.Y3:
                                state = MarkdownState.Y3E;
                                break;
                            case MarkdownState.Y3E:
                                goto exitLoop;
                            case MarkdownState.YL:
                                state = MarkdownState.YLE;
                                break;
                            case MarkdownState.YLE:
                                state = MarkdownState.YLE;
                                break;
                            default:
                                state = MarkdownState.X;
                                break;
                        }
                        break;
                    case '*':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.A1;
                                break;
                            case MarkdownState.A1:
                                state = MarkdownState.A2;
                                break;
                            case MarkdownState.A1E:
                                state = MarkdownState.ALT;
                                break;
                            case MarkdownState.ALT:
                                state = MarkdownState.AL;
                                break;
                            case MarkdownState.ALTE:
                                state = MarkdownState.AL;
                                break;
                            case MarkdownState.A2:
                                state = MarkdownState.A3;
                                break;
                            case MarkdownState.A2E:
                                state = MarkdownState.AL;
                                break;
                            case MarkdownState.A3:
                                state = MarkdownState.AL;
                                break;
                            case MarkdownState.A3E:
                                state = MarkdownState.AL;
                                break;
                            case MarkdownState.AL:
                                state = MarkdownState.AL;
                                break;
                            case MarkdownState.ALE:
                                state = MarkdownState.AL;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '-':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.Y1;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            case MarkdownState.Y1:
                                state = MarkdownState.Y2;
                                break;
                            case MarkdownState.Y1E:
                                state = MarkdownState.YL;
                                break;
                            case MarkdownState.YLT:
                                state = MarkdownState.YL;
                                break;
                            case MarkdownState.YLTE:
                                state = MarkdownState.YL;
                                break;
                            case MarkdownState.Y2:
                                state = MarkdownState.Y3;
                                break;
                            case MarkdownState.Y2E:
                                state = MarkdownState.YL;
                                break;
                            case MarkdownState.Y3:
                                state = MarkdownState.YL;
                                break;
                            case MarkdownState.Y3E:
                                state = MarkdownState.YL;
                                break;
                            case MarkdownState.YL:
                                state = MarkdownState.YL;
                                break;
                            case MarkdownState.YLE:
                                state = MarkdownState.YL;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '_':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.U1;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            case MarkdownState.U1:
                                state = MarkdownState.U2;
                                break;
                            case MarkdownState.U1E:
                                state = MarkdownState.UL;
                                break;
                            case MarkdownState.ULT:
                                state = MarkdownState.UL;
                                break;
                            case MarkdownState.ULTE:
                                state = MarkdownState.UL;
                                break;
                            case MarkdownState.U2:
                                state = MarkdownState.U3;
                                break;
                            case MarkdownState.U2E:
                                state = MarkdownState.UL;
                                break;
                            case MarkdownState.U3:
                                state = MarkdownState.UL;
                                break;
                            case MarkdownState.U3E:
                                state = MarkdownState.UL;
                                break;
                            case MarkdownState.UL:
                                state = MarkdownState.UL;
                                break;
                            case MarkdownState.ULE:
                                state = MarkdownState.UL;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '#':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.H1;
                                break;
                            case MarkdownState.H1:
                                state = MarkdownState.H2;
                                break;
                            case MarkdownState.H2:
                                state = MarkdownState.H3;
                                break;
                            case MarkdownState.H3:
                                state = MarkdownState.H4;
                                break;
                            case MarkdownState.H4:
                                state = MarkdownState.H5;
                                break;
                            case MarkdownState.H5:
                                state = MarkdownState.H6;
                                break;
                            case MarkdownState.H6:
                                goto exitLoop;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '=':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.E;
                                break;
                            case MarkdownState.E:
                                state = MarkdownState.EH;
                                break;
                            case MarkdownState.EH:
                                state = MarkdownState.EH;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '+':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.P;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '>':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.G;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '`':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.B;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '\\':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.S;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '|':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.T;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '[':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.I1T;
                                break;
                            case MarkdownState.R1T:
                                state = MarkdownState.RT;
                                break;
                            case MarkdownState.R2T:
                                state = MarkdownState.RN;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case ']':
                        switch (state)
                        {
                            case MarkdownState.IN:
                                state = MarkdownState.I2T;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.R2T;
                                break;
                            case MarkdownState.RN:
                                state = MarkdownState.R;
                                break;
                            case MarkdownState.X:
                                state = MarkdownState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case ':':
                        switch (state)
                        {
                            case MarkdownState.I2T:
                                state = MarkdownState.I3T;
                                break;
                            case MarkdownState.LH4:
                                state = MarkdownState.LH5;
                                break;
                            case MarkdownState.LM6:
                                state = MarkdownState.LM7;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case '"':
                        switch (state)
                        {
                            case MarkdownState.IPE:
                                state = MarkdownState.IO;
                                break;
                            case MarkdownState.IO:
                                state = MarkdownState.IOE;
                                break;
                            case MarkdownState.RP:
                                state = MarkdownState.RO;
                                break;
                            case MarkdownState.RO:
                                state = MarkdownState.ROT;
                                break;
                            case MarkdownState.X:
                                state = MarkdownState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '!':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.R1T;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case '(':
                        switch (state)
                        {
                            case MarkdownState.R2T:
                                state = MarkdownState.R3T;
                                break;
                            case MarkdownState.X:
                                state = MarkdownState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case ')':
                        switch (state)
                        {
                            case MarkdownState.R2T:
                                state = MarkdownState.R3T;
                                break;
                            case MarkdownState.RP:
                                state = MarkdownState.R;
                                break;
                            case MarkdownState.ROT:
                                state = MarkdownState.R;
                                break;
                            case MarkdownState.X:
                                state = MarkdownState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '/':
                        switch (state)
                        {
                            case MarkdownState.LH5:
                                state = MarkdownState.LH6;
                                break;
                            case MarkdownState.LH6:
                                state = MarkdownState.LH7;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'h':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.LH1;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'a':
                        switch (state)
                        {
                            case MarkdownState.LM1:
                                state = MarkdownState.LM2;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'i':
                        switch (state)
                        {
                            case MarkdownState.LM2:
                                state = MarkdownState.LM3;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'l':
                        switch (state)
                        {
                            case MarkdownState.LM3:
                                state = MarkdownState.LM4;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'm':
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.LM1;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'o':
                        switch (state)
                        {
                            case MarkdownState.LM5:
                                state = MarkdownState.LM6;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'p':
                        switch (state)
                        {
                            case MarkdownState.LH3:
                                state = MarkdownState.LH4;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 't':
                        switch (state)
                        {
                            case MarkdownState.I1T:
                                state = MarkdownState.X;
                                break;
                            case MarkdownState.LH1:
                                state = MarkdownState.LH2;
                                break;
                            case MarkdownState.LH2:
                                state = MarkdownState.LH3;
                                break;
                            case MarkdownState.LM4:
                                state = MarkdownState.LM5;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    default:
                        Default:
                        switch (state)
                        {
                            case MarkdownState.None:
                                state = MarkdownState.X;
                                break;
                            case MarkdownState.A1:
                                goto exitLoop;
                            case MarkdownState.A1E:
                                goto exitLoop;
                            case MarkdownState.ALT:
                                goto exitLoop;
                            case MarkdownState.ALTE:
                                goto exitLoop;
                            case MarkdownState.A2:
                                goto exitLoop;
                            case MarkdownState.A2E:
                                goto exitLoop;
                            case MarkdownState.A3:
                                goto exitLoop;
                            case MarkdownState.A3E:
                                goto exitLoop;
                            case MarkdownState.AL:
                                goto exitLoop;
                            case MarkdownState.ALE:
                                goto exitLoop;
                            case MarkdownState.B:
                                goto exitLoop;
                            case MarkdownState.E:
                                goto exitLoop;
                            case MarkdownState.EH:
                                goto exitLoop;
                            case MarkdownState.G:
                                goto exitLoop;
                            case MarkdownState.H1:
                                goto exitLoop;
                            case MarkdownState.H2:
                                goto exitLoop;
                            case MarkdownState.H3:
                                goto exitLoop;
                            case MarkdownState.H4:
                                goto exitLoop;
                            case MarkdownState.H5:
                                goto exitLoop;
                            case MarkdownState.H6:
                                goto exitLoop;
                            case MarkdownState.I1T:
                                state = MarkdownState.IN;
                                break;
                            case MarkdownState.IN:
                                state = MarkdownState.IN;
                                break;
                            case MarkdownState.I3T:
                                state = MarkdownState.IP;
                                break;
                            case MarkdownState.IP:
                                state = MarkdownState.IP;
                                break;
                            case MarkdownState.IPE:
                                goto exitLoop;
                            case MarkdownState.IO:
                                state = MarkdownState.IO;
                                break;
                            case MarkdownState.IOE:
                                goto exitLoop;
                            case MarkdownState.LH7:
                                state = MarkdownState.LT;
                                break;
                            case MarkdownState.LM7:
                                state = MarkdownState.LT;
                                break;
                            case MarkdownState.LT:
                                state = MarkdownState.LT;
                                break;
                            case MarkdownState.P:
                                goto exitLoop;
                            case MarkdownState.R1T:
                                state = MarkdownState.X;
                                break;
                            case MarkdownState.RT:
                                state = MarkdownState.RT;
                                break;
                            case MarkdownState.R2T:
                                goto exitLoop;
                            case MarkdownState.RN:
                                state = MarkdownState.RN;
                                break;
                            case MarkdownState.R3T:
                                state = MarkdownState.RP;
                                break;
                            case MarkdownState.RP:
                                state = MarkdownState.RP;
                                break;
                            case MarkdownState.RO:
                                state = MarkdownState.RO;
                                break;
                            case MarkdownState.ROT:
                                goto exitLoop;
                            case MarkdownState.S:
                                goto exitLoop;
                            case MarkdownState.T:
                                goto exitLoop;
                            case MarkdownState.U1:
                                goto exitLoop;
                            case MarkdownState.U1E:
                                goto exitLoop;
                            case MarkdownState.ULT:
                                goto exitLoop;
                            case MarkdownState.ULTE:
                                goto exitLoop;
                            case MarkdownState.U2:
                                goto exitLoop;
                            case MarkdownState.U2E:
                                goto exitLoop;
                            case MarkdownState.U3:
                                goto exitLoop;
                            case MarkdownState.U3E:
                                goto exitLoop;
                            case MarkdownState.UL:
                                goto exitLoop;
                            case MarkdownState.ULE:
                                goto exitLoop;
                            case MarkdownState.Y1:
                                goto exitLoop;
                            case MarkdownState.Y1E:
                                goto exitLoop;
                            case MarkdownState.YLT:
                                goto exitLoop;
                            case MarkdownState.YLTE:
                                goto exitLoop;
                            case MarkdownState.Y2:
                                goto exitLoop;
                            case MarkdownState.Y2E:
                                goto exitLoop;
                            case MarkdownState.Y3:
                                goto exitLoop;
                            case MarkdownState.Y3E:
                                goto exitLoop;
                            case MarkdownState.YL:
                                goto exitLoop;
                            case MarkdownState.YLE:
                                goto exitLoop;
                            default:
                                state = MarkdownState.X;
                                break;
                        }
                        break;

                }

                position++;
            }
            while (token.Next());

        exitLoop:

            var morpheme = MarkdownMorpheme.Text;

            switch (state)
            {
                case MarkdownState.None:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.A1:
                    morpheme = MarkdownMorpheme.Asterisk1;
                    break;
                case MarkdownState.A1E:
                    morpheme = MarkdownMorpheme.Asterisk1;
                    position--;
                    break;
                case MarkdownState.ALT:
                    morpheme = MarkdownMorpheme.Asterisk1;
                    position = orign + 1;
                    break;
                case MarkdownState.ALTE:
                    morpheme = MarkdownMorpheme.Asterisk1;
                    position = orign + 1;
                    break;
                case MarkdownState.A2:
                    morpheme = MarkdownMorpheme.Asterisk2;
                    break;
                case MarkdownState.A2E:
                    morpheme = MarkdownMorpheme.Asterisk2;
                    position--;
                    break;
                case MarkdownState.A3:
                    morpheme = MarkdownMorpheme.Asterisk3;
                    break;
                case MarkdownState.A3E:
                    morpheme = MarkdownMorpheme.Asterisk3;
                    position--;
                    break;
                case MarkdownState.AL:
                    morpheme = MarkdownMorpheme.HorizontaleLinie;
                    break;
                case MarkdownState.ALE:
                    morpheme = MarkdownMorpheme.HorizontaleLinie;
                    break;
                case MarkdownState.B:
                    morpheme = MarkdownMorpheme.Code;
                    break;
                case MarkdownState.E:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.EH:
                    morpheme = MarkdownMorpheme.Headheadline1Marker;
                    break;
                case MarkdownState.G:
                    morpheme = MarkdownMorpheme.Quote;
                    break;
                case MarkdownState.H1:
                    morpheme = MarkdownMorpheme.Headheadline1;
                    break;
                case MarkdownState.H2:
                    morpheme = MarkdownMorpheme.Headheadline2;
                    break;
                case MarkdownState.H3:
                    morpheme = MarkdownMorpheme.Headheadline3;
                    break;
                case MarkdownState.H4:
                    morpheme = MarkdownMorpheme.Headheadline4;
                    break;
                case MarkdownState.H5:
                    morpheme = MarkdownMorpheme.Headheadline5;
                    break;
                case MarkdownState.H6:
                    morpheme = MarkdownMorpheme.Headheadline6;
                    break;
                case MarkdownState.I1T:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.IN:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.I2T:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.I3T:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.IP:
                    morpheme = MarkdownMorpheme.Image;
                    break;
                case MarkdownState.IPE:
                    morpheme = MarkdownMorpheme.Image;
                    break;
                case MarkdownState.IO:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.IOE:
                    morpheme = MarkdownMorpheme.Image;
                    break;
                case MarkdownState.LH1:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LH2:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LH3:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LH4:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LH5:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LH6:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LH7:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LM1:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LM2:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LM3:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LM4:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LM5:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LM6:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LM7:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.LT:
                    morpheme = MarkdownMorpheme.Link;
                    break;
                case MarkdownState.L:
                    morpheme = MarkdownMorpheme.Link;
                    break;
                case MarkdownState.P:
                    morpheme = MarkdownMorpheme.Plus;
                    break;
                case MarkdownState.R1T:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.RT:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.R2T:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.RN:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.R:
                    morpheme = MarkdownMorpheme.ImageReference;
                    break;
                case MarkdownState.R3T:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.RP:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.RO:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.ROT:
                    morpheme = MarkdownMorpheme.Text;
                    break;
                case MarkdownState.S:
                    morpheme = MarkdownMorpheme.Mask;
                    break;
                case MarkdownState.T:
                    morpheme = MarkdownMorpheme.Pipe;
                    break;
                case MarkdownState.U1:
                    morpheme = MarkdownMorpheme.Underline1;
                    break;
                case MarkdownState.U1E:
                    morpheme = MarkdownMorpheme.Underline1;
                    position--;
                    break;
                case MarkdownState.ULT:
                    morpheme = MarkdownMorpheme.Underline1;
                    position = orign + 1;
                    break;
                case MarkdownState.ULTE:
                    morpheme = MarkdownMorpheme.Underline1;
                    position = orign + 1;
                    break;
                case MarkdownState.U2:
                    morpheme = MarkdownMorpheme.Underline2;
                    break;
                case MarkdownState.U2E:
                    morpheme = MarkdownMorpheme.Underline2;
                    position--;
                    break;
                case MarkdownState.U3:
                    morpheme = MarkdownMorpheme.Underline3;
                    break;
                case MarkdownState.U3E:
                    morpheme = MarkdownMorpheme.Underline3;
                    position--;
                    break;
                case MarkdownState.UL:
                    morpheme = MarkdownMorpheme.HorizontaleLinie;
                    break;
                case MarkdownState.ULE:
                    morpheme = MarkdownMorpheme.HorizontaleLinie;
                    position--;
                    break;
                case MarkdownState.Y1:
                    morpheme = MarkdownMorpheme.Hyphen1;
                    break;
                case MarkdownState.Y1E:
                    morpheme = MarkdownMorpheme.Hyphen1;
                    position--;
                    break;
                case MarkdownState.YLT:
                    morpheme = MarkdownMorpheme.Hyphen1;
                    position = orign + 1;
                    break;
                case MarkdownState.YLTE:
                    morpheme = MarkdownMorpheme.Hyphen1;
                    position = orign + 1;
                    break;
                case MarkdownState.Y2:
                    morpheme = MarkdownMorpheme.Hyphen2;
                    break;
                case MarkdownState.Y2E:
                    morpheme = MarkdownMorpheme.Hyphen2;
                    position--;
                    break;
                case MarkdownState.Y3:
                    morpheme = MarkdownMorpheme.Hyphen3;
                    break;
                case MarkdownState.Y3E:
                    morpheme = MarkdownMorpheme.Hyphen3;
                    position--;
                    break;
                case MarkdownState.YL:
                    morpheme = MarkdownMorpheme.HorizontaleLinie;
                    break;
                case MarkdownState.YLE:
                    morpheme = MarkdownMorpheme.HorizontaleLinie;
                    position--;
                    break;
            }

            token.Position = position;

            return new MarkdownFragment()
            {
                Type = morpheme,
                Text = token.Text.Substring(orign, position - orign)
            };
        }

        /// <summary>
        /// Splitet den Einganetext an Zeilenumbrüchen auf
        /// </summary>
        /// <param name="text">Der Eingabetext</param>
        /// <returns>Der in Zeilen aufgeteilte Eingabetext</returns>
        private IEnumerable<string> SplitLines(string text)
        {
            using (var tr = new StringReader(text))
            {
                while (tr.ReadLine() is string line)
                {
                    yield return line;
                }
            }
        }
    }
}
