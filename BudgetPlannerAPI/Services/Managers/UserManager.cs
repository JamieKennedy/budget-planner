using Common.Models;
using Common.Results.Error.Base;
using Common.Results.Error.User;

using FluentResults;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts.Managers;

namespace Services.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _loggerManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserManager(IRepositoryManager repositoryManager, IConfiguration configuration, ILoggerManager loggerManager, IPasswordHasher<User> passwordHasher)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
            _loggerManager = loggerManager;
            _passwordHasher = passwordHasher;
        }

        public Result<User> CreateUser(User user, string password)
        {
            var passwordHash = _passwordHasher.HashPassword(user, password);

            if (string.IsNullOrEmpty(passwordHash))
            {
                return Result.Fail(new BadRequestError("Unable to create the user").CausedBy("Unable to hash the provided password"));
            }

            user.PasswordHash = passwordHash;

            try
            {
                var createdUser = _repositoryManager.User.CreateUser(user);
                _repositoryManager.Save();

                return createdUser;
            }
            catch (Exception ex)
            {
                return Result.Fail(new BadRequestError("Unable to create the user").CausedBy(ex));
            }
        }

        public Result<User> UpdateUser(User user)
        {
            try
            {
                user = _repositoryManager.User.UpdateUser(user);
                _repositoryManager.Save();

                return user;
            }
            catch (Exception ex)
            {
                return new BadRequestError("Unable to update the user").CausedBy(ex);
            }
        }

        public Result DeleteUser(User user)
        {
            try
            {
                _repositoryManager.User.DeleteUser(user);
                _repositoryManager.Save();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return new BadRequestError("Unable to delete the user").CausedBy(ex);
            }
        }

        public Result<User> FindUser(Guid userId, bool trackChanges = false)
        {
            try
            {
                var user = _repositoryManager.User.SelectById(userId, trackChanges);

                if (user is null) return new UserNotFoundError(userId);

                return user;
            }
            catch (Exception ex)
            {
                return new UserNotFoundError(userId).CausedBy(ex);
            }
        }

        public Result<User> FindUser(string emailAddress, bool trackChanges = false)
        {
            try
            {
                var user = _repositoryManager.User.SelectByEmail(emailAddress);

                if (user is null) return new UserNotFoundError(emailAddress);

                return user;
            }
            catch (Exception ex)
            {
                return new UserNotFoundError(emailAddress).CausedBy(ex);
            }
        }

        public Result<List<User>> GetAll(bool trackChanges = false)
        {
            try
            {
                var users = _repositoryManager.User.SelectAll(trackChanges) ?? new List<User>();

                return users;
            }
            catch (Exception ex)
            {
                return new BadRequestError("An error occured retreiving the users").CausedBy(ex);
            }
        }

        public Result VerifyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new UnauthorisedError("Invalid password");
            }

            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                var reHashResult = UpdatePasswordHash(user, password);
                return Result.FailIf(reHashResult.IsFailed, reHashResult.Errors.FirstOrDefault());
            }

            return Result.Ok();
        }

        public Result IncrementAccessFailedCount(User user)
        {
            user.AccessFailedCount++;

            var result = UpdateUser(user);

            return Result.FailIf(result.IsFailed, result.Errors.FirstOrDefault());
        }

        private Result UpdatePasswordHash(User user, string password)
        {
            var passwordHash = _passwordHasher.HashPassword(user, password);

            if (string.IsNullOrEmpty(passwordHash))
            {
                return new BadRequestError("Unable to create the user").CausedBy("Unable to hash the provided password");
            }

            user.PasswordHash = passwordHash;

            var result = UpdateUser(user);

            return Result.FailIf(result.IsFailed, result.Errors.FirstOrDefault());
        }

    }
}
