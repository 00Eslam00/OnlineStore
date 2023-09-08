using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Data.Enums;
using OnlineStore.Models;

namespace OnlineStore.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="nvarchar(50)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; }
    [PersonalData]
    public string MyUserName { get; set; }
    public string Address { get; set; }
    public UserType UserType { get; set; }
    public Gender Gender { get; set; }
}

