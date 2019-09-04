using MA.Web.Areas.Admin.Data;
using MA.Web.Areas.Admin.Models;
using MA.Web.Data;
using MA.Web.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MA.Web.Areas.Admin.Views.Contacts
{
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(
       ApplicationDbContext context,
       IAuthorizationService authorizationService,
       UserManager<AppUser> userManager)
       : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Contact = await Context.Contact.FirstOrDefaultAsync(
                                                 m => m.ContactId == id);

            if (Contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Contact,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            Context.Contact.Remove(Contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }

    }
}
