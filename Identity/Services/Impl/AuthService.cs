using AutoMapper;
using DomainEntity.CustomerEntities;
using DomainEntity.CustomerEntities.Enums;
using DomainEntity.Exchange;
using DomainEntity.Request;
using Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;

namespace Identity.Services.Impl;

public class AuthService : IAuthService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher<Customer> _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        ICustomerRepository customerRepository,
        IPasswordHasher<Customer> passwordHasher,
        ITokenService tokenService,
        IMapper mapper,
        ILogger<AuthService> logger)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<(string Token, DateTime ValidTo, CustomerExchange Customer)> RegisterAsync(CustomerRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new AuthException("Email та пароль є обов'язковими.");
        }

        var email = request.Email.Trim().ToLowerInvariant();

        var alreadyExists = await _customerRepository.GetAll().AnyAsync(c => c.Email == email, cancellationToken);
        if (alreadyExists)
        {
            _logger.LogWarning("Registration failed: email {Email} already registered", email);
            throw new AuthException("Користувач з таким email уже зареєстрований.");
        }

        var customer = _mapper.Map<Customer>(request);
        customer.Email = email;
        customer.Type = CustomerType.Customer;
        customer.Password = _passwordHasher.HashPassword(customer, request.Password);

        _customerRepository.Add(customer);
        await _customerRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Customer registered. Id: {CustomerId}, Email: {Email}", customer.Id, email);

        var (token, validTo) = _tokenService.Generate(customer);
        return (token, validTo, _mapper.Map<CustomerExchange>(customer));
    }

    public async Task<(string Token, DateTime ValidTo, CustomerExchange Customer)> LoginAsync(CustomerRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new AuthException("Невірний email або пароль.");
        }

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

        if (verification == PasswordVerificationResult.SuccessRehashNeeded)
        {
            customer.Password = _passwordHasher.HashPassword(customer, request.Password);
            customer.UpdatedAt = DateTime.UtcNow;
            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("Customer logged in. Id: {CustomerId}", customer.Id);

        var (token, validTo) = _tokenService.Generate(customer);
        return (token, validTo, _mapper.Map<CustomerExchange>(customer));
    }
}
