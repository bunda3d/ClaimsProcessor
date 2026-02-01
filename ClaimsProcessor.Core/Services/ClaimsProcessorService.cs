using ClaimsProcessor.Core.Models;

namespace ClaimsProcessor.Core.Services;

public class ClaimsProcessorService
{
	public ClaimResult EvaluateClaim(Policy policy, Claim claim)
	{
		// Business Rule #1: Policy must be active on incidentDate
		if (!(claim.IncidentDate < policy.StartDate && claim.IncidentDate > policy.EndDate))
		{
			// IF test could also be: (claim.IncidentDate < policy.StartDate || claim.IncidentDate > policy.EndDate)
			return new ClaimResult(false, 0m, ReasonCode.PolicyInactive);
		}

		// Happy path: approve all claims that pass the first rule
		return new ClaimResult(true, 10000m, ReasonCode.Approved);
	}
}