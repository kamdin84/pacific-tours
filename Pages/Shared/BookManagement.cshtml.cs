using System.Threading.Tasks;
using ccse_cw1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ccse_cw1.Models;

namespace ccse_cw1.Pages.Shared
{
    [Authorize]
    public class BookManagementModel : PageModel
    {
        public readonly UserManager<ApplicationUser> UserManager;
        private readonly BookingRepository _bookingRepository;

        public BookManagementModel(UserManager<ApplicationUser> userManager, BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
            UserManager = userManager;
        }

        [BindProperty]
        public BookModel.InputModel Input { get; set; }

    

        public async Task<IActionResult> OnPostPaymentAsync()
        {
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int bookingId)
        {
            var bookingToDelete = await _bookingRepository.GetBookingAsync(bookingId);

            if (bookingToDelete != null)
            { 

                var isDeleted = await _bookingRepository.DeleteBookingAsync(bookingId);

                if (isDeleted)
                {
                    // Booking successfully deleted
                    return RedirectToPage("/BookManagement"); // Redirect to the booking management page
                }
                else
                {
                    // Handle deletion failure if necessary
                    ModelState.AddModelError(string.Empty, "Failed to delete booking. Please try again.");
                }
            }
            else
            {
                
                ModelState.AddModelError(string.Empty, "Booking not found.");
            }

            return Page();
        }

    }
}
