﻿using MA.Web.Areas.Admin.Data;
using MA.Web.Areas.Admin.Models;
using MA.Web.Data;
using MA.Web.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MA.Web.Areas.Admin.Views.Contacts
{
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
           ApplicationDbContext context,
           IAuthorizationService authorizationService,
           UserManager<AppUser> userManager)
           : base(context, authorizationService, userManager)
        {
        }

        public IList<Contact> Contact { get; set; }

        public async Task OnGetAsync()
        {
            var contacts = from c in Context.Contact
                           select c;

            var isAuthorized = User.IsInRole(Constants.ContactManagersRole) ||
                               User.IsInRole(Constants.ContactAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                contacts = contacts.Where(c => c.Status == ContactStatus.Approved
                                            || c.OwnerID == currentUserId);
            }

            Contact = await contacts.ToListAsync();
        }
    }
}
