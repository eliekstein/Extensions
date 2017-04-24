using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Extensions
{
    [Obsolete("Update your SweetHelpers namespace to SweetHelpers.Reflection")]
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

namespace SweetHelpers.Reflection
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
