using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.User;
using MediaMarket.Application.DTO.Response.Auth;
using MediaMarket.Application.DTO.Response.User;
using MediaMarket.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Application.Services
{
    public class UserService(
        UserManager<User> userManager,
        IUserRepository userRepository,
        IMapper mapper,
        IUser user
    ) : BaseResponseHandler, IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUser _user = user;

        public async Task<BaseResponse<PaginatedResult<UserManageResponse>>> GetListPaginated(GetListUserPaginatedRequest request)
        {
            var response = await _userRepository.GetListPaginated(request);
            return Success(response);
        }

        public async Task<BaseResponse<MyCurrentBalanceResponse>> GetMyCurrentBalance()
        {
            var user = await _userRepository.FindByIdAsync(_user.Id);
            return Success(new MyCurrentBalanceResponse() { Balance = user.Balance });
        }

        public async Task<BaseResponse<UserResponse>> GetUserInfo(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return Success(_mapper.Map<UserResponse>(user));
        }
    }
}
