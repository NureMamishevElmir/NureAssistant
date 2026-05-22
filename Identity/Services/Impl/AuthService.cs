using DomainEntity.CustomerEntities;
using DomainEntity.CustomerEntities.Enums;
using Identity.Exceptions;
using Identity.Models.Requests;
using Identity.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;

namespace Identity.Services.Impl;

/// <inheritdoc cref="IAuthService"/>
public class AuthService : IAuthService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher<Customer> _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        ICustomerRepository customerRepository,
        IPasswordHasher<Customer> passwordHasher,
        ITokenService tokenService,
        ILogger<AuthService> logger)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<SignInResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var alreadyExists = await _customerRepository.GetAll().AnyAsync(c => c.Email == email, cancellationToken);
        if (alreadyExists)
        {
            _logger.LogWarning("Registration failed: email {Email} already registered", email);
            throw new AuthException("Користувач з таким email уже зареєстрований.");
        }

        var customer = new Customer
        {
            Name = request.Name,
            Email = email,
            Phone = request.Phone,
            Type = CustomerType.Customer,
        };

        // Зберігаємо лише хеш пароля, ніколи не зберігаємо пароль у відкритому вигляді.
        customer.Password = _passwordHasher.HashPassword(customer, request.Password);

        _customerRepository.Add(customer);
        await _customerRepository.SaveChangesAsync();

        _logger.LogInformation("Customer registered. Id: {CustomerId}, Email: {Email}", customer.Id, email);

        var (token, validTo) = _tokenService.Generate(customer);
        return new SignInResponse(token, validTo, customer.Id);
    }

    public async Task<SignInResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var customer = await _customerRepository.GetAll().FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        if (customer is null || string.IsNullOrEmpty(customer.Password))
        {
            _logger.LogWarning("Login failed: user {Email} not found", email);
            throw new AuthException("Невірний email або пароль.");
        }

        var verification = _passwordHasher.VerifyHashedPassword(customer, customer.Password, request.Password);
        if (verification == PasswordVerificationResult.Failed)
        {
            _logger.LogWarning("Login failed: wrong password for {Email}", email);
            throw new AuthException("Невірний email або пароль.");
        }

        // Якщо хеш застарів (зміна алгоритму/параметрів) — оновлюємо його.
        if (verification == PasswordVerificationResult.SuccessRehashNeeded)
        {
            customer.Password = _passwordHasher.HashPassword(customer, request.Password);
            customer.UpdatedAt = DateTime.UtcNow;
            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();
        }

        _logger.LogInformation("Customer logged in. Id: {CustomerId}", customer.Id);

        var (token, validTo) = _tokenService.Generate(customer);
        return new SignInResponse(token, validTo, customer.Id);
    }
}
