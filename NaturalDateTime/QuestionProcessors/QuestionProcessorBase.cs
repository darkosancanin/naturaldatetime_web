using System;

namespace NaturalDateTime
{
	public abstract class QuestionProcessorBase
	{
		public Type City { get { return typeof(CityToken); } }
		public Type Date { get { return typeof(DateToken); } }
		public Type DaylightSavings { get { return typeof(DaylightSavingsToken); } }
		public Type LiteralDateOrTime { get { return typeof(LiteralDateOrTimeToken); } }
		public Type TimeUnit { get { return typeof(TimeUnitToken); } }
		public Type Time { get { return typeof(TimeToken); } }
	}
}

