﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dsl.Parser
{
    struct DslError
    {
        internal DslError(DslLog log)
        {
            mLog = log;
            mHasError = false;
        }

        internal bool HasError
        {
            get { return mHasError; }
        }

        internal short mismatch(short terminal, short token, ref DslToken dslToken)
        {
            mHasError = true;
            mLog.Log("[error] expecting {0} but found {1}, last token {2} line {3}, cur token {4} line {5}", DslString.GetSymbolName(terminal), DslString.GetSymbolName(token), dslToken.getLastToken(), dslToken.getLastLineNumber(), dslToken.getCurToken(), dslToken.getLineNumber());
            return token;
        }

        internal short no_entry(short nonterminal, short token, int level, ref DslToken dslToken)
        {
            mHasError = true;
            mLog.Log("[error] syntax error: skipping input {0}, last token {1} line {2}, cur token {3} line {4}", DslString.GetSymbolName(token), dslToken.getLastToken(), dslToken.getLastLineNumber(), dslToken.getCurToken(), dslToken.getLineNumber());
            token = dslToken.get(); // advance the input
            return token;
        }

        internal void input_left(ref DslToken dslToken)
        {
            mHasError = true;
            mLog.Log("[error] input left, last token {0} line {1}, cur token {2} line {3}", dslToken.getLastToken(), dslToken.getLastLineNumber(), dslToken.getCurToken(), dslToken.getLineNumber());
        }

        internal void message(string message, ref DslToken dslToken)
        {
            mHasError = true;
            mLog.Log("[info] {0}, last token {1} line {2}, cur token {3} line {4}", message, dslToken.getLastToken(), dslToken.getLastLineNumber(), dslToken.getCurToken(), dslToken.getLineNumber());
        }

        private DslLog mLog;
        private bool mHasError;
    }
}
