﻿namespace HairConnect.Web.Areas.Admin.Models
{
    using AutoMapper;
    using HairConnect.Common.Mapping;
    using HairConnect.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ListUsersModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
