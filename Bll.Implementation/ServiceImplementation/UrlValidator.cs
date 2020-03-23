using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Bll.Contract.Interfaces;

namespace Bll.Implementation.ServiceImplementation
{
    public class UrlValidator : IValidator<string>
    {
        private readonly string pattern;

        public UrlValidator(string pattern)
        {
            this.pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        }

        public bool IsValid(string source)
        {
            return Regex.IsMatch(source, this.pattern) && Uri.TryCreate(source, UriKind.RelativeOrAbsolute, out _);
        }
    }
}
