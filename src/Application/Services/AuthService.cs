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
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwt;

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtTokenGenerator jwt)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt;
        }

        public async Task<AuthResponse>
            LoginAsync(
                LoginRequest request)
        {
            var user =
                await _unitOfWork.Users
                    .GetByUserNameAsync(
                        request.UserName);

            if (user == null)
                throw new UnauthorizedException(
                    "Invalid credentials");

            var valid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!valid)
                throw new UnauthorizedException(
                    "Invalid credentials");

            var accessToken =
                _jwt.GenerateToken(user);

            var refreshToken =
                _jwt.GenerateRefreshToken();

            await _unitOfWork.RefreshTokens
                .AddAsync(new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshToken,
                    ExpiryDate =
                        DateTime.UtcNow.AddDays(7),
                    CreatedOn = DateTime.UtcNow
                });

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration =
                    DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<AuthResponse>
            RefreshTokenAsync(
                string refreshToken)
        {
            var token =
                await _unitOfWork.RefreshTokens
                    .GetByTokenAsync(refreshToken);

            if (token == null ||
                token.IsRevoked ||
                token.ExpiryDate <
                DateTime.UtcNow)
            {
                throw new UnauthorizedException(
                    "Invalid refresh token");
            }

            token.IsRevoked = true;

            var access =
                _jwt.GenerateToken(token.User);

            var newRefresh =
                _jwt.GenerateRefreshToken();

            await _unitOfWork.RefreshTokens
                .AddAsync(new RefreshToken
                {
                    UserId = token.UserId,
                    Token = newRefresh,
                    ExpiryDate =
                        DateTime.UtcNow.AddDays(7),
                    CreatedOn = DateTime.UtcNow
                });

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = access,
                RefreshToken = newRefresh,
                Expiration =
                    DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task LogoutAsync(
            string refreshToken)
        {
            var token =
                await _unitOfWork.RefreshTokens
                    .GetByTokenAsync(
                        refreshToken);

            if (token == null)
                return;

            token.IsRevoked = true;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
