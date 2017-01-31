using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace Extensions
{
    public class Helper
    {



        public static MemberInfo GetMemberInfo<TObject, TProperty>(
            Expression<Func<TObject, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                return member.Member;
            }

            throw new ArgumentException("expression");
        }
    }
}
