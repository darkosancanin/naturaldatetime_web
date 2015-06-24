using System;

namespace NaturalDateTime
{
	public class CityResolverResult
	{
		public City City { get; set; }
		public CityResolverResultStatus Status { get;set; }
		
		public CityResolverResult(City city)
		{
			City = city;
			Status = CityResolverResultStatus.SUCCESSFUL;
		}
		
		public CityResolverResult(CityResolverResultStatus status)
		{
			Status = status;
		}
	}
	
	public enum CityResolverResultStatus
	{
		SUCCESSFUL,
		FAILED
	}
}

