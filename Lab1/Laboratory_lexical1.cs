using Lab1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Laboratory_lexical1
    {
        public List<Lexeme> Lexemes { get; private set; }

        public Laboratory_lexical1()
        {
            Lexemes = new List<Lexeme>();
        }

        public bool Start(string text)
        {
            Lexemes = new List<Lexeme>();

            LexemeState LexemeState = LexemeState.Start;
            LexemeState prevState;

            bool add;

            text += " ";

            StringBuilder nextLexeme = new();
            StringBuilder curLexeme = new();

            int textIndex = 0;

            while (LexemeState != LexemeState.Error && LexemeState != LexemeState.Final)
            {
                prevState = LexemeState;

                add = true;

                if (textIndex == text.Length && LexemeState != LexemeState.Error)
                {
                    LexemeState = LexemeState.Final;
                    break;
                }

                if (textIndex == text.Length)
                {
                    break;
                }

                char symbol = text[textIndex];

                switch (LexemeState)
                {
                    case LexemeState.Start:
                        {
                            if (char.IsWhiteSpace(symbol)) LexemeState = LexemeState.Start;

                            else if (char.IsDigit(symbol)) LexemeState = LexemeState.Constant;

                            else if (char.IsLetter(symbol)) LexemeState = LexemeState.Identifier;

                            else if (symbol == '>') LexemeState = LexemeState.Comparison;
                            else if (symbol == '<') LexemeState = LexemeState.ReverseComparison;

                            else if (symbol == '+' || symbol == '-' || symbol == '/' || symbol == '*') LexemeState = LexemeState.ArithmeticOperation;

                            else if (symbol == '=') LexemeState = LexemeState.Assignment;

                            else LexemeState = LexemeState.Error;

                            add = false;

                            if (!char.IsWhiteSpace(symbol)) curLexeme.Append(symbol);

                            break;
                        }

                    case LexemeState.Comparison:
                        {
                            if (char.IsWhiteSpace(symbol)) LexemeState = LexemeState.Start;

                            else if (symbol == '=')
                            {
                                LexemeState = LexemeState.Start;
                                curLexeme.Append(symbol);
                            }

                            else if (char.IsLetter(symbol))
                            {
                                LexemeState = LexemeState.Identifier;
                                nextLexeme.Append(symbol);
                            }

                            else if (char.IsDigit(symbol))
                            {
                                LexemeState = LexemeState.Constant;
                                nextLexeme.Append(symbol);
                            }

                            else
                            {
                                LexemeState = LexemeState.Error;
                                add = false;
                            }

                            break;
                        }

                    case LexemeState.ReverseComparison:
                        {
                            if (char.IsWhiteSpace(symbol)) LexemeState = LexemeState.Start;

                            else if (symbol == '>')
                            {
                                LexemeState = LexemeState.Start;
                                curLexeme.Append(symbol);
                            }

                            else if (symbol == '=')
                            {
                                LexemeState = LexemeState.Start;
                                curLexeme.Append(symbol);
                            }

                            else if (char.IsLetter(symbol))
                            {
                                LexemeState = LexemeState.Identifier;
                                nextLexeme.Append(symbol);
                            }

                            else if (char.IsDigit(symbol))
                            {
                                LexemeState = LexemeState.Constant;
                                nextLexeme.Append(symbol);
                            }

                            else
                            {
                                LexemeState = LexemeState.Error;
                                add = false;
                            }

                            break;
                        }

                    case LexemeState.Assignment:
                        {
                            if (symbol == '=')
                            {
                                LexemeState = LexemeState.Comparison;
                                curLexeme.Append(symbol);
                            }

                            else //if (char.IsWhiteSpace(symbol))
                            {
                                LexemeState = LexemeState.Identifier;
                                nextLexeme.Append(symbol);
                            }

                            //else
                            //{
                            //    LexemeState = LexemeState.Error;
                            //    add = false;
                            //}

                            break;
                        }

                    case LexemeState.Constant:
                        {
                            if (char.IsWhiteSpace(symbol)) LexemeState = LexemeState.Start;

                            else if (char.IsDigit(symbol))
                            {
                                add = false;
                                LexemeState = LexemeState.Constant;
                                curLexeme.Append(symbol);
                            }

                            else if (symbol == '<')

                            {
                                LexemeState = LexemeState.ReverseComparison;
                                nextLexeme.Append(symbol);
                            }

                            else if (symbol == '>')
                            {
                                LexemeState = LexemeState.Comparison;
                                nextLexeme.Append(symbol);
                            }

                            else if (symbol == '=')
                            {
                                LexemeState = LexemeState.Assignment;
                                nextLexeme.Append(symbol);
                            }


                            else if (symbol == '+' || symbol == '-' || symbol == '/' || symbol == '*')
                            {
                                LexemeState = LexemeState.ArithmeticOperation;
                                nextLexeme.Append(symbol);
                            }

                            else
                            {
                                LexemeState = LexemeState.Error;
                                add = false;
                            }

                            break;
                        }

                    case LexemeState.Identifier:
                        {
                            if (char.IsWhiteSpace(symbol)) LexemeState = LexemeState.Start;

                            else if (char.IsDigit(symbol) || char.IsLetter(symbol))
                            {
                                LexemeState = LexemeState.Identifier;
                                add = false;
                                curLexeme.Append(symbol);
                            }

                            else if (symbol == '<')
                            {
                                LexemeState = LexemeState.ReverseComparison;
                                nextLexeme.Append(symbol);
                            }

                            else if (symbol == '>')
                            {
                                LexemeState = LexemeState.Comparison;
                                nextLexeme.Append(symbol);
                            }

                            else if (symbol == '=')
                            {
                                LexemeState = LexemeState.Assignment;
                                nextLexeme.Append(symbol);
                            }

                            else if (symbol == '+' || symbol == '-' || symbol == '/' || symbol == '*')
                            {
                                LexemeState = LexemeState.ArithmeticOperation;
                                nextLexeme.Append(symbol);
                            }

                            else if (symbol == ':')
                            {
                                LexemeState = LexemeState.Assignment;
                                nextLexeme.Append(symbol);
                            }

                            else
                            {
                                LexemeState = LexemeState.Error;
                                add = false;
                            }

                            break;
                        }

                    case LexemeState.ArithmeticOperation:
                        {
                            if (char.IsWhiteSpace(symbol))
                            {
                                LexemeState = LexemeState.Start;
                            }

                            else if (char.IsLetter(symbol))
                            {
                                LexemeState = LexemeState.Identifier;
                                nextLexeme.Append(symbol);
                            }

                            else if (char.IsDigit(symbol))
                            {
                                LexemeState = LexemeState.Constant;
                                nextLexeme.Append(symbol);
                            }

                            else if (symbol == '-' || symbol == '+' || symbol == '/' || symbol == '*')
                            {
                                LexemeState = LexemeState.ArithmeticOperation;
                                nextLexeme.Append(symbol);
                            }

                            else
                            {
                                LexemeState = LexemeState.Error;
                                add = false;
                            }

                            break;
                        }
                }

                if (add)
                {
                    AddNewLexeme(prevState, curLexeme.ToString());
                    curLexeme = new StringBuilder(nextLexeme.ToString());
                    nextLexeme.Clear();
                }

                textIndex++;
            }

            return LexemeState == LexemeState.Final;
        }

        private void AddNewLexeme(LexemeState prevLexemeState, string value)
        {
            LexemeType typeLexeme = LexemeType.Undefined;

            LexemeClass classLexeme = LexemeClass.Undefined;

            if (prevLexemeState == LexemeState.ArithmeticOperation)
            {
                typeLexeme = LexemeType.ArithmeticOperation;
                classLexeme = LexemeClass.SpecialSymbols;
            }
            else if (prevLexemeState == LexemeState.Assignment)
            {
                classLexeme = LexemeClass.SpecialSymbols;
                if (value == "==")
                {
                    typeLexeme = LexemeType.Relation;
                }
                else
                {
                    typeLexeme = LexemeType.Assignment;
                }
            }
            else if (prevLexemeState == LexemeState.Constant)
            {
                typeLexeme = LexemeType.Undefined;
                classLexeme = LexemeClass.Constant;
            }
            else if (prevLexemeState == LexemeState.ReverseComparison)
            {
                typeLexeme = LexemeType.Relation;
                classLexeme = LexemeClass.SpecialSymbols;
            }
            else if (prevLexemeState == LexemeState.Comparison)
            {
                typeLexeme = LexemeType.Relation;
                classLexeme = LexemeClass.SpecialSymbols;
            }
            else if (prevLexemeState == LexemeState.Identifier)
            {

                bool isKeyword = true;

                if (value == "end")
                {
                    typeLexeme = LexemeType.End;
                }
                else if (value == "and")
                {
                    typeLexeme = LexemeType.And;
                }
                else if (value == "or")
                {
                    typeLexeme = LexemeType.Or;
                }
                else if (value == "default")
                {
                    typeLexeme = LexemeType.Default;
                }
                else if (value == "output")
                {
                    typeLexeme = LexemeType.Output;
                }
                else if (value == "select")
                {
                    typeLexeme = LexemeType.Select;
                }
                else if (value == "case")
                {
                    typeLexeme = LexemeType.Case;
                }
                else
                {
                    typeLexeme = LexemeType.Undefined;
                    isKeyword = false;
                }

                if (isKeyword)
                {
                    classLexeme = LexemeClass.Keyword;
                }
                else
                {
                    classLexeme = LexemeClass.Identifier;
                }
            }

            var lexeme = new Lexeme
            {
                Class = classLexeme,
                Type = typeLexeme,
                Value = value.Trim(),
            };

            if (lexeme.Value.Length > 0)
            {
                Lexemes.Add(lexeme);
            }
        }
    }
}
