using System;
using System.Web;

namespace Blog.Presentation.Framework.Localization
{
    public class LocalizedString : MarshalByRefObject, IHtmlString
    {

        #region Properties

        public string Scope { get; }

        public string TextHint { get; }

        public object[] Args { get; }

        public string Text { get; }

        #endregion

        #region Constructor

        public LocalizedString(string localized)
        {
            Text = localized;
        }

        public LocalizedString(string localized, string scope, string textHint, object[] args)
        {
            Text = localized;
            Scope = scope;
            TextHint = textHint;
            Args = args;
        }

        #endregion

        public override string ToString()
        {
            return Text;
        }

        public string ToHtmlString()
        {
            return Text;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            if (Text != null)
                hashCode ^= Text.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var that = (LocalizedString)obj;
            return string.Equals(Text, that.Text);
        }

        public static LocalizedString TextOrDefault(string text, LocalizedString defaultValue)
        {
            if (string.IsNullOrEmpty(text))
                return defaultValue;
            return new LocalizedString(text);
        }

    }
}
