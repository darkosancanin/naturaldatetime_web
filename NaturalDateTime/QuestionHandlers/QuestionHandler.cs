using System;

namespace NaturalDateTime
{
	public abstract class QuestionHandler
	{
        public Type DateOrTimeToken { get { return typeof(DateOrTimeToken); } }
        public Type CityOrTimezoneToken { get { return typeof(CityOrTimezoneToken ); } }
        public Type Timezone { get { return typeof(TimezoneToken); } }
        public Type City { get { return typeof(CityToken); } }
		public Type Date { get { return typeof(DateToken); } }
		public Type DaylightSavings { get { return typeof(DaylightSavingsToken); } }
		public Type LiteralDateOrTime { get { return typeof(LiteralDateOrTimeToken); } }
		public Type Time { get { return typeof(TimeToken); } }
        public virtual bool UnderstoodQuestion
        {
            get { return true; }
        }
    }
}
