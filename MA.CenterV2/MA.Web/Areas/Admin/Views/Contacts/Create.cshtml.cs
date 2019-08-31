using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Web.Areas.Admin.Data;

namespace MA.Web.Areas.Admin.Views.Contacts
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(
         ApplicationDbContext context,
         IAuthorizationService authorizationService,
         UserManager<IdentityUser> userManager)
         : base(context, authorizationService, userManager)
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Contact.OwnerID = UserManager.GetUserId(User);

            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Contact,
                                                        ContactOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            Context.Contact.Add(Contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }



    }
}
