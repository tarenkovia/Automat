﻿namespace Lab1
{
    public class Entry
    {
        public int Index { get; set; }
        public EntryType EntryType { get; set; }
        public Cmd? Cmd { get; set; }
        public string Value { get; set; }
        public int? CurrentValue { get; set; }
        public int? CmdPtr { get; set; }
    }
}
