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
            var stack = new Stack<MarkdownMorpheme>();

            foreach (var line in lines)
            {
                foreach (var morphemes in ConvertLine(line)?.Morphemes)
                {

                }
            }

            var html = new List<IHtmlNode>();


            return new HtmlList(html);
        }

        /// <summary>
        /// Ermittelt den Typ
        /// </summary>
        /// <param name="text">Die zu prüfende Zeile</param>
        /// <returns>Die geprüfte und bestimmte Zeile</returns>
        private MarkdownMorphemes ConvertLine(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new MarkdownMorphemes();
            }

            var morphemes = new MarkdownMorphemes();
            var fragment = null as MarkdownFragment;
            var token = new MarkdownToken()
            {
                Text = text,
                Position = 0
            };

            while ((fragment = GetFragment(token)) != null)
            {
                morphemes.Add(fragment.Type, fragment.Text);
            }

            morphemes.Completed();

            return morphemes;
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
            var state = MarkdownTokenState.None;
            var orign = token.Position;
            var position = orign;

            if (token.Empty)
            {
                return new MarkdownFragment()
                {
                    Text = string.Empty,
                    Type = MarkdownFragmentState.Newline
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
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.W1;
                                break;
                            case MarkdownTokenState.A1:
                                state = MarkdownTokenState.A1E;
                                break;
                            case MarkdownTokenState.A1E:
                                goto exitLoop;
                            case MarkdownTokenState.ALT:
                                state = MarkdownTokenState.ALTE;
                                break;
                            case MarkdownTokenState.ALTE:
                                goto exitLoop;
                            case MarkdownTokenState.A2:
                                state = MarkdownTokenState.A2E;
                                break;
                            case MarkdownTokenState.A2E:
                                state = MarkdownTokenState.A1;
                                goto exitLoop;
                            case MarkdownTokenState.A3:
                                state = MarkdownTokenState.A3E;
                                break;
                            case MarkdownTokenState.A3E:
                                goto exitLoop;
                            case MarkdownTokenState.AL:
                                state = MarkdownTokenState.ALE;
                                break;
                            case MarkdownTokenState.ALE:
                                state = MarkdownTokenState.ALE;
                                break;
                            case MarkdownTokenState.B:
                                goto exitLoop;
                            case MarkdownTokenState.D1:
                                state = MarkdownTokenState.D1E;
                                break;
                            case MarkdownTokenState.D1E:
                                goto exitLoop;
                            case MarkdownTokenState.DLT:
                                state = MarkdownTokenState.DLTE;
                                break;
                            case MarkdownTokenState.DLTE:
                                goto exitLoop;
                            case MarkdownTokenState.D2:
                                state = MarkdownTokenState.D2E;
                                break;
                            case MarkdownTokenState.D2E:
                                state = MarkdownTokenState.D1;
                                goto exitLoop;
                            case MarkdownTokenState.D3:
                                state = MarkdownTokenState.D3E;
                                break;
                            case MarkdownTokenState.D3E:
                                goto exitLoop;
                            case MarkdownTokenState.DL:
                                state = MarkdownTokenState.DLE;
                                break;
                            case MarkdownTokenState.DLE:
                                state = MarkdownTokenState.DLE;
                                break;
                            case MarkdownTokenState.E:
                                goto exitLoop;
                            case MarkdownTokenState.EH:
                                goto exitLoop;
                            case MarkdownTokenState.G:
                                goto exitLoop;
                            case MarkdownTokenState.H1:
                                goto exitLoop;
                            case MarkdownTokenState.H2:
                                goto exitLoop;
                            case MarkdownTokenState.H3:
                                goto exitLoop;
                            case MarkdownTokenState.H4:
                                goto exitLoop;
                            case MarkdownTokenState.H5:
                                goto exitLoop;
                            case MarkdownTokenState.H6:
                                goto exitLoop;
                            case MarkdownTokenState.I1T:
                                state = MarkdownTokenState.IN;
                                break;
                            case MarkdownTokenState.IN:
                                state = MarkdownTokenState.IN;
                                break;
                            case MarkdownTokenState.I2T:
                                state = MarkdownTokenState.I2T;
                                break;
                            case MarkdownTokenState.I3T:
                                state = MarkdownTokenState.I3T;
                                break;
                            case MarkdownTokenState.IP:
                                state = MarkdownTokenState.IPE;
                                break;
                            case MarkdownTokenState.IO:
                                state = MarkdownTokenState.IO;
                                break;
                            case MarkdownTokenState.LT:
                                state = MarkdownTokenState.L;
                                break;
                            case MarkdownTokenState.P:
                                goto exitLoop;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            case MarkdownTokenState.R2T:
                                state = MarkdownTokenState.R2T;
                                break;
                            case MarkdownTokenState.RN:
                                state = MarkdownTokenState.RN;
                                break;
                            case MarkdownTokenState.R3T:
                                state = MarkdownTokenState.RP;
                                break;
                            case MarkdownTokenState.RP:
                                state = MarkdownTokenState.RP;
                                break;
                            case MarkdownTokenState.RO:
                                state = MarkdownTokenState.RO;
                                break;
                            case MarkdownTokenState.ROT:
                                state = MarkdownTokenState.ROT;
                                break;
                            case MarkdownTokenState.S:
                                goto exitLoop;
                            case MarkdownTokenState.T:
                                goto exitLoop;
                            case MarkdownTokenState.U1:
                                state = MarkdownTokenState.U1E;
                                break;
                            case MarkdownTokenState.U1E:
                                goto exitLoop;
                            case MarkdownTokenState.ULT:
                                state = MarkdownTokenState.ULTE;
                                break;
                            case MarkdownTokenState.ULTE:
                                goto exitLoop;
                            case MarkdownTokenState.U2:
                                state = MarkdownTokenState.U2E;
                                break;
                            case MarkdownTokenState.U2E:
                                state = MarkdownTokenState.U1;
                                goto exitLoop;
                            case MarkdownTokenState.U3:
                                state = MarkdownTokenState.U3E;
                                break;
                            case MarkdownTokenState.U3E:
                                goto exitLoop;
                            case MarkdownTokenState.UL:
                                state = MarkdownTokenState.ULE;
                                break;
                            case MarkdownTokenState.ULE:
                                state = MarkdownTokenState.ULE;
                                break;
                            case MarkdownTokenState.W1:
                                state = MarkdownTokenState.W2;
                                break;
                            case MarkdownTokenState.W2:
                                state = MarkdownTokenState.W3;
                                break;
                            case MarkdownTokenState.W3:
                                state = MarkdownTokenState.W4;
                                break;
                            case MarkdownTokenState.W4:
                                goto exitLoop;
                            case MarkdownTokenState.Y1:
                                state = MarkdownTokenState.Y1E;
                                break;
                            case MarkdownTokenState.Y1E:
                                goto exitLoop;
                            case MarkdownTokenState.YLT:
                                state = MarkdownTokenState.YLTE;
                                break;
                            case MarkdownTokenState.YLTE:
                                goto exitLoop;
                            case MarkdownTokenState.Y2:
                                state = MarkdownTokenState.Y2E;
                                break;
                            case MarkdownTokenState.Y2E:
                                state = MarkdownTokenState.Y1;
                                goto exitLoop;
                            case MarkdownTokenState.Y3:
                                state = MarkdownTokenState.Y3E;
                                break;
                            case MarkdownTokenState.Y3E:
                                goto exitLoop;
                            case MarkdownTokenState.YL:
                                state = MarkdownTokenState.YLE;
                                break;
                            case MarkdownTokenState.YLE:
                                state = MarkdownTokenState.YLE;
                                break;
                            default:
                                state = MarkdownTokenState.X;
                                break;
                        }
                        break;
                    case '*':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.A1;
                                break;
                            case MarkdownTokenState.A1:
                                state = MarkdownTokenState.A2;
                                break;
                            case MarkdownTokenState.A1E:
                                state = MarkdownTokenState.ALT;
                                break;
                            case MarkdownTokenState.ALT:
                                state = MarkdownTokenState.AL;
                                break;
                            case MarkdownTokenState.ALTE:
                                state = MarkdownTokenState.AL;
                                break;
                            case MarkdownTokenState.A2:
                                state = MarkdownTokenState.A3;
                                break;
                            case MarkdownTokenState.A2E:
                                state = MarkdownTokenState.AL;
                                break;
                            case MarkdownTokenState.A3:
                                state = MarkdownTokenState.AL;
                                break;
                            case MarkdownTokenState.A3E:
                                state = MarkdownTokenState.AL;
                                break;
                            case MarkdownTokenState.AL:
                                state = MarkdownTokenState.AL;
                                break;
                            case MarkdownTokenState.ALE:
                                state = MarkdownTokenState.AL;
                                break;
                            case MarkdownTokenState.W1:
                                state = MarkdownTokenState.A1;
                                break;
                            case MarkdownTokenState.W2:
                                state = MarkdownTokenState.A1;
                                break;
                            case MarkdownTokenState.W3:
                                state = MarkdownTokenState.A1;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '~':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.D1;
                                break;
                            case MarkdownTokenState.D1:
                                state = MarkdownTokenState.D2;
                                break;
                            case MarkdownTokenState.D1E:
                                state = MarkdownTokenState.DLT;
                                break;
                            case MarkdownTokenState.DLT:
                                state = MarkdownTokenState.DL;
                                break;
                            case MarkdownTokenState.DLTE:
                                state = MarkdownTokenState.DL;
                                break;
                            case MarkdownTokenState.D2:
                                state = MarkdownTokenState.D3;
                                break;
                            case MarkdownTokenState.D2E:
                                state = MarkdownTokenState.DL;
                                break;
                            case MarkdownTokenState.D3:
                                state = MarkdownTokenState.DL;
                                break;
                            case MarkdownTokenState.D3E:
                                state = MarkdownTokenState.DL;
                                break;
                            case MarkdownTokenState.DL:
                                state = MarkdownTokenState.DL;
                                break;
                            case MarkdownTokenState.DLE:
                                state = MarkdownTokenState.DL;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            case MarkdownTokenState.W1:
                                state = MarkdownTokenState.D1;
                                break;
                            case MarkdownTokenState.W2:
                                state = MarkdownTokenState.D1;
                                break;
                            case MarkdownTokenState.W3:
                                state = MarkdownTokenState.D1;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '-':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.Y1;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            case MarkdownTokenState.W1:
                                state = MarkdownTokenState.Y1;
                                break;
                            case MarkdownTokenState.W2:
                                state = MarkdownTokenState.Y1;
                                break;
                            case MarkdownTokenState.W3:
                                state = MarkdownTokenState.Y1;
                                break;
                            case MarkdownTokenState.Y1:
                                state = MarkdownTokenState.Y2;
                                break;
                            case MarkdownTokenState.Y1E:
                                state = MarkdownTokenState.YLT;
                                break;
                            case MarkdownTokenState.YLT:
                                state = MarkdownTokenState.YL;
                                break;
                            case MarkdownTokenState.YLTE:
                                state = MarkdownTokenState.YL;
                                break;
                            case MarkdownTokenState.Y2:
                                state = MarkdownTokenState.Y3;
                                break;
                            case MarkdownTokenState.Y2E:
                                state = MarkdownTokenState.YL;
                                break;
                            case MarkdownTokenState.Y3:
                                state = MarkdownTokenState.YL;
                                break;
                            case MarkdownTokenState.Y3E:
                                state = MarkdownTokenState.YL;
                                break;
                            case MarkdownTokenState.YL:
                                state = MarkdownTokenState.YL;
                                break;
                            case MarkdownTokenState.YLE:
                                state = MarkdownTokenState.YL;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '_':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.U1;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            case MarkdownTokenState.U1:
                                state = MarkdownTokenState.U2;
                                break;
                            case MarkdownTokenState.U1E:
                                state = MarkdownTokenState.ULT;
                                break;
                            case MarkdownTokenState.ULT:
                                state = MarkdownTokenState.UL;
                                break;
                            case MarkdownTokenState.ULTE:
                                state = MarkdownTokenState.UL;
                                break;
                            case MarkdownTokenState.U2:
                                state = MarkdownTokenState.U3;
                                break;
                            case MarkdownTokenState.U2E:
                                state = MarkdownTokenState.UL;
                                break;
                            case MarkdownTokenState.U3:
                                state = MarkdownTokenState.UL;
                                break;
                            case MarkdownTokenState.U3E:
                                state = MarkdownTokenState.UL;
                                break;
                            case MarkdownTokenState.UL:
                                state = MarkdownTokenState.UL;
                                break;
                            case MarkdownTokenState.ULE:
                                state = MarkdownTokenState.UL;
                                break;
                            case MarkdownTokenState.W1:
                                state = MarkdownTokenState.U1;
                                break;
                            case MarkdownTokenState.W2:
                                state = MarkdownTokenState.U1;
                                break;
                            case MarkdownTokenState.W3:
                                state = MarkdownTokenState.U1;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '#':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.H1;
                                break;
                            case MarkdownTokenState.H1:
                                state = MarkdownTokenState.H2;
                                break;
                            case MarkdownTokenState.H2:
                                state = MarkdownTokenState.H3;
                                break;
                            case MarkdownTokenState.H3:
                                state = MarkdownTokenState.H4;
                                break;
                            case MarkdownTokenState.H4:
                                state = MarkdownTokenState.H5;
                                break;
                            case MarkdownTokenState.H5:
                                state = MarkdownTokenState.H6;
                                break;
                            case MarkdownTokenState.H6:
                                goto exitLoop;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '=':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.E;
                                break;
                            case MarkdownTokenState.E:
                                state = MarkdownTokenState.EH;
                                break;
                            case MarkdownTokenState.EH:
                                state = MarkdownTokenState.EH;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '+':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.P;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '>':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.G;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '`':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.B;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '\\':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.S;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '|':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.T;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '[':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.I1T;
                                break;
                            case MarkdownTokenState.R1T:
                                state = MarkdownTokenState.RT;
                                break;
                            case MarkdownTokenState.R2T:
                                state = MarkdownTokenState.RN;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case ']':
                        switch (state)
                        {
                            case MarkdownTokenState.IN:
                                state = MarkdownTokenState.I2T;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.R2T;
                                break;
                            case MarkdownTokenState.RN:
                                state = MarkdownTokenState.R;
                                break;
                            case MarkdownTokenState.X:
                                state = MarkdownTokenState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case ':':
                        switch (state)
                        {
                            case MarkdownTokenState.I2T:
                                state = MarkdownTokenState.I3T;
                                break;
                            case MarkdownTokenState.LH4:
                                state = MarkdownTokenState.LH5;
                                break;
                            case MarkdownTokenState.LM6:
                                state = MarkdownTokenState.LM7;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case '"':
                        switch (state)
                        {
                            case MarkdownTokenState.IPE:
                                state = MarkdownTokenState.IO;
                                break;
                            case MarkdownTokenState.IO:
                                state = MarkdownTokenState.IOE;
                                break;
                            case MarkdownTokenState.RP:
                                state = MarkdownTokenState.RO;
                                break;
                            case MarkdownTokenState.RO:
                                state = MarkdownTokenState.ROT;
                                break;
                            case MarkdownTokenState.X:
                                state = MarkdownTokenState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '!':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.R1T;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case '(':
                        switch (state)
                        {
                            case MarkdownTokenState.R2T:
                                state = MarkdownTokenState.R3T;
                                break;
                            case MarkdownTokenState.X:
                                state = MarkdownTokenState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case ')':
                        switch (state)
                        {
                            case MarkdownTokenState.R2T:
                                state = MarkdownTokenState.R3T;
                                break;
                            case MarkdownTokenState.RP:
                                state = MarkdownTokenState.R;
                                break;
                            case MarkdownTokenState.ROT:
                                state = MarkdownTokenState.R;
                                break;
                            case MarkdownTokenState.X:
                                state = MarkdownTokenState.X;
                                break;
                            default:
                                goto exitLoop;
                        }
                        break;
                    case '/':
                        switch (state)
                        {
                            case MarkdownTokenState.LH5:
                                state = MarkdownTokenState.LH6;
                                break;
                            case MarkdownTokenState.LH6:
                                state = MarkdownTokenState.LH7;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'h':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.LH1;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'a':
                        switch (state)
                        {
                            case MarkdownTokenState.LM1:
                                state = MarkdownTokenState.LM2;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'i':
                        switch (state)
                        {
                            case MarkdownTokenState.LM2:
                                state = MarkdownTokenState.LM3;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'l':
                        switch (state)
                        {
                            case MarkdownTokenState.LM3:
                                state = MarkdownTokenState.LM4;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'm':
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.LM1;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'o':
                        switch (state)
                        {
                            case MarkdownTokenState.LM5:
                                state = MarkdownTokenState.LM6;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 'p':
                        switch (state)
                        {
                            case MarkdownTokenState.LH3:
                                state = MarkdownTokenState.LH4;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    case 't':
                        switch (state)
                        {
                            case MarkdownTokenState.I1T:
                                state = MarkdownTokenState.X;
                                break;
                            case MarkdownTokenState.LH1:
                                state = MarkdownTokenState.LH2;
                                break;
                            case MarkdownTokenState.LH2:
                                state = MarkdownTokenState.LH3;
                                break;
                            case MarkdownTokenState.LM4:
                                state = MarkdownTokenState.LM5;
                                break;
                            default:
                                goto Default;
                        }
                        break;
                    default:
                    Default:
                        switch (state)
                        {
                            case MarkdownTokenState.None:
                                state = MarkdownTokenState.X;
                                break;
                            case MarkdownTokenState.A1:
                                goto exitLoop;
                            case MarkdownTokenState.A1E:
                                goto exitLoop;
                            case MarkdownTokenState.ALT:
                                goto exitLoop;
                            case MarkdownTokenState.ALTE:
                                goto exitLoop;
                            case MarkdownTokenState.A2:
                                goto exitLoop;
                            case MarkdownTokenState.A2E:
                                goto exitLoop;
                            case MarkdownTokenState.A3:
                                goto exitLoop;
                            case MarkdownTokenState.A3E:
                                goto exitLoop;
                            case MarkdownTokenState.AL:
                                goto exitLoop;
                            case MarkdownTokenState.ALE:
                                goto exitLoop;
                            case MarkdownTokenState.B:
                                goto exitLoop;
                            case MarkdownTokenState.D1:
                                goto exitLoop;
                            case MarkdownTokenState.D1E:
                                goto exitLoop;
                            case MarkdownTokenState.DLT:
                                goto exitLoop;
                            case MarkdownTokenState.DLTE:
                                goto exitLoop;
                            case MarkdownTokenState.D2:
                                goto exitLoop;
                            case MarkdownTokenState.D2E:
                                goto exitLoop;
                            case MarkdownTokenState.D3:
                                goto exitLoop;
                            case MarkdownTokenState.D3E:
                                goto exitLoop;
                            case MarkdownTokenState.DL:
                                goto exitLoop;
                            case MarkdownTokenState.DLE:
                                goto exitLoop;
                            case MarkdownTokenState.E:
                                goto exitLoop;
                            case MarkdownTokenState.EH:
                                goto exitLoop;
                            case MarkdownTokenState.G:
                                goto exitLoop;
                            case MarkdownTokenState.H1:
                                goto exitLoop;
                            case MarkdownTokenState.H2:
                                goto exitLoop;
                            case MarkdownTokenState.H3:
                                goto exitLoop;
                            case MarkdownTokenState.H4:
                                goto exitLoop;
                            case MarkdownTokenState.H5:
                                goto exitLoop;
                            case MarkdownTokenState.H6:
                                goto exitLoop;
                            case MarkdownTokenState.I1T:
                                state = MarkdownTokenState.IN;
                                break;
                            case MarkdownTokenState.IN:
                                state = MarkdownTokenState.IN;
                                break;
                            case MarkdownTokenState.I3T:
                                state = MarkdownTokenState.IP;
                                break;
                            case MarkdownTokenState.IP:
                                state = MarkdownTokenState.IP;
                                break;
                            case MarkdownTokenState.IPE:
                                goto exitLoop;
                            case MarkdownTokenState.IO:
                                state = MarkdownTokenState.IO;
                                break;
                            case MarkdownTokenState.IOE:
                                goto exitLoop;
                            case MarkdownTokenState.LH7:
                                state = MarkdownTokenState.LT;
                                break;
                            case MarkdownTokenState.LM7:
                                state = MarkdownTokenState.LT;
                                break;
                            case MarkdownTokenState.LT:
                                state = MarkdownTokenState.LT;
                                break;
                            case MarkdownTokenState.P:
                                goto exitLoop;
                            case MarkdownTokenState.R1T:
                                state = MarkdownTokenState.X;
                                break;
                            case MarkdownTokenState.RT:
                                state = MarkdownTokenState.RT;
                                break;
                            case MarkdownTokenState.R2T:
                                goto exitLoop;
                            case MarkdownTokenState.RN:
                                state = MarkdownTokenState.RN;
                                break;
                            case MarkdownTokenState.R3T:
                                state = MarkdownTokenState.RP;
                                break;
                            case MarkdownTokenState.RP:
                                state = MarkdownTokenState.RP;
                                break;
                            case MarkdownTokenState.RO:
                                state = MarkdownTokenState.RO;
                                break;
                            case MarkdownTokenState.ROT:
                                goto exitLoop;
                            case MarkdownTokenState.S:
                                goto exitLoop;
                            case MarkdownTokenState.T:
                                goto exitLoop;
                            case MarkdownTokenState.U1:
                                goto exitLoop;
                            case MarkdownTokenState.U1E:
                                goto exitLoop;
                            case MarkdownTokenState.ULT:
                                goto exitLoop;
                            case MarkdownTokenState.ULTE:
                                goto exitLoop;
                            case MarkdownTokenState.U2:
                                goto exitLoop;
                            case MarkdownTokenState.U2E:
                                goto exitLoop;
                            case MarkdownTokenState.U3:
                                goto exitLoop;
                            case MarkdownTokenState.U3E:
                                goto exitLoop;
                            case MarkdownTokenState.UL:
                                goto exitLoop;
                            case MarkdownTokenState.ULE:
                                goto exitLoop;
                            case MarkdownTokenState.Y1:
                                goto exitLoop;
                            case MarkdownTokenState.Y1E:
                                goto exitLoop;
                            case MarkdownTokenState.YLT:
                                goto exitLoop;
                            case MarkdownTokenState.YLTE:
                                goto exitLoop;
                            case MarkdownTokenState.Y2:
                                goto exitLoop;
                            case MarkdownTokenState.Y2E:
                                goto exitLoop;
                            case MarkdownTokenState.Y3:
                                goto exitLoop;
                            case MarkdownTokenState.Y3E:
                                goto exitLoop;
                            case MarkdownTokenState.YL:
                                goto exitLoop;
                            case MarkdownTokenState.YLE:
                                goto exitLoop;
                            default:
                                state = MarkdownTokenState.X;
                                break;
                        }
                        break;

                }

                position++;
            }
            while (token.Next());

        exitLoop:

            var morpheme = MarkdownFragmentState.Text;

            switch (state)
            {
                case MarkdownTokenState.None:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.A1:
                    morpheme = MarkdownFragmentState.Asterisk1;
                    break;
                case MarkdownTokenState.A1E:
                    morpheme = MarkdownFragmentState.Asterisk1;
                    position--;
                    break;
                case MarkdownTokenState.ALT:
                    morpheme = MarkdownFragmentState.Asterisk1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.ALTE:
                    morpheme = MarkdownFragmentState.Asterisk1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.A2:
                    morpheme = MarkdownFragmentState.Asterisk2;
                    break;
                case MarkdownTokenState.A2E:
                    morpheme = MarkdownFragmentState.Asterisk2;
                    position--;
                    break;
                case MarkdownTokenState.A3:
                    morpheme = MarkdownFragmentState.Asterisk3;
                    break;
                case MarkdownTokenState.A3E:
                    morpheme = MarkdownFragmentState.Asterisk3;
                    position--;
                    break;
                case MarkdownTokenState.AL:
                    morpheme = MarkdownFragmentState.HorizontalLine;
                    break;
                case MarkdownTokenState.ALE:
                    morpheme = MarkdownFragmentState.HorizontalLine;
                    break;
                case MarkdownTokenState.B:
                    morpheme = MarkdownFragmentState.Code;
                    break;
                case MarkdownTokenState.D1:
                    morpheme = MarkdownFragmentState.Tilde1;
                    break;
                case MarkdownTokenState.D1E:
                    morpheme = MarkdownFragmentState.Tilde1;
                    position--;
                    break;
                case MarkdownTokenState.DLT:
                    morpheme = MarkdownFragmentState.Tilde1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.DLTE:
                    morpheme = MarkdownFragmentState.Tilde1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.D2:
                    morpheme = MarkdownFragmentState.Tilde2;
                    break;
                case MarkdownTokenState.D2E:
                    morpheme = MarkdownFragmentState.Tilde2;
                    position--;
                    break;
                case MarkdownTokenState.D3:
                    morpheme = MarkdownFragmentState.Tilde3;
                    break;
                case MarkdownTokenState.D3E:
                    morpheme = MarkdownFragmentState.Tilde3;
                    position--;
                    break;
                case MarkdownTokenState.DL:
                    morpheme = MarkdownFragmentState.HorizontalLine;
                    break;
                case MarkdownTokenState.DLE:
                    morpheme = MarkdownFragmentState.HorizontalLine;
                    position--;
                    break;
                case MarkdownTokenState.E:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.EH:
                    morpheme = MarkdownFragmentState.Headheadline1Marker;
                    break;
                case MarkdownTokenState.G:
                    morpheme = MarkdownFragmentState.Quote;
                    break;
                case MarkdownTokenState.H1:
                    morpheme = MarkdownFragmentState.Headheadline1;
                    break;
                case MarkdownTokenState.H2:
                    morpheme = MarkdownFragmentState.Headheadline2;
                    break;
                case MarkdownTokenState.H3:
                    morpheme = MarkdownFragmentState.Headheadline3;
                    break;
                case MarkdownTokenState.H4:
                    morpheme = MarkdownFragmentState.Headheadline4;
                    break;
                case MarkdownTokenState.H5:
                    morpheme = MarkdownFragmentState.Headheadline5;
                    break;
                case MarkdownTokenState.H6:
                    morpheme = MarkdownFragmentState.Headheadline6;
                    break;
                case MarkdownTokenState.I1T:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.IN:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.I2T:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.I3T:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.IP:
                    morpheme = MarkdownFragmentState.Image;
                    break;
                case MarkdownTokenState.IPE:
                    morpheme = MarkdownFragmentState.Image;
                    break;
                case MarkdownTokenState.IO:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.IOE:
                    morpheme = MarkdownFragmentState.Image;
                    break;
                case MarkdownTokenState.LH1:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LH2:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LH3:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LH4:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LH5:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LH6:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LH7:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LM1:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LM2:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LM3:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LM4:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LM5:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LM6:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LM7:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.LT:
                    morpheme = MarkdownFragmentState.Link;
                    break;
                case MarkdownTokenState.L:
                    morpheme = MarkdownFragmentState.Link;
                    break;
                case MarkdownTokenState.P:
                    morpheme = MarkdownFragmentState.Plus;
                    break;
                case MarkdownTokenState.R1T:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.RT:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.R2T:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.RN:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.R:
                    morpheme = MarkdownFragmentState.ImageReference;
                    break;
                case MarkdownTokenState.R3T:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.RP:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.RO:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.ROT:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.S:
                    morpheme = MarkdownFragmentState.Mask;
                    break;
                case MarkdownTokenState.T:
                    morpheme = MarkdownFragmentState.Pipe;
                    break;
                case MarkdownTokenState.U1:
                    morpheme = MarkdownFragmentState.Underline1;
                    break;
                case MarkdownTokenState.U1E:
                    morpheme = MarkdownFragmentState.Underline1;
                    position--;
                    break;
                case MarkdownTokenState.ULT:
                    morpheme = MarkdownFragmentState.Underline1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.ULTE:
                    morpheme = MarkdownFragmentState.Underline1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.U2:
                    morpheme = MarkdownFragmentState.Underline2;
                    break;
                case MarkdownTokenState.U2E:
                    morpheme = MarkdownFragmentState.Underline2;
                    position--;
                    break;
                case MarkdownTokenState.U3:
                    morpheme = MarkdownFragmentState.Underline3;
                    break;
                case MarkdownTokenState.U3E:
                    morpheme = MarkdownFragmentState.Underline3;
                    position--;
                    break;
                case MarkdownTokenState.UL:
                    morpheme = MarkdownFragmentState.HorizontalLine;
                    break;
                case MarkdownTokenState.ULE:
                    morpheme = MarkdownFragmentState.HorizontalLine;
                    position--;
                    break;
                case MarkdownTokenState.W1:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.W2:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.W3:
                    morpheme = MarkdownFragmentState.Text;
                    break;
                case MarkdownTokenState.W4:
                    morpheme = MarkdownFragmentState.Space4;
                    break;
                case MarkdownTokenState.Y1:
                    morpheme = MarkdownFragmentState.Hyphen1;
                    break;
                case MarkdownTokenState.Y1E:
                    morpheme = MarkdownFragmentState.Hyphen1;
                    position--;
                    break;
                case MarkdownTokenState.YLT:
                    morpheme = MarkdownFragmentState.Hyphen1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.YLTE:
                    morpheme = MarkdownFragmentState.Hyphen1;
                    position = orign + 1;
                    break;
                case MarkdownTokenState.Y2:
                    morpheme = MarkdownFragmentState.Hyphen2;
                    break;
                case MarkdownTokenState.Y2E:
                    morpheme = MarkdownFragmentState.Hyphen2;
                    position--;
                    break;
                case MarkdownTokenState.Y3:
                    morpheme = MarkdownFragmentState.Hyphen3;
                    break;
                case MarkdownTokenState.Y3E:
                    morpheme = MarkdownFragmentState.Hyphen3;
                    position--;
                    break;
                case MarkdownTokenState.YL:
                    morpheme = MarkdownFragmentState.HorizontalLine;
                    break;
                case MarkdownTokenState.YLE:
                    morpheme = MarkdownFragmentState.HorizontalLine;
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
