using Lab1.Enums;
using System.Text;

namespace Lab1
{
    public class Lexical
    {
        public List<Lexeme> Lexemes { get; private set; }

        public Lexical()
        {
            Lexemes = new List<Lexeme>();
        }

        public bool Run(string text)
        {
            Lexemes = new List<Lexeme>();

            LexemeState state = LexemeState.Start;
            LexemeState prevState;

            bool isAbleToAdd;

            text += " ";

            StringBuilder lexBufNext = new();

            StringBuilder lexBufCur = new();

            int textIndex = 0;

            while (state != LexemeState.Error && state != LexemeState.Final)
            {
                prevState = state;

                isAbleToAdd = true;

                if (textIndex == text.Length && state != LexemeState.Error)
                {
                    state = LexemeState.Final;
                    break;
                }

                if (textIndex == text.Length)
                {
                    break;
                }

                char symbol = text[textIndex];

                switch (state)
                {
                    case LexemeState.Start:
                        {
                            if (char.IsWhiteSpace(symbol)) state = LexemeState.Start;

                            else if (char.IsDigit(symbol)) state = LexemeState.Constant;

                            else if (char.IsLetter(symbol)) state = LexemeState.Identifier;

                            else if (symbol == '>') state = LexemeState.Comparison;
                            else if (symbol == '<') state = LexemeState.ReverseComparison;

                            else if (symbol == '+' || symbol == '-' || symbol == '/' || symbol == '*') state = LexemeState.ArithmeticOperation;

                            else if (symbol == '=') state = LexemeState.Assignment;

                            else if (symbol == '(' || symbol == ')') state = LexemeState.Assignment;

                            else state = LexemeState.Error;

                            isAbleToAdd = false;

                            if (!char.IsWhiteSpace(symbol)) lexBufCur.Append(symbol);

                            break;
                        }

                    case LexemeState.Comparison:
                        {
                            if (char.IsWhiteSpace(symbol)) state = LexemeState.Start;

                            else if (symbol == '=')
                            {
                                state = LexemeState.Start;
                                lexBufCur.Append(symbol);
                            }

                            else if (char.IsLetter(symbol))
                            {
                                state = LexemeState.Identifier;
                                lexBufNext.Append(symbol);
                            }

                            else if (char.IsDigit(symbol))
                            {
                                state = LexemeState.Constant;
                                lexBufNext.Append(symbol);
                            }

                            else
                            {
                                state = LexemeState.Error;
                                isAbleToAdd = false;
                            }

                            break;
                        }

                    case LexemeState.ReverseComparison:
                        {
                            if (char.IsWhiteSpace(symbol)) state = LexemeState.Start;

                            else if (symbol == '>')
                            {
                                state = LexemeState.Start;
                                lexBufCur.Append(symbol);
                            }

                            else if (symbol == '=')
                            {
                                state = LexemeState.Start;
                                lexBufCur.Append(symbol);
                            }

                            else if (char.IsLetter(symbol))
                            {
                                state = LexemeState.Identifier;
                                lexBufNext.Append(symbol);
                            }

                            else if (char.IsDigit(symbol))
                            {
                                state = LexemeState.Constant;
                                lexBufNext.Append(symbol);
                            }

                            else
                            {
                                state = LexemeState.Error;
                                isAbleToAdd = false;
                            }

                            break;
                        }

                    case LexemeState.Assignment:
                        {
                            if (symbol == '=')
                            {
                                state = LexemeState.Comparison;
                                lexBufCur.Append(symbol);
                            }

                            else if (symbol == '(' || symbol == ')')
                            {
                                state = LexemeState.Assignment;
                                lexBufCur.Append(symbol);
                            }

                            else //if (char.IsWhiteSpace(symbol))
                            {
                                state = LexemeState.Identifier;
                                lexBufNext.Append(symbol);
                            }

                            //else
                            //{
                            //    state = State.Error;
                            //    isAbleToAdd = false;
                            //}

                            break;
                        }

                    case LexemeState.Constant:
                        {
                            if (char.IsWhiteSpace(symbol)) state = LexemeState.Start;

                            else if (char.IsDigit(symbol))
                            {
                                isAbleToAdd = false;
                                state = LexemeState.Constant;
                                lexBufCur.Append(symbol);
                            }

                            else if (symbol == '<')

                            {
                                state = LexemeState.ReverseComparison;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '>')
                            {
                                state = LexemeState.Comparison;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '=')
                            {
                                state = LexemeState.Assignment;
                                lexBufNext.Append(symbol);
                            }


                            else if (symbol == '+' || symbol == '-' || symbol == '/' || symbol == '*')
                            {
                                state = LexemeState.ArithmeticOperation;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '(' || symbol == ')')
                            {
                                state = LexemeState.Assignment;
                                lexBufNext.Append(symbol);
                            }

                            else
                            {
                                state = LexemeState.Error;
                                isAbleToAdd = false;
                            }

                            break;
                        }

                    case LexemeState.Identifier:
                        {
                            if (char.IsWhiteSpace(symbol)) state = LexemeState.Start;

                            else if (char.IsDigit(symbol) || char.IsLetter(symbol))
                            {
                                state = LexemeState.Identifier;
                                isAbleToAdd = false;
                                lexBufCur.Append(symbol);
                            }

                            else if (symbol == '<')
                            {
                                state = LexemeState.ReverseComparison;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '>')
                            {
                                state = LexemeState.Comparison;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '=')
                            {
                                state = LexemeState.Assignment;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '+' || symbol == '-' || symbol == '/' || symbol == '*')
                            {
                                state = LexemeState.ArithmeticOperation;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == ':')
                            {
                                state = LexemeState.Assignment;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '(' || symbol == ')')
                            {
                                state = LexemeState.Assignment;
                                lexBufNext.Append(symbol);
                            }

                            else
                            {
                                state = LexemeState.Error;
                                isAbleToAdd = false;
                            }

                            break;
                        }

                    case LexemeState.ArithmeticOperation:
                        {
                            if (char.IsWhiteSpace(symbol))
                            {
                                state = LexemeState.Start;
                            }

                            else if (char.IsLetter(symbol))
                            {
                                state = LexemeState.Identifier;
                                lexBufNext.Append(symbol);
                            }

                            else if (char.IsDigit(symbol))
                            {
                                state = LexemeState.Constant;
                                lexBufNext.Append(symbol);
                            }

                            else if (symbol == '-' || symbol == '+' || symbol == '/' || symbol == '*')
                            {
                                state = LexemeState.ArithmeticOperation;
                                lexBufNext.Append(symbol);
                            }

                            else
                            {
                                state = LexemeState.Error;
                                isAbleToAdd = false;
                            }

                            break;
                        }
                }

                if (isAbleToAdd)
                {
                    AddLexeme(prevState, lexBufCur.ToString());
                    lexBufCur = new StringBuilder(lexBufNext.ToString());
                    lexBufNext.Clear();
                }

                textIndex++;
            }

            return state == LexemeState.Final;
        }

        private void AddLexeme(LexemeState prevState, string value)
        {
            LexemeType lexType = LexemeType.Undefined;

            LexemeClass lexClass = LexemeClass.Undefined;

            if (prevState == LexemeState.ArithmeticOperation)
            {
                lexType = LexemeType.ArithmeticOperation;
                lexClass = LexemeClass.SpecialSymbols;
            }
            else if (prevState == LexemeState.Assignment)
            {
                lexClass = LexemeClass.SpecialSymbols;
                if (value == "==")
                {
                    lexType = LexemeType.Relation;
                }
                else if (value == ")" || value == "(")
                {
                    lexType = LexemeType.Brackets;
                }
                else
                {
                    lexType = LexemeType.Assignment;
                }
            }
            else if (prevState == LexemeState.Constant)
            {
                lexType = LexemeType.Undefined;
                lexClass = LexemeClass.Constant;
            }
            else if (prevState == LexemeState.ReverseComparison)
            {
                lexType = LexemeType.Relation;
                lexClass = LexemeClass.SpecialSymbols;
            }
            else if (prevState == LexemeState.Comparison)
            {
                lexType = LexemeType.Relation;
                lexClass = LexemeClass.SpecialSymbols;
            }
            else if (prevState == LexemeState.Identifier)
            {

                bool isKeyword = true;

                if (value == "select") lexType = LexemeType.Select;
                else if (value == "case") lexType = LexemeType.Case;
                else if (value == "default") lexType = LexemeType.Default;
                else if (value == "end") lexType = LexemeType.End;
                else if (value == "while") lexType = LexemeType.While;
                else if (value == "do") lexType = LexemeType.Do;
                else if (value == "loop") lexType = LexemeType.Loop;
                else if (value == "output") lexType = LexemeType.Output;
                else
                {
                    lexType = LexemeType.Undefined;
                    isKeyword = false;
                }

                if (isKeyword) lexClass = LexemeClass.Keyword;
                else lexClass = LexemeClass.Identifier;
            }

            var lexeme = new Lexeme
            {
                Class = lexClass,
                Type = lexType,
                Value = value.Trim(),
            };

            if (lexeme.Value.Length > 0)
            {
                Lexemes.Add(lexeme);
            }
        }
    }
}
