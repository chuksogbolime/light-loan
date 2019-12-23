using System;

namespace Com.LightLoan.Application.Interface
{
    public interface IUserService
    {
        Guid UserId {get;set;}
        bool IsAuthenticated {get; set;}
    }
}