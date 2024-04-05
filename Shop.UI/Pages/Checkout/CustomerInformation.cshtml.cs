using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IHostingEnvironment _env;

        public CustomerInformationModel(IHostingEnvironment env)
        {
            _env = env;
        }


        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }


        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            // Get Cart
            var information = getCustomerInformation.Do();

            if(information == null)
            {
                if(_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "A",
                        LastName = "A",
                        Email = "A@a.com",
                        PhoneNumber = "11",
                        Address1 = "A",
                        Address2 = "A",
                        City = "A",
                        PostCode = "A",
                    };
                }
                return Page();
            }
            else
            {
                // If Cart exists go to payment
                return RedirectToPage("/Checkout/Payment");
            }
        }


        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            // Post Cart
            addCustomerInformation.Do(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
