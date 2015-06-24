using System;

namespace NaturalDateTime
{
	public class WhatTimeInCityQuestionProcessor : QuestionProcessorBase, IQuestionProcessor
	{
		public bool CanAnswerQuestion(Question question)
		{
            return question.Contains(LiteralDateOrTime, City) || question.Tokens.Count == 0;
		}
		
		public Answer GetAnswer(Question question)
		{
			if(question.ContainsAnyOfTheFollowing(Date, Time) && question.ContainsMultipleOccurrences(City, 2)){
				if(question.ContainsTokensInFollowingOrder(LiteralDateOrTime, City, City))
				   return GetAnswerToWhatTimeInUnknownCityWhenTimeInKnownCity(question);
				else
				   return GetAnswerToWhenTimeInKnownCityWhatTimeInUnknownCity(question);
			}
            
			return GetAnswerToWhatTimeInCity(question);
		}

        private Answer GetAnswerToWhenTimeInKnownCityWhatTimeInUnknownCity(Question question)
        {
            var knownCityToken = question.GetToken<CityToken>();
            var knownTimeToken = question.GetToken<TimeToken>();
            var knownDateToken = question.GetToken<DateToken>();
            var unknownCityToken = question.GetToken<CityToken>(2);
            return GetAnswerToTimeConversionQuestion(question, knownDateToken, knownTimeToken, knownCityToken, unknownCityToken);
        }

        private Answer GetAnswerToWhatTimeInUnknownCityWhenTimeInKnownCity(Question question)
        {
            var unknownCityToken = question.GetToken<CityToken>();
            var knownCityToken = question.GetToken<CityToken>(2);
            var knownTimeToken = question.GetToken<TimeToken>();
            var knownDateToken = question.GetToken<DateToken>();
            return GetAnswerToTimeConversionQuestion(question, knownDateToken, knownTimeToken, knownCityToken, unknownCityToken);
        }

        private Answer GetAnswerToTimeConversionQuestion(Question question, DateToken knownDateToken, TimeToken knownTimeToken, CityToken knownCityToken, CityToken unknownCityToken)
		{
            var cityResolver = new CityResolver();
            var unknownCityResolverResult = cityResolver.Resolve(unknownCityToken);
            var knownCityResolverResult = cityResolver.Resolve(knownCityToken);

            if (unknownCityResolverResult.Status == CityResolverResultStatus.FAILED || 
                knownCityResolverResult.Status == CityResolverResultStatus.FAILED)
            {
                return new Answer(question, true, false, ErrorMessages.UnableToRecognizeCity);
            }
			
			if (unknownCityResolverResult.City.HasNoTimezone || knownCityResolverResult.City.HasNoTimezone)
                return new Answer(question, true, false, ErrorMessages.NoTimezone);
            
            var dateTimeManager = new DateTimeManager();
            var knownLocalDateTimeInSecondCityResult = dateTimeManager.GetLocalDateTime(knownDateToken, knownTimeToken, knownCityResolverResult.City.Timezone);
            
			if(knownLocalDateTimeInSecondCityResult.ResultType == LocalDateTimeResultType.FAILED)
				return new Answer(question, true, false, ErrorMessages.InvalidDateTime);
				
			var knownLocalDateTimeInSecondCity = knownLocalDateTimeInSecondCityResult.LocalDateTime;
			var convertedZonedDateTimeResult = dateTimeManager.ConvertDateTime(knownLocalDateTimeInSecondCity, 
                                                                knownCityResolverResult.City.Timezone, 
                                                                unknownCityResolverResult.City.Timezone);
			
            var answerText = String.Format("It is {0} in {1} when it is {2} in {3}",
                                            convertedZonedDateTimeResult.ConvertedZonedDateTime.LocalDateTime.GetFormattedTimeAndDate(),
                                            unknownCityResolverResult.City.GetFormattedNameAndTimezone(convertedZonedDateTimeResult.ConvertedZonedDateTimeTimezoneAbbreviation),
                                            convertedZonedDateTimeResult.UnambiguousKnownTimeZonedDateTimeResult.UnambiguousZonedDateTime.LocalDateTime.GetFormattedTimeAndDate(),
                                            knownCityResolverResult.City.GetFormattedNameAndTimezone(convertedZonedDateTimeResult.UnambiguousKnownTimeZonedDateTimeResult.UnambiguousZonedDateTimeTimezoneAbbreviation));

            var answer = new Answer(question, true, true, answerText);
            if(convertedZonedDateTimeResult.UnambiguousKnownTimeZonedDateTimeResult.ConversionResultType == UnambiguousZonedDateTimeConversionResultType.Ambiguous)
            {
                var note = String.Format("In {0} the time {1} occurs twice due to daylight saving time changes when clocks are put back, we are using the first occurance of that time.",
                    knownCityResolverResult.City.FormattedName,
                    convertedZonedDateTimeResult.UnambiguousKnownTimeZonedDateTimeResult.OriginalLocalDateTime.GetFormattedTimeAndDate());
                answer.Note = note;
            }
            else if (convertedZonedDateTimeResult.UnambiguousKnownTimeZonedDateTimeResult.ConversionResultType == UnambiguousZonedDateTimeConversionResultType.Skipped)
            {
                var note = String.Format("In {0} the time {1} does not occur due to daylight saving time changes when clocks are put forward, we are using the next valid time which is 3am.",
                    knownCityResolverResult.City.FormattedName,
                    convertedZonedDateTimeResult.UnambiguousKnownTimeZonedDateTimeResult.OriginalLocalDateTime.GetFormattedTimeAndDate());
                answer.Note = note;
            }

            return answer;
		}

        private Answer GetAnswerToWhatTimeInCity(Question question)
		{
			var possibleCityNameBecauseNoTokensFound = question.Tokens.Count == 0;
			CityToken cityToken;
			if(possibleCityNameBecauseNoTokensFound)
				cityToken = CityTokenizer.CreateCityTokenFromQuestionWithNoTokens(question);
			else
				cityToken = question.GetToken<CityToken>();
			var cityResolver = new CityResolver();
			var dateTimeManager = new DateTimeManager();
			var cityResolverResult = cityResolver.Resolve(cityToken);
			
			if(cityResolverResult.Status == CityResolverResultStatus.FAILED){
				if(possibleCityNameBecauseNoTokensFound)
					return new Answer(question, true, false, ErrorMessages.DidNotUnderstandQuestion);
				else
					return new Answer(question, true, false, ErrorMessages.UnableToRecognizeCity);
			}
			
			if (cityResolverResult.City.HasNoTimezone)
                return new Answer(question, true, false, ErrorMessages.NoTimezone);
			
            var convertedDateTimeResult = dateTimeManager.GetCurrentTimeInTimezone(cityResolverResult.City.Timezone);
			var answerText = String.Format("The current time in {0} is {1}.", 
                                            cityResolverResult.City.GetFormattedNameAndTimezone(convertedDateTimeResult.ConvertedZonedDateTimeTimezoneAbbreviation), 
                                            convertedDateTimeResult.ConvertedZonedDateTime.LocalDateTime.GetFormattedTimeAndDate());
			return new Answer(question, true, true, answerText);	
		}
	}
}