using Microsoft.AspNetCore.Identity;
using Minio.DataModel.Notification;
using System.Security.Principal;

namespace Domain.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; }
}
