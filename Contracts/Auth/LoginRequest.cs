using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Auth;

public record LoginRequest([Required] string Login, [Required] string Password);
