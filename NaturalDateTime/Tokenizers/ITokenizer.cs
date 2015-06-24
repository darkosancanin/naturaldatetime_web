using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalDateTime
{
    public interface ITokenizer
    {
        void TokenizeTheQuestion(Question question);
    }
}
