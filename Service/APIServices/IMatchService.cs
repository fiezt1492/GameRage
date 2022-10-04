﻿using Model.Request;
using Model.Response;
using Model.Result;

namespace Service.APIServices;

public interface IMatchService
{
    Task<ApiResult<string>> AddMatch(MatchRequest matchRequest);
    Task<ApiResult<string>> DeleteMatchByBeginAt(DeleteMatchRequest request);
    ApiResult<IEnumerable<MatchResponse>> GetMatches(PagingRequest pagingRequest);
}