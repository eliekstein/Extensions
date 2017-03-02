using System;
using System.Linq.Expressions;
using System.Reflection;

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
