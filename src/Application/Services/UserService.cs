using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse>
            RegisterAsync(
                RegisterRequest request)
        {
            var exists =
                await _unitOfWork.Users
                    .GetByUserNameAsync(
                        request.UserName);

            if (exists != null)
                throw new BadRequestException(
                    "User already exists");

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        request.Password),
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<IEnumerable<UserResponse>>
            GetAllAsync()
        {
            var users =
                await _unitOfWork.Users.GetAllAsync();

            return users.Select(x =>
                new UserResponse
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Role = x.Role
                });
        }

        public async Task<UserResponse?>
            GetByIdAsync(int id)
        {
            var user =
                await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException(
                    "User not found");

            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task UpdateAsync(
            int id,
            UpdateUserRequest request)
        {
            var user =
                await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException(
                    "User not found");

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Role = request.Role;

            _unitOfWork.Users.Update(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user =
                await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException(
                    "User not found");

            _unitOfWork.Users.Delete(user);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
