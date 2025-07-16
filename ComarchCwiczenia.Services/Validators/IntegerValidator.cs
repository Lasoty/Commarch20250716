using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia.Services.Validators;

public class IntegerValidator
{
    public bool NumberIsPositive(int number)
    {
        return number > 0;
    }
}
