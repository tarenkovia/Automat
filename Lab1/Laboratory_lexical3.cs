using Lab1.Enums;

namespace Lab1
{
    public class Laboratory_lexical3
    {
        private List<Lexeme> _lexemeList;
        private IEnumerator<Lexeme> _lexemeEnumerator;

        private List<Entry> EntryList { get; set; }

        public bool Run(string code, out List<Entry> postfixEntries)
        {
            EntryList = new List<Entry>();

            Lexical analyser = new();
            var result = analyser.Run(string.Join(Environment.NewLine, code));
            if (!result)
            {
                throw new Exception("Errors were occurred in lexical analyze");
            }

            bool res = IsSelectCaseStatement(analyser.Lexemes);
            postfixEntries = new(EntryList);
            return res;
        }

        private bool IsSelectCaseStatement(List<Lexeme> lexemeList)
        {
            _lexemeList = lexemeList;
            if (lexemeList.Count == 0) return false;

            _lexemeEnumerator = lexemeList.GetEnumerator();

            if (!_lexemeEnumerator.MoveNext() || _lexemeEnumerator.Current.Type != LexemeType.Select) { ErrorType.Error("Ожидается select", _lexemeList.IndexOf(_lexemeEnumerator.Current)); }
            _lexemeEnumerator.MoveNext();

            if (!IsOperand()) return false;

            if (!RelationalExpression()) return false;

            while (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Type == LexemeType.Case)
            {

                if (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Type != LexemeType.Case) { ErrorType.Error("Ожидается case", _lexemeList.IndexOf(_lexemeEnumerator.Current)); }
                _lexemeEnumerator.MoveNext();

                if (!IsOperand()) return false;

                WriteCmd(Cmd.JZ);

                if (!IsStatement()) return false;

                WriteCmd(Cmd.JMP);
            }

            if (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Type == LexemeType.Default)
            {

                if (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Type != LexemeType.Default) { ErrorType.Error("Ожидается default", _lexemeList.IndexOf(_lexemeEnumerator.Current)); }
                _lexemeEnumerator.MoveNext();


                if (!IsStatement()) return false;
            }

            if (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Type != LexemeType.End) { ErrorType.Error("Ожидается end", _lexemeList.IndexOf(_lexemeEnumerator.Current)); }
            _lexemeEnumerator.MoveNext();

            WriteCmd(Cmd.JMP);


            if (_lexemeEnumerator.MoveNext()) { ErrorType.Error("Лишние символы", _lexemeList.IndexOf(_lexemeEnumerator.Current)); }
            return true;
        }

        private bool IsCondition()
        {
            if (!IsLogicalExpression()) return false;
            while (_lexemeEnumerator.Current != null && _lexemeEnumerator.Current.Type == LexemeType.Or)
            {
                _lexemeEnumerator.MoveNext();
                if (!IsLogicalExpression()) return false;

                WriteCmd(Cmd.OR);
            }
            return true;
        }

        private bool RelationalExpression()
        {
            //if (!IsOperand()) return false;
            if (_lexemeEnumerator.Current != null && _lexemeEnumerator.Current.Type == LexemeType.Relation)
            {
                var cmd = _lexemeEnumerator.Current.Value switch
                {
                    "<" => Cmd.CMPL,
                    "<=" => Cmd.CMPLE,
                    ">" => Cmd.CMPG,
                    ">=" => Cmd.CMPGE,
                    "==" => Cmd.CMPE,
                    "<>" => Cmd.CMPNE,
                    _ => throw new ArgumentException(_lexemeEnumerator.Current.Value)
                };

                _lexemeEnumerator.MoveNext();
                if (!IsOperand()) return false;

                WriteCmd(cmd);
            }
            return true;
        }

        private bool IsLogicalExpression()
        {
            if (!RelationalExpression()) return false;
            while (_lexemeEnumerator.Current != null && _lexemeEnumerator.Current.Type == LexemeType.And)
            {
                _lexemeEnumerator.MoveNext();
                if (!RelationalExpression()) return false;

                WriteCmd(Cmd.AND);
            }
            return true;
        }

        private bool IsIdentifier()
        {
            if (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Class != LexemeClass.Identifier)
            {
                ErrorType.Error("Ожидается переменная", _lexemeList.IndexOf(_lexemeEnumerator.Current));
                return false;
            }

            WriteVar(_lexemeList.IndexOf(_lexemeEnumerator.Current));

            _lexemeEnumerator.MoveNext();
            return true;
        }

        private bool IsOperand()
        {
            if (_lexemeEnumerator.Current == null || (_lexemeEnumerator.Current.Class != LexemeClass.Identifier && _lexemeEnumerator.Current.Class != LexemeClass.Constant))
            {
                ErrorType.Error("Ожидается переменная или константа", _lexemeList.IndexOf(_lexemeEnumerator.Current));
                return false;
            }

            if (_lexemeEnumerator.Current.Class == LexemeClass.Identifier)
            {
                WriteVar(_lexemeList.IndexOf(_lexemeEnumerator.Current));
            }
            else
            {
                WriteConst(_lexemeList.IndexOf(_lexemeEnumerator.Current));
            }

            _lexemeEnumerator.MoveNext();
            return true;
        }

        private bool IsStatement()
        {
            if (_lexemeEnumerator.Current != null && _lexemeEnumerator.Current.Type == LexemeType.End) return false;

            if (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Class != LexemeClass.Identifier)
            {
                if (_lexemeEnumerator.Current.Type == LexemeType.Output)
                {
                    _lexemeEnumerator.MoveNext();
                    if (!IsOperand()) return false;

                    WriteCmd(Cmd.OUTPUT);

                    return true;
                }
                ErrorType.Error("Ожидается переменная", _lexemeList.IndexOf(_lexemeEnumerator.Current));
                return false;
            }

            WriteVar(_lexemeList.IndexOf(_lexemeEnumerator.Current));

            _lexemeEnumerator.MoveNext();

            if (_lexemeEnumerator.Current == null || _lexemeEnumerator.Current.Type != LexemeType.Assignment)
            {
                ErrorType.Error("Ожидается присваивание", _lexemeList.IndexOf(_lexemeEnumerator.Current));
                return false;
            }
            _lexemeEnumerator.MoveNext();

            if (!IsArithmeticExpression()) return false;

            WriteCmd(Cmd.SET);

            return true;
        }

        private bool IsArithmeticExpression()
        {
            if (!IsOperand()) return false;
            while (_lexemeEnumerator.Current.Type == LexemeType.ArithmeticOperation)
            {
                var cmd = _lexemeEnumerator.Current.Value switch
                {
                    "+" => Cmd.ADD,
                    "-" => Cmd.SUB,
                    "*" => Cmd.MUL,
                    "/" => Cmd.DIV,
                    _ => throw new ArgumentException(_lexemeEnumerator.Current.Value)
                };

                _lexemeEnumerator.MoveNext();
                if (!IsOperand()) return false;

                WriteCmd(cmd);
            }
            return true;
        }

        private int WriteCmd(Cmd cmd)
        {
            var command = new Entry
            {
                EntryType = EntryType.Cmd,
                Cmd = cmd,
            };
            EntryList.Add(command);
            return EntryList.Count - 1;
        }

        private int WriteVar(int index)
        {
            var variable = new Entry
            {
                EntryType = EntryType.Var,
                Value = _lexemeList[index].Value
            };
            EntryList.Add(variable);
            return EntryList.Count - 1;
        }

        private int WriteConst(int index)
        {
            var variable = new Entry
            {
                EntryType = EntryType.Const,
                Value = _lexemeList[index].Value
            };
            EntryList.Add(variable);
            return EntryList.Count - 1;
        }

        private int WriteCmdPtr(int ptr)
        {
            var cmdPtr = new Entry
            {
                EntryType = EntryType.CmdPtr,
                CmdPtr = ptr,
            };
            EntryList.Add(cmdPtr);
            return EntryList.Count - 1;
        }

        private void SetCmdPtr(int index, int ptr)
        {
            EntryList[index].CmdPtr = ptr;
        }
    }
}
